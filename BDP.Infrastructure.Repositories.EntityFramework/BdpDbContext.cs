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

    /// <summary>
    /// Configures database context on creation
    /// </summary>
    /// <param name="builder"></param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // configure user unique fields
        builder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        builder.Entity<User>().HasIndex(u => u.Email).IsUnique();

        // configure transaction unique fields
        builder.Entity<Transaction>().HasIndex(t => t.ConfirmationToken).IsUnique();

        // set decimal bakcend-type
        builder.Entity<Product>()
            .Property(s => s.Price)
            .HasPrecision(18, 6);
        builder.Entity<Service>()
            .Property(s => s.Price)
            .HasPrecision(18, 6);
        builder.Entity<Transaction>()
            .Property(s => s.Amount)
            .HasPrecision(18, 6);
        builder.Entity<FinancialRecord>()
            .Property(s => s.Amount)
            .HasPrecision(18, 6);

        // break cycles
        builder.Entity<Transaction>()
            .HasOne(t => t.From)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Transaction>()
            .HasOne(t => t.To)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<FinancialRecord>()
            .HasOne(t => t.MadeBy)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<FinancialRecordVerification>()
            .HasOne(t => t.VerifiedBy)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<FinancialRecord>()
            .HasOne(r => r.Verification)
            .WithOne(v => v.FinancialRecord)
            .HasForeignKey<FinancialRecordVerification>(v => v.FinancialRecordId);

        builder.Entity<Transaction>()
            .HasOne(t => t.Confirmation)
            .WithOne(c => c.Transaction)
            .HasForeignKey<TransactionConfirmation>(t => t.TransactionId);

        builder.Entity<ProductReview>()
            .HasOne(r => r.LeftBy)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<ServiceReview>()
            .HasOne(r => r.LeftBy)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<ProductOrder>()
            .HasOne(o => o.Item)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ServiceReservation>()
            .HasOne(s => s.Item)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        // auto include
        builder.Entity<User>()
            .Navigation(u => u.ProfilePicture)
            .AutoInclude();
        builder.Entity<User>()
            .Navigation(u => u.CoverPicture)
            .AutoInclude();
        builder.Entity<Sellable>()
            .Navigation(s => s.OfferedBy)
            .AutoInclude();

        builder.Entity<FinancialRecord>()
            .Navigation(t => t.Verification)
            .AutoInclude();

        builder.Entity<Transaction>()
            .Navigation(t => t.Confirmation)
            .AutoInclude();

        builder.Entity<SellableReview>()
            .Navigation(r => r.LeftBy)
            .AutoInclude();

        builder.Entity<ProductOrder>()
            .Navigation(o => o.Product)
            .AutoInclude();

        builder.Entity<ServiceReservation>()
            .Navigation(o => o.Service)
            .AutoInclude();

        builder.Entity<Transaction>()
            .Navigation(o => o.From)
            .AutoInclude();
        builder.Entity<Transaction>()
            .Navigation(o => o.To)
            .AutoInclude();

        // seed data
        builder.Entity<EventType>()
            .HasData(new() { Name = "Wedding" },
                     new() { Name = "Birth Day" },
                     new() { Name = "Engagement Party" },
                     new() { Name = "Graduation Ceremony" },
                     new() { Name = "Graduation Party" },
                     new() { Name = "Other" }
                    );

        // strong-typed identifiers
        SetIdConversion<Attachment>(builder);
        SetIdConversion<Confirmation>(builder);
        SetIdConversion<Event>(builder);
        SetIdConversion<EventType>(builder);
        SetIdConversion<FinancialRecord>(builder);
        SetIdConversion<FinancialRecordVerification>(builder);
        SetIdConversion<Log>(builder);
        SetIdConversion<LogTag>(builder);
        SetIdConversion<Product>(builder);
        SetIdConversion<ProductOrder>(builder);
        SetIdConversion<ProductReview>(builder);
        SetIdConversion<Service>(builder);
        SetIdConversion<ServiceReservation>(builder);
        SetIdConversion<ServiceReview>(builder);
        SetIdConversion<RefreshToken>(builder);
        SetIdConversion<Transaction>(builder);
        SetIdConversion<TransactionConfirmation>(builder);
        SetIdConversion<User>(builder);
        SetIdConversion<UserProfile>(builder);
    }

    #endregion Protected Methods

    #region Private Methods

    private static void SetIdConversion<T>(ModelBuilder builder) where T : AuditableEntity<T>
    {
        builder.Entity<T>()
            .Property(e => e.Id)
            .HasConversion(i => i.Id, g => new EntityKey<T>(g));
    }

    #endregion Private Methods
}