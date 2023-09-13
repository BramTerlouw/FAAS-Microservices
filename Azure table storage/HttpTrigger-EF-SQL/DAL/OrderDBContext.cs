using HttpTrigger_EF_SQL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpTrigger_EF_SQL.DAL
{
    public class OrderDBContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
    }
}
