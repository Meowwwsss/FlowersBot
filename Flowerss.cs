using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.WebSocket;

using FlowersBot.Core.Data;
using FlowerBot.Database;
using System.Linq;

namespace FlowersBot.Currency
{
    public class Flowerss : ModuleBase<SocketCommandContext>
    {
        [Group("flower"), Alias("flowers"), Summary("Group to manage stuff to do with stones")]
        public class FlowerssGroup : ModuleBase<SocketCommandContext>
        {
            [Command(""), Alias("me", "my"), Summary("Shows all of your current flowers")]
            public async Task Me(IUser User = null)
            {
                if (User == null)
                    await Context.Channel.SendMessageAsync($"{Context.User}, you have {Data.GetFlowers(Context.User.Id)} Flowers!");
                else
                    await Context.Channel.SendMessageAsync($"{User.Username}, you have {Data.GetFlowers(User.Id)} Flowers!");
            }

            /*[Command("leaderboard"), Alias("Leaderboard"), Summary("Shows the users with the most flowers")]
            public async Task leaderboard(IUser User = null, int Amount =0)
            {
                User.Sort((s1, s2) => s1.Flowers.CompareTo(s2.Flowers));
                User.Reverse();
            }*/

            [Command("give"), Alias("gift"), Summary("Used to give flowers")]
            public async Task Give(IUser User = null, int Amount = 0)
            {
                //flowers give @Meow 1000
                //group   cmd  user  amount

                //Checks
                //Does the user have permission?
                //Does the user have enough stones?
                if (User == null)
                {
                    //The executer has not mentioned a user
                    await Context.Channel.SendMessageAsync(":x: You didn't mention a user to give the flowers to! Please use this syntax !flowers give **<@user>** <amount>");
                    return;
                }

                if(User.IsBot)
                {
                    await Context.Channel.SendMessageAsync(":x: Bots can't use this bot, so you can't give flowers to a bot");
                    return;
                }

                if(Amount == 0)
                {
                    await Context.Channel.SendMessageAsync($":x: You need to specify a valid amount of flowers that I need to give to {User.Username}");
                    return;
                }

                SocketGuildUser User1 = Context.User as SocketGuildUser;
                if(!User1.GuildPermissions.Administrator)
                {
                    await Context.Channel.SendMessageAsync($":x: You don't have administrator permissions in this discord server! Ask a moderator or the owner to execute this command!");
                }
                //Execution
                //Calculation (games)
                //Telling the user what he has gotten
                await Context.Channel.SendMessageAsync($":tada: Merry Christmas {User.Mention} you have received {Amount} flowers from {Context.User.Username}!");

                //Saving the data
                //Save data to the database
                //Save a file
                await Data.SaveFlowers(User.Id, Amount);
            }
            [Command("reset"), Summary("Resets the user's entire progress")]
            public async Task Reset(IUser User = null)
            {
                //checks
                if(User == null)
                {
                    await Context.Channel.SendMessageAsync($":x: You need to tell me which user you want to reset the flowers of! !flowers reset {Context.User.Mention}");
                    return;
                }

                if(User.IsBot)
                {
                    await Context.Channel.SendMessageAsync(":x: Bots can't use this bot, so you also can't reset the progress of bots! :robot:");
                }

                SocketGuildUser User1 = Context.User as SocketGuildUser;
                if (!User1.GuildPermissions.Administrator)
                {
                    await Context.Channel.SendMessageAsync($":x: You don't have administrator permissions in this discord server! Ask a moderator or the owner to execute this command!");
                    return;
                }

                //Execution
                await Context.Channel.SendMessageAsync($":skull: {User.Mention}, you have been reset by {Context.User.Username}! This means you lost all of your flowers!");

                //Saving the database
                using (var DbContext = new SqliteDbContext())
                {
                    DbContext.Flowers.RemoveRange(DbContext.Flowers.Where(x => x.UserId == User.Id));
                    await DbContext.SaveChangesAsync();
                }
            }
        }
    }
}
