using Chatbot.Domain.Models;
using Chatbot.Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Chatbot.Infrastructure.Data
{
    public class ChatbotDataContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<Avaliation> Avaliations { get; set; }

        public ChatbotDataContext(DbContextOptions<ChatbotDataContext> options)
            : base(options) // Passa as opções para o construtor da classe base DbContext
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=localhost;Database=Chatbot;Integrated Security=True;TrustServerCertificate=True");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientMap());
            modelBuilder.ApplyConfiguration(new ProfileMap());
            modelBuilder.ApplyConfiguration(new InteractionMap());
            modelBuilder.ApplyConfiguration(new AvaliationMap());
        }
    }
}

