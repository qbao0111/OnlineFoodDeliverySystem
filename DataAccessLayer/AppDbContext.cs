using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<RestaurantOwner> RestaurantOwners => Set<RestaurantOwner>();
    public DbSet<Driver> Drivers => Set<Driver>();
    public DbSet<Admin> Admins => Set<Admin>();
    public DbSet<Restaurant> Restaurants => Set<Restaurant>();
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();
    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Delivery> Deliveries => Set<Delivery>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Notification> Notifications => Set<Notification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasDiscriminator<string>("UserType")
            .HasValue<Customer>("Customer")
            .HasValue<RestaurantOwner>("RestaurantOwner")
            .HasValue<Driver>("Driver")
            .HasValue<Admin>("Admin");

        modelBuilder.Entity<User>().HasIndex(user => user.Email).IsUnique();

        modelBuilder.Entity<Customer>()
            .HasOne(customer => customer.Cart)
            .WithOne(cart => cart.Customer)
            .HasForeignKey<Cart>(cart => cart.CustomerId);

        modelBuilder.Entity<Order>()
            .HasOne(order => order.Payment)
            .WithOne(payment => payment.Order)
            .HasForeignKey<Payment>(payment => payment.OrderId);

        modelBuilder.Entity<Order>()
            .HasOne(order => order.Delivery)
            .WithOne(delivery => delivery.Order)
            .HasForeignKey<Delivery>(delivery => delivery.OrderId);

        modelBuilder.Entity<CartItem>()
            .HasIndex(item => new { item.CartId, item.MenuItemId })
            .IsUnique();

        foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(entity => entity.GetForeignKeys()))
        {
            foreignKey.DeleteBehavior = DeleteBehavior.NoAction;
        }

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        const string demoPasswordHash = "2A97516C354B68848CDBD8F54A226A0A55B21ED138E207AD6C5CBB9C00AA5AEA";

        modelBuilder.Entity<Customer>().HasData(new Customer { Id = 1, FullName = "Demo Customer", Email = "customer@example.com", PasswordHash = demoPasswordHash, PhoneNumber = "0900000001", Role = "Customer", Address = "Ho Chi Minh City" });
        modelBuilder.Entity<RestaurantOwner>().HasData(new RestaurantOwner { Id = 2, FullName = "Demo Owner", Email = "owner@example.com", PasswordHash = demoPasswordHash, PhoneNumber = "0900000002", Role = "Restaurant" });
        modelBuilder.Entity<Driver>().HasData(new Driver { Id = 3, FullName = "Demo Driver", Email = "driver@example.com", PasswordHash = demoPasswordHash, PhoneNumber = "0900000003", Role = "Driver", VehicleNumber = "59A-12345", IsAvailable = true });
        modelBuilder.Entity<Admin>().HasData(new Admin { Id = 4, FullName = "Demo Admin", Email = "admin@example.com", PasswordHash = demoPasswordHash, PhoneNumber = "0900000004", Role = "Admin" });
        modelBuilder.Entity<Restaurant>().HasData(new Restaurant { Id = 1, Name = "Campus Bites", Address = "University Street", PhoneNumber = "0280000001", Status = "Active", RestaurantOwnerId = 2 });
        modelBuilder.Entity<MenuItem>().HasData(
            new MenuItem { Id = 1, RestaurantId = 1, Name = "Chicken Rice", Description = "Grilled chicken with rice", Price = 45000, IsAvailable = true },
            new MenuItem { Id = 2, RestaurantId = 1, Name = "Milk Tea", Description = "Classic milk tea", Price = 25000, IsAvailable = true });
        modelBuilder.Entity<Cart>().HasData(new Cart { Id = 1, CustomerId = 1 });
    }
}
