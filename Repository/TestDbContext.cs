﻿using Bogus;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Services;



namespace ProvaPub.Repository
{

    public class TestDbContext : DbContext
    {
    
        private readonly IConfiguration Configuration;
        private readonly IProvedorPagamentoPix _provedorPagamentoPix;
        private readonly IProvedorPagamentoPaypal _provedorPagamentoPaypal;
        private readonly IProvedorPagamentoCredit _provedorPagamentoCredit;

        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
        }
        public TestDbContext(IConfiguration configuration, DbContextOptions<TestDbContext> options,
                             IProvedorPagamentoPix provedorPagamentoPix,
                             IProvedorPagamentoPaypal provedorPagamentoPaypal,
                             IProvedorPagamentoCredit provedorPagamentoCredit) : base(options)
        {
              Configuration = configuration;
            _provedorPagamentoPix = provedorPagamentoPix;
            _provedorPagamentoPaypal = provedorPagamentoPaypal;
            _provedorPagamentoCredit = provedorPagamentoCredit;        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().HasData(getCustomerSeed());
            modelBuilder.Entity<Product>().HasData(getProductSeed());
        }

        private Customer[] getCustomerSeed()
        {

            List<Customer> result = new();
            for (int i = 0; i < 20; i++)
            {
                result.Add(new Customer()
                {
                    Id = i + 1,
                    Name = new Faker().Person.FullName,
                });
            }

            return result.ToArray();
        }
        private Product[] getProductSeed()
        {
            List<Product> result = new();
            for (int i = 0; i < 20; i++)
            {
                result.Add(new Product()
                {
                    Id = i + 1,
                    Name = new Faker().Commerce.ProductName()
                });
            }
            return result.ToArray();
        }
        public OrderService CreateOrderService()
        {
            return new OrderService(_provedorPagamentoPix,
                                   _provedorPagamentoCredit,
                                   _provedorPagamentoPaypal);
        }


        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
