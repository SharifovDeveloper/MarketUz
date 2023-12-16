using Bogus;
using MarketUz.Domain.Entities;
using MarketUz.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MarketUz.Extensions
{
    public static class DatabaseSeeder
    {
        private static Faker _faker = new Faker();

        public static void SeedDatabase(this IServiceCollection _, IServiceProvider serviceProvider)
        {
            var options = serviceProvider.GetRequiredService<DbContextOptions<MarketUzDbContext>>();
            using var context = new MarketUzDbContext(options);

            CreateCategories(context);
            CreateProducts(context);
            CreateCustomers(context);
            CreateSales(context);
            CreateSaleItems(context);
            CreateSuppliers(context);
            CreateSupplies(context);
            CreateSupplyItems(context);
        }

        private static void CreateCategories(MarketUzDbContext context)
        {
            if (context.Categories.Any()) return;

            List<string> categoryNames = new();
            List<Category> categories = new();

            for (int i = 0; i < 25; i++)
            {
                var categoryName = _faker.Commerce
                    .Categories(1)
                    .First()
                    .FirstLetterToUpper();
                int attempts = 0;

                while (categoryNames.Contains(categoryName) && attempts < 100)
                {
                    categoryName = _faker.Commerce
                        .Categories(1)
                        .First()
                        .FirstLetterToUpper();
                    attempts++;
                }

                categoryNames.Add(categoryName);
                categories.Add(new Category
                {
                    Name = categoryName,
                });
            }

            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        public static void CreateProducts(MarketUzDbContext context)
        {
            if (context.Products.Any()) return;

            var categories = context.Categories.ToList();
            var productNames = new List<string>();
            var products = new List<Product>();

            foreach (var category in categories)
            {
                var productsCount = new Random().Next(5, 35);

                for (int i = 0; i < productsCount; i++)
                {
                    var productName = _faker.Commerce.ProductName().FirstLetterToUpper();
                    int attempts = 0;

                    while (productNames.Contains(productName) && attempts < 100)
                    {
                        productName = _faker.Commerce
                            .ProductName()
                            .FirstLetterToUpper();

                        attempts++;
                    }

                    productNames.Add(productName);

                    products.Add(new Product
                    {
                        Name = productName,
                        Description = _faker.Commerce.ProductDescription(),
                        Price = _faker.Random.Decimal(10_000, 2_000_000),
                        CategoryId = category.Id,
                    });
                }
            }
            context.Products.AddRange(products);
            context.SaveChanges();
        }
        private static void CreateCustomers(MarketUzDbContext context)
        {
            if (context.Customers.Any()) return;
            List<Customer> customers = new List<Customer>();

            for (int i = 0; i < 125; i++)
            {
                customers.Add(new Customer()
                {
                    FirstName = _faker.Name.FirstName(),
                    LastName = _faker.Name.LastName(),
                    PhoneNumber = _faker.Phone.PhoneNumber("+998-(##) ###-##-##")
                });
            }

            context.Customers.AddRange(customers);
            context.SaveChanges();
        }
        private static void CreateSales(MarketUzDbContext context)
        {
            if (context.Sales.Any()) return;

            var customers = context.Customers.ToList();
            List<Sale> sales = new List<Sale>();

            foreach (var customer in customers)
            {
                int salesCount = new Random().Next(5, 50);
                for (int i = 0; i < salesCount; i++)
                {
                    sales.Add(new Sale()
                    {
                        CustomerId = customer.Id,
                        SaleDate = _faker.Date.Between(DateTime.Now.AddYears(-2), DateTime.Now),
                    });
                }
            }

            context.Sales.AddRange(sales);
            context.SaveChanges();
        }

        private static void CreateSaleItems(MarketUzDbContext context)
        {
            if (context.SaleItems.Any()) return;

            var sales = context.Sales.ToList();
            var products = context.Products.ToList();
            List<SaleItem> saleItems = new List<SaleItem>();

            foreach (var sale in sales)
            {
                int saleItemsCount = new Random().Next(1, 20);

                for (int i = 0; i < saleItemsCount; i++)
                {
                    var randomProduct = _faker.PickRandom(products);
                    int attempts = 0;

                    while (saleItems.Any(si => si.Id == randomProduct.Id) && attempts < 100)
                    {
                        randomProduct = _faker.PickRandom(products);
                    }

                    var quantity = new Random().Next(1, 50);

                    saleItems.Add(new SaleItem()
                    {
                        ProductId = randomProduct.Id,
                        SaleId = sale.Id,
                        Quantity = quantity,
                        UnitPrice = randomProduct.Price,
                    });
                }
            }

            context.SaleItems.AddRange(saleItems);
            context.SaveChanges();
        }

        private static void CreateSuppliers(MarketUzDbContext context)
        {
            if (context.Suppliers.Any()) return;
            List<Supplier> suppliers = new List<Supplier>();

            for (int i = 0; i < 125; i++)
            {
                suppliers.Add(new Supplier()
                {
                    FirstName = _faker.Name.FirstName(),
                    LastName = _faker.Name.LastName(),
                    PhoneNumber = _faker.Phone.PhoneNumber("+998-(##) ###-##-##"),
                    Company = _faker.Company.CompanyName(),
                });
            }

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
        }

        private static void CreateSupplies(MarketUzDbContext context)
        {
            if (context.Supplies.Any()) return;

            var suppliers = context.Suppliers.ToList();
            List<Supply> supplies = new List<Supply>();

            foreach (var supplier in suppliers)
            {
                int suppliesCount = new Random().Next(5, 50);
                for (int i = 0; i < suppliesCount; i++)
                {
                    supplies.Add(new Supply()
                    {
                        SupplierId = supplier.Id,
                        SupplyDate = _faker.Date.Between(DateTime.Now.AddYears(-2), DateTime.Now),
                    });
                }
            }

            context.Supplies.AddRange(supplies);
            context.SaveChanges();
        }

        private static void CreateSupplyItems(MarketUzDbContext context)
        {
            if (context.SupplyItems.Any()) return;

            var supplies = context.Supplies.ToList();
            var products = context.Products.ToList();
            List<SupplyItem> supplyItems = new List<SupplyItem>();

            foreach (var supply in supplies)
            {
                int supplyItemsCount = new Random().Next(1, 20);

                for (int i = 0; i < supplyItemsCount; i++)
                {
                    var randomProduct = _faker.PickRandom(products);
                    int attempts = 0;

                    while (supplyItems.Any(si => si.Id == randomProduct.Id) && attempts < 100)
                    {
                        randomProduct = _faker.PickRandom(products);
                    }

                    var quantity = new Random().Next(1, 50);

                    supplyItems.Add(new SupplyItem()
                    {
                        ProductId = randomProduct.Id,
                        SupplyId = supply.Id,
                        Quantity = quantity,
                        UnitPrice = randomProduct.Price
                    });
                }
            }

            context.SupplyItems.AddRange(supplyItems);
            context.SaveChanges();
        }
    }
}
