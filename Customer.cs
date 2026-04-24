using System;

namespace CustomerManagement
{
    public class Customer
    {
        private string _firstName;
        private string _lastName;
        private int _orderCount;
        private double _totalSales;

        public string FirstName
        {
            get { return _firstName; }
            set 
            { 
                if (string.IsNullOrWhiteSpace(value)) 
                    throw new Exception("First Name cannot be empty.");
                _firstName = value.Trim(); 
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set 
            { 
                if (string.IsNullOrWhiteSpace(value)) 
                    throw new Exception("Last Name cannot be empty.");
                _lastName = value.Trim(); 
            }
        }

        public int OrderCount
        {
            get { return _orderCount; }
            set 
            { 
                if (value <= 0) throw new Exception("Orders must be > 0.");
                _orderCount = value; 
            }
        }

        public double TotalSales
        {
            get { return _totalSales; }
            set 
            { 
                if (value < 0) throw new Exception("Sales cannot be negative.");
                _totalSales = value; 
            }
        }

        public string FullName { get { return LastName + "," + FirstName; } }
        
        public double AverageOrder { get { return TotalSales / OrderCount; } }

        public string CustomerTier
        {
            get
            {
                if (OrderCount < 10) return "Bronze";
                else if (OrderCount < 50) return "Silver";
                else return "Gold";
            }
        }

        public Customer(string fName, string lName, int count, double sales)
        {
            FirstName = fName;
            LastName = lName;
            OrderCount = count;
            TotalSales = sales;
        }

        public string ToCsvLine()
        {
            return FirstName + "," + LastName + "," + OrderCount + "," + TotalSales;
        }
    }
}