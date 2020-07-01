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


        /// <summary>
        /// Count for next order ID
        /// </summary>
        /// <returns></returns>
        public int NewOrder()
        {

            int thisOrderId = table.Count() + 3;
            return thisOrderId;

        }

        //public Orders GetWithNavigations(int orderid)
        //{
        //    var pastOrder = table
        //        .Include(o => o.OrderLine)
        //            .ThenInclude(or => or.Product);
        //    //.FirstOrDefault();

        //    var businessOrders = mapper.Map<Orders>(pastOrder);
        //    return businessOrders;
        //}



        /// <summary>
        /// Returns the order to be added to the database, given customer ID and location ID
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="locationId"></param>
        /// <returns></returns>
        public Orders AddingANewOrder(int customerId, int locationId)
        {
            DateTime date = DateTime.Now;
            var orderDate = DateTime.Today;
            var orderTime = date.TimeOfDay;
            var theOrderToBeAdded = new Orders { CustomerId = customerId, LocationId = locationId, Date = orderDate, Time = orderTime };
            return theOrderToBeAdded;
        }
    }
}
