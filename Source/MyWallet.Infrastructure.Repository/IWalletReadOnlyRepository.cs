using MyWallet.Business.Entities;
using System;
using System.Threading.Tasks;

namespace MyWallet.Infrastructure.Repository
{
    public interface IWalletReadOnlyRepository
    {
        Task<Wallet> GetWallet(Guid walletId);
        Task<Wallet> GetWallet(string walletName);
    }
}
