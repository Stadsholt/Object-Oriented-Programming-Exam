using System.Linq;

namespace OOP
{
    class StregsystemCommandParser
    {
        private IStregsystemUI _stregsystemUI;
        private IStregsystem _stregsystem;

        public StregsystemCommandParser(IStregsystemUI ui, IStregsystem stregsystem)
        {
            _stregsystemUI = ui;
            _stregsystem = stregsystem;
            _stregsystemUI.CommandEntered += OnCommandEntered;
        }

        void OnCommandEntered(string command)
        {
            ParseCommand(command);
        }

        void ParseCommand(string command)
        {
            int commandCase = SplitCommand(command).Count();
            User user;
            Product product;
            int count;

            if (CheckAdmin(SplitCommand(command)[0]))
            {
                AdminCommands(SplitCommand(command));
            }

            if (!CheckAdmin(SplitCommand(command)[0]))
            { 
                switch (commandCase)
                {
                    case 1:
                        try
                        {
                            user = _stregsystem.GetUserByUsername(SplitCommand(command)[0]);
                            SimpleUsername(user);
                        }
                        catch (UserNotFound)
                        {
                            _stregsystemUI.DisplayUserNotFound(SplitCommand(command)[0]);
                        }
                        break;

                    case 2:
                        try
                        {
                            user = _stregsystem.GetUserByUsername(SplitCommand(command)[0]);
                            CheckProductInt(SplitCommand(command)[1]);
                            _stregsystem.GetProductByID(int.Parse(SplitCommand(command)[1]));
                            product = _stregsystem.GetActiveProductByID(int.Parse(SplitCommand(command)[1]));
                            Buy(user,product);
                        }
                        catch (UserNotFound)
                        {
                            _stregsystemUI.DisplayUserNotFound(SplitCommand(command)[0]);
                        }
                        catch (ProductIDNotInt)
                        {
                            _stregsystemUI.DisplayProductIDNotInt(SplitCommand(command)[1]);
                        }
                        catch (ProductNotFound)
                        {
                            _stregsystemUI.DisplayProductNotFound(SplitCommand(command)[1]);
                        }

                        catch (ProductNotActive)
                        {
                            _stregsystemUI.DisplayProductNotActive(SplitCommand(command)[1]);
                        }
                        break;

                    case 3:
                        try
                        {
                            user = _stregsystem.GetUserByUsername(SplitCommand(command)[0]);
                            CheckProductInt(SplitCommand(command)[1]);
                            _stregsystem.GetProductByID(int.Parse(SplitCommand(command)[1]));
                            product = _stregsystem.GetActiveProductByID(int.Parse(SplitCommand(command)[1]));
                            CheckCountInt(SplitCommand(command)[2]);
                            count = int.Parse(SplitCommand(command)[2]);
                            MultiBuy(user, product, count);
                        }
                        catch (UserNotFound)
                        {
                            _stregsystemUI.DisplayUserNotFound(SplitCommand(command)[0]);
                        }
                        catch (ProductIDNotInt)
                        {
                            _stregsystemUI.DisplayProductIDNotInt(SplitCommand(command)[1]);
                        }
                        catch (ProductNotFound)
                        {
                            _stregsystemUI.DisplayProductNotFound(SplitCommand(command)[1]);
                        }
                        catch (ProductNotActive)
                        {
                            _stregsystemUI.DisplayProductNotActive(SplitCommand(command)[1]);
                        }
                        catch (CountNotInt)
                        {
                            _stregsystemUI.DisplayCountNotInt(SplitCommand(command)[2]);
                        }
                        break;

                    default:
                        _stregsystemUI.DisplayTooManyArgumentsError(SplitCommand(command)[2]);
                        break;
                }
            }
        }

        void SimpleUsername(User user)
        {
            _stregsystemUI.DisplayUserInfo(user);
        }

