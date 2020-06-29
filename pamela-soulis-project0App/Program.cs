using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using pamela_soulis_project0DataAccess;
using pamela_soulis_project0DataAccess.Model;
using pamela_soulis_project0Library.Repositories;
using pamelasoulisproject0Library; 
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace pamela_soulis_project0App
{
    public static class Program
    {
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public static readonly DbContextOptions<pamelasoulisproject0Context> Options = new DbContextOptionsBuilder<pamelasoulisproject0Context>()
            .UseLoggerFactory(MyLoggerFactory)
            .UseSqlServer(SecretConfiguration.ConnectionString)
            .Options;

        public static pamelasoulisproject0Context context = new pamelasoulisproject0Context(Options);



        public static void AddAnOrder(int orderid, int productid, int quantity)
        {
            using var context = new pamelasoulisproject0Context(Options);
            var neworder = new pamela_soulis_project0DataAccess.Model.OrderLine { OrderId = orderid, ProductId = productid, Quantity = quantity };
            context.OrderLine.Add(neworder);
            context.SaveChanges();
        }
        public static int NewOrder()
        {
            using var context = new pamelasoulisproject0Context(Options);
            int thisOrderId = context.Orders.Count() + 2;
            return thisOrderId;

        }

        


        static void Main(string[] args)
        {
            GenericRepository<pamela_soulis_project0DataAccess.Model.Customer, pamelasoulisproject0Library.Customer > crepo = new GenericRepository<pamela_soulis_project0DataAccess.Model.Customer, pamelasoulisproject0Library.Customer>(context);
            GenericRepository<pamela_soulis_project0DataAccess.Model.Location, pamelasoulisproject0Library.Location> lrepo = new GenericRepository<pamela_soulis_project0DataAccess.Model.Location, pamelasoulisproject0Library.Location>(context);
            GenericRepository<pamela_soulis_project0DataAccess.Model.Product, pamelasoulisproject0Library.Product> prepo = new GenericRepository<pamela_soulis_project0DataAccess.Model.Product, pamelasoulisproject0Library.Product>(context);
            GenericRepository<pamela_soulis_project0DataAccess.Model.Inventory, pamelasoulisproject0Library.Inventory> irepo = new GenericRepository<pamela_soulis_project0DataAccess.Model.Inventory, pamelasoulisproject0Library.Inventory>(context);
            GenericRepository<pamela_soulis_project0DataAccess.Model.OrderLine, pamelasoulisproject0Library.OrderLine> olrepo = new GenericRepository<pamela_soulis_project0DataAccess.Model.OrderLine, pamelasoulisproject0Library.OrderLine>(context);



            Console.WriteLine("Hello! Welcome to the store!");
            while (true)
            {
                Console.WriteLine("a:\tDisplay store locations.");
                Console.WriteLine("b:\tYou are a new customer.");
                Console.WriteLine("c:\tYou are a returning customer.");
                Console.WriteLine("d:\tYou want to place an order.");
               
                Console.WriteLine("q:\tExit.");
                var input = Console.ReadLine();

                //this works
                //display store locations to customers
                if (input == "a")
                {
                    var TheLocationsAvailable = lrepo.GetAll().ToList();
                    foreach (var store in TheLocationsAvailable)
                    {
                        Console.WriteLine($"Location: {store.Name}.");
                        //}
                        //while (TheLocationsAvailable.Count > 0)
                        //{
                        //for( var i = 1; i <= TheLocationsAvailable.Count; i++)
                        //{
                        //    var store = TheLocationsAvailable[i - 1];
                        //    var storeString = $"{i}: \"{store.Name}\"";
                        //    Console.WriteLine(storeString);
                        //}

                    }

                }

                //this works
                //add a customer to the DB given user input
                else if (input == "b")
                {

                    Console.WriteLine("Enter your firstname: ");
                    string name1 = Console.ReadLine();
                    Console.WriteLine("Enter your lastname: ");
                    string name2 = Console.ReadLine();
                    var newcustomer = new pamelasoulisproject0Library.Customer { FirstName = name1, LastName = name2 };
                    crepo.Insert(newcustomer);
                    crepo.SaveToDB();
                }

                //get a customer by id and display their orderhistory
                else if (input == "c")
                {

                    Console.WriteLine("Enter your id number: ");
                    int id = int.Parse(Console.ReadLine());
                    //var newcustomer = new pamela_soulis_project0DataAccess.Model.Customer { CustomerId = id };
                    //var ReturningCustomerId = crepo.GetById(id);
                    var TheCustomer = crepo.GetAll()
                        .Where(c => c.CustomerId == id)
                        .ToList();
                    foreach (var person in TheCustomer)
                    { 
                        Console.WriteLine($"Your name is {person.FirstName} {person.LastName}.");

                    }
                }

                //this workd
                //Display products, display inventory, add to OrderLine
                else if (input == "d")
                {
                    var TheProductsAvailable = prepo.GetAll().ToList();
                    foreach (var item in TheProductsAvailable)
                    {

                        Console.WriteLine($"Product: {item.Name} with Id {item.ProductId}. We are selling this item for ${item.Price}");
                    
                    }

                    Console.WriteLine("Enter a location ID to place an order at this store.");
                    int location1 = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter a product ID to add it to your cart.");
                    int product1 = int.Parse(Console.ReadLine());

                    var TheAmountAvailable = irepo.GetAll()
                            .Where(i => (i.ProductId == product1 && i.LocationId == location1))
                            .ToList();
                    foreach (var amount in TheAmountAvailable)
                    {
                        Console.WriteLine($"We have {amount.Quantity} left");
                    }

                    Console.WriteLine("How many would you like?");
                    int amount1 = int.Parse(Console.ReadLine());
                    int thisNewOrderId = NewOrder();
                    var neworder = new pamelasoulisproject0Library.OrderLine { OrderId = thisNewOrderId, ProductId = product1, Quantity = amount1 };
                    olrepo.Insert(neworder);
                    olrepo.SaveToDB();

                }
                

                else if (input == "q")
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Invalid input \"{input}\".");
                }
                 
               
            }





        
        }
              
       


    }
}
                
            


        //public static void DeleteSomeData()
        //{
        //    using var context = new pamelasoulisproject0Context(Options);
        //    var OrderToDelete = context.Orders
        //        .Where(o => o.CustomerId == 1)
        //        .First();
        //    context.Location.Remove(OrderToDelete);
        //    context.SaveChanges();
        //    //throw new NotImplementedException();

        //    using var context = new pamelasoulisproject0Context(Options);
        //    // this deletes all the students in the physics course, and the physics course too
        //    var course = context.Course
        //        .Include(c => c.Enrollment)
        //            .ThenInclude(e => e.Student)
        //        .FirstOrDefault(c => c.Id == 1000);
        //    context.Course.Remove(course); // this will remove the enrollments too bc the FK constraint has ON DELETE CASCADE
        //    context.Student.RemoveRange(course.Enrollment.Select(e => e.Student)); //remove every student reachable from that course
        //    context.SaveChanges();
        //}


        // the following method: get the first customer the database has to give, print all tracks purchased
        //public static void DoSomethingWithInvoices()
        //{
        //    using var context = new pamelasoulisproject0Context(Options);

        //    var customer = context.Customer
        //        .Include(c => c.Invoice)
        //            .ThenInclude(i => i.InvoiceLine)
        //                .ThenInclude(i1 => i1.Track)
        //        .FirstOrDefault();

        //    // select many works like select, except you can select a whole collection of stuff for each
        //    //       element and it will flatten that out to one big list
        //    IEnumerable<Track> tracks = customer.Invoice
        //        .SelectMany(i => i.InvoiceLine.Select(i1 => i1.Track));

        //    foreach(var track in tracks)
        //    {
        //        Console.WriteLine(track.Name);

        //    }

        //}

        //public static void UpdateSomeData()
        //{
        //    //    using var context = new pamelasoulisproject0Context(Options);
        //    //    var EmployeeToUpdate = context.Employee.First();
        //    //    EmployeeToUpdate.FirstName = "Alejandro";
        //    //    context.SaveChanges();
        //    //    //throw new NotImplementedException();

        //    // Nick code
        //    using var context = new pamelasoulisproject0Context(Options);
        //    var course = context.Course.Find(1000); //get the course with PK 1000 (hardcoded in the Add method)
        //    course.CourseNumber = "PHYS102";
        //    context.SaveChanges();



        //}

        //public static void AddSomeDataFromUserInput()
        //{
            //using var context = new pamelasoulisproject0Context(Options);
            //var thisEmployee = new Employee { EmployeeId = 10, FirstName = "Jane", LastName = "Doe" };
            //context.Employee.Add(thisEmployee);            
            //context.SaveChanges();

            //    //throw new NotImplementedException();




            // add a location:
            //using var context = new pamelasoulisproject0Context(Options);
            //var store1 = new Location { Name = "Clothing Downtown" };
            //var store2 = new Location { Name = "Clothing Uptown" };
            //context.Location.Add(store1);
            //context.Location.Add(store2);
            //context.SaveChanges();


            // ADDED PRODUCTS TO DB
            //using var context = new pamelasoulisproject0Context(Options);
            //var product1 = new Product { Name = "Jeans" , Price = 40};
            //var product2 = new Product { Name = "Shirt" , Price = 25};
            //var product3 = new Product { Name = "Shoes", Price = 70};
            //context.Product.Add(product1);
            //context.Product.Add(product2);
            //context.Product.Add(product3);
            //context.SaveChanges();

           





