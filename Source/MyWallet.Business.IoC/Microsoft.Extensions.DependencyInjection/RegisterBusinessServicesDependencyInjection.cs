using MyWallet.Business.Components.Services;
using MyWallet.Business.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RegisterBusinessServicesDependencyInjection
    {
        public static IServiceCollection AddMyWalletServices(this IServiceCollection services)
        {
            services.AddScoped<IWalletService, WalletService>();

            return services;
        }
    }
}
