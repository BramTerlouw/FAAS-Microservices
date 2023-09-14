using HttpTrigger_EF_SQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpTrigger_EF_SQL.Service
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAllOrders();

        //Order GetOrder(int id);

        //Order CreateOrder(Order order);

        //void UpdateOrder(Order order);

        //void DeleteOrder(int id);
    }
}
