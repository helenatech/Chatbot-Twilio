using System;
using System.Collections.Generic;
using Chatbot.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chatbot.Infrastructure.Data.Mappings
{
    public class ClientMap : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client");

            builder.HasKey(x => x.ClientId);

            builder.Property(x => x.ClientId)
                .HasColumnName("ClientId")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(20);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.CpfPrefix)
                .IsRequired()
                .HasColumnName("CpfPrefix")
                .HasColumnType("CHAR")
                .HasMaxLength(4)
                .IsFixedLength(); //pra forçar o char 4

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(255);

            builder.Property(x => x.Status)
                .HasColumnName("Status")
                .HasColumnType("NVARCHAR(20)")
                .IsRequired();

            builder.Property(x => x.LastUpdatedAt)
                .IsRequired()
                .HasColumnName("LastUpdatedAt")
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.ProfileId)
                .HasColumnName("ProfileId")
                .IsRequired();

            builder.HasOne(x => x.Profile)
                .WithMany(x => x.Clients)
                .HasForeignKey(x => x.ProfileId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.Email)
                 .HasDatabaseName("IX_Client_Email");
        }
    }
}