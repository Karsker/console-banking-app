using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp
{
    internal class User
    {
        private string name;
        private string password;
        private List<Account> accounts = new List<Account>();
        public string Name { get { return name; } }
        public List<Account> Accounts { get { return accounts; } }
        public User(string name, string password)
        {
            this.name = name;
            this.password = password;
        }

        // Method to compare input password and real password
        public bool VerifyPassword(string inpPassword) {
            return this.password.Equals(inpPassword);
        }

        // Method to add an account
        public void AddAccount(Account account) { 
            this.accounts.Add(account);
        }
    }
}
