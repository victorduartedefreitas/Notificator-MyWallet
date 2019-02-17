using MyWallet.Business.ValueTypes;
using System;

namespace MyWallet.Business.Entities
{
    public sealed class DebitTransaction : Transaction
    {
        public DebitTransaction(Amount amount, string description, DateTime? transactionDate)
        {
            TransactionType = Enums.ETransactionType.Debit;
            Amount = amount;
            Description = description;

            if (transactionDate == null || !transactionDate.HasValue)
                TransactionDate = DateTime.Now;
            else
                TransactionDate = transactionDate.Value;
        }
    }
}
