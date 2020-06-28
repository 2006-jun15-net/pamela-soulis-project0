//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Linq;

//namespace pamela_soulis_project0DataAccess
//{
//    public static class LocationMapper : IMapper
//    {
//        //maps an EF location to a business model
//        public static pamelasoulisproject0Library.Location MapLocationWithOrders(Model.Location location)
//        {
//            return new pamelasoulisproject0Library.Location
//            {
//                LocationId = location.LocationId,
//                Name = location.Name,
//                StoreOrders = location.Orders.Select(Map).ToList()
//                StoreInventory = location.Inventory.Select(Map).ToList()
//            };
//        }
//        public static Model.Location MapLocationWithOrders(pamelasoulisproject0Library.Location location)
//        {
//            return new Model.Location
//            {
//                LocationId = location.LocationId,
//                Name = location.Name,
//                StoreOrders = location.Orders.Select(Map).ToList()
//                StoreInventory = location.Inventory.Select(Map).ToList()
//            };
//        }


//    }
//}
