using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp
{
    enum TransactionType
    {
        withdrawal,
        deposit
    }
    internal class Transaction
    {
        int id;
        DateTime date;
        TransactionType type;
        double amount;

        public int Id { get { return id; } }
        public DateTime Date { get { return date; } }
        public TransactionType Type { get { return type; } }
        public double Amount { get { return amount; } }

        public Transaction(int id, TransactionType type, double amount)
        {
            this.id = id;
            this.amount = amount;
            this.type = type;
            this.date = DateTime.Now;
        }

    }
}
