using MassTransit;
using Messaging;

namespace Billing.API.Consumers
{
    public class OrderProceededConsumer : IConsumer<IOrderProceededEvent>
    {
        private readonly ILogger<OrderProceededConsumer> logger;
        public OrderProceededConsumer(ILogger<OrderProceededConsumer> logger)
        {
            this.logger = logger;
        }

        public Task Consume(ConsumeContext<IOrderProceededEvent> context)
        {
            this.logger.LogInformation($"Order proceded consumer :{context.Message.OrderId}");
            return Task.CompletedTask;
        }
    }
}
