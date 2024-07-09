using System.Text.RegularExpressions;

namespace OOP
{
    public abstract class Transaction
    {
        public Transaction(User user, decimal amount)
        {
            ID = _id++;
            User = user;
            Date = DateTime.Now.ToString("dd MMM, yyy, HH':'mm':'ss");
            Amount = amount;
        }

        private static int _id = 1;
        public int ID { get; private set; }
        public User User { get; private set; }
        
        private string _date;
        public string Date
        {
            get {return _date;}
            set
            {
                if (value != DateTime.Now.ToString("dd MMM, yyy, HH':'mm':'ss"))
                {
                    throw new Exception("Time is not set right");
                }
                _date = value;
            }
        }

        private decimal _amount;
        public decimal Amount
        {
            get{return _amount;}
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("Amount can't be less than one!");
                }
                else
                {
                    _amount = value;
                }
            }
        }
        public abstract void Execute();
    }
}