        void Buy(User user, Product product)
        {
            try
            {
                BuyTransaction transaction;
                _stregsystem.BuyProduct(user, product);
                transaction = (BuyTransaction)_stregsystem.GetTransactions(user, 1).First();
                _stregsystemUI.DisplayUserBuysProduct(transaction);
            }
            catch (InsufficientCreditsException)
            {
                _stregsystemUI.DisplayInsufficientCash(user, product, 1);
            }
        }

        void MultiBuy(User user, Product product, int count)
        {
            BuyTransaction transaction;
            if ((count > 0 && (count * product.Price) < user.Balance) || (count * product.Price) == user.Balance || product.CanBeBoughtOnCredit == true)
            {
                for (int i = 0; i < count; i++)
                {
                    _stregsystem.BuyProduct(user, product);
                }
                transaction = (BuyTransaction)_stregsystem.GetTransactions(user, 1).First();
                _stregsystemUI.DisplayUserBuysProduct(count, transaction);
            }
            else
            {
                _stregsystemUI.DisplayInsufficientCash(user, product, count);
            }
        }

        void AdminCommands(List<string> SplitCommand)
        {
            User user;
            Product product;
            switch (SplitCommand[0])
            {
                case ":q":
                case ":quit":
                    _stregsystemUI.DisplayAdminCommandMessage("Admin quit application");
                    _stregsystemUI.Close();
                    break;

                case ":activate":
                    product = _stregsystem.GetProductByID(int.Parse(SplitCommand[1]));
                    product.Active = true;
                    _stregsystemUI.DisplayClear();
                    _stregsystemUI.DisplayAdminCommandMessage("Admin activated product: " + product);
                    _stregsystemUI.Start();
                    break;

                case ":deactivate":
                    product = _stregsystem.GetProductByID(int.Parse(SplitCommand[1]));
                    product.Active = false;
                    _stregsystemUI.DisplayClear();
                    _stregsystemUI.DisplayAdminCommandMessage("Admin deactivated product: " + product);
                    _stregsystemUI.Start();
                    break;

                case ":crediton":
                    product = _stregsystem.GetProductByID(int.Parse(SplitCommand[1]));
                    product.CanBeBoughtOnCredit = true;
                    _stregsystemUI.DisplayClear();
                    _stregsystemUI.DisplayAdminCommandMessage("Admin activated credit on product: " + product);
                    _stregsystemUI.Start();
                    break;

                case ":creditoff":
                    product = _stregsystem.GetProductByID(int.Parse(SplitCommand[1]));
                    product.CanBeBoughtOnCredit = false;
                    _stregsystemUI.DisplayClear();
                    _stregsystemUI.DisplayAdminCommandMessage("Admin deactivated credit on product: " + product);
                    _stregsystemUI.Start();
                    break;

                case ":addcredits":
                    user = _stregsystem.GetUserByUsername(SplitCommand[1]);
                    _stregsystem.AddCreditsToAccount(user, decimal.Parse(SplitCommand[2]));
                    _stregsystemUI.DisplayClear();
                    _stregsystemUI.DisplayAdminCommandMessage("Admin added " + decimal.Parse(SplitCommand[2]) + " DKK to user " + user);
                    _stregsystemUI.Start();
                    break;

                default:
                    _stregsystemUI.DisplayAdminCommandNotFoundMessage(SplitCommand[0]);
                    break;
            }
        }

        List<string> SplitCommand(string command)
        {
            string[] values = command.Split(' ');
            return values.ToList();
        }

        bool CheckAdmin(string command)
        {
            if (command[0] == ':')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void CheckProductInt(string intstring)
        {
            if (intstring.All(char.IsDigit) == false || intstring == "" || intstring == " ")
            {
                throw new ProductIDNotInt();
            }
        }

        void CheckCountInt(string intstring)
        {
            if (intstring.All(char.IsDigit) == false || intstring == "" || intstring == " ")
            {
                throw new CountNotInt();
            }
        }
    }
}

