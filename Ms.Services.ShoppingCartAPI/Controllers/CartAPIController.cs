using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ms.MessageBus;
using Ms.Services.ShoppingCartAPI.Models;
using Ms.Services.ShoppingCartAPI.Models.Dto;
using Ms.Services.ShoppingCartAPI.Service.IService;
using MS.Services.ShoppingCartAPI.Data;
using MS.Services.ShoppingCartAPI.Models.Dto;
using System.Reflection.PortableExecutable;

namespace Ms.Services.ShoppingCartAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    [Authorize]
    public class CartAPIController : ControllerBase
    {
        private ResponseDto _response;
        private IMapper _mapper;
        private readonly AppDbContext _appDbContext;
        private IProductService _productService;
        private ICouponService _couponService;
        private IConfiguration _configuration;
        private readonly IMessageBus _messageBus;

        public CartAPIController(IMapper mapper, AppDbContext appDbContext, IProductService productService, ICouponService couponService, IConfiguration configuration, IMessageBus messageBus)
        {
            this._response = new ResponseDto();
            _mapper = mapper;
            _appDbContext = appDbContext;
            _productService = productService;
            _couponService = couponService;
            _configuration = configuration;
            _messageBus = messageBus;
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<ResponseDto> GetCart(string userId)
        {
            try
            {
                CartDto cart = new()
                {
                    CartHeader = _mapper.Map<CartHeaderDto>(_appDbContext.CartHeaders.First(u => u.UserId == userId))
                };
                cart.CartDetails= _mapper.Map<IEnumerable<CartDetailsDto>>(_appDbContext.CartDetails.Where(u=>u.CartHeaderId==cart.CartHeader.Id));

                IEnumerable<ProductDto> productDtos = await _productService.GetProductsAsync();

                foreach (var item in cart.CartDetails)
                {
                    item.Product = productDtos.FirstOrDefault(u => u.Id == item.ProductId);
                    cart.CartHeader.CartTotal += (item.Count * item.Product.Price);
                }

                //apply coupon if any
                if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
                {
                    CouponDto coupon = await _couponService.GetCoupon(cart.CartHeader.CouponCode);
                    if (coupon != null && cart.CartHeader.CartTotal > coupon.MinAmount)
                    {
                        cart.CartHeader.CartTotal -= coupon.DiscountAmount;
                        cart.CartHeader.Discount=coupon.DiscountAmount;
                    }
                }
                _response.Result= cart;
            }
            catch (Exception ex)
            {
                _response.Message= ex.Message;
                _response.IsSuccess= false;
            }
            return _response;
        }
        [HttpPost("CartUpsert")]
        public async Task<ResponseDto> CartUpsert(CartDto cartDto)
        {
            try
            {
                var cartHeaderFromDb = await _appDbContext.CartHeaders.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == cartDto.CartHeader.UserId);
                if (cartHeaderFromDb == null)
                {
                    //create header and details
                    CartHeader cartHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);
                    _appDbContext.CartHeaders.Add(cartHeader);
                   await  _appDbContext.SaveChangesAsync();

                    cartDto.CartDetails.First().CartHeaderId=cartHeader.Id;
                    _appDbContext.CartDetails.Add(_mapper.Map<CartDetail>(cartDto.CartDetails.First()));
                    await _appDbContext.SaveChangesAsync();
                }
                else
                {
                    //check if details has same product
                    var cartDetailsFromDb = await _appDbContext.CartDetails.AsNoTracking().FirstOrDefaultAsync(u => u.ProductId == cartDto.CartDetails.First().ProductId && u.CartHeaderId == cartHeaderFromDb.Id);
                    if (cartDetailsFromDb == null)
                    {
                        //create cart details
                        cartDto.CartDetails.First().CartHeaderId = cartHeaderFromDb.Id;
                        _appDbContext.CartDetails.Add(_mapper.Map<CartDetail>(cartDto.CartDetails.First()));
                        await _appDbContext.SaveChangesAsync();
                    }
                    else
                    {
                        //update count in cart details
                        cartDto.CartDetails.First().Count += cartDetailsFromDb.Count;
                        cartDto.CartDetails.First().CartHeaderId = cartDetailsFromDb.CartHeaderId;
                        cartDto.CartDetails.First().Id = cartDetailsFromDb.Id;
                        _appDbContext.CartDetails.Update(_mapper.Map<CartDetail>(cartDto.CartDetails.First()));
                        await _appDbContext.SaveChangesAsync();
                    }
                }
                _response.Result = cartDto;
            }
            catch (Exception ex)
            {
                _response.Message= ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpDelete("RemoveCart")]
        public async Task<ResponseDto> RemoveCart([FromBody]int cartDetailsId)
        {
            try
            {
                CartDetail cartDetail = await _appDbContext.CartDetails.FirstAsync(u => u.Id == cartDetailsId);
                int totalCountOfCartItem = _appDbContext.CartDetails.Where(u=>u.CartHeaderId==cartDetail.CartHeaderId).Count();
                _appDbContext.CartDetails.Remove(cartDetail);
                if (totalCountOfCartItem == 1)
                {
                    var cartHeaderToRemove = await _appDbContext.CartHeaders.FirstOrDefaultAsync(u => u.Id == cartDetail.CartHeaderId);

                    _appDbContext.CartHeaders.Remove(cartHeaderToRemove);
                }
                await _appDbContext.SaveChangesAsync();

                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.Message= ex.Message; 
                _response.IsSuccess = false;
            }
            return _response;
        }
        [HttpPost("ApplyCoupon")]
        public async Task<object> ApplyCoupon([FromBody]CartDto cartDto)
        {
            try
            {
                var cartFromDb = await _appDbContext.CartHeaders.FirstAsync(u => u.UserId == cartDto.CartHeader.UserId);
                cartFromDb.CouponCode = cartDto.CartHeader.CouponCode;
                _appDbContext.CartHeaders.Update(cartFromDb);
                await _appDbContext.SaveChangesAsync();
                _response.Result=true;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }
            return _response;

        }

        [HttpPost("EmailCartRequest")]
        public async Task<object> EmailCartRequest([FromBody] CartDto cartDto)
        {
            try
            {
                await _messageBus.PublishMessage(cartDto, _configuration.GetValue<string>("TopicAndQueueNames:EmailShoppingCart"));
                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }
            return _response;

        }
        [HttpDelete("RemoveCoupon")]
        public async Task<object> RemoveCoupon([FromBody] CartDto cartDto)
        {
            try
            {
                var cartFromDb = await _appDbContext.CartHeaders.FirstAsync(u => u.UserId == cartDto.CartHeader.UserId);
                cartFromDb.CouponCode = "";
                _appDbContext.CartHeaders.Update(cartFromDb);
                await _appDbContext.SaveChangesAsync();
                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }
            return _response;

        }
    }
}
