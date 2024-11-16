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
        private List<Transaction> transactions = new List<Transaction>();
        public string AccountNumber { get { return accountNumber; } }
        public double Balance { get { return balance;  }}

        public List<Transaction> Transactions { get { return transactions; } }
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
            Transaction newTransaction = new Transaction(
                this.transactions.Count + 1,
                TransactionType.withdrawal,
                amount
            );
            this.transactions.Add( newTransaction );
            return true;
        }

        // Function to add amount to the account
        // Returns true if the amoount was added, and false otherwise
        public bool Add(double amount)
        {
            if (amount <= 0)
            {
                return false;
            }
            balance += amount;
            Transaction newTransaction = new Transaction(
                this.transactions.Count + 1,
                TransactionType.deposit,
                amount
            );
            this.transactions.Add(newTransaction);
            return true;
        }
    }
}
