using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using pamela_soulis_project0DataAccess;
using pamela_soulis_project0DataAccess.Model;
using pamelasoulisproject0Library.Repositories;
using pamelasoulisproject0Library;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using pamela_soulis_project0Library.Repositories;

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



        
        


        static void Main(string[] args)
        {
            CustomerRepository crepo = new CustomerRepository(context);
            LocationRepository locrepo = new LocationRepository(context);
            InventoryRepository invrepo = new InventoryRepository(context);
            OrdersRepository ordrepo = new OrdersRepository(context);
            GenericRepository<pamela_soulis_project0DataAccess.Model.Location, pamelasoulisproject0Library.Location> lrepo = new GenericRepository<pamela_soulis_project0DataAccess.Model.Location, pamelasoulisproject0Library.Location>(context);
            GenericRepository<pamela_soulis_project0DataAccess.Model.Product, pamelasoulisproject0Library.Product> prepo = new GenericRepository<pamela_soulis_project0DataAccess.Model.Product, pamelasoulisproject0Library.Product>(context);
            //GenericRepository<pamela_soulis_project0DataAccess.Model.Inventory, pamelasoulisproject0Library.Inventory> irepo = new GenericRepository<pamela_soulis_project0DataAccess.Model.Inventory, pamelasoulisproject0Library.Inventory>(context);
            GenericRepository<pamela_soulis_project0DataAccess.Model.OrderLine, pamelasoulisproject0Library.OrderLine> olrepo = new GenericRepository<pamela_soulis_project0DataAccess.Model.OrderLine, pamelasoulisproject0Library.OrderLine>(context);
            //GenericRepository<pamela_soulis_project0DataAccess.Model.Orders, pamelasoulisproject0Library.Orders> orepo = new GenericRepository<pamela_soulis_project0DataAccess.Model.Orders, pamelasoulisproject0Library.Orders>(context);


            


            Console.WriteLine("Hello! Welcome to the store!");
            while (true)
            {
                Console.WriteLine("a:\tDisplay store locations.");
                Console.WriteLine("b:\tYou are a new customer.");
                Console.WriteLine("c:\tYou are a returning customer.");
                Console.WriteLine("d:\tYou want to place an order.");
                Console.WriteLine("x:\tTo view your past order details.");
                Console.WriteLine("y:\tTo Display customer order history.");
                Console.WriteLine("z:\tTo Display location order history.");


                Console.WriteLine("q:\tExit.");
                var input = Console.ReadLine();


                //this works, but need the date
                //display customer order history
                if (input == "y")
                {
                    var maxAmountForOrder = invrepo.GetProductQuantity(1);
                    Console.WriteLine($"{maxAmountForOrder.Quantity}");
                    //var customerOrderHistory = crepo.GetWithNavigations(1);
                    //foreach (var order in customerOrderHistory.Orders)
                    //{
                    //    Console.WriteLine($"{order.Date} {string.Join(", ", order.OrderLine.Select(ol=>ol.Product.Name))}");
                    //}
                                        

                }
                //this works, but need the date
                //display location order history
                else if (input == "z")
                {
                    var locationOrderHistory = locrepo.GetOrderHistory(5);
                    foreach (var order in locationOrderHistory.Orders)
                    {
                        Console.WriteLine($"{order.Date} {string.Join(", ", order.OrderLine.Select(ol => ol.Product.Name))}");
                    }
                }
                



                //this works
                //display store locations to customers
                if (input == "a")
                {
                   var TheLocationsAvailable = lrepo.GetAll().ToList();
                   foreach (var store in TheLocationsAvailable)
                   {
                       Console.WriteLine($"Location: {store.Name}.");


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

                //this works
                //get a customer by id
                else if (input == "c")
                {

                    Console.WriteLine("Enter your ID number: ");
                    int id = int.Parse(Console.ReadLine());
                    var ReturningCustomer = crepo.GetById(id);
                    Console.WriteLine($"Hi {ReturningCustomer.FirstName} {ReturningCustomer.LastName}!");
                }
                //display the date and time of last order
                else if (input == "x")
                {
                    Console.WriteLine("Enter your order ID number: ");
                    int id = int.Parse(Console.ReadLine());
                    var ReturningCustomerOrderHistory = ordrepo.GetById(id); //orderId
                    Console.WriteLine($"You placed your last order at {ReturningCustomerOrderHistory.Date} {ReturningCustomerOrderHistory.Time}");
                }
                


                //this works, but not w/ separation
                //Display products, display inventory, add to OrderLine
                else if (input == "d")
                {

                    Console.WriteLine("Enter your customer ID number: ");
                    int orderingCustomerId = int.Parse(Console.ReadLine());
                    
                     
                    var TheProductsAvailable = prepo.GetAll().ToList();
                    foreach (var item in TheProductsAvailable)
                    {

                        Console.WriteLine($"Product: {item.Name} with Id {item.ProductId}. We are selling this item for ${item.Price}");

                    }

                    Console.WriteLine("Enter a location ID to place an order at this store.");
                    int location1 = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter a product ID to add it to your cart.");
                    int product1 = int.Parse(Console.ReadLine());

                    //display the amount left of a product at a particular location
                    var locationInventory = locrepo.GetWithNavigations(location1);
                    
                    foreach (var productAmount in locationInventory.Inventory)
                    {
                        if (productAmount.ProductId == product1)
                        {
                            Console.WriteLine($" We have {productAmount.Quantity} left at this location");
                        }
                        
                   
                    }
                    //check if customer asks for too much product based on inventory available
                    Console.WriteLine("How many would you like?");
                    int amountOfProduct = int.Parse(Console.ReadLine());
                    while (amountOfProduct <= 0)
                    {
                        Console.WriteLine("Invalid input, please enter a valid product quantity.");
                    }
                    //var maxAmountForOrder = invrepo.GetProductQuantity(product1);
                    //Console.WriteLine(maxAmountForOrder);




                    int thisNewOrderId = ordrepo.NewOrder(); //orderid
                    //var newOrder = new pamelasoulisproject0Library.Orders { CustomerId = orderingCustomerId, LocationId = location1 };
                    //ordrepo.Insert(newOrder);
                    //ordrepo.SaveToDB();
                    var thisNewOrder = new pamelasoulisproject0Library.OrderLine { OrderId = thisNewOrderId, ProductId = product1, Quantity = amountOfProduct };
                    olrepo.Insert(thisNewOrder);
                    olrepo.SaveToDB();

                    //display order details
                    //var customerOrderHistory = crepo.GetWithNavigations(orderingCustomerId);
                    //foreach (var order in customerOrderHistory.Orders)
                    //{
                    //    Console.WriteLine($"Congratulations! Your order for {string.Join(", ", order.OrderLine.Select(ol => ol.Product.Name))} was placed on {order.Date}");
                    //}
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
