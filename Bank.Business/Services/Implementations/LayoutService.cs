using Bank.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.Services.Implementations
{
    public class LayoutService
    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, string>> GetSettingsAsync()
        {
            var settings = await _context.Settings
                .Where(x => !x.IsDeleted)
                .ToDictionaryAsync(k => k.Key, v => v.Value);

            return settings;
        }
    }
}