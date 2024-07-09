namespace OOP
{
    public class BuyTransaction : Transaction
    {
        public BuyTransaction(User user, decimal amount, Product product)
            : base(user, amount)
        {
            Product = product;
        }

        public Product Product { get; set; }
 
        public override string ToString()
        {
            return "Transaction ID " + ID.ToString() + ": User " + User.Username + " bought 1 " + Product.Name + " for " + Product.Price + " DKK at " + Date;
        }

        public override void Execute()
        {
            if (Amount > User.Balance && Product.CanBeBoughtOnCredit == true)
            {
                User.Balance -= Amount;
            }

            if (Amount > User.Balance && Product.CanBeBoughtOnCredit == false)
            {
                throw new InsufficientCreditsException();
            }

            if (Amount <= User.Balance)
            {
                User.Balance -= Amount;
            }
        }
    }
}







