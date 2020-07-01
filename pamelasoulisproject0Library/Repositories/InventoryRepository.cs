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



    public class InventoryRepository : GenericRepository<pamela_soulis_project0DataAccess.Model.Inventory, pamelasoulisproject0Library.Inventory>
    {

        public InventoryRepository(pamelasoulisproject0Context _context) : base(_context)
        {

        }


        /// <summary>
        /// Returns the quantity available for a particular product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        
        public Inventory GetProductQuantity(int productId)
        {
            var inventoryAvailable = table
                .First(i => i.ProductId == productId);
                

            var businessInventory = mapper.Map<Inventory>(inventoryAvailable);
            return businessInventory;
        }



        /// <summary>
        /// Method to update the quantity available for a product at a specific store location
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="locationId"></param>
        /// <param name="newUpdatedQuantity"></param>
        /// <returns></returns>
        public Inventory UpdateTheQuantity(int productId, int locationId, int newUpdatedQuantity)
        {
            var inventoryAvailable = table
                .First(i => (i.ProductId == productId) && (i.LocationId == locationId));

            var businessInventory = mapper.Map<Inventory>(inventoryAvailable);
            businessInventory.Quantity = newUpdatedQuantity;
            return businessInventory;

        }
        //public static void UpdateSomeData()
        //{
        //    //    using var context = new pamelasoulisproject0Context(Options);
        //    //    var EmployeeToUpdate = context.Employee.First();
        //    //    EmployeeToUpdate.FirstName = "Alejandro";
        //    //    context.SaveChanges();


    }
}
