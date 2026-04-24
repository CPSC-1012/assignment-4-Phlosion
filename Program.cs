
using System;
using System.Collections.Generic;
using System.IO;

namespace CustomerManagement
{
    class Program
    {
        static List<Customer> customerList = new List<Customer>();
        const string FILE_NAME = "customers.csv";

        static void Main(string[] args)
        {
            LoadFromFile(); 

            bool keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("\n--- Main Menu ---");
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. List All Customers");
                Console.WriteLine("3. View Detailed Info");
                Console.WriteLine("4. Quit");
                Console.Write("Choice: ");
                
                string input = Console.ReadLine();

                if (input == "1") AddCustomer();
                else if (input == "2") ListAll();
                else if (input == "3") ViewDetails();
                else if (input == "4")
                {
                    SaveToFile(); 
                    keepRunning = false;
                }
                else Console.WriteLine("Invalid choice.");
            }
        }

        static void AddCustomer()
        {
            try 
            {
                Console.Write("First Name: ");
                string fn = Console.ReadLine();
                Console.Write("Last Name: ");
                string ln = Console.ReadLine();
                Console.Write("Order Count: ");
                int count = int.Parse(Console.ReadLine());
                Console.Write("Total Sales: ");
                double sales = double.Parse(Console.ReadLine());

                Customer newCust = new Customer(fn, ln, count, sales);
                customerList.Add(newCust);
                Console.WriteLine("Success!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void ListAll()
        {
            Console.WriteLine("\nAll Customers:");
            foreach (Customer c in customerList)
            {
                Console.WriteLine("- " + c.FullName);
            }
        }

        static void ViewDetails()
        {
            Console.Write("Enter Last Name to search: ");
            string search = Console.ReadLine();
            bool found = false;

            foreach (Customer c in customerList)
            {
                if (c.LastName.ToLower() == search.ToLower())
                {
                    Console.WriteLine("Name: " + c.FullName);
                    Console.WriteLine("Tier: " + c.CustomerTier);
                    Console.WriteLine("Avg:  " + c.AverageOrder.ToString("C")); // "C" formats as currency
                    found = true;
                }
            }
            if (!found) Console.WriteLine("Customer not found.");
        }

        static void SaveToFile()
        {
            StreamWriter writer = new StreamWriter(FILE_NAME);
            foreach (Customer c in customerList)
            {
                writer.WriteLine(c.ToCsvLine());
            }
            writer.Close();
        }

        static void LoadFromFile()
        {
            if (File.Exists(FILE_NAME))
            {
                string[] lines = File.ReadAllLines(FILE_NAME);
                foreach (string line in lines)
                {
                    string[] data = line.Split(',');
                    Customer loadedCust = new Customer(data[0], data[1], int.Parse(data[2]), double.Parse(data[3]));
                    customerList.Add(loadedCust);
                }
            }
        }
    }
}
