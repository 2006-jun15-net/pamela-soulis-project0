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



        public Inventory GetProductQuantity(int productId)
        {
            var inventoryAvailable = table
                .First(i => i.ProductId == productId);
                

            var businessInventory = mapper.Map<Inventory>(inventoryAvailable);
            return businessInventory;
        }

       

    }
}
