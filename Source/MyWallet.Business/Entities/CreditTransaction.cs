using MyWallet.Business.ValueTypes;
using System;

namespace MyWallet.Business.Entities
{
    public sealed class CreditTransaction : Transaction
    {
        public CreditTransaction(Amount amount, string description, DateTime? transactionDate)
        {
            TransactionType = Enums.ETransactionType.Credit;
            Amount = amount;
            Description = description;

            if (transactionDate == null || !transactionDate.HasValue)
                TransactionDate = DateTime.Now;
            else
                TransactionDate = transactionDate.Value;
        }
    }
}
