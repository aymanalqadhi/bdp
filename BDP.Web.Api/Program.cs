﻿using BDP.Application.App;
using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Services;
using BDP.Infrastructure.Repositories.EntityFramework;
using BDP.Infrastructure.Services;
using BDP.Web.Api.Auth;
using BDP.Web.Api.Auth.Jwt;
using BDP.Web.Api.Auth.Requirements.Handlers;
using BDP.Web.Api.Filters;
using BDP.Web.Api.Services;
using BDP.Web.Dtos.Converters;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using System.Text;

var builder = WebApplication.CreateBuilder(args);
var appSettingsSvc = new AppSettingsConfigurationService(builder.Configuration);

#region Services

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(o => o.LowercaseUrls = true);

// jwt/bearer authentication
builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(o =>
    {
        var jwtSettings = new JwtSettings();
        appSettingsSvc.Bind(nameof(JwtSettings), jwtSettings);

        o.SaveToken = true;
        o.RequireHttpsMetadata = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.AccessTokenSecret)),
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            ClockSkew = TimeSpan.Zero
        };
    });

// swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BDP API", Version = "v1" });
    // c.EnableAnnotations();

    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

// database
builder.Services.AddDbContext<BdpDbContext>(cfg =>
    cfg.UseSqlServer(appSettingsSvc.Get("ConnectionStrings:DefaultConnection"))
);

// services
builder.Services.AddSingleton<IConfigurationService>(appSettingsSvc);
builder.Services.AddSingleton<IRandomGeneratorService, SimpleRandomGeneratorService>();
builder.Services.AddSingleton<IPasswordHashingService, Argon2PasswordHashingService>();
builder.Services.AddScoped<IUnitOfWork, BdpUnitOfWork>();
builder.Services.AddSingleton<IEmailService, SmtpEmailService>();
builder.Services.AddScoped<IAttachmentsService, LocalAttachmentsService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IFinancialRecordsService, FinancialRecordsService>();
builder.Services.AddScoped<ITransactionsService, TransactionsService>();
builder.Services.AddScoped<IFinanceService, FinanceService>();
builder.Services.AddScoped<IProductReviewsService, ProductReviewsService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IPurchasesService, PurchasesService>();
builder.Services.AddScoped<ISearchSuggestionsService, SearchSuggestionsService>();
builder.Services.AddScoped<IEventsService, EventsService>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();

// Autmapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Requirement handlers
builder.Services.AddSingleton<IAuthorizationHandler, HasAllRolesRequirementHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PoliciesProvider>();

// strongly-typed id
builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new StronglyTypedIdJsonConverter<Attachment>());
            options.JsonSerializerOptions.Converters.Add(new StronglyTypedIdJsonConverter<Category>());
            options.JsonSerializerOptions.Converters.Add(new StronglyTypedIdJsonConverter<Confirmation>());
            options.JsonSerializerOptions.Converters.Add(new StronglyTypedIdJsonConverter<Event>());
            options.JsonSerializerOptions.Converters.Add(new StronglyTypedIdJsonConverter<EventType>());
            options.JsonSerializerOptions.Converters.Add(new StronglyTypedIdJsonConverter<FinancialRecord>());
            options.JsonSerializerOptions.Converters.Add(new StronglyTypedIdJsonConverter<FinancialRecordVerification>());
            options.JsonSerializerOptions.Converters.Add(new StronglyTypedIdJsonConverter<Log>());
            options.JsonSerializerOptions.Converters.Add(new StronglyTypedIdJsonConverter<LogTag>());
            options.JsonSerializerOptions.Converters.Add(new StronglyTypedIdJsonConverter<Order>());
            options.JsonSerializerOptions.Converters.Add(new StronglyTypedIdJsonConverter<Product>());
            options.JsonSerializerOptions.Converters.Add(new StronglyTypedIdJsonConverter<ProductReview>());
            options.JsonSerializerOptions.Converters.Add(new StronglyTypedIdJsonConverter<ProductVariant>());
            options.JsonSerializerOptions.Converters.Add(new StronglyTypedIdJsonConverter<RefreshToken>());
            options.JsonSerializerOptions.Converters.Add(new StronglyTypedIdJsonConverter<ReservationWindow>());
            options.JsonSerializerOptions.Converters.Add(new StronglyTypedIdJsonConverter<StockBatch>());
            options.JsonSerializerOptions.Converters.Add(new StronglyTypedIdJsonConverter<Transaction>());
            options.JsonSerializerOptions.Converters.Add(new StronglyTypedIdJsonConverter<TransactionConfirmation>());
            options.JsonSerializerOptions.Converters.Add(new StronglyTypedIdJsonConverter<User>());
            options.JsonSerializerOptions.Converters.Add(new StronglyTypedIdJsonConverter<UserProfile>());
        });

#endregion Services

#region Filters

builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
});

#endregion Filters

var app = builder.Build();

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// authentication/authorization
app.UseAuthentication();
app.UseAuthorization();
//app.UseHttpsRedirection();

app.MapControllers();
app.Run();