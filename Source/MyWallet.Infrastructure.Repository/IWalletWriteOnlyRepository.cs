using MyWallet.Business.Entities;
using System.Threading.Tasks;

namespace MyWallet.Infrastructure.Repository
{
    public interface IWalletWriteOnlyRepository
    {
        Task SaveWallet(Wallet wallet);
    }
}
