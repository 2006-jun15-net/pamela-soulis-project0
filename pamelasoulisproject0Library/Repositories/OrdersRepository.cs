using Microsoft.EntityFrameworkCore;
using pamela_soulis_project0DataAccess.Model;
using pamela_soulis_project0Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace pamelasoulisproject0Library.Repositories
{



    public class OrdersRepository : GenericRepository<pamela_soulis_project0DataAccess.Model.Orders, pamelasoulisproject0Library.Orders>
    {

        public OrdersRepository(pamelasoulisproject0Context _context) : base(_context)
        {

        }

        public int NewOrder()
        {

            int thisOrderId = table.Count() + 1;
            return thisOrderId;

        }

        public Orders GetWithNavigations(int orderid)  
        {
            var pastOrder = table
                .Include(o => o.OrderLine)
                    .ThenInclude(or => or.Product);
                //.FirstOrDefault();

            var businessOrders = mapper.Map<Orders>(pastOrder);
            return businessOrders; 
        }
    }

}
