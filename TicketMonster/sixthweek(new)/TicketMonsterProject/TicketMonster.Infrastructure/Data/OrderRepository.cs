using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMonster.ApplicationCore.Entities;
using TicketMonster.ApplicationCore.Interfaces;

namespace TicketMonster.Infrastructure.Data
{
    public class OrderRepository : EfRepository<Order> ,CreateOrderRepository
    {

        public OrderRepository(TicketMonsterContext dbContext) : base(dbContext)
        {
        }

        public void CreateOrderAndDetail(Order order, IEnumerable<OrderDetail> orderdetails)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    Add(order);
                    DbContext.SaveChanges();
                    var detailEntites = orderdetails.ToList();
                    foreach (var entity in detailEntites)
                    {
                        entity.OrderId = order.Id;
                    }
                    DbContext.OrderDetails.AddRange(detailEntites);
                    DbContext.SaveChanges();
                    transaction.Commit();
                }
                catch{
                    transaction.Rollback();
                }
            }
        }       
    }
}
