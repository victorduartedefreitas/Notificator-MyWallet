using MyWallet.Business.Entities;
using MyWallet.Business.Enums;
using MyWallet.Business.ValueTypes;
using System;
using System.Threading.Tasks;

namespace MyWallet.Business.Services
{
    public interface IWalletService
    {
        Task<Wallet> CreateWallet(string walletName);
        Task AddTransaction(Guid walletId, Transaction transaction);
        Task AddTransaction(Guid walletId, ETransactionType transactionType, Amount amount, string description);
        Task Save(Wallet wallet);
        Task<Wallet> GetWallet(Guid walletId);
        Task<Wallet> GetWallet(string walletName);
    }
}
