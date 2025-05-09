using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Chatbot.Domain.Models;

namespace Chatbot.Infrastructure.Data.Mappings
{
    public class ProfileMap : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {

            builder.ToTable("Profile");

            builder.HasKey(x => x.ProfileId);

            builder.Property(x => x.ProfileId)
                .HasColumnName("ProfileId")
                .HasColumnType("INT")
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.HasIndex(x => x.Name)
                .IsUnique()
                .HasDatabaseName("IX_Profile_Name");
        }
    }
}

