using Microsoft.EntityFrameworkCore;
using pinguinera_final_module.Database.Interfaces;
using pinguinera_final_module.Models.Persistence;

namespace pinguinera_final_module.Database;

public partial class Database : DbContext, IDatabase
{
    public Database()
    {
    }

    public Database(DbContextOptions<Database> options)
        : base(options)
    {
    }

    public DbSet<Admin> Admins { get; set; }

    public DbSet<Assistant> Assistants { get; set; }

    public DbSet<BookLibraryItem> BookLibraryItems { get; set; }

    public DbSet<BookSupplierItem> BookSupplierItems { get; set; }

    public DbSet<LibraryItem> LibraryItems { get; set; }

    public DbSet<LibraryItemLoan> LibraryItemLoans { get; set; }

    public DbSet<Loan> Loans { get; set; }

    public DbSet<NovelLibraryItem> NovelLibraryItems { get; set; }

    public DbSet<NovelSupplierItem> NovelSupplierItems { get; set; }

    public DbSet<Quote> Quotes { get; set; }

    public DbSet<QuoteSupplierItem> QuoteSupplierItems { get; set; }

    public DbSet<Reader> Readers { get; set; }

    public DbSet<Supplier> Suppliers { get; set; }

    public DbSet<SupplierItem> SupplierItems { get; set; }

