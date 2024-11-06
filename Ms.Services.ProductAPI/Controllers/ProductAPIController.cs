using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ms.Services.ProductAPI.Models;
using Ms.Services.ProductAPI.Models.Dto;
using MS.Services.ProductAPI.Data;
using MS.Services.ProductAPI.Models.Dto;

namespace Ms.Services.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _responseDto;
        private IMapper _mapper;
        public ProductAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _responseDto = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Product> objList = _db.Products.ToList();
                _responseDto.Result = _mapper.Map<IEnumerable<ProductDto>>(objList);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Product coupon = _db.Products.First(u => u.Id == id);
                _responseDto.Result = _mapper.Map<ProductDto>(coupon);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
        //[HttpGet]
        //[Route("GetByCode/{code}")]
        //public ResponseDto GetByCode(string code)
        //{
        //    try
        //    {
        //        Product coupon = _db.Products.FirstOrDefault(u => u.Produc.ToLower() == code.ToLower());
        //        if (coupon == null)
        //            _responseDto.IsSuccess = false;
        //        _responseDto.Result = _mapper.Map<ProductDto>(coupon);
        //    }
        //    catch (Exception ex)
        //    {
        //        _responseDto.IsSuccess = false;
        //        _responseDto.Message = ex.Message;
        //    }
        //    return _responseDto;
        //}
        [HttpPost]
        [Authorize(Roles = "ADMIN")]

        public ResponseDto Post([FromBody] ProductDto productDto)
        {
            try
            {
                _db.Products.Add(_mapper.Map<Product>(productDto));
                _db.SaveChanges();
                _responseDto.Result = productDto;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
        [HttpPut]
        [Authorize(Roles = "ADMIN")]

        public ResponseDto Put([FromBody] ProductDto productDto)
        {
            try
            {
                _db.Products.Update(_mapper.Map<Product>(productDto));
                _db.SaveChanges();
                _responseDto.Result = productDto;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]

        public ResponseDto Delete(int id)
        {
            try
            {
                Product coupon = _db.Products.Find(id);
                _db.Products.Remove(coupon);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
    }
}

