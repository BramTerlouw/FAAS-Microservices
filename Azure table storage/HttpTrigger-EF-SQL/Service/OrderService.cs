using HttpTrigger_EF_SQL.DAL;
using HttpTrigger_EF_SQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpTrigger_EF_SQL.Service
{
    public class OrderService : IOrderService
    {
        private readonly OrderDBContext _dbcontext;

        public OrderService(OrderDBContext orderDBContext)
        {
            _dbcontext = orderDBContext;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _dbcontext.Orders;
        }

        //Order IOrderService.CreateOrder(Order order)
        //{
        //    throw new NotImplementedException();
        //}

        //void IOrderService.DeleteOrder(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //Order IOrderService.GetOrder(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //void IOrderService.UpdateOrder(Order order)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
