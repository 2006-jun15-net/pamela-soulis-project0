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
            //.UseLoggerFactory(MyLoggerFactory)
            .UseSqlServer(SecretConfiguration.ConnectionString)
            .Options;

        public static pamelasoulisproject0Context context = new pamelasoulisproject0Context(Options);

                            


        static void Main(string[] args)
        {
            CustomerRepository crepo = new CustomerRepository(context);
            LocationRepository locrepo = new LocationRepository(context);
            InventoryRepository invrepo = new InventoryRepository(context);
            OrdersRepository ordrepo = new OrdersRepository(context);
            OrderLineRepository ordlirepo = new OrderLineRepository(context);
            GenericRepository<pamela_soulis_project0DataAccess.Model.Product, pamelasoulisproject0Library.Product> prepo = new GenericRepository<pamela_soulis_project0DataAccess.Model.Product, pamelasoulisproject0Library.Product>(context);
                        
                        


            Console.WriteLine("Hello! Welcome to the store!");
            while (true)
            {
                Console.WriteLine("a:\tDisplay store locations.");
                Console.WriteLine("b:\tYou are a new customer.");
                //for presentation, to show that the new customer was added successfully
                Console.WriteLine("c:\tDisplay customers.");
                Console.WriteLine("d:\tYou are a returning customer.");
                Console.WriteLine("e:\tYou want to view your order history.");
                Console.WriteLine("f:\tYou want to place an order.");               
                Console.WriteLine("z:\tTo display location order history.");


                Console.WriteLine("q:\tExit.");
                var input = Console.ReadLine();



                //display store locations to customers
                if (input == "a")
                {
                    var TheLocationsAvailable = locrepo.GetAll().ToList();
                    foreach (var store in TheLocationsAvailable)
                    {
                        Console.WriteLine($"Location: {store.Name} with ID number {store.LocationId}.");

                    }

                }



                //add a customer to the DB given user input
                else if (input == "b")
                {

                    Console.WriteLine("Enter your firstname: ");
                    string name1 = Console.ReadLine();
                    Console.WriteLine("Enter your lastname: ");
                    string name2 = Console.ReadLine();
                    var newCustomer = crepo.AddingANewCustomer(name1, name2);
                    crepo.Insert(newCustomer);
                    crepo.SaveToDB();
                }


                //display all customers to check if last one was added successfully
                else if (input == "c")
                {

                    var AllTheCustomers = crepo.GetAll().ToList();
                    foreach (var person in AllTheCustomers)
                    {
                        Console.WriteLine($"Here are our returning customers: {person.FirstName} {person.LastName} with ID number {person.CustomerId}.");

                    }

                }



                //get a customer by id
                else if (input == "d")
                {


                    Console.WriteLine("Enter your ID number: ");
                    int id = int.Parse(Console.ReadLine());
                    if (id == 5 || id == 6)
                    {
                        var ReturningCustomer = crepo.GetById(id);
                        Console.WriteLine($"Hi {ReturningCustomer.FirstName} {ReturningCustomer.LastName}! Welcome back!");
                    }

                    else
                    {
                        throw new ArgumentOutOfRangeException();

                    }



                }





                
                //display customer order history
                else if (input == "e")
                {
                    Console.WriteLine("Enter your customer ID number: ");
                    int CustomerIdOrderHistory = int.Parse(Console.ReadLine());

                    var customerOrderHistory = crepo.GetWithNavigations(CustomerIdOrderHistory);
                    foreach (var order in customerOrderHistory.Orders)
                    {
                        Console.WriteLine($"On {order.Date} at {order.Time} you ordered {string.Join(", ", order.OrderLine.Select(ol => ol.Product.Name))}.");
                    }

                }
                


                
                //Display products, display inventory, add an order
                else if (input == "f")
                {

                    Console.WriteLine("Enter your customer ID number: ");
                    int orderingCustomerId = int.Parse(Console.ReadLine());
                    
                     
                    var TheProductsAvailable = prepo.GetAll().ToList();
                    foreach (var item in TheProductsAvailable)
                    {

                        Console.WriteLine($"Product: {item.Name} with Id {item.ProductId}. We are selling this item for ${item.Price}.");

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
                            Console.WriteLine($" We have {productAmount.Quantity} left at this location.");
                        }
                                       
                    }

                    //check if customer asks for too much product based on inventory available
                    Console.WriteLine("How many would you like?");
                    int amountOfProduct = int.Parse(Console.ReadLine());
                    while (amountOfProduct <= 0)
                    {
                        Console.WriteLine("Invalid input, please enter a valid product quantity.");
                    }

                    //get the max amount available to order for this product
                    var maxAmountForOrder = invrepo.GetProductQuantity(product1);

                    
                    if(maxAmountForOrder.Quantity == 0)
                    {
                        Console.WriteLine($"We apologize, but this product is out of stock.");
                    }
                    else if (amountOfProduct > maxAmountForOrder.Quantity)
                    {
                        Console.WriteLine($" Oups, seems like you tried to order too much! We only have {maxAmountForOrder.Quantity}.");
                    }
                    else
                    {
                        //add the order 
                        int thisNewOrderId = ordrepo.NewOrder();
                        var newOrder = ordrepo.AddingANewOrder(orderingCustomerId, location1);
                        ordrepo.Insert(newOrder);
                        ordrepo.SaveToDB();
                        var thisNewOrder = ordlirepo.AddingANewOrderLine(thisNewOrderId, product1, amountOfProduct);
                        ordlirepo.Insert(thisNewOrder);
                        ordlirepo.SaveToDB();
                        
                        

                        //decrease inventory
                        maxAmountForOrder.Quantity = maxAmountForOrder.Quantity - amountOfProduct;
                        var newInventory = invrepo.UpdateTheQuantity(product1, location1, maxAmountForOrder.Quantity);
                        Console.WriteLine($"We now only have {newInventory.Quantity} of this product.");
                        invrepo.SaveToDB();

                        var customerOrderHistory = crepo.GetWithNavigations(orderingCustomerId);
                        foreach (var order in customerOrderHistory.Orders)
                        {
                            Console.WriteLine($"Congratulations! Your order for {string.Join(", ", order.OrderLine.Select(ol => ol.Product.Name))} was placed successfully.");
                        }
                    }

                }

                
                //display location order history
                else if (input == "z")
                {
                    Console.WriteLine("Please enter the location ID number.");
                    int theLocationID = int.Parse(Console.ReadLine());
                    var locationOrderHistory = locrepo.GetOrderHistory(theLocationID);
                    foreach (var order in locationOrderHistory.Orders)
                    {
                        Console.WriteLine($"On {order.Date} {string.Join(", ", order.OrderLine.Select(ol => ol.Product.Name))} was ordered from this location");
                    }
                }




                //display order details
                //else if (input == "x")
                //{
                //    Console.WriteLine("Enter your order ID number: ");
                //    int id = int.Parse(Console.ReadLine());
                //    var ReturningCustomerOrderHistory = ordrepo.GetById(id); //orderId
                //    Console.WriteLine($"You placed your last order at {ReturningCustomerOrderHistory.Date} {ReturningCustomerOrderHistory.Time}");
                //}



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
                   
