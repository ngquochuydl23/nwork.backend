using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using n.work.Entity;
using n.work.Models;

namespace n.work.DataContext
{
  public class DatabaseContext : DbContext
  {
    public DbSet<Account> Account { get; set; }
    public DbSet<Profile> Profile { get; set; }
    public DbSet<WorkerAccount> WorkerAccount { get; set; }
    public DbSet<WorkerProfile> WorkerProfile { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<ItemOrder> ItemOrder { get; set; }
    public DbSet<OrderDetail> OrderDetail { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // Map table names
      CreateAccountModelMap(modelBuilder.Entity<Account>());
      CreateProfileModelMap(modelBuilder.Entity<Profile>());
      //Image
      CreateImageModelMap(modelBuilder.Entity<Image>());
      //Partner
      CreateWorkderAccountModelMap(modelBuilder.Entity<WorkerAccount>());
      CreateWorkerProfileModelMap(modelBuilder.Entity<WorkerProfile>());
      //Activity
      CreateOrderModelMap(modelBuilder.Entity<OrderDetail>());
      CreateItemOrderModelMap(modelBuilder.Entity<ItemOrder>());

      base.OnModelCreating(modelBuilder);
    }

    private void CreateImageModelMap(EntityTypeBuilder<Image> builder)
    {
      builder.ToTable(nameof(Images));
      builder.HasKey(itemActivity => itemActivity.ImageId);
    }

    private void CreateOrderModelMap(EntityTypeBuilder<OrderDetail> builder)
    {
      builder.ToTable(nameof(OrderDetail));
      builder.HasKey(order => order.OrderId);
      builder
        .HasOne(order => order.ItemOrder)
        .WithOne(itemOrder => itemOrder.OrderDetail)
        .HasForeignKey<ItemOrder>(itemOrder => itemOrder.OrderId)    
        .IsRequired();

      builder
        .HasOne(order => order.Worker)
        .WithMany(worker => worker.OrderDetail)
        .HasForeignKey(order => order.WorkerId)
        .IsRequired();

      builder
        .HasOne(order => order.Customer)
        .WithMany(user => user.OrderDetail)
        .HasForeignKey(order => order.CustomerId)
        .IsRequired();

    }

    private void CreateItemOrderModelMap(EntityTypeBuilder<ItemOrder> builder)
    {
      builder.HasKey(itemOrder => itemOrder.OrderId);
    }

    private void CreateAccountModelMap(EntityTypeBuilder<Account> builder)
    {
      builder.ToTable(nameof(Account));
      builder.HasKey(account => account.Id);
      builder
        .HasOne(acc => acc.Profile)
        .WithOne(profile => profile.Account)
        .HasForeignKey<Profile>(profile => profile.AccountId)
        .IsRequired();
    }

    private void CreateProfileModelMap(EntityTypeBuilder<Profile> builder)
    {
      builder.HasKey(profile => profile.AccountId);
    }

    private void CreateWorkderAccountModelMap(EntityTypeBuilder<WorkerAccount> builder)
    {
      builder.ToTable(nameof(WorkerAccount));
      builder.HasKey(account => account.Id);

      builder
        .HasOne(acc => acc.WorkerProfile)
        .WithOne(profile => profile.WorkerAccount)
        .HasForeignKey<WorkerProfile>(profile => profile.AccountId)
        .IsRequired();

    }

    private void CreateWorkerProfileModelMap(EntityTypeBuilder<WorkerProfile> builder)
    {
      builder.HasKey(profile => profile.AccountId);
    }
  }
}
