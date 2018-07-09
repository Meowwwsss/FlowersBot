using FlowersBot.Resources.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlowerBot.Database
{
    public class SqliteDbContext : DbContext
    {
        public DbSet<Flower> Flowers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder Options)
        {
            string DbLocation = Assembly.GetEntryAssembly().Location.Replace(@"bin\Debug\netcoreapp2.0\FlowersBot.dll", @"Core\Data\");
            Options.UseSqlite($"Data Source={DbLocation}Database.sqlite");
        }
    }
}
