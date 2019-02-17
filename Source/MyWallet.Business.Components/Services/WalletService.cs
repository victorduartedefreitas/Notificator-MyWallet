using MyWallet.Business.Entities;
using MyWallet.Business.Enums;
using MyWallet.Business.Services;
using MyWallet.Business.ValueTypes;
using MyWallet.Infrastructure.Repository;
using System;
using System.Threading.Tasks;

namespace MyWallet.Business.Components.Services
{
    public class WalletService : IWalletService
    {
        private IWalletReadOnlyRepository walletReadOnlyRepository;
        private IWalletWriteOnlyRepository walletWriteOnlyRepository;

        public WalletService(IWalletReadOnlyRepository walletReadOnlyRepository, IWalletWriteOnlyRepository walletWriteOnlyRepository)
        {
            this.walletReadOnlyRepository = walletReadOnlyRepository ?? throw new ArgumentNullException(nameof(walletReadOnlyRepository));
            this.walletWriteOnlyRepository = walletWriteOnlyRepository ?? throw new ArgumentNullException(nameof(walletWriteOnlyRepository));
        }

        public async Task AddTransaction(Guid walletId, Transaction transaction)
        {
            var wallet =  await walletReadOnlyRepository.GetWallet(walletId);
            if (wallet == null)
                return;

            wallet.AddTransaction(transaction);

            await Save(wallet);
        }

        public async Task AddTransaction(Guid walletId, ETransactionType transactionType, Amount amount, string description)
        {
            var wallet = await walletReadOnlyRepository.GetWallet(walletId);
            if (wallet == null)
                return;

            Transaction transaction = null;

            if (transactionType == ETransactionType.Credit)
                transaction = new CreditTransaction(amount, description, DateTime.Now);
            else if (transactionType == ETransactionType.Debit)
                transaction = new DebitTransaction(amount, description, DateTime.Now);

            wallet.AddTransaction(transaction);

            await Save(wallet);
        }

        public async Task<Wallet> CreateWallet(string walletName)
        {
            var wallet = new Wallet(walletName);
            return wallet;
        }

        public async Task<Wallet> GetWallet(Guid walletId)
        {
            return await walletReadOnlyRepository.GetWallet(walletId);
        }

        public async Task<Wallet> GetWallet(string walletName)
        {
            return await walletReadOnlyRepository.GetWallet(walletName);
        }

        public async Task Save(Wallet wallet)
        {
            if (wallet == null)
                throw new ArgumentNullException(nameof(wallet));

            wallet.Validate();

            if (!wallet.IsValid)
                return;

            foreach (var t in wallet.Transactions)
                if (!t.IsValid)
                    return;

            await walletWriteOnlyRepository.SaveWallet(wallet);
        }
    }
}
