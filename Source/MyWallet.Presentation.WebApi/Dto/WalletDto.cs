using System;
using System.Collections.Generic;

namespace MyWallet.Presentation.WebApi.Dto
{
    public class WalletDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<TransactionDto> Transactions { get; set; }
        public double Balance { get; set; }
    }
}
