using Microsoft.EntityFrameworkCore;
using pinguinera_final_module.Models.Persistence;

namespace pinguinera_final_module.Database.Interfaces;

public interface IDatabase {
      DbSet<Admin> Admins { get; set; }

      DbSet<Assistant> Assistants { get; set; }

      DbSet<BookLibraryItem> BookLibraryItems { get; set; }

      DbSet<BookSupplierItem> BookSupplierItems { get; set; }

      DbSet<LibraryItem> LibraryItems { get; set; }

      DbSet<LibraryItemLoan> LibraryItemLoans { get; set; }

      DbSet<Loan> Loans { get; set; }

      DbSet<NovelLibraryItem> NovelLibraryItems { get; set; }

      DbSet<NovelSupplierItem> NovelSupplierItems { get; set; }

      DbSet<Quote> Quotes { get; set; }

      DbSet<QuoteSupplierItem> QuoteSupplierItems { get; set; }

      DbSet<Reader> Readers { get; set; }

      DbSet<Supplier> Suppliers { get; set; }

      DbSet<SupplierItem> SupplierItems { get; set; }

      DbSet<User> Users { get; set; }
      Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}