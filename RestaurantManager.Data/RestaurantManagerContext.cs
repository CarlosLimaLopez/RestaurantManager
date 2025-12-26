namespace RestaurantManager.Context
{
    using Restaurant;

    /// <summary>
    /// Database context for the RestaurantManager application.
    /// </summary>
    public class RestaurantManagerContext : DbContext
    {
        public RestaurantManagerContext(DbContextOptions<RestaurantManagerContext> options)
            : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuSection> MenuSections { get; set; }
        public DbSet<Dish> Dishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Restaurant>(restaurant =>
            {
                restaurant.HasMany(r => r.Menus)
                    .WithOne()
                    .HasForeignKey("RestaurantId")
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Menu>(menu =>
            {
                menu.HasMany(r => r.Sections)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<MenuSection>(menuSection =>
            {
                menuSection.HasMany(r => r.Dishes)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
