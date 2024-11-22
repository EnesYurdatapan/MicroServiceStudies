using Azure.Messaging.ServiceBus;
using Ms.Services.EmailAPI.Models.Dtos;
using Ms.Services.EmailAPI.Services;
using Newtonsoft.Json;
using System.Text;

namespace Ms.Services.EmailAPI.Messaging
{
    public class AzureServiceBusConsumer:IAzureServiceBusConsumer
    {
        private readonly string serviceBusConnectionString;
        private readonly string emailCartQueue;
        private readonly IConfiguration _configuration;
        private ServiceBusProcessor _processor;
        private readonly EmailService _emailService;

        public AzureServiceBusConsumer(IConfiguration configuration, ServiceBusProcessor processor, EmailService emailService)
        {
            _configuration = configuration;
            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            emailCartQueue = _configuration.GetValue<string>("TopicAndQueueNames:EmailShoppingCartQueue");

            var client = new ServiceBusClient(serviceBusConnectionString);
            _processor = client.CreateProcessor(emailCartQueue); // for listening to the queue for any new messages
            _emailService = emailService;
        }

        public async Task Start()
        {
            _processor.ProcessMessageAsync += OnEmailCartRequestReceived;
            _processor.ProcessErrorAsync += ErrorHandler;
            await _processor.StartProcessingAsync();
        }

        private async Task OnEmailCartRequestReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            CartDto objMessage = JsonConvert.DeserializeObject<CartDto>(body);
            try
            {
                //TODO --- try to log email
                await _emailService.EmailCartAndLog(objMessage);
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async Task ErrorHandler(ProcessErrorEventArgs args)
        {
            await Console.Out.WriteLineAsync(args.Exception.ToString());
        }

        public async Task Stop()
        {
            await _processor.StopProcessingAsync();
            await _processor.DisposeAsync();
        }
    }
}
