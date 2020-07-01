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



    public class LocationRepository : GenericRepository<pamela_soulis_project0DataAccess.Model.Location, pamelasoulisproject0Library.Location>
    {

        public LocationRepository(pamelasoulisproject0Context _context) : base(_context)
        {

        }


        /// <summary>
        /// Returns a Location Business Entity, with access to navigation properties
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        public Location GetWithNavigations(int locationId)
        {
            var location = table
                .Include(l => l.Inventory)
                    .ThenInclude(i => i.Product)
                .FirstOrDefault();


            var businessLocation = mapper.Map<Location>(location);
            return businessLocation;
        }



        /// <summary>
        /// Method to get the order history of a location
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        public Location GetOrderHistory(int locationId)
        {
            var location = table
                .Include(l => l.Orders)
                    .ThenInclude(o => o.OrderLine)
                        .ThenInclude(o1 => o1.Product)
                .First(i => i.LocationId == locationId);

            var businessLocation = mapper.Map<Location>(location);
            return businessLocation; 
        }
    }
} 