    public DbSet<User> Users { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(
            "Host=monorail.proxy.rlwy.net;Port=32785;Database=railway;Username=postgres;Password=ncFRZnJaaUeCLcGAiyaCfHDeSMIyRXur");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("Admin_pkey");

            entity.ToTable("Admin");

            entity.Property(e => e.AdminId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("adminId");

            entity.HasOne(d => d.AdminNavigation).WithOne(p => p.Admin)
                .HasForeignKey<Admin>(d => d.AdminId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("adminId");
        });

        modelBuilder.Entity<Assistant>(entity =>
        {
            entity.HasKey(e => e.AssistantId).HasName("Assistant_pkey");

            entity.ToTable("Assistant");

            entity.Property(e => e.AssistantId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("assistantId");

            entity.HasOne(d => d.AssistantNavigation).WithOne(p => p.Assistant)
                .HasForeignKey<Assistant>(d => d.AssistantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("assistantId");
        });

        modelBuilder.Entity<BookLibraryItem>(entity =>
        {
            entity.HasKey(e => e.BookLibraryItemId).HasName("book_library_item_pkey");

            entity.ToTable("book_library_item");

            entity.Property(e => e.BookLibraryItemId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("book_library_item_id");
            entity.Property(e => e.KnowledgeArea)
                .HasColumnType("character varying")
                .HasColumnName("knowledge_area");
            entity.Property(e => e.Pages).HasColumnName("pages");
        });

        modelBuilder.Entity<BookSupplierItem>(entity =>
        {
            entity.HasKey(e => e.BookSuplierItemId).HasName("book_supplier_Item_pkey");

            entity.ToTable("book_supplier_item");

            entity.Property(e => e.BookSuplierItemId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("book_supplier_item_id");
            entity.Property(e => e.KnowledgeArea)
                .HasColumnType("character varying")
                .HasColumnName("knowledge_area");
            entity.Property(e => e.Pages).HasColumnName("pages");
        });

        modelBuilder.Entity<LibraryItem>(entity =>
        {
            entity.HasKey(e => e.LibraryItemId).HasName("library_item_pkey");

            entity.ToTable("library_item");

            entity.Property(e => e.LibraryItemId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("library_item_id");
            entity.Property(e => e.Author)
                .HasColumnType("character varying")
                .HasColumnName("author");
            entity.Property(e => e.BorrowedQuantity).HasColumnName("borrowed_quantity");
            entity.Property(e => e.Stock).HasColumnName("stock");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");

            entity.HasOne(e => e.BookLibraryItem)
                .WithOne(b => b.LibraryItem)
                .HasForeignKey<BookLibraryItem>(b => b.BookLibraryItemId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.NovelLibraryItem)
                .WithOne(n => n.LibraryItem)
                .HasForeignKey<NovelLibraryItem>(n => n.NovelLibraryItemId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<LibraryItemLoan>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("LibraryItem_Loan");

            entity.Property(e => e.LibraryItemLibraryItemId).HasColumnName("LibraryItem_libraryItemId");
            entity.Property(e => e.LoanLoanId).HasColumnName("Loan_loanId");

            entity.HasOne(d => d.LibraryItemLibraryItem).WithMany()
                .HasForeignKey(d => d.LibraryItemLibraryItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LibraryItem_Loan_LibraryItem_libraryItemId_fkey");

            entity.HasOne(d => d.LoanLoan).WithMany()
                .HasForeignKey(d => d.LoanLoanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LibraryItem_Loan_Loan_loanId_fkey");
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(e => e.LoanId).HasName("Loan_pkey");

            entity.ToTable("Loan");

            entity.Property(e => e.LoanId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("loanId");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.Observation)
                .HasColumnType("character varying")
                .HasColumnName("observation");
            entity.Property(e => e.PickUpDate).HasColumnName("pickUpDate");
            entity.Property(e => e.ReaderId).HasColumnName("readerId");
            entity.Property(e => e.Status)
                .HasColumnType("character varying")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");

            entity.HasOne(d => d.Reader).WithMany(p => p.Loans)
                .HasForeignKey(d => d.ReaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("readerId");
        });

        modelBuilder.Entity<NovelLibraryItem>(entity =>
        {
            entity.HasKey(e => e.NovelLibraryItemId).HasName("novel_library_item_pkey");

            entity.ToTable("novel_library_item");

            entity.Property(e => e.NovelLibraryItemId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("novel_library_item_id");
            entity.Property(e => e.Genre)
                .HasColumnType("character varying")
                .HasColumnName("genre");
            entity.Property(e => e.SuggestedAge).HasColumnName("suggested_age");
        });

        modelBuilder.Entity<NovelSupplierItem>(entity =>
        {
            entity.HasKey(e => e.NovelSupplierItemId).HasName("novel_supplier_item_pkey");

            entity.ToTable("novel_supplier_item");

            entity.Property(e => e.NovelSupplierItemId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("novel_supplier_item_id");
            entity.Property(e => e.Genre)
                .HasColumnType("character varying")
                .HasColumnName("genre");
            entity.Property(e => e.SuggestedAge).HasColumnName("suggested_age");
        });

        modelBuilder.Entity<Quote>(entity =>
        {
            entity.HasKey(e => e.QuoteId).HasName("quote_pkey");

            entity.ToTable("quote");

            entity.Property(e => e.QuoteId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("quote_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.RetailOverPrice).HasColumnName("retail_over_price");
            entity.Property(e => e.SeniorityDiscount).HasColumnName("seniority_discount");
            entity.Property(e => e.TotalPrice).HasColumnName("total_price");
            entity.Property(e => e.Type)
                .HasColumnType("character varying")
                .HasColumnName("type");
            entity.Property(e => e.TypeDiscount).HasColumnName("type_discount");
        });

        modelBuilder.Entity<QuoteSupplierItem>(entity =>
        {
            entity.HasKey(e => new { e.QuoteQuoteId, e.SupplierItemSupplierItemId });
            entity.ToTable("quote_supplier_item");

            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.QuoteQuoteId).HasColumnName("quote_quote_id");
            entity.Property(e => e.SupplierItemSupplierItemId).HasColumnName("supplier_item_supplier_item_id");

            entity.HasOne(d => d.QuoteQuote)
                .WithMany()
                .HasForeignKey(d => d.QuoteQuoteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Quote_SupplierItem_Quote_quoteId_fkey");

            entity.HasOne(d => d.SupplierItemSupplierItem)
                .WithMany()
                .HasForeignKey(d => d.SupplierItemSupplierItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Quote_SupplierItem_SupplierItem_supplierItemId_fkey");
        });

        modelBuilder.Entity<Reader>(entity =>
        {
            entity.HasKey(e => e.ReaderId).HasName("Reader_pkey");

            entity.ToTable("Reader");

            entity.Property(e => e.ReaderId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("readerId");

            entity.HasOne(d => d.ReaderNavigation).WithOne(p => p.Reader)
                .HasForeignKey<Reader>(d => d.ReaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("readerId");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("supplier_pkey");

            entity.ToTable("supplier");

            entity.Property(e => e.SupplierId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("supplier_id");

            entity.HasOne(d => d.SupplierNavigation).WithOne(p => p.Supplier)
                .HasForeignKey<Supplier>(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("supplier_id");
        });

        modelBuilder.Entity<SupplierItem>(entity =>
        {
            entity.HasKey(e => e.SupplierItemId).HasName("supplier_item_pkey");

            entity.ToTable("supplier_item");

            entity.Property(e => e.SupplierItemId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("supplier_item_id");
            entity.Property(e => e.Author)
                .HasColumnType("character varying")
                .HasColumnName("author");
            entity.Property(e => e.BasePrice).HasColumnName("base_price");
            entity.Property(e => e.SellPrice).HasColumnName("sell_price");
            entity.Property(e => e.Stock).HasColumnName("stock");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");

            entity.HasOne(d => d.Supplier).WithMany(p => p.SupplierItems)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("supplier_id");

            entity.HasOne(d => d.BookSupplierItem)
                .WithOne()
                .HasForeignKey<BookSupplierItem>(b => b.BookSuplierItemId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("bookSupplierItemId");

            entity.HasOne(d => d.NovelSupplierItem)
                .WithOne()
                .HasForeignKey<NovelSupplierItem>(n => n.NovelSupplierItemId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("novelSupplierItemId");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("User_pkey");

            entity.ToTable("user");

            entity.Property(e => e.UserId)
                .HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
            entity.Property(e => e.Salt)
                .HasColumnType("character bytea")
                .HasColumnName("salt");
            entity.Property(e => e.RefreshToken)
                .HasColumnType("character varying")
                .HasColumnName("refresh_token");
            entity.Property(e => e.RegisterAt).HasColumnName("registered_at");
            entity.Property(e => e.Role)
                .HasColumnType("character varying")
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasColumnType("character varying")
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}