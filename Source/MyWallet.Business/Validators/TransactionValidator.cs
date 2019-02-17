using MyWallet.Business.Entities;
using Notificator.Validations.Validators;
using System;

namespace MyWallet.Business.Validators
{
    public sealed class TransactionValidator : EntityValidator<Transaction>
    {
        public TransactionValidator(Transaction entity) : base(entity)
        {
        }

        protected override void CreateValidatorInstance()
        {
            CreateValidation()
                .IsNotNull(Entity.TransactionDate, "TransactionDateNotNullRuleViolation", "Transaction Date cannot be null")
                .IsLowerThanOrEquals(Entity.TransactionDate, DateTime.Now, "TransactionDateLowerThanOrEqualsNowRuleViolation", "Transaction Date must be lower than or equals to now.")
                .IsGreaterThanOrEquals(Entity.Amount, 0, "AmountGreaterThanOrEqualsToZeroRuleViolation", "Amount value must be greater than or equals to 0.");
        }
    }
}
