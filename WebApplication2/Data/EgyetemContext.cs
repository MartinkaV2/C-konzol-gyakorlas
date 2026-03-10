using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace DiakKurzusApi.Data
{
    public class EgyetemContext : DbContext
    {
        public EgyetemContext(DbContextOptions<EgyetemContext> options) : base(options)
        {
        }

        public DbSet<Diak> Diakok { get; set; }
        public DbSet<Kurzus> Kurzusok { get; set; }
    }
}