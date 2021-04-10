using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataLayer.Configuration
{
    /**
     * Класс конфигурации начальных данных по категориям
     * Используется для заполнения таблицы Категории
     */
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        /*
         * Метод для конфигурации данных
         * @builder - переменная обработки
         */
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData
            (
                new Category
                {
                    Id = new Guid("13dbace0-88b3-4e8d-8582-55c212db02fb"),
                    CategoryName = "TV",
                },
                new Category
                {
                    Id = new Guid("846a0f2c-5cc7-4af0-945f-ee18559261a7"),
                    CategoryName = "Smartphone",
                },
                new Category
                {
                    Id = new Guid("0b461695-96cc-485c-8b6e-ec6ef5e10a0f"),
                    CategoryName = "Computer",
                }
            );
        }
    }
}
