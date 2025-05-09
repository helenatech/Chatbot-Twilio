using Chatbot.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chatbot.Infrastructure.Data.Mappings
{
    public class AvaliationMap : IEntityTypeConfiguration<Avaliation>
    {
        public void Configure(EntityTypeBuilder<Avaliation> builder)
        {
            builder.ToTable("Avaliation");

            builder.HasKey("Id");

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .HasColumnType("INT")
                .UseIdentityColumn();


            builder.Property(x => x.Rating)
                .HasColumnName("Rating")
                .HasColumnType("TINYINT")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                 .HasColumnName("CreatedAt")
                 .HasColumnType("DATETIME")
                 .HasDefaultValueSql("GETDATE()")
                 .IsRequired();
        }
    }
}
