using MassTransit;
using Messaging;

namespace Order.API.Consumers
{
    public class OrderRecievedConsumer : IConsumer<IOrderRecievedEvent>
    {
        private readonly ILogger<OrderRecievedConsumer> logger;

        public OrderRecievedConsumer(ILogger<OrderRecievedConsumer> logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<IOrderRecievedEvent> context)
        {
            this.logger.LogInformation($"Order recieved :{context.Message.OrderId}");



            await context.Publish<IOrderProceededEvent>(new
            {
                CorrelationId = context.Message.CorrelationId,
                OrderId = context.Message.OrderId
            });

        }
    }
}
