using MyWallet.Business.Enums;
using MyWallet.Business.ValueTypes;
using System;

namespace MyWallet.Presentation.WebApi.Dto
{
    public class TransactionDto
    {
        public ETransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
