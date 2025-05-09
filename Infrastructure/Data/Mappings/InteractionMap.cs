using Chatbot.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chatbot.Infrastructure.Data.Mappings
{
    public class InteractionMap : IEntityTypeConfiguration<Interaction>
    {
        public void Configure(EntityTypeBuilder<Interaction> builder)
        {

            builder.ToTable("Interaction");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .HasColumnType("INT")
                .UseIdentityColumn();

            builder.Property(x => x.ClientMessage)
                .IsRequired()
                .HasColumnName("ClientMessage")
                .HasColumnType("NVARCHAR(1000)");

            builder.Property(x => x.Intention)
                .HasColumnName("Intention")
                .HasColumnType("NVARCHAR(255)")
                .IsRequired(false);

            builder.Property(x => x.BotMessage)
                .HasColumnName("BotMessage")
                .HasColumnType("NVARCHAR(1000)")
                .IsRequired(false);

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt")
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.SessionId)
                .HasColumnName("SessionId")
                .HasColumnType("UNIQUEIDENTIFIER");

            builder.HasOne(x => x.Avaliation)
                .WithOne(x => x.Interaction)
                .HasForeignKey<Avaliation>(x => x.InteractionId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Client)         
                 .WithMany(u => u.Interactions) 
                 .HasForeignKey(x => x.ClientId)
                 .OnDelete(DeleteBehavior.SetNull); //Config para as interações com os usuários deletados não sejam deletadas

        }
    }
}
