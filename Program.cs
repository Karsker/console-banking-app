﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        
        // Function to manage an existing account
        static void HandleAccountOps(ref User user)
        {
            Console.Clear();
            Console.WriteLine("Choose an account: ");
            for (int i = 0; i < user.Accounts.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {user.Accounts[i]}");
            }
            Console.ReadKey();
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
            int op = Convert.ToInt32(Console.ReadLine());
            switch(op)
            {
                case 1:
                    handleNewAccountMenu(ref user);
                    break;
                case 2:
                    HandleAccountOps(ref user);
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
       static void handleLoginMenu(ref List<User> users)
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
                Console.ReadKey();
                handleUserMenu(ref user);
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
            while (true)
            {
                Console.Clear();
                // DEBUG: Show current users
                foreach (User user in users)
                {
                    Console.WriteLine($"{user.Name}");
                }
                // Header
                Console.WriteLine("BANKING APPLICATION");
                Console.WriteLine(string.Join("", Enumerable.Repeat("-", 50)));

                // Display options to user for login and sign up
                Console.WriteLine("Press 1 to Login");
                Console.WriteLine("Press 2 to Sign Up");
                Console.WriteLine("Press 3 to exit");
                Console.Write("\nEnter an option: ");

                int op = Convert.ToInt32(Console.ReadLine());
                switch(op)
                {
                    case 1: 
                        handleLoginMenu(ref users);
                        break;
                    case 2:
                        handleSignUpMenu(ref users);
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Enter a valid option");
                        break;
                }
            }
        }
    }
}
