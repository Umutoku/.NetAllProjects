using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;


namespace SagaStateMachineWorkerService.Models
{
    public class OrderStateDbContext : SagaDbContext
    {
        public OrderStateDbContext(DbContextOptions<OrderStateDbContext> options)
            : base(options)
        {
        }
        protected override IEnumerable<ISagaClassMap> Configurations // This is the configuration of the saga instance, which is the OrderStateMap class
        {
            get { yield return new OrderStateMap(); } // yield return ile OrderStateMap sınıfını döndürüyoruz. Yield özelliği ile bir metot birden fazla değer döndürebilir.
        }
    }
}
