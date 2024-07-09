using System.Text.RegularExpressions;

namespace OOP
{
    public class User
    {
        public User(int id, string firstname, string lastname, string username, decimal balance, string email) 
        {
            ID = id;
            Firstname = firstname;
            Lastname = lastname;
            Username = username;
            Email = email;
            Balance = balance;
        }

        private int _id;
        public int ID
        {
            get{return _id;}
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

        private string _firstname;
        public string Firstname
        {
            get{return _firstname;}
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 1 || char.IsLower(value[0]))
                {
                    throw new ArgumentException("Firstname can't be empty!");
                }
                else
                {
                    _firstname = value;
                }
            }
        }

        private string _lastname;
        public string Lastname
        {
            get{return _lastname;}
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 1 || char.IsLower(value[0]))
                {
                    throw new ArgumentException("Lastname can't be empty!");
                }
                else
                {
                    _lastname = value;
                }
            }
        }

        private string _username;
        public string Username
        {
            get{return _username;}
            set
            {
                var regex = @"^[a-zA-Z0-9_]+$";
                Match match = Regex.Match(value, regex, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    _username = value;
                }
                else
                {
                    throw new ArgumentException("Firstname can't be empty!");
                }
            }
        }

        private string _email;
        public string Email
        {
            get{return _email;}
            set
            {
                var regex2 = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9]+[a-zA-Z0-9.-]+[a-zA-Z0-9]\.[a-zA-Z]{2,4}$";
                Match match2 = Regex.Match(value, regex2, RegexOptions.IgnoreCase);
                if (match2.Success)
                {
                    _email = value;
                }
                else
                {
                    throw new ArgumentException("Email can't be empty!");
                }
            }
        }
        public decimal Balance { get; set; }

        public override string ToString()
        {
            return Firstname + " " + Lastname + " " + Email;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(User)) return false;
            User Users = (User)obj;
            return Users.Username.Equals(Username);
        }

        public override int GetHashCode()
        {
            return (Username.GetHashCode());
        }

        public int CompareTo(User? other)
        {
            return this.ID.CompareTo(other?.ID);
        }


        //public event UserBalanceNotification UserBalanceWarning; //TODO Delegate and event
    }
    //public delegate void UserBalanceNotification(Object sender, ThresholdReachedEventArgs e); //TODO Delegate and event
}
