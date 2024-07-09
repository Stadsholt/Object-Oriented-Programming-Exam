using System;
using System.Collections.Generic;
using System.IO;
namespace OOP
{
    class Stregsystem : IStregsystem
    {
        private List<User> UserList = new List<User>();
        private List<Product> ProductList = new List<Product>();
        private List<Transaction> TransactionList = new List<Transaction>();
        public event UserBalanceNotification UserBalanceWarning;
        public Stregsystem()
        {
            string[] ProductStrings = File.ReadAllLines(@"..\..\..\Data\products.csv");
            ProductStrings = ProductStrings.Skip(1).ToArray();
            foreach (string var in ProductStrings)
            {
                string[] values = var.Replace("\"", "")
                    .Replace("<h1>", "").Replace("</h1>", "")
                    .Replace("<h2>", "").Replace("</h2>", "")
                    .Replace("<h3>", "").Replace("</h3>", "")
                    .Replace("<b>", "").Replace("</b>", "")
                    .Replace("<blink>", "").Replace("</blink>", "")
                    .Split(';');
                ProductList.Add(new Product(int.Parse(values[0]), values[1], (int.Parse(values[2])) / 100, Convert.ToBoolean(int.Parse(values[3])), false));
            }

            string[] UserStrings = File.ReadAllLines(@"..\..\..\Data\users.csv");
            UserStrings = UserStrings.Skip(1).ToArray();
            foreach (string var in UserStrings)
            {
                string[] values = var.Split(',');
                UserList.Add(new User(int.Parse(values[0]), values[1], values[2], values[3], Decimal.Parse(values[4]), values[5]));
            }
        }

        public BuyTransaction BuyProduct(User user, Product product)
        {
            BuyTransaction transaction = new BuyTransaction(user, product.Price, product);
            ExecuteTransaction(transaction);


            UserBalanceWarning(user, 50);

            return transaction;
        }

        public InsertCashTransaction AddCreditsToAccount(User user, decimal amount)
        {
            InsertCashTransaction transaction = new InsertCashTransaction(user, amount);
            ExecuteTransaction(transaction);
            return transaction;
        }

        public void ExecuteTransaction(Transaction transaction)  
        {
            transaction.Execute();
            TransactionList.Add(transaction);
            string[] start = { DateTime.Now + " - " + transaction.ToString()};
            File.AppendAllLines(@"..\..\..\Data\Log.txt", start);
        }

        public User GetUserByUsername(string username)
        {
            foreach (User user in UserList)
            {
                if (user.Username.Equals(username))
                {
                    return user;
                }
            }
            throw new UserNotFound();
        }

        public Product GetProductByID(int id)
        {
            foreach (Product product in ProductList)
            {
                if (product.ID == id)
                {
                    return product;
                }
            }
            throw new ProductNotFound();
        }

        public Product GetActiveProductByID(int id)
        {
            foreach (Product product in ActiveProducts())
            {
                if (product.ID == id)
                {
                    return product;
                }
            }
            throw new ProductNotActive();
        }

        public IEnumerable<User> GetUsers(Func<User, bool> predicate)
        {
            return UserList.Where(predicate);
        }

        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            List<Transaction> trans = new List<Transaction>();

            foreach (Transaction transaction in TransactionList)
            {
                if (transaction.User == user)
                {
                    trans.Add(transaction);
                }
            }
            return trans.TakeLast(count).Reverse();
        }

        public IEnumerable<Product> ActiveProducts()
        {
            return ProductList.Where(p => p.Active == true);
        }
    }
}







