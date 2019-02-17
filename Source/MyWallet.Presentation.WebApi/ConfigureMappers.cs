using AutoMapper;
using MyWallet.Business.Entities;
using MyWallet.Presentation.WebApi.Dto;

namespace MyWallet.Presentation.WebApi
{
    public class ConfigureMappers : Profile
    {
        public ConfigureMappers()
        {
            CreateMap<WalletDto, Wallet>()
                    .ForMember(d => d.WalletName, o => o.MapFrom(s => s.Name))
                    .ForMember(d => d.WalletId, o => o.MapFrom(s => s.Id));

            CreateMap<Wallet, WalletDto>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.WalletName))
                .ForMember(d => d.Id, o => o.MapFrom(s => s.WalletId));

            CreateMap<Transaction, TransactionDto>()
                .ForMember(d => d.Date, o => o.MapFrom(s => s.TransactionDate));

            CreateMap<TransactionDto, Transaction>()
                .ForMember(d => d.TransactionDate, o => o.MapFrom(s => s.Date));
        }
    }
}
