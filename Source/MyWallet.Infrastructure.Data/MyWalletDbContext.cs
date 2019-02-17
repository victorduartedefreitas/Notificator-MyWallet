using MyWallet.Business.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MyWallet.Infrastructure.Data
{
    public sealed class MyWalletDbContext
    {
        private MyWalletDbContext()
        {
            _wallets = new List<WalletStage>();
            _nonSavedWallets = new List<WalletStage>();
        }

        private static MyWalletDbContext _instance;
        private List<WalletStage> _wallets;
        private List<WalletStage> _nonSavedWallets;

        public static MyWalletDbContext Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MyWalletDbContext();

                return _instance;
            }
        }

        public IReadOnlyCollection<WalletStage> Wallets
        {
            get
            {
                var allWallets = new List<WalletStage>();

                foreach (var w in _wallets)
                    allWallets.Add(w);

                foreach (var w in _nonSavedWallets)
                    allWallets.Add(w);

                return allWallets;
            }
        }

        public void AddWallet(Wallet wallet)
        {
            EStageType stageType = _wallets.Any(f => f.WalletId == wallet.WalletId) ? EStageType.Update : EStageType.Create;
            
            _nonSavedWallets.Add(new WalletStage(wallet.WalletId,
                wallet.WalletName,
                wallet.Transactions,
                wallet.Balance,
                stageType));
        }

        public void DeleteWallet(Wallet wallet)
        {
            _nonSavedWallets.Add(new WalletStage(wallet.WalletId,
                wallet.WalletName,
                wallet.Transactions,
                wallet.Balance,
                EStageType.Delete));
        }

        public void SaveChanges()
        {
            foreach (var w in _nonSavedWallets)
            {
                switch(w.StageType)
                {
                    case EStageType.Delete:
                        _wallets.Remove(w);
                        break;
                    case EStageType.Create:
                        _wallets.Add(new WalletStage(w.WalletId, w.WalletName, w.Transactions, w.Balance, EStageType.None));
                        break;
                    case EStageType.Update:
                        _wallets.Remove(_wallets.First(f => f.WalletId == w.WalletId));
                        _wallets.Add(new WalletStage(w.WalletId, w.WalletName, w.Transactions, w.Balance, EStageType.None));
                        break;
                }
            }

            _nonSavedWallets.Clear();
        }
    }
}
