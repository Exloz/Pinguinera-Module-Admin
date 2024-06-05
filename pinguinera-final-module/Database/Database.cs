using Microsoft.EntityFrameworkCore;
using pinguinera_final_module.Database.Interfaces;

namespace pinguinera_final_module.Database;

public class Database : DbContext, IDatabase{

    public Database(DbContextOptions options) : base(options) {
    }

    public async Task<bool> SaveAsync() {
        return await SaveChangesAsync() > 0;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        EntityConfiguration(modelBuilder);
    }

    private void EntityConfiguration(ModelBuilder modelBuilder) {
    }
}