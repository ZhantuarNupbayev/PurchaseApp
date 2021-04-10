using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataLayer.Configuration
{
    class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
          builder.HasData
          (
              new Product
              {
                  Id = Guid.NewGuid(),
                  ProductName = "LG Flatron",
                  CategoryId = new Guid("13dbace0-88b3-4e8d-8582-55c212db02fb"),
                  Price = Double.Parse("2500")
              },
              new Product
              {
                  Id = Guid.NewGuid(),
                  ProductName = "IPhone 8s",
                  CategoryId = new Guid("846a0f2c-5cc7-4af0-945f-ee18559261a7"),
                  Price = Double.Parse("2500")
              },
              new Product
              {
                  Id = Guid.NewGuid(),
                  ProductName = "HP Desktop 3000",
                  CategoryId = new Guid("0b461695-96cc-485c-8b6e-ec6ef5e10a0f"),
                  Price = Double.Parse("2500")
              }
          );
        }
    }
}
