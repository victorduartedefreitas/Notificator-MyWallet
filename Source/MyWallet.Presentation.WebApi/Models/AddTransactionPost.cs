using MyWallet.Presentation.WebApi.Dto;
using System;

namespace MyWallet.Presentation.WebApi.Models
{
    public class AddTransactionPost
    {
        public Guid WalletId { get; set; }
        public TransactionDto Transaction { get; set; }
    }
}
