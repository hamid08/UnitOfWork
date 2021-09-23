using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RohaniShop.Entities;

namespace RohaniShop.Data.Mapping
{
   public class FactorMapping : IEntityTypeConfiguration<Factor>
    {
       
            public void Configure(EntityTypeBuilder<Factor> builder)
            {
                builder.HasKey(t => t.FactorId);
                builder
                    .HasOne(p => p.Customer)
                    .WithMany(t => t.Factors)
                    .HasForeignKey(f => f.CustomerId);
        }
        
    }
}
