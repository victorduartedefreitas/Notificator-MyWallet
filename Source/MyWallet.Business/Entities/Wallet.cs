using MyWallet.Business.Validators;
using Notificator.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWallet.Business.Entities
{
    public sealed class Wallet : NotifiableEntity
    {
        public Wallet(string walletName)
            : this(Guid.NewGuid(), walletName)
        {
        }

        public Wallet(Guid walletId, string walletName)
        {
            WalletId = walletId;
            WalletName = walletName;
            walletValidator = new WalletValidator(this);
            _transactions = new List<Transaction>();
        }

        private WalletValidator walletValidator;
        private List<Transaction> _transactions;

        public Guid WalletId { get; set; }
        public string WalletName { get; set; }
        public IReadOnlyCollection<Transaction> Transactions => _transactions;
        public decimal Balance => GetBalance();

        public void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(WalletName);
            sb.Append("============================");
            foreach (var t in _transactions)
                sb.Append(t.ToString());

            return sb.ToString();
        }

        protected override void DoValidate()
        {
            walletValidator.Validate();
            foreach (var t in Transactions)
                t.Validate();
        }

        private decimal GetBalance()
        {
            decimal balance = 0m;

            foreach (var transaction in _transactions)
            {
                if (transaction.WasValidated && !transaction.IsValid)
                    continue;

                if (transaction.TransactionType == Enums.ETransactionType.Credit)
                    balance += transaction.Amount;
                else
                    balance -= transaction.Amount;
            }

            return balance;
        }
    }
}
