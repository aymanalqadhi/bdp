using BDP.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace BDP.Infrastructure.Repositories.EntityFramework;

public class BdpDbContext : DbContext
{
    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="options">Options to be used to configure the database context</param>
    public BdpDbContext(DbContextOptions options) : base(options)
    {
    }

    #endregion Public Constructors

    #region Properties

    /// <summary>
    /// Gets the attachments table
    /// </summary>
    public DbSet<Attachment> Attachments => Set<Attachment>();

    /// <summary>
    /// Gets the confirmations table
    /// </summary>
    public DbSet<Confirmation> Confirmations => Set<Confirmation>();

    /// <summary>
    /// Gets the events table
    /// </summary>
    public DbSet<Event> Events => Set<Event>();

    /// <summary>
    /// Gets the event types table
    /// </summary>
    public DbSet<EventType> EventTypes => Set<EventType>();

    /// <summary>
    /// Gets the financial records table
    /// </summary>
    public DbSet<FinancialRecord> FinancialRecords => Set<FinancialRecord>();

    /// <summary>
    /// Gets the financial record verfications table
    /// </summary>
    public DbSet<FinancialRecordVerification> FinancialRecordVerifications => Set<FinancialRecordVerification>();

    /// <summary>
    /// Gets the logs table
    /// </summary>
    public DbSet<Log> Logs => Set<Log>();

    /// <summary>
    /// Gets the log tags table
    /// </summary>
    public DbSet<LogTag> LogTags => Set<LogTag>();

    /// <summary>
    /// Gets the product orders table
    /// </summary>
    public DbSet<ProductOrder> ProductOrders => Set<ProductOrder>();

    /// <summary>
    /// Gets the products table
    /// </summary>
    public DbSet<Product> Products => Set<Product>();

    /// <summary>
    /// Gets the referesh tokens table
    /// </summary>
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    /// <summary>
    /// Gets the product reviews
    /// </summary>
    public DbSet<ProductReview> ProductReviews => Set<ProductReview>();

    /// <summary>
    /// Gets the product reviews
    /// </summary>
    public DbSet<ServiceReview> ServiceReviews => Set<ServiceReview>();

    /// <summary>
    /// Gets the service reservations table
    /// </summary>
    public DbSet<ServiceReservation> ServiceReservations => Set<ServiceReservation>();

    /// <summary>
    /// Gets the services table
    /// </summary>
    public DbSet<Service> Services => Set<Service>();

    /// <summary>
    /// Gets the transaction confirmations table
    /// </summary>
    public DbSet<TransactionConfirmation> TransactionConfirmations => Set<TransactionConfirmation>();

    /// <summary>
    /// Gets the transactions table
    /// </summary>
    public DbSet<Transaction> Transactions => Set<Transaction>();

    /// <summary>
    /// Gets the users table
    /// </summary>
    public DbSet<User> Users => Set<User>();

    /// <summary>
    /// Gets the user profiles table
    /// </summary>
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();

    #endregion Properties

    #region Protected Methods

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder builder)
        => builder.ApplyConfigurationsFromAssembly(typeof(BdpDbContext).Assembly);

    #endregion Protected Methods
}