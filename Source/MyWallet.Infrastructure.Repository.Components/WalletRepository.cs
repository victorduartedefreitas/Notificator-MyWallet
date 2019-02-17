using MyWallet.Business.Entities;
using MyWallet.Infrastructure.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallet.Infrastructure.Repository.Components
{
    public sealed class WalletRepository : IWalletReadOnlyRepository, IWalletWriteOnlyRepository
    {
        private MyWalletDbContext dbContext;

        public WalletRepository()
        {
            dbContext = MyWalletDbContext.Instance;
        }

        public async Task<Wallet> GetWallet(Guid walletId)
        {
            var walletStage = dbContext.Wallets.FirstOrDefault(f => f.WalletId == walletId);
            if (walletStage == null)
                return null;

            var wallet = new Wallet(walletId, walletStage.WalletName);
            foreach (var t in walletStage.Transactions)
            {
                if (t.TransactionType == Business.Enums.ETransactionType.Credit)
                    wallet.AddTransaction(new CreditTransaction(t.Amount, t.Description, t.TransactionDate));
                else
                    wallet.AddTransaction(new DebitTransaction(t.Amount, t.Description, t.TransactionDate));
            }

            return wallet;
        }

        public async Task<Wallet> GetWallet(string walletName)
        {
            var walletStage = dbContext.Wallets.FirstOrDefault(f => f.WalletName.ToUpper().Trim() == walletName.ToUpper().Trim());
            if (walletStage == null)
                return null;

            var wallet = new Wallet(walletStage.WalletId, walletStage.WalletName);
            foreach (var t in walletStage.Transactions)
            {
                if (t.TransactionType == Business.Enums.ETransactionType.Credit)
                    wallet.AddTransaction(new CreditTransaction(t.Amount, t.Description, t.TransactionDate));
                else
                    wallet.AddTransaction(new DebitTransaction(t.Amount, t.Description, t.TransactionDate));
            }

            return wallet;
        }

        public async Task SaveWallet(Wallet wallet)
        {
            dbContext.AddWallet(wallet);
            dbContext.SaveChanges();
        }
    }
}
