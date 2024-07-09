namespace OOP
{
    public interface IStregsystem
    {   
        InsertCashTransaction AddCreditsToAccount(User user, decimal amount);
        BuyTransaction BuyProduct(User user, Product product);
        Product GetProductByID(int id);
        Product GetActiveProductByID(int id);
        IEnumerable<Transaction> GetTransactions(User user, int count);
        IEnumerable<User> GetUsers(Func<User, bool> predicate);
        User GetUserByUsername(string username);
        void ExecuteTransaction(Transaction transaction);
        IEnumerable<Product> ActiveProducts();

        event UserBalanceNotification UserBalanceWarning;
    }
    public delegate void UserBalanceNotification(User user, decimal threshold);
}


