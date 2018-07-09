using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using FlowerBot.Database;
using FlowersBot.Resources.Database;
using System.Linq;
using System.Collections;
using FlowersBot.Core.Data;
namespace FlowersBot.Core.Data
{
    public static class Data
    {
        public static int GetFlowers(ulong UserId)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Flowers.Where(x => x.UserId == UserId).Count() < 1)
                    return 0;
                return DbContext.Flowers.Where(x => x.UserId == UserId).Select(x => x.Amount).FirstOrDefault();
            }
        }

        public static async Task SaveFlowers(ulong UserId, int Amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Flowers.Where(x => x.UserId == UserId).Count() < 1)
                {
                    //The user doesn't have a row yet, create one for him
                    DbContext.Flowers.Add(new Flower
                    {
                        UserId = UserId,
                        Amount = Amount
                    });
                }
                else
                {
                    Flower Current = DbContext.Flowers.Where(x => x.UserId == UserId).FirstOrDefault();
                    Current.Amount += Amount;
                    DbContext.Flowers.Update(Current);
                }
                await DbContext.SaveChangesAsync();
            }
        }
    }
    }
