﻿using Microsoft.EntityFrameworkCore;
using WebApp.Decorator.Models;

namespace WebApp.Decorator.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppIdentityDbContext _context;

        public ProductRepository(AppIdentityDbContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetAll(string userId)
        {
            return await _context.Products.Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public Task<Product> GetById(int id)
        {
            return _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Remove(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> Save(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task Update(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
