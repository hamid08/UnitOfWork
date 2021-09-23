using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RohaniShop.Entities;

namespace RohaniShop.Data.Mapping
{
   public class FactorProductMapping: IEntityTypeConfiguration<FactorProduct>
    {
        public void Configure(EntityTypeBuilder<FactorProduct> builder)
        {
            builder
                .HasOne(p => p.Product)
                .WithMany(t => t.FactorProducts)
                .HasForeignKey(f => f.ProductId);

            builder
                .HasOne(p => p.Factor)
                .WithMany(t => t.FactorProducts)
                .HasForeignKey(f => f.FactorId);
        }
    }
}
