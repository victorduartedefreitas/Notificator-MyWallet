using MyWallet.Business.Entities;
using Notificator.Validations.Validators;

namespace MyWallet.Business.Validators
{
    public sealed class WalletValidator : EntityValidator<Wallet>
    {
        public WalletValidator(Wallet entity) : base(entity)
        {
        }

        protected override void CreateValidatorInstance()
        {
            CreateValidation()
                .IsNotNullOrEmpty(Entity.WalletName, "WalletNameNotNullOrEmptyRuleViolation", "Wallet name cannot be null.")
                .HasMinLenght(Entity.WalletName, 10, "WalletNameMinLenghtRuleViolation", "The wallet name must have min. lenght of 10 characters.");
        }
    }
}
