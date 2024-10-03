using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PassionProject.Models;

namespace PassionProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        // used for connecting to the database
        // card.cs will map to a cards table
        public DbSet<Card> Cards { get; set; }

        // color.cs will map to the colors table
        public DbSet<Color> Colors { get; set; }

        // artist.cs will map to the artists table
        public DbSet<Artist> Artists { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
