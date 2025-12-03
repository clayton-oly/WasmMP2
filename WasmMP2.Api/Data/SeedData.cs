using WasmMP2.Api.Models;

namespace WasmMP2.Api.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            //cria base de dados e tabelas se elas ainda não existem
            context.Database.EnsureCreated();

            //se tem produtos, ele não faz nada
            if (context.Products.Any()) return;

            var catBurger = new Category { Name = "Lanches" };
            var catDrink = new Category { Name = "Bebidas" };
            var catDessert = new Category { Name = "Sobremessa" };
            context.Categories.AddRange(catBurger, catDrink, catDessert);

            context.Products.AddRange(
                new Product
                {
                    Name = "xBurger",
                    Price = 25.9m,
                    ImageUrl = "https://via.placeholder.com/300x200?text=XBurger",
                    IsFavorite = true,
                    Category = catBurger
                },
                new Product
                {
                    Name = "Coca-Cola",
                    Price = 7.0m,
                    ImageUrl = "https://via.placeholder.com/300x200?text=XBurger",
                    IsFavorite = true,
                    Category = catDrink
                },
                new Product
                {
                    Name = "Pudim",
                    Price = 10.9m,
                    ImageUrl = "https://via.placeholder.com/300x200?text=batata+frita",
                    IsFavorite = true,
                    Category = catDessert
                }
            );

            context.SaveChanges();
        }
    }
}
