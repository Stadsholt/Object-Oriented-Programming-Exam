namespace OOP
{
    public interface IStregsystemUI
    {
        void Start();
        void DisplayUserNotFound(string username);
        void DisplayProductNotFound(string product);
        void DisplayProductIDNotInt(string id);
        void DisplayProductNotActive(string product);
        void DisplayCountNotInt(string count);
        void DisplayUserInfo(User user);
        void DisplayTooManyArgumentsError(string command);
        void DisplayAdminCommandNotFoundMessage(string adminCommand);
        void DisplayAdminCommandMessage(string adminCommand);
        void DisplayUserBuysProduct(BuyTransaction transaction);
        void DisplayUserBuysProduct(int count, BuyTransaction transaction);
        void DisplayInsufficientCash(User user, Product product, int count);
        void DisplayBalanceWarning(User user, decimal threshold);
        void DisplayGeneralError(string errorString);
        void DisplayClear();
        void Close();

        event StregsystemEvent CommandEntered;
    }
    public delegate void StregsystemEvent(string commandEntered);
}