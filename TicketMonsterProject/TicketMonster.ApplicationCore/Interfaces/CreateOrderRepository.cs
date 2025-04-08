using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMonster.ApplicationCore.Entities;

namespace TicketMonster.ApplicationCore.Interfaces
{   
    public interface CreateOrderRepository : IRepository<Order>
    {
        void CreateOrderAndDetail(Order order, IEnumerable<OrderDetail> orderDetails);
    }
}
