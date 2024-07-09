namespace OOP
{
    public class Product
    {
        public Product(int id, string name, decimal price, bool active, bool canbeboughtoncredit)
        {
            ID = id;
            Name = name;
            Price = price;
            Active = active;
            CanBeBoughtOnCredit = canbeboughtoncredit;
        }

        private int _id;
        public int ID
        {
            get { return _id; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("ID can't be less than zero!");
                }
                else
                {
                    _id = value;
                }
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 1)
                {
                    throw new ArgumentException("Firstname can't be empty!");
                }
                else
                {
                    _name = value;
                }
            }
        }

        public decimal Price { get; private set; }

        public bool Active { get; set; }

        public bool CanBeBoughtOnCredit { get; set; }

        public override string ToString()
        {
            return "ID: " + ID.ToString() + ", Name: " + Name + ", Price: " + Price.ToString() + ", Active: " + Active + ", CanBeBoughtOnCredit: " + CanBeBoughtOnCredit;
        }
    }
}


