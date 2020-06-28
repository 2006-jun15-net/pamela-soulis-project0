////using pamela_soulis_project0DataAccess.Model;
////using System;
////using System.Collections;
////using System.Collections.Generic;
////using System.Linq;
////using System.Text;

////namespace pamela_soulis_project0DataAccess
////{
////    public class CustomerRepository : ICustomerRepository
////    {
////        private pamelasoulisproject0Context context;

////        public CustomerRepository(pamelasoulisproject0Context context)
////        {
////            this.context = context;
////        }

////        public IEnumerable GetCustomers()
////        {
////            return context.Customer.ToList();
////        }

////        public Customer FindCustomer(string lastname, string firstname)
////        {
////            List<Customer> returningcustomer = context.Customer
////                .Where(c => (c.LastName == lastname) && (c.FirstName == firstname))
////                .ToList();
////            return returningcustomer;

//        }
        
//        public void AddNewCustomer(Customer customer)
//        {
//            context.Customer.Add(customer);
//        }

//        public void SaveToDB()
//        {
//            context.SaveChanges();
//        }

//        //IEnumerable ICustomerRepository.GetCustomers()
//        //{
//        //    throw new NotImplementedException();
//        //}
//    }
//}