//place a hard coded order:
//using var context = new pamelasoulisproject0Context(Options);
//var neworder = new Orders { LocationId = 5, CustomerId = 1 };
//context.Orders.Add(neworder);
//context.SaveChanges();

//place an order based on customerID and locationID
//var neworder = context.Orders
//.Include(o => o.)






// create course if it doesn't exist
// returns the first result that fits the condition: "Or Default" : returns null instead 
//  of throwing exceptions in case of no result 
//var course = context.Course
//    .Include(c => c.Enrollment)
//    .FirstOrDefault(c => c.CourseNumber == "PHYS101");
//if (course == null)
//{
//    course = new Course { CourseNumber = "PHYS101" };
//    context.Course.Add(course);
//}

// add the student (and an enrollment indirecty via the course)
//      so, you can add and update stuff via the navigation properties
//course.Enrollment.Add(new Enrollment { Student = student });
//context.Customer.Add(customer); //don't need this line anymore bc add the student via the navigation properties





//}

//private static void DisplayData()
//{
//using var context = new pamelasoulisproject0Context(Options);




//display the order:
//List<Orders> firstorder = context.Orders
//    .ToList();
//foreach (var order in firstorder)
//{
//    Console.WriteLine($"This order was placed in store number {order.LocationId} and its order ID is {order.OrderId}");
//}




