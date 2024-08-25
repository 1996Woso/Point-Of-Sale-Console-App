using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static POS.Program;

namespace POS
{
    internal class Program
    {

        public class Customer
        {
            public int Customer_Id { get; set; }
            public string Customer_Name { get; set; }
            public bool Loyalty_Card { get; set; }
        }
        public class Product
        {
            public int Product_Id { get; set; }
            public string Product_Name { get; set; }
            public double Product_Price { get; set; }
            public bool Standard_Discount { get; set; }
        }
        //Method to add customer
        public static void AddCustomer(List<Customer> customer_list, int id, string name, bool has_loyalty_card)
        {
            customer_list.Add(new Customer
            {
                Customer_Id = id,
                Customer_Name = name,
                Loyalty_Card = has_loyalty_card
            });
        }
        //Method to add product
        public static void AddProduct(List<Product> product_list, int id, string name, double price, bool has_discount)
        {
            product_list.Add(new Product
            {
                Product_Id = id,
                Product_Name = name,
                Product_Price = price,
                Standard_Discount = has_discount
            });
        }
        public static void Main(string[] args)
        {
            //Initialise customer list
            var customer_list = new List<Customer>();
            //Adding customers to the List
            AddCustomer(customer_list, 1, "Sam Smit", true);
            AddCustomer(customer_list, 2, "Peter Parker", false);
            AddCustomer(customer_list, 3, "Grace Samson", true);

            //Initialise Product list
            var product_list = new List<Product>();
            //Adding customers to the List
            AddProduct(product_list, 1, "Apples", 21.99, false);
            AddProduct(product_list, 2, "Bread", 18.99, false);
            AddProduct(product_list, 3, "Eggs", 69.99, true);
            AddProduct(product_list, 4, "Milk", 110.99, true);
            AddProduct(product_list, 5, "Sugar", 49.99, false);
            AddProduct(product_list, 6, "Meat", 150.99, false);
            AddProduct(product_list, 7, "Oranges", 31.99, true);
            //declaring and intialising the following variables
            int customerId_input;
            bool valid_customerId_input = true;
            int productId_input;
            bool valid_productId_input = true;
            double grand_total = 0;//Grand total
            double original_amount = 0;//Original amount
            double discounted_amount = 0;//Discounted amount
            Console.WriteLine("------------------------ INPUT ------------------------\n");
            while (!valid_customerId_input)
            {
                Console.Write("Enter your Customer Id: ");
                if (int.TryParse(Console.ReadLine(), out customerId_input))//Returns true if user input is an integer
                {
                    var selected_customer = customer_list.Find(c => c.Customer_Id == customerId_input);//Finds the customer by using Customer_Id
                    if (selected_customer != null)//Returns true if customer is found on the customer_list
                    {
                        valid_customerId_input = false;//You no longer have option to enter Customer_Id since customer is found.
                        while (!valid_productId_input)//this while loop allows user to select multiple products unless fonfirmed the last product
                        {
                            //Declares and initialises the price and dicount of the selected product
                            double discount = 0;
                            double amount = 0;
                            Console.Write("Enter Product Id (or Done to Finish):");
                            string input = Console.ReadLine();
                            if(input.ToLower() != "done" && int.TryParse(input,out productId_input))//Returns true if user input is an integer and not 'done'
                            {
                                var selected_product = product_list.Find(p => p.Product_Id == productId_input);//Finds the product by using Product_Id
                                if (selected_product != null)
                                {
                                    
                                    amount += selected_product.Product_Price;//Adds price of the selected product to the initial value of amount
                                    //Apply Standard Discount
                                    if (selected_product.Standard_Discount == true)
                                    {
                                        discount+= (5.0 / 100) * amount;//Adds discount of the selected product to the initial value of discount
                                    }
                                    //Apply Loyalt Card Discount
                                    if(selected_customer.Loyalty_Card == true)
                                    {
                                        discount += (10.0 / 100) * (amount-discount);
                                    }
                                    original_amount += amount;
                                    discounted_amount += discount;
                                    grand_total += amount-discount;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Product ID. Please try again.");
                                }
                            }
                            else if(input.ToLower() == "done")//When user confirms the last product will no longer select another product
                            {
                                valid_productId_input = false;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Customer ID. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a numeric value.");
                }
            }
            
            Console.WriteLine($"------------------------ OUTPUT ------------------------\nGrand Total: {grand_total.ToString("C2")}\n" +
                $"Original Amount: {original_amount.ToString("C2")}\n" +
                $"Discounted Amount: {discounted_amount.ToString("C2")}");
            Console.ReadLine();
        }
    }
}
