using MyWallet.Business.Entities;
using System;
using System.Collections.Generic;

namespace MyWallet.Infrastructure.Data
{
    public class WalletStage
    {
        public WalletStage(Guid walletId, string walletName, IEnumerable<Transaction> transactions, decimal balance, EStageType stageType)
        {
            WalletId = walletId;
            WalletName = walletName;
            Balance = balance;
            StageType = stageType;
            _transactions = new List<Transaction>();

            foreach (var t in transactions)
                _transactions.Add(t);
        }

        private List<Transaction> _transactions;

        public Guid WalletId { get; }
        public string WalletName { get; }
        public IReadOnlyCollection<Transaction> Transactions => _transactions;
        public decimal Balance { get; }
        public EStageType StageType { get; }
    }
}