//retrieves the employees that have sales in their title
//List<Employee> salespeople = context.Employee
//    .Where(e => e.Title.Contains("sales"))
//    .ToList();

//foreach (Employee person in salespeople)
//{
//    Console.WriteLine($"{person.Title} {person.FirstName} {person.LastName}");
//}



// retrieves the worker in class Worker who has Pamela as a firstname
//List<Worker> TheOneIWant = context.Worker
//    .Where(w => w.FirstName.Contains("Pamela"))
//    .ToList();

//foreach (Worker person in TheOneIWant)
//{
//    Console.WriteLine($"This worker has {person.Id} as an ID and her name is {person.FirstName} {person.LastName}");

//}




// retrieves employee from class Employee with FirstName Jane
//List<Employee> GetAddedEmployee = context.Employee
//    .Where(e => e.FirstName.Contains("Jane"))
//    .ToList();

//foreach (Employee person in GetAddedEmployee)
//{
//    Console.WriteLine($"This employee's first name is {person.FirstName}");
//}


// Nick code
//this gets the entire table's worth of data: next line of code lets you access the Student table
//      then you use the Include method (eager loading) to relate to Enrollment
//      and then to Course
//List<Student> students = context.Student
//    .Include(s => s.Enrollment) // access the navigation property
//        .ThenInclude(e => e.Course) // access the navigation property
//    .ToList();

//foreach (var student in students)
//{
//    // take each element of the Enrollment collection and get the course number: 
//    var courses = student.Enrollment.Select(e => e.Course.CourseNumber);

//    // join the courses together: convert a string 
//    var coursesString = string.Join(",", courses);
//    Console.WriteLine($"{student.Id} {student.Name}: {coursesString}");
//}




// display the order based on the customer?
//List<Customer> customers = context.Customer
//    .Include(c => c.Orders)
//        .ThenInclude(o => o.OrderLine)
//    .ToList();
//foreach (var customer in customers)
//{
//    var OrderHistory = customer.Orders.Select(o => o.OrderLine.OrderId);
//}

//}

//}
//}
