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


        //this gives the quantity available for a particular product 
        public Inventory GetProductQuantity(int productId)
        {
            var inventoryAvailable = table
                .First(i => i.ProductId == productId);
                

            var businessInventory = mapper.Map<Inventory>(inventoryAvailable);
            return businessInventory;
        }

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


        //    var course = context.Course.Find(1000); //get the course with PK 1000 (hardcoded in the Add method)
        //    course.CourseNumber = "PHYS102";
        //    context.SaveChanges();

    }
}
