using BDP.Domain.Repositories;
using BDP.Domain.Services;
using BDP.Infrastructure.Repositories.EntityFramework;
using BDP.Tests.Infrastructure.Repositories.EntityFramework.Util;

using Microsoft.Extensions.DependencyInjection;

namespace BDP.Application.App.Tests;

public class Startup
{
    public static void ConfigurationServices(IServiceCollection services)
    {
        services.AddTransient(_ => TestDbContext.Create());
        services.AddTransient<IUnitOfWork, BdpUnitOfWork>();

        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IUsersService, UsersService>();
        services.AddTransient<IFinancialRecordsService, FinancialRecordsService>();
        services.AddTransient<ITransactionsService, TransactionsService>();
        services.AddTransient<IFinanceService, FinanceService>();
        services.AddTransient<ISellablesService, SellablesService>();
        services.AddTransient<ISellableReviewsService, SellableReviewsService>();
        services.AddTransient<IProductsService, ProductsService>();
        services.AddTransient<IServicesService, ServicesService>();
        services.AddTransient<IPurchasesService, PurchasesService>();
        services.AddTransient<ISearchSuggestionsService, SearchSuggestionsService>();
        services.AddTransient<IEventsService, EventsService>();
    }
}