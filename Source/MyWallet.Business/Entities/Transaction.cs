using MyWallet.Business.Enums;
using MyWallet.Business.Validators;
using MyWallet.Business.ValueTypes;
using Notificator.Notifications;
using System;

namespace MyWallet.Business.Entities
{
    public abstract class Transaction : NotifiableEntity
    {
        public Transaction()
        {
            transactionValidator.OnValidated += TransactionValidator_OnValidated;
        }

        public ETransactionType TransactionType { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public Amount Amount { get; set; }
        public bool WasValidated { get; set; }

        protected TransactionValidator transactionValidator => new TransactionValidator(this);

        private void TransactionValidator_OnValidated(object sender, EventArgs e)
        {
            WasValidated = true;
        }

        public override string ToString()
        {
            return $"{TransactionDate} - {Description} : {Amount.ToString()}";
        }

        protected override void DoValidate()
        {
            transactionValidator.Validate();
        }
    }
}
