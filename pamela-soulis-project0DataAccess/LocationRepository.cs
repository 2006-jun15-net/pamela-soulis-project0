//using pamela_soulis_project0DataAccess.Model;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Collections;
//using System.Linq;
//using Microsoft.Extensions.DependencyModel;

//namespace pamela_soulis_project0DataAccess
//{
//    public class LocationRepository : ILocationRepository
//    {
//        private readonly pamelasoulisproject0Context context;

//        public LocationRepository(pamelasoulisproject0Context context)
//        {
//            this.context = context;
//        }

        

//        public IEnumerable<Library.Location> GetLocations(string name)
//        {

//            //return context.Location.ToList();
//            return Mapper.MapLocationWithOrders(context.Location.Find(name));
//        }
//    }
    
    
//}
