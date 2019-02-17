using MyWallet.Infrastructure.Repository;
using MyWallet.Infrastructure.Repository.Components;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RegisterRepositoriesDependencyInjection
    {
        public static IServiceCollection AddMyWalletRepositories(this IServiceCollection services)
        {
            services.AddScoped<IWalletReadOnlyRepository, WalletRepository>();
            services.AddScoped<IWalletWriteOnlyRepository, WalletRepository>();

            return services;
        }
    }
}
