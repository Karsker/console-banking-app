using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace BankingApp
{
    public enum accountType
    {
       savings,
       checking
    }
    internal class Account
    {
        private string accountNumber;
        private double balance = 0;
        private accountType accType;
        public string AccountNumber { get { return accountNumber; } }
        public Account(string accountNumber, accountType accType, double initialAmount) {
            this.accountNumber = accountNumber;
            this.accType = accType;
            this.balance = initialAmount;
        }


        // Function to withdraw amount from the account
        // Returns true of the operation was successful, false otherwise
        public bool Withdraw(double amount)
        {
            if (balance < amount)
            {
                return false;
            }

            balance -= amount;
            return true;
        }

        // Function to add amount to the account
        public void Add(double amount)
        {
            balance += amount;
        }
    }
}
