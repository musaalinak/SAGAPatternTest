using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Messaging;
using SagaService.Events;

namespace SagaService
{
    public class OrderSagaStateMachine : MassTransitStateMachine<OrderSagaState>
    {
        public State Received { get; set; }

        public State Proceeded { get; set; }

        public Event<IOrderCommand> OrderCommand { get; set; }

        public Event<IOrderProceededEvent> OrderProceeded { get; set; }

        public OrderSagaStateMachine()
        {
            InstanceState(i => i.CurrentState);

            Event(() => OrderCommand, x => x.CorrelateBy(state => state.OrderId.ToString(), context => context.Message.OrderID.ToString()).SelectId(s => Guid.NewGuid()));

            Event(() => OrderProceeded, c => c.CorrelateById(s => s.Message.CorrelationId));

            Initially(
                When(OrderCommand).Then(context =>
                {
                    context.Saga.OrderCode = context.Message.OrderCode;
                    context.Saga.OrderId = context.Message.OrderID;
                }).ThenAsync(context => Console.Out.WriteLineAsync($"OrderRecived {context.Message.OrderID} ")).TransitionTo(Received)
                .Publish(context=>new OrderRecievedEvent { CorrelationId=context.Saga.CorrelationId,OrderId=context.Saga.OrderId,OrderCode=context.Saga.OrderCode}));
            During(Received,
                When(OrderProceeded).ThenAsync(context =>
                    Console.Out.WriteLineAsync($"Saga Order Proceeded{context.Message.OrderId}"))
                .TransitionTo(Proceeded)
                .Finalize());

            SetCompletedWhenFinalized();
        }
    }
}
