using System;
using System.Collections.Generic;
using System.IO;

namespace OOP
{
    class StregsystemCLI : IStregsystemUI
    {
        private IStregsystem _stregsystem;
        private bool run = true;
        public event StregsystemEvent CommandEntered;
        public StregsystemCLI(IStregsystem stregsystem)
        {
            _stregsystem = stregsystem;
            _stregsystem.UserBalanceWarning += DisplayBalanceWarning; //subscribe to event
        }

        public void Start()
        {
            StartDisplay();
            while (run)
            {
                string userInput = Console.ReadLine();
                CommandEntered(userInput);
            }
            DisplayClear();
        }

        public void StartDisplay()
        {
            Console.WriteLine("{0,46}", "TREOENs STREGSYSTEM : Jægerstuen");
            Console.WriteLine("________________________________________________________________________");
            Console.WriteLine("| {0,-4} | {1,-36} | {2,-13} | {3,-6} |", "ID", "Produkt", "Pris", "Credit");
            Console.WriteLine("|______|______________________________________|_______________|________|");
            foreach (Product product in _stregsystem.ActiveProducts())
            {
                Console.WriteLine("| {0,-4} | {1,-36} | {2,-9:N2} DKK | {3,-6} |", product.ID, product.Name, product.Price, product.CanBeBoughtOnCredit);
            }
            Console.WriteLine("|______|______________________________________|_______________|________|");
        }

        public void DisplayUserNotFound(string username)
        {
            Console.WriteLine("User \"" + username + "\" was not found!");
        }

        public void DisplayProductNotFound(string product)
        {
            Console.WriteLine("Product \"" + product + "\" was not found!");
        }

        public void DisplayProductIDNotInt(string id)
        {
            Console.WriteLine("Product ID \"" + id + "\" was not a number!");
        }
        public void DisplayCountNotInt(string count)
        {
            Console.WriteLine("Count \"" + count + "\" was not a number!");
        }

        public void DisplayProductNotActive(string product)
        {
            Console.WriteLine("Product \"" + product + "\" is not active!");
        }

        public void DisplayUserInfo(User user)
        {
            Console.WriteLine("\nUser ID: " + user.ID + "\n"
                + "First name: " + user.Firstname + "\n"
                + "Last name: " + user.Lastname + "\n"
                + "Username: " + user.Username + "\n"
                + "Email: " + user.Email + "\n"
                + "Balance: " + user.Balance + " DKK" + "\n");

            if (_stregsystem.GetTransactions(user, 10)?.Any() != false)
            {
                Console.WriteLine("User transaction history:");
                Console.WriteLine("_________________________________________________________________________________________________________________\n");

                foreach (Transaction transaction in _stregsystem.GetTransactions(user, 10))
                {
                    Console.WriteLine(transaction);
                }
                Console.WriteLine("_________________________________________________________________________________________________________________\n");
            } 
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            Console.WriteLine("Command can take a maximum of 3 arguments but has been given \"" + command + "\"!");
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.WriteLine(transaction);
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            Console.WriteLine ("Multibuy: User " + transaction.User.Username 
                + " bought " + count + " \"" + transaction.Product.Name 
                + "\" for " + transaction.Product.Price * count
                + " DKK at " + transaction.Date);
        }

        public void DisplayInsufficientCash(User user, Product product, int count)
        {
            if (count == 0 || count < 1)
            {
                Console.WriteLine("Amount of products to buy can't be less than 1!");
            }

            if (count == 1)
            { 
                Console.WriteLine(user.Username + " balance of " + user.Balance + " DKK is not sufficient to buy \"" + product.Name + "\" for " + product.Price + " DKK!");
            }
            else if (count > 0)
            {
                Console.WriteLine(user.Username + " balance of " + user.Balance + " DKK is not sufficient to buy " + count + " \"" + product.Name + "\" for " + (product.Price * count) + " DKK!");
            }
        }

        public void DisplayBalanceWarning(User user, decimal threshold)
        {
            if (user.Balance < threshold)
            { 
            Console.WriteLine("Less than " + threshold + " DKK warning: " + user.Username + " currently has " + user.Balance + " DKK on their account, please refill when possible!");
            }
        }

        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            Console.WriteLine("Admin command \"" + adminCommand + "\" was not found!");
        }

        public void DisplayAdminCommandMessage(string adminCommand)
        {
            Console.WriteLine(adminCommand);
        }

        public void DisplayGeneralError(string errorString)
        {
            Console.WriteLine(errorString);
        }

        public void DisplayClear()
        {
            Console.Clear();
        }

        public void Close()
        {
            run = false;
            Environment.Exit(0); 
        }
    }
}







