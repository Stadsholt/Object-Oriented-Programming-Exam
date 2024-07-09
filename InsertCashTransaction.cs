namespace OOP
{
    public class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(User user, decimal amount) 
            : base(user, amount) {}
        
        public override string ToString()
        {
            return "Transaction ID " + ID.ToString() + ": User " + User.Username + " inserted " + Amount.ToString() + " DKK at " + Date;
        }

        public override void Execute()
        {
            User.Balance += Amount;
        }
    }
}







