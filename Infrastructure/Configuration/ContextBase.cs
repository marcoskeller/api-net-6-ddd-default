using Entities.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configuration
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {


        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {

        }


        public DbSet<Message> Message { get; set; }
        
        public DbSet<ApplicationUser> ApplicationUser { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ObterStringConexao());
                base.OnConfiguring(optionsBuilder);
            }
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);

            base.OnModelCreating(builder);
        }


        //Método alternativo para obter a string de conexão com o banco de dados caso não exista no arquivo appSettings.json
        public string ObterStringConexao()
        {
            return "Data Source=SQNOT14190\\SQLEXPRESS;Initial Catalog=API_DDD_DEFAULT;Integrated Security=False;User ID=sa;Password=596874;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
        }
    }
}
