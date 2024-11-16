using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingApp
{

    internal class Program
    {
        private static Random random = new Random();



        // Function to generate a random string of a given length
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        // Function to view all transactions for an account
        static void ViewAllTransactions(ref User user, int accIndex)
        {
            Console.WriteLine($"{"ID"}{"Date",20}{"Type",20}{"Amount",20}");
            foreach (var transaction in user.Accounts[accIndex].Transactions)
            {
                Console.WriteLine($"{transaction.Id}{transaction.Date,20}{transaction.Type,20}{transaction.Amount,20}");
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }


        // Function to handle transactions for a single account
        static void HandleAccountTransactions(ref User user, int accIndex)
        {
            List<int> ops = new List<int> {1, 2, 3, 4};
            while (true)
            {

                Console.Clear();
                Account acc = user.Accounts[accIndex];

                Console.WriteLine($"Account: {acc.AccountNumber}");
                Console.WriteLine($"Balance: {acc.Balance}");

                // Menu
                Console.WriteLine(string.Join("", Enumerable.Repeat("-", 50)));
                Console.WriteLine("Press 1 to depost amount");
                Console.WriteLine("Press 2 to withdraw amount");
                Console.WriteLine("Press 3 to view all transactions");
                Console.WriteLine("Press 4 to go back");
                Console.WriteLine("Press any other key to go back");
                Console.WriteLine();
                Console.Write("Enter choice: ");

                // Operations based on user choice
                int op = Convert.ToInt32(Console.ReadLine());

                // Keep running the interface until user enters a valid choice
                if (!ops.Contains(op)) {
                    continue;
                }

                if (op == 4)
                {
                    return;
                }

                
                if (op == 3)
                {
                    ViewAllTransactions(ref user, accIndex);
                    return;
                }


                Console.Write("Enter amount: ");
                double amount = Convert.ToDouble(Console.ReadLine());
                while (amount <= 0)
                {
                    Console.Write("Enter an amount greater than 0: ");
                    amount = Convert.ToDouble(Console.ReadLine());
                }

                if (op == 1)
                {
                    user.Accounts[accIndex].Add(amount);
                    Console.WriteLine("Amount deposited successfully. Press any key to continue...");
                }
                else
                {
                    if (user.Accounts[accIndex].Balance < amount)
                    {
                        Console.WriteLine("Insufficient balance to withdraw the specified amount. Press any key to continue...");
                    }
                    else
                    {
                        user.Accounts[accIndex].Withdraw(amount);
                        Console.WriteLine("Amount withdrawn successfully. Press any key to continue...");
                        Console.ReadLine();

                    }
                }
            }


        }

        // Function to manage an existing account
        static void HandleAccountOps(ref User user)
        {
            Console.Clear();
            Console.WriteLine("Choose an account: ");
            for (int i = 0; i < user.Accounts.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {user.Accounts[i].AccountNumber}");
            }
            try
            {
                int op = Convert.ToInt32(Console.ReadLine());
                HandleAccountTransactions(ref user, op - 1);
                Console.ReadKey();
            }
            catch (Exception e)
            {
                return;
            }
        }

        // Function to create a new account for a user
        static void handleNewAccountMenu(ref User user)
        {
            Console.Clear();
            Console.WriteLine("OPEN ACCOUNT");
            Console.WriteLine("Press 1 for savings account");
            Console.WriteLine("Press 2 for checking account");
            int op = Convert.ToInt32(Console.ReadLine());

            // Get initial ammount
            Console.Write("Enter initial deposit amount: ");
            double initialAmount = Convert.ToDouble(Console.ReadLine());

            // Check if amount is valid
            while (initialAmount <= 0)
            {
                Console.WriteLine("Enter an amount value greater than 0: ");
                initialAmount = Convert.ToDouble(Console.ReadLine());
            }

            switch (op)
            {
                case 1:
                    Account newAccount = new Account(RandomString(8), accountType.savings, initialAmount);
                    user.AddAccount(newAccount);
                    Console.WriteLine($"Savings account {newAccount.AccountNumber}  created successfully");
                    break;
                case 2:
                    newAccount = new Account(RandomString(8), accountType.checking, initialAmount);
                    user.AddAccount(newAccount);
                    Console.WriteLine($"Checking account {newAccount.AccountNumber} created successfully");
                    break;

                default:
                    Console.WriteLine("Choose a valid option");
                    break;



            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            return;

        }

        // Function to show the interface for a logged in user
        static void handleUserMenu(ref User user)
        {
            Console.Clear();
            Console.WriteLine(user.Name);
            Console.WriteLine();
            Console.WriteLine("Press 1 to open a new account");
            Console.WriteLine("Press 2 to use an existing account");
            Console.WriteLine("Press 3 to sign out");
            int op = Convert.ToInt32(Console.ReadLine());
            switch (op)
            {
                case 1:
                    handleNewAccountMenu(ref user);
                    break;
                case 2:
                    HandleAccountOps(ref user);
                    break;
                case 3:
                    user = null;
                    break;
                default:
                    Console.WriteLine("Choose a valid option");
                    break;

            }


        }
        // Function to sign up a user
        static void handleSignUpMenu(ref List<User> users)
        {
            // Clear the console window before proceeding
            Console.Clear();

            string username = null;
            string password = null;

            Console.WriteLine("SIGN UP");
            while (username == null || username == "")
            {
                Console.Write("Enter username: ");
                username = Console.ReadLine();
            }

            while (password == null || password == "")
            {
                Console.Write("Enter password: ");
                password = Console.ReadLine();
            }

            // Create a user object
            User newUser = new User(username, password);
            users.Add(newUser);

            Console.WriteLine("User account created successfully. Press any key to continue");
            Console.ReadKey();
            return;

        }

        // Function to login user
        static void handleLoginMenu(ref List<User> users, ref User currentUser)
        {
            // Clear the screen
            Console.Clear();
            Console.WriteLine("LOGIN");
            string username = null;
            string password = null;

            while (username == null || username == "")
            {
                Console.Write("Enter username: ");
                username = Console.ReadLine();
            }


            // Search for the user
            User user;
            user = users.Find(u => u.Name == username);
            if (user == null)
            {
                Console.WriteLine("Error: User not found");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return;

            }

            while (password == null || password == "")
            {
                Console.Write("Enter password: ");
                password = Console.ReadLine();
            }

            if (user.VerifyPassword(password))
            {
                Console.WriteLine("Login successful. Press any key to continue");
                currentUser = user;
                Console.ReadKey();
                return;
            }
            else
            {
                Console.WriteLine("Error: Incorrect password");
            }
            Console.ReadKey();

        }
        static void Main(string[] args)
        {
            // Collection of users
            List<User> users = new List<User>();

            // Maintain currently logged in user info
            User currentUser = null;

            // If user is logged in, show the options for user
            // Otherwise show main menu
            while (true)
            {

                if (currentUser == null)
                {

                    Console.Clear();

                    // Header
                    Console.WriteLine("BANKING APPLICATION");
                    Console.WriteLine(string.Join("", Enumerable.Repeat("-", 50)));



                    // Display options to user for login and sign up
                    Console.WriteLine("Press 1 to Login");
                    Console.WriteLine("Press 2 to Sign Up");
                    Console.WriteLine("Press 3 to exit");
                    Console.Write("\nEnter an option: ");

                    int op = Convert.ToInt32(Console.ReadLine());
                    switch (op)
                    {
                        case 1:
                            handleLoginMenu(ref users, ref currentUser);
                            break;
                        case 2:
                            handleSignUpMenu(ref users);
                            break;
                        case 3:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Enter a valid option");
                            break;
                    }


                }
                else
                {
                    handleUserMenu(ref currentUser);
                }
            }


        }
    }
}
