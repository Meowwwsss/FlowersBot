using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace FlowersBot.Core.Commands
{
    public class Luis : ModuleBase<SocketCommandContext>
    {
        [Command("Luis"), Alias("luis", "pedophile", "Pedophile"), Summary("Luis Command")]
        public async Task luisCommand()
        {
            await Context.Channel.SendMessageAsync("Luis is a pedophile be careful egirls of all ages!");
        }
        [Command("embed"), Summary("Embed test command")]
        public async Task embed([Remainder]string Input = "None")
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithAuthor("Test embed", Context.User.GetAvatarUrl());
            Embed.WithColor(40, 200, 150);
            Embed.WithFooter("The footer of the embed", Context.Guild.Owner.GetAvatarUrl());
            Embed.WithDescription("This is a **dummy** description");

            Embed.AddInlineField("User Input", Input);

            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("MrAmumu"), Alias("mramumu", "popping pills", "poppingpills"), Summary("Mramumu Command")]
        public async Task amumuCommand()
        {
            await Context.Channel.SendMessageAsync("Mr.Amumu is actually autistic and needs to leave the server!");
        }

        [Command("MrAmumu"), Alias("boros", "Boros", "Boros Legion", "boros legion"), Summary("Boros Command")]
        public async Task borosCommand()
        {
            await Context.Channel.SendMessageAsync("Boros is the best admin in the world, but he sucks at league!");
        }

        [Command("Kick")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        public async Task KickUser(IGuildUser user, string reason = "No reason provided.")
        {
            await user.KickAsync(reason);
        }

        [Command("Ban")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task BanUser(IGuildUser user, string reason = "No reason provided.")
        {
            await user.Guild.AddBanAsync(user, 5, reason);
        }
    }
}
