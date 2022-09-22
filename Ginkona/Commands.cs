using DSharpPlus.SlashCommands;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.EventArgs;
using static Ginkona.Enums;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace Ginkona
{
    public class Commands : ApplicationCommandModule
    {

        public static Dictionary<DiscordChannel, Booking> SearchChannel = new Dictionary<DiscordChannel, Booking>();
        public static Dictionary<DiscordChannel, DateTime> MessageDeletePool = new Dictionary<DiscordChannel, DateTime>();
        public static Dictionary<DiscordChannel, DateTime> VoiceDeletePool = new Dictionary<DiscordChannel, DateTime>();
        public static bool IsInitialized = false;
     


        public class SignInObject
        {

            public ulong UniqueMessageId { get; }
            public DiscordMember Member { get; }
            public RoleEnum Role { get; }
            public ClassEnum Class { get; }
            public bool RoleAccepted { set; get; }
            public bool ClassAccepted { set; get; }
            public SignInObject(ulong id, DiscordMember member, RoleEnum role, ClassEnum @class)
            {
                UniqueMessageId = id;
                Member = member;
                Role = role;
                Class = @class;
                RoleAccepted = false;
                ClassAccepted = false;
            }

        }

        public static void Init(DiscordClient discordClient)
        {
            if (!IsInitialized)
            {
                Task.Run(DeleteMessageLoop);
                discordClient.MessageReactionAdded += (e, s) => OnMessageReactionAdded(e, s);
                discordClient.MessageReactionRemoved += (e, s) => OnMessageReactionRemoved(e, s);
                IsInitialized = true;
                discordClient.Logger.Log(LogLevel.Information ,new EventId(5, "Init"), $"Succesfully initialized commands");

            }

        }

        public class ServerCommand : SlashCheckBaseAttribute
        {
            public override async Task<bool> ExecuteChecksAsync(InteractionContext ctx)
            {
                if (ctx.Channel.IsPrivate)
                {
                    await ctx.CreateResponseAsync($"You cannot execute this command in the DMs", true);
                    return false;
                }    
                else
                    return true;
            }
        }

        [SlashCommandGroup("Search", "Search Commands")]
        public class GroupContainer : ApplicationCommandModule
        {

            [SlashCommand("booking", "Creates a booking as Advertiser")]
            [ServerCommand]
            public async Task SearchCommand(InteractionContext ctx,
            [Option("key", "Select Key")] KeyEnum key, // Titel
            [Option("keyLevel", "Enter Keylevel (Numeric)")] double keyLevel,
            [Option("pot", "Enter Pot (Numeric)")] double pot,
            [Option("cut", "Enter Cut (Numeric)")] double cut, //25000
            [Option("server", "Enter Server")] string server, //Rashgarroth
            [Option("whisper", "Enter your whisper")] string whisper,
            [Option("Armorstack", "Select the armorstack")] ArmorstackEnum armorstack,
            [Option("tank", "Select needed tanks")] TankEnum tank,
            [Option("dd", "Select needed damage dealers")] DamageDealerEnum dd,
            [Option("healer", "Select needed healers")] HealerEnum healer,
            [Option("note", "Add a custom note (Optional)")] string note = "")
            {
                Init(ctx.Client);
                DiscordEmbedBuilder Embed = new DiscordEmbedBuilder
                {
                    Title = key.GetName()
                };
                Embed.AddField("Server:", server);
                Embed.AddField("Cut:", String.Format("{0:n0}", cut), true);
                Embed.AddField("Pot:", String.Format("{0:n0}", pot), true);
                Embed.WithAuthor("Ginkona", "", Config.Url_Author_Icon);
                var emojis = ctx.Guild.GetEmojisAsync().Result.ToList();
                Embed.AddField("Required roles:",
                    ((tank.GetName().Split().First() == "0x") && (dd.GetName().Split().First() == "0x") && (healer.GetName().Split().First() == "0x")) ? "None" :
                    (((tank.GetName().Split().First() != "0x") ? (tank.GetName().Substring(0, 2) + $" {emojis.Find(x => x.Name == Config.Emoji_Role_Tank)}") : "")
                    + ((dd.GetName().Split().First() != "0x") ? "\n" : "") + ((dd.GetName().Split().First() != "0x") ? (dd.GetName().Substring(0, 2) + $" {emojis.Find(x => x.Name == Config.Emoji_Role_Damagedealer)}") : "")
                    + "\n" + ((healer.GetName().Split().First() != "0x") ? (healer.GetName().Substring(0, 2) + $" {emojis.Find(x => x.Name == Config.Emoji_Role_Healer)}") : "")));
                if (armorstack != ArmorstackEnum.option5)
                    Embed.AddField("Armorstack:", armorstack.GetName());
                if (note != "")
                    Embed.AddField("Note:", note);





                switch (armorstack)
                {
                    case ArmorstackEnum.option1:
                        Embed.WithThumbnail(Config.Url_Armorstack_Kette);
                        break;
                    case ArmorstackEnum.option2:
                        Embed.WithThumbnail(Config.Url_Armorstack_Platte);
                        break;
                    case ArmorstackEnum.option3:
                        Embed.WithThumbnail(Config.Url_Armorstack_Leder);
                        break;
                    case ArmorstackEnum.option4:
                        Embed.WithThumbnail(Config.Url_Armorstack_Stoff);
                        break;
                }

                Embed.WithFooter($"Advertiser: {ctx.Member.DisplayName}", ctx.Member.AvatarUrl);

                switch (key)
                {
                    case KeyEnum.option1:
                        Embed.WithImageUrl(Config.Url_Key_Junkyard);
                        Embed.WithColor(Config.Color_Key_Junkyard);
                        break;
                    case KeyEnum.option2:
                        Embed.WithImageUrl(Config.Url_Key_Streets);
                        Embed.WithColor(Config.Color_Key_Streets);
                        break;
                    case KeyEnum.option3:
                        Embed.WithImageUrl(Config.Url_Key_Upper);
                        Embed.WithColor(Config.Color_Key_Upper);
                        break;
                    case KeyEnum.option4:
                        Embed.WithImageUrl(Config.Url_Key_Gambit);
                        Embed.WithColor(Config.Color_Key_Gambit);
                        break;
                    case KeyEnum.option5:
                        Embed.WithImageUrl(Config.Url_Key_GD);
                        Embed.WithColor(Config.Color_Key_GD);
                        break;
                    case KeyEnum.option6:
                        Embed.WithImageUrl(Config.Url_Key_Lower);
                        Embed.WithColor(Config.Color_Key_Lower);
                        break;
                    case KeyEnum.option7:
                        Embed.WithImageUrl(Config.Url_Key_ID);
                        Embed.WithColor(Config.Color_Key_ID);
                        break;
                    case KeyEnum.option8:
                        Embed.WithImageUrl(Config.Url_Key_Workshop);
                        Embed.WithColor(Config.Color_Key_Workshop);
                        break;
                    case KeyEnum.option9:
                        Embed.WithImageUrl(Config.Url_Key_Random);
                        Embed.WithColor(Config.Color_Key_Random);
                        break;

                }

                var channel = await ctx.Guild.CreateChannelAsync($"{key.GetName()}-{keyLevel}-{ctx.Member.DisplayName}", ChannelType.Text, reason: $"By {ctx.Member.DisplayName}", parent: ctx.Guild.GetChannel(Config.Id_Search_Category));
                var msg = await channel.SendMessageAsync($"{ctx.Guild.GetRole(Config.Role_Booster_General).Mention}", embed: Embed.Build());
                SearchChannel.Add(channel, new Booking(ctx.Member, key, keyLevel, pot, cut, server, whisper, armorstack, tank, dd, healer, msg, note));
                


                if (key != KeyEnum.option9)
                {
                    await msg.CreateReactionAsync(DiscordEmoji.FromName(ctx.Client, Config.Emoji_General_Key, true));

                    // await channel.AddOverwriteAsync(ctx.Guild.GetRole(696816795104837632), deny: Permissions.AddReactions); // Member
                    await channel.AddOverwriteAsync(ctx.Guild.EveryoneRole, deny: Permissions.AddReactions | Permissions.SendMessages | Permissions.AccessChannels);
                    await channel.AddOverwriteAsync(ctx.Member, allow: Permissions.SendMessages | Permissions.AccessChannels); //Adv
                }

                await ctx.CreateResponseAsync($"Booking successful created!", true);
            }

            [SlashCommand("slave", "Creates a slave booking as Advertiser")]
            [ServerCommand]
            public async Task SlaveCommand(InteractionContext ctx,
            [Option("server", "Select your withdraw server")] string server,
            [Option("note", "Add some note")] string note) 
            {
                Init(ctx.Client);
                DiscordEmbedBuilder embed = new DiscordEmbedBuilder
                {
                    Title = "Search slave!"
                };
                embed.AddField("Server:", server);
                embed.AddField("Note:", note);
                embed.WithFooter($"Advertiser: {ctx.Member.DisplayName}", ctx.Member.AvatarUrl);
                embed.WithColor(Config.Color_Slavery);
                

                var channel = await ctx.Guild.CreateChannelAsync($"Slave-{ctx.Member.DisplayName}", ChannelType.Text, reason: $"By {ctx.Member.DisplayName}", parent: ctx.Guild.GetChannel(Config.Id_Category_Slavery));
                await channel.SendMessageAsync($"{ctx.Guild.GetRole(Config.Role_Slave).Mention}", embed: embed);

                await channel.AddOverwriteAsync(ctx.Guild.EveryoneRole, deny: Permissions.AccessChannels);
                await channel.AddOverwriteAsync(ctx.Guild.GetRole(Config.Role_Slave), allow: Permissions.AccessChannels | Permissions.SendMessages);
                await channel.AddOverwriteAsync(ctx.Guild.GetRole(Config.Role_RaidBooster), allow: Permissions.AccessChannels | Permissions.SendMessages);
                await channel.AddOverwriteAsync(ctx.Guild.GetRole(Config.Role_Booster_General), allow: Permissions.AccessChannels | Permissions.SendMessages);

                await ctx.CreateResponseAsync($"Slavery booking successful created!", true);
            }
        }
        



        [SlashCommand("Signin", "Sign in as Booster")]
        [ServerCommand]
        public async Task SignInCommand(InteractionContext ctx,
         [Option("role", "Select your Role")] RoleEnum role,
         [Option("class", "Select your Class")] ClassEnum @class,
         [Option("twinkRio", "Enter your Twink Rio (Optional)")] string twinkRio = "",
         [Option("note", "Some note (Optional)")] string note = "")
        {

            foreach (KeyValuePair<DiscordChannel, Booking> chn in SearchChannel)
            {
                if (chn.Key.Id == ctx.Channel.Id)
                {
                    //if((chn.Value.SignInObjects.Find(x => x.Member == ctx.Member)) != null)
                    //{
                    //    await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("You only can once sign in!"));
                    //    return;
                    //}
                    if (note.Length > 160)
                    {
                        await ctx.CreateResponseAsync($"The note can only be 160 characters long! Your note is {note.Length} characters long!", true);
                        return;
                    }

                    DiscordEmbedBuilder Embed = new DiscordEmbedBuilder
                    {
                        Title = "Application"
                    };
                    Embed.AddField("Applicant", ctx.Member.Mention, true);
                    if (ctx.Member.Roles.Count() == 0)
                    {
                        await ctx.CreateResponseAsync($"Please obtain a Rio Role in {ctx.Guild.GetChannel(Config.Id_Text_Botspam).Mention}!", true);
                        return;
                    }
                    foreach (DiscordRole r in ctx.Member.Roles)
                    {
                        //if (Config.Role_Booster_2700 == r.Id || Config.Role_Booster_2600 == r.Id || Config.Role_Booster_2500 == r.Id || Config.Role_Booster_2400 == r.Id || Config.Role_Booster_2300 == r.Id || Config.Role_Booster_To2300 == r.Id)
                        //{
                        //    await ctx.CreateResponseAsync($"You need at least {ctx.Guild.GetRole(Config.Role_Booster_2000).Mention} Rio to boost!", true);
                        //    return;
                        //}
                        //else
                        if (Config.Role_Booster.Find(x => x == r.Id) != 0)
                        {
                            Embed.AddField("Rio", ctx.Guild.GetRole(Config.Role_Booster.Find(x => x == r.Id)).Mention, true);
                            break;
                        }
                        else if (r == ctx.Member.Roles.Last())
                        {
                            await ctx.CreateResponseAsync($"Please obtain a Rio Role in {ctx.Guild.GetChannel(Config.Id_Text_Botspam).Mention}!", true);
                            return;
                        }
                    }

                    if (twinkRio != "")
                        Embed.AddField("Twink Rio", twinkRio, true);
                    if (note != "")
                        Embed.AddField("Note", note);

                    switch (@class)
                    {
                        case ClassEnum.option1:
                            Embed.WithColor(new DiscordColor(163, 48, 201));
                            break;
                        case ClassEnum.option2:
                            Embed.WithColor(new DiscordColor(196, 30, 58));
                            break;
                        case ClassEnum.option3:
                            Embed.WithColor(new DiscordColor(255, 124, 10));
                            break;
                        case ClassEnum.option4:
                            Embed.WithColor(new DiscordColor(63, 199, 235));
                            break;
                        case ClassEnum.option5:
                            Embed.WithColor(new DiscordColor(0, 255, 152));
                            break;
                        case ClassEnum.option6:
                            Embed.WithColor(new DiscordColor(244, 140, 186));
                            break;
                        case ClassEnum.option7:
                            Embed.WithColor(new DiscordColor(255, 255, 255));
                            break;
                        case ClassEnum.option8:
                            Embed.WithColor(new DiscordColor(255, 244, 104));
                            break;
                        case ClassEnum.option9:
                            Embed.WithColor(new DiscordColor(0, 112, 221));
                            break;
                        case ClassEnum.option10:
                            Embed.WithColor(new DiscordColor(135, 136, 238));
                            break;
                        case ClassEnum.option11:
                            Embed.WithColor(new DiscordColor(198, 155, 109));
                            break;

                    }



                    var msg = await ctx.Channel.SendMessageAsync(embed: Embed.Build());
                    chn.Value.SignInObjects.Add(new SignInObject(msg.Id, ctx.Member, role, @class));

                    var emojis = ctx.Guild.GetEmojisAsync().Result.ToList();



                    switch (role)
                    {
                        case RoleEnum.option1:
                            await msg.CreateReactionAsync(emojis.Find(x => x.Name == Config.Emoji_Role_Tank));
                            break;
                        case RoleEnum.option2:
                            await msg.CreateReactionAsync(emojis.Find(x => x.Name == Config.Emoji_Role_Damagedealer));
                            break;
                        case RoleEnum.option3:
                            await msg.CreateReactionAsync(emojis.Find(x => x.Name == Config.Emoji_Role_Healer));
                            break;
                    }

                    switch (@class)
                    {
                        case ClassEnum.option1:
                            await msg.CreateReactionAsync(emojis.Find(x => x.Name == Config.Emoji_Class_Demonhunter));
                            break;
                        case ClassEnum.option2:
                            await msg.CreateReactionAsync(emojis.Find(x => x.Name == Config.Emoji_Class_Deathknight));
                            break;
                        case ClassEnum.option3:
                            await msg.CreateReactionAsync(emojis.Find(x => x.Name == Config.Emoji_Class_Druid));
                            break;
                        case ClassEnum.option4:
                            await msg.CreateReactionAsync(emojis.Find(x => x.Name == Config.Emoji_Class_Mage));
                            break;
                        case ClassEnum.option5:
                            await msg.CreateReactionAsync(emojis.Find(x => x.Name == Config.Emoji_Class_Monk));
                            break;
                        case ClassEnum.option6:
                            await msg.CreateReactionAsync(emojis.Find(x => x.Name == Config.Emoji_Class_Paladin));
                            break;
                        case ClassEnum.option7:
                            await msg.CreateReactionAsync(emojis.Find(x => x.Name == Config.Emoji_Class_Priest));
                            break;
                        case ClassEnum.option8:
                            await msg.CreateReactionAsync(emojis.Find(x => x.Name == Config.Emoji_Class_Rogue));
                            break;
                        case ClassEnum.option9:
                            await msg.CreateReactionAsync(emojis.Find(x => x.Name == Config.Emoji_Class_Schaman));
                            break;
                        case ClassEnum.option10:
                            await msg.CreateReactionAsync(emojis.Find(x => x.Name == Config.Emoji_Class_Warlock));
                            break;
                        case ClassEnum.option11:
                            await msg.CreateReactionAsync(emojis.Find(x => x.Name == Config.Emoji_Class_Warrior));
                            break;
                        case ClassEnum.option12:
                            await msg.CreateReactionAsync(emojis.Find(x => x.Name == Config.Emoji_Class_Hunter));
                            break;

                    }

                    await ctx.CreateResponseAsync("Signed in!", true); return;
                }
            }
            await ctx.CreateResponseAsync("You must enter this command in a booking channel", true); return;
        }

        [SlashCommand("Signout", "You can sign out with that command")]
        [ServerCommand]
        public async Task SignOutCommand(InteractionContext ctx)
        {

            foreach (KeyValuePair<DiscordChannel, Booking> chn in SearchChannel)
            {
                if (chn.Key.Id == ctx.Channel.Id)
                {
                    var member = chn.Value.SignInObjects.FindAll(x => x.Member == ctx.Member);
                    if (member == null)
                    {
                        await ctx.CreateResponseAsync("You are not signed in!", true);
                    }
                    else
                    {
                        foreach(SignInObject m in member)
                        {
                            await chn.Key.DeleteMessageAsync(await chn.Key.GetMessageAsync(m.UniqueMessageId));
                            chn.Value.SignInObjects.Remove(m);
                        }
                        await ctx.CreateResponseAsync("Succesfully signed out!", true);
                    }
                }
            }
        }

        [SlashCommand("Group", "Create Group")]
        [ServerCommand]
        public async Task GroupCommand(InteractionContext ctx)
        {

            foreach (KeyValuePair<DiscordChannel, Booking> chn in SearchChannel)
            {
                if (chn.Key.Id == ctx.Channel.Id)
                {
                    if (ctx.Member == chn.Value.Advertiser)
                    {
                        var tanks = chn.Value.SignInObjects.FindAll(x => x.Role == RoleEnum.option1);
                        var dds = chn.Value.SignInObjects.FindAll(x => x.Role == RoleEnum.option2);
                        var healer = chn.Value.SignInObjects.FindAll(x => x.Role == RoleEnum.option3);
                        var emojis = ctx.Guild.GetEmojisAsync().Result.ToList();
                        if (chn.Value.SignInObjects.FindAll(x => x.ClassAccepted == true).Count == 4)
                        {
                            var tankCount = chn.Value.SignInObjects.FindAll(x => x.Role == RoleEnum.option1 && x.ClassAccepted == true).Count;
                            var ddCount = chn.Value.SignInObjects.FindAll(x => x.Role == RoleEnum.option2 && x.ClassAccepted == true).Count;
                            var healerCount = chn.Value.SignInObjects.FindAll(x => x.Role == RoleEnum.option3 && x.ClassAccepted == true).Count;
                            if (tankCount != 1 || ddCount != 2 || healerCount != 1)
                            {
                                await ctx.CreateResponseAsync($"You need 1x Tank, 2x Damagedealer, 1x Healer.\nYou have currently selected {tankCount}x Tank, {ddCount}x Damagedealer, {healerCount}x Healer", true);
                                return;
                            }

                            var vc = await ctx.Guild.CreateChannelAsync($"{chn.Value.Key.GetName().ToLower()}-{chn.Value.KeyLevel}-{chn.Value.Advertiser.DisplayName}", ChannelType.Voice, parent: ctx.Guild.GetChannel(Config.Id_Voice_Category));
                            chn.Value.Voice = vc;
                            
                            DiscordEmbedBuilder Embed = new DiscordEmbedBuilder
                            {
                                Title = "Gruppe " + $"{ chn.Value.Key.GetName() }-{ chn.Value.KeyLevel }-{ chn.Value.Advertiser.DisplayName }",
                                Color = new DiscordColor(0, 142, 68)
                            };
                            Embed.AddField("Whisper", "/w " + chn.Value.Whisper);
                            Embed.AddField("Group", $"{emojis.Find(x => x.Name == Config.Emoji_Role_Tank)} {chn.Value.SignInObjects.Find(x => x.Role == RoleEnum.option1 && x.ClassAccepted == true).Member.Mention} {((chn.Value.KeyHolder == chn.Value.SignInObjects.Find(x => x.Role == RoleEnum.option1).Member) ? $"{DiscordEmoji.FromName(ctx.Client, Config.Emoji_General_Key)}" : "")}\n" +
                                $"{emojis.Find(x => x.Name == Config.Emoji_Role_Damagedealer)} {chn.Value.SignInObjects.FindAll(x => x.Role == RoleEnum.option2 && x.ClassAccepted == true)[0].Member.Mention} {((chn.Value.KeyHolder == chn.Value.SignInObjects.FindAll(x => x.Role == RoleEnum.option2)[0].Member) ? $"{DiscordEmoji.FromName(ctx.Client, Config.Emoji_General_Key)}" : "")}\n" +
                                $"{emojis.Find(x => x.Name == Config.Emoji_Role_Damagedealer)} {chn.Value.SignInObjects.FindAll(x => x.Role == RoleEnum.option2 && x.ClassAccepted == true)[1].Member.Mention} {((chn.Value.KeyHolder == chn.Value.SignInObjects.FindAll(x => x.Role == RoleEnum.option2)[1].Member) ? $"{DiscordEmoji.FromName(ctx.Client, Config.Emoji_General_Key)}" : "")}\n" +
                                $"{emojis.Find(x => x.Name == Config.Emoji_Role_Healer)} {chn.Value.SignInObjects.Find(x => x.Role == RoleEnum.option3 && x.ClassAccepted == true).Member.Mention} {((chn.Value.KeyHolder == chn.Value.SignInObjects.Find(x => x.Role == RoleEnum.option3).Member) ? $"{DiscordEmoji.FromName(ctx.Client, Config.Emoji_General_Key)}" : "")}\n");

                            await ctx.CreateResponseAsync($"Voice: {vc.Mention}", embed: Embed);

                            //DM
                            DiscordEmbedBuilder DmEmbed = new DiscordEmbedBuilder
                            {
                                Title = chn.Value.Key.GetName(),
                                Color = new DiscordColor(0, 142, 68)
                            };
                            DmEmbed.AddField("Server:", chn.Value.Server);
                            DmEmbed.AddField("Cut:", String.Format("{0:n0}", chn.Value.Cut), true);
                            DmEmbed.AddField("Pot:", String.Format("{0:n0}", chn.Value.Pot), true);
                            DmEmbed.WithAuthor("Ginkona", "", Config.Url_Author_Icon);

                            DmEmbed.AddField("Required roles:",
                                ((chn.Value.Tank.GetName().Split().First() == "0x") && (chn.Value.Dd.GetName().Split().First() == "0x") && (chn.Value.Healer.GetName().Split().First() == "0x")) ? "None" :
                                (((chn.Value.Tank.GetName().Split().First() != "0x") ? (chn.Value.Tank.GetName().Substring(0, 2) + $" {emojis.Find(x => x.Name == Config.Emoji_Role_Tank)}") : "")
                                + ((chn.Value.Dd.GetName().Split().First() != "0x") ? "\n" : "") + ((chn.Value.Dd.GetName().Split().First() != "0x") ? (chn.Value.Dd.GetName().Substring(0, 2) + $" {emojis.Find(x => x.Name == Config.Emoji_Role_Damagedealer)}") : "")
                                + "\n" + ((chn.Value.Healer.GetName().Split().First() != "0x") ? (chn.Value.Healer.GetName().Substring(0, 2) + $" {emojis.Find(x => x.Name == Config.Emoji_Role_Healer)}") : "")));
                            if (chn.Value.Armorstack != ArmorstackEnum.option5)
                                DmEmbed.AddField("Armorstack:", chn.Value.Armorstack.GetName());
                            DmEmbed.AddField("Whisper", "/w " + chn.Value.Whisper);
                            DmEmbed.AddField("Group", $"{emojis.Find(x => x.Name == Config.Emoji_Role_Tank)} {chn.Value.SignInObjects.Find(x => x.Role == RoleEnum.option1 && x.ClassAccepted == true).Member.DisplayName + "#" + chn.Value.SignInObjects.Find(x => x.Role == RoleEnum.option1).Member.Discriminator} {((chn.Value.KeyHolder == chn.Value.SignInObjects.Find(x => x.Role == RoleEnum.option1).Member) ? $"{DiscordEmoji.FromName(ctx.Client, Config.Emoji_General_Key)}" : "")}\n" +
                                $"{emojis.Find(x => x.Name == Config.Emoji_Role_Damagedealer)} {chn.Value.SignInObjects.FindAll(x => x.Role == RoleEnum.option2 && x.ClassAccepted == true)[0].Member.DisplayName + "#" + chn.Value.SignInObjects.FindAll(x => x.Role == RoleEnum.option2)[0].Member.Discriminator} {((chn.Value.KeyHolder == chn.Value.SignInObjects.FindAll(x => x.Role == RoleEnum.option2)[0].Member) ? $"{DiscordEmoji.FromName(ctx.Client, Config.Emoji_General_Key)}" : "")}\n" +
                                $"{emojis.Find(x => x.Name == Config.Emoji_Role_Damagedealer)} {chn.Value.SignInObjects.FindAll(x => x.Role == RoleEnum.option2 && x.ClassAccepted == true)[1].Member.DisplayName + "#" + chn.Value.SignInObjects.FindAll(x => x.Role == RoleEnum.option2)[1].Member.Discriminator} {((chn.Value.KeyHolder == chn.Value.SignInObjects.FindAll(x => x.Role == RoleEnum.option2)[1].Member) ? $"{DiscordEmoji.FromName(ctx.Client, Config.Emoji_General_Key)}" : "")}\n" +
                                $"{emojis.Find(x => x.Name == Config.Emoji_Role_Healer)} {chn.Value.SignInObjects.Find(x => x.Role == RoleEnum.option3 && x.ClassAccepted == true).Member.DisplayName + "#" + chn.Value.SignInObjects.Find(x => x.Role == RoleEnum.option3).Member.Discriminator} {((chn.Value.KeyHolder == chn.Value.SignInObjects.Find(x => x.Role == RoleEnum.option3).Member) ? $"{DiscordEmoji.FromName(ctx.Client, Config.Emoji_General_Key)}" : "")}\n");
                            if (chn.Value.Note != "")
                                DmEmbed.AddField("Note:", chn.Value.Note);



                            switch (chn.Value.Armorstack)
                            {
                                case ArmorstackEnum.option1:
                                    DmEmbed.WithThumbnail(Config.Url_Armorstack_Kette);
                                    break;
                                case ArmorstackEnum.option2:
                                    DmEmbed.WithThumbnail(Config.Url_Armorstack_Platte);
                                    break;
                                case ArmorstackEnum.option3:
                                    DmEmbed.WithThumbnail(Config.Url_Armorstack_Leder);
                                    break;
                                case ArmorstackEnum.option4:
                                    DmEmbed.WithThumbnail(Config.Url_Armorstack_Stoff);
                                    break;
                            }
                            switch (chn.Value.Key)
                            {
                                case KeyEnum.option1:
                                    Embed.WithImageUrl(Config.Url_Key_Junkyard);
                                    Embed.WithColor(Config.Color_Key_Junkyard);
                                    break;
                                case KeyEnum.option2:
                                    Embed.WithImageUrl(Config.Url_Key_Streets);
                                    Embed.WithColor(Config.Color_Key_Streets);
                                    break;
                                case KeyEnum.option3:
                                    Embed.WithImageUrl(Config.Url_Key_Upper);
                                    Embed.WithColor(Config.Color_Key_Upper);
                                    break;
                                case KeyEnum.option4:
                                    Embed.WithImageUrl(Config.Url_Key_Gambit);
                                    Embed.WithColor(Config.Color_Key_Gambit);
                                    break;
                                case KeyEnum.option5:
                                    Embed.WithImageUrl(Config.Url_Key_GD);
                                    Embed.WithColor(Config.Color_Key_GD);
                                    break;
                                case KeyEnum.option6:
                                    Embed.WithImageUrl(Config.Url_Key_Lower);
                                    Embed.WithColor(Config.Color_Key_Lower);
                                    break;
                                case KeyEnum.option7:
                                    Embed.WithImageUrl(Config.Url_Key_ID);
                                    Embed.WithColor(Config.Color_Key_ID);
                                    break;
                                case KeyEnum.option8:
                                    Embed.WithImageUrl(Config.Url_Key_Workshop);
                                    Embed.WithColor(Config.Color_Key_Workshop);
                                    break;
                                case KeyEnum.option9:
                                    Embed.WithImageUrl(Config.Url_Key_Random);
                                    Embed.WithColor(Config.Color_Key_Random);
                                    break;

                            }
                            DmEmbed.WithFooter($"Advertiser: {chn.Value.Advertiser.DisplayName}", chn.Value.Advertiser.AvatarUrl);


                            List<DiscordMember> members = new List<DiscordMember>() { chn.Value.SignInObjects[0].Member, chn.Value.SignInObjects[1].Member, chn.Value.SignInObjects[2].Member, chn.Value.SignInObjects[3].Member };
                            List<DiscordMember> membersDistinct = members.Distinct().ToList();
                            foreach (DiscordMember m in membersDistinct)
                            {
                                await m.SendMessageAsync(embed: DmEmbed.Build());
                            }
                            return;
                        }
                        else
                        {
                            await ctx.CreateResponseAsync($"Grouping failed!\n" +
                                $"You have selected {chn.Value.SignInObjects.FindAll(x => x.ClassAccepted == true).Count} player but you need 4 player!", true); return;

                        }
                    }
                    else
                    {
                        await ctx.CreateResponseAsync("Only the advertiser can use this command!", true); return;
                    }
                }
            }
            await ctx.CreateResponseAsync("You must enter this command in a booking channel", true); return;

        }

        [SlashCommand("Go", "Delete Booking Channel")]
        [ServerCommand]
        public async Task GoCommand(InteractionContext ctx)
        {
            foreach (KeyValuePair<DiscordChannel, Booking> chn in SearchChannel)
            {
                if (chn.Key.Id == ctx.Channel.Id)
                {
                    if (ctx.Member == chn.Value.Advertiser || ctx.Member.Roles.Contains(ctx.Guild.GetRole(Config.Role_Management)))
                    {
                        DiscordEmbedBuilder DeleteEmbed = new DiscordEmbedBuilder
                        {
                            Title = "The Booking channel will be deleted in 5 minutes!",
                            Color = new DiscordColor(101, 222, 241)
                        };
                        

                       
                        await ctx.Channel.SendMessageAsync(embed: DeleteEmbed);

                        await chn.Key.AddOverwriteAsync(ctx.Guild.GetRole(Config.Id_Role_Member), deny: Permissions.AccessChannels | Permissions.SendMessages );
                        await chn.Key.AddOverwriteAsync(ctx.Guild.EveryoneRole, deny: Permissions.AccessChannels | Permissions.SendMessages);
                        await chn.Key.AddOverwriteAsync(ctx.Member, allow: Permissions.AccessChannels | Permissions.SendMessages);
                        var emojis = ctx.Guild.GetEmojisAsync().Result.ToList();

                        if (chn.Value.SignInObjects.FindAll(x => x.ClassAccepted == true).Count == 4)
                        {

                        
                            //Log Embed
                            DiscordEmbedBuilder DmEmbed = new DiscordEmbedBuilder
                            {
                                Title = chn.Value.Key.GetName(),
                                Color = new DiscordColor(101, 222, 241)
                            };
                            DmEmbed.AddField("Server:", chn.Value.Server);
                            DmEmbed.AddField("Cut:", String.Format("{0:n0}", chn.Value.Cut), true);
                            DmEmbed.AddField("Pot:", String.Format("{0:n0}", chn.Value.Pot), true);
                            DmEmbed.WithAuthor("Ginkona", "", Config.Url_Author_Icon);

                            DmEmbed.AddField("Required roles:",
                                ((chn.Value.Tank.GetName().Split().First() == "0x") && (chn.Value.Dd.GetName().Split().First() == "0x") && (chn.Value.Healer.GetName().Split().First() == "0x")) ? "None" :
                                (((chn.Value.Tank.GetName().Split().First() != "0x") ? (chn.Value.Tank.GetName().Substring(0, 2) + $" {emojis.Find(x => x.Name == Config.Emoji_Role_Tank)}") : "")
                                + ((chn.Value.Dd.GetName().Split().First() != "0x") ? "\n" : "") + ((chn.Value.Dd.GetName().Split().First() != "0x") ? (chn.Value.Dd.GetName().Substring(0, 2) + $" {emojis.Find(x => x.Name == Config.Emoji_Role_Damagedealer)}") : "")
                                + "\n" + ((chn.Value.Healer.GetName().Split().First() != "0x") ? (chn.Value.Healer.GetName().Substring(0, 2) + $" {emojis.Find(x => x.Name == Config.Emoji_Role_Healer)}") : "")));
                            if (chn.Value.Armorstack != ArmorstackEnum.option5)
                                DmEmbed.AddField("Armorstack:", chn.Value.Armorstack.GetName());
                            DmEmbed.AddField("Whisper", "/w " + chn.Value.Whisper);
                            DmEmbed.AddField("Group", $"{emojis.Find(x => x.Name == Config.Emoji_Role_Tank)} {chn.Value.SignInObjects.Find(x => x.Role == RoleEnum.option1).Member.Mention} {((chn.Value.KeyHolder == chn.Value.SignInObjects.Find(x => x.Role == RoleEnum.option1).Member) ? $"{DiscordEmoji.FromName(ctx.Client, Config.Emoji_General_Key)}" : "")}\n" +
                             $"{emojis.Find(x => x.Name == Config.Emoji_Role_Damagedealer)} {chn.Value.SignInObjects.FindAll(x => x.Role == RoleEnum.option2)[0].Member.Mention} {((chn.Value.KeyHolder == chn.Value.SignInObjects.FindAll(x => x.Role == RoleEnum.option2)[0].Member) ? $"{DiscordEmoji.FromName(ctx.Client, Config.Emoji_General_Key)}" : "")}\n" +
                             $"{emojis.Find(x => x.Name == Config.Emoji_Role_Damagedealer)} {chn.Value.SignInObjects.FindAll(x => x.Role == RoleEnum.option2)[1].Member.Mention} {((chn.Value.KeyHolder == chn.Value.SignInObjects.FindAll(x => x.Role == RoleEnum.option2)[1].Member) ? $"{DiscordEmoji.FromName(ctx.Client, Config.Emoji_General_Key)}" : "")}\n" +
                             $"{emojis.Find(x => x.Name == Config.Emoji_Role_Healer)} {chn.Value.SignInObjects.Find(x => x.Role == RoleEnum.option3).Member.Mention} {((chn.Value.KeyHolder == chn.Value.SignInObjects.Find(x => x.Role == RoleEnum.option3).Member) ? $"{DiscordEmoji.FromName(ctx.Client, Config.Emoji_General_Key)}" : "")}\n");
                            if (chn.Value.Note != "")
                                DmEmbed.AddField("Note:", chn.Value.Note);



                            switch (chn.Value.Armorstack)
                            {
                                case ArmorstackEnum.option1:
                                    DmEmbed.WithThumbnail(Config.Url_Armorstack_Kette);
                                    break;
                                case ArmorstackEnum.option2:
                                    DmEmbed.WithThumbnail(Config.Url_Armorstack_Platte);
                                    break;
                                case ArmorstackEnum.option3:
                                    DmEmbed.WithThumbnail(Config.Url_Armorstack_Leder);
                                    break;
                                case ArmorstackEnum.option4:
                                    DmEmbed.WithThumbnail(Config.Url_Armorstack_Stoff);
                                    break;
                            }
                            switch (chn.Value.Key)
                            {
                                case KeyEnum.option1:
                                    DmEmbed.WithImageUrl(Config.Url_Key_Junkyard);
                                    DmEmbed.WithColor(Config.Color_Key_Junkyard);
                                    break;
                                case KeyEnum.option2:
                                    DmEmbed.WithImageUrl(Config.Url_Key_Streets);
                                    DmEmbed.WithColor(Config.Color_Key_Streets);
                                    break;
                                case KeyEnum.option3:
                                    DmEmbed.WithImageUrl(Config.Url_Key_Upper);
                                    DmEmbed.WithColor(Config.Color_Key_Upper);
                                    break;
                                case KeyEnum.option4:
                                    DmEmbed.WithImageUrl(Config.Url_Key_Gambit);
                                    DmEmbed.WithColor(Config.Color_Key_Gambit);
                                    break;
                                case KeyEnum.option5:
                                    DmEmbed.WithImageUrl(Config.Url_Key_GD);
                                    DmEmbed.WithColor(Config.Color_Key_GD);
                                    break;
                                case KeyEnum.option6:
                                    DmEmbed.WithImageUrl(Config.Url_Key_Lower);
                                    DmEmbed.WithColor(Config.Color_Key_Lower);
                                    break;
                                case KeyEnum.option7:
                                    DmEmbed.WithImageUrl(Config.Url_Key_ID);
                                    DmEmbed.WithColor(Config.Color_Key_ID);
                                    break;
                                case KeyEnum.option8:
                                    DmEmbed.WithImageUrl(Config.Url_Key_Workshop);
                                    DmEmbed.WithColor(Config.Color_Key_Workshop);
                                    break;
                                case KeyEnum.option9:
                                    DmEmbed.WithImageUrl(Config.Url_Key_Random);
                                    DmEmbed.WithColor(Config.Color_Key_Random);
                                    break;

                            }

                            DmEmbed.WithFooter($"Advertiser: {chn.Value.Advertiser.DisplayName}", chn.Value.Advertiser.AvatarUrl);
                            await ctx.Guild.GetChannel(Config.Id_Channel_Log).SendMessageAsync(embed: DmEmbed);
                        }
                        chn.Value.SignInObjects.Clear();

                        SearchChannel.Remove(chn.Key);
                        MessageDeletePool.Add(ctx.Channel, DateTime.Now.AddMinutes(5));
                        if(chn.Value.Voice != null)
                            VoiceDeletePool.Add(chn.Value.Voice, DateTime.Now.AddMinutes(5));
                        await ctx.CreateResponseAsync("Channel will be deleted in 5min!", true);
                        return;
                    }
                    else
                        await ctx.CreateResponseAsync("Only the advertiser can use this command!", true); return;

                }

            }
            await ctx.CreateResponseAsync("This command can only be used in booking channels!", true);
        }

        [SlashCommand("Info", "Information")]
        public async Task InfoCommand(InteractionContext ctx)
        {
            DiscordEmbedBuilder Embed = new DiscordEmbedBuilder
            {
                Title = $"Information",
                Color = new DiscordColor(255, 115, 250)
            };
            Embed.AddField("Node", $"{ctx.Client.ShardId}", true);
            Embed.AddField("Websocket Latency", $"{ctx.Client.Ping}ms", true);
            Embed.AddField("Developer", $"{ctx.Client.GetUserAsync(267645496020041729).Result.Username}#{ctx.Client.GetUserAsync(267645496020041729).Result.Discriminator}");
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            DateTime buildDate = new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.Revision * 2);
            Embed.AddField("Bot Creation Time", $"{ctx.User.CreationTimestamp.UtcDateTime}");
            Embed.AddField("Version", $"{version}", true);
            Embed.AddField("Build Date", $"{buildDate}", true);
            await ctx.CreateResponseAsync(embed: Embed, true);
        }

        public async static void DeleteMessageLoop()
        {
            while (true)
            {
                foreach (KeyValuePair<DiscordChannel, DateTime> entry in MessageDeletePool)
                {
                    if (entry.Value <= DateTime.Now)
                    {
                        await entry.Key.DeleteAsync();
                        MessageDeletePool.Remove(entry.Key);
                    }

                }
                foreach (KeyValuePair<DiscordChannel, DateTime> entry in VoiceDeletePool)
                {
                    if (entry.Value <= DateTime.Now)
                    {
                        if(entry.Key.Users.Count == 0 && entry.Key.Type == ChannelType.Voice)
                        {
                            await entry.Key.DeleteAsync();
                            VoiceDeletePool.Remove(entry.Key);
                        }
                    }

                }
                await Task.Delay(5000);
            }
        }

        private static async Task OnMessageReactionAdded(DiscordClient e, MessageReactionAddEventArgs s)
        {
            try
            {
                foreach (KeyValuePair<DiscordChannel, Booking> chn in SearchChannel)
                {
                    if (chn.Key.Id == s.Channel.Id && s.Channel.IsPrivate == false)
                    {
                      
                        

                            if (((s.User != chn.Value.Advertiser) && (s.User != e.CurrentUser))
                         || ((chn.Value.SignInObjects.FindAll(x => x.UniqueMessageId == s.Message.Id && x.ClassAccepted == true).Count > 0)
                         && (s.Emoji.Name == Config.Emoji_Role_Damagedealer || s.Emoji.Name == Config.Emoji_Role_Tank || s.Emoji.Name == Config.Emoji_Role_Healer)))
                            {
                                if (s.Emoji.GetDiscordName() == Config.Emoji_General_Key)
                                {
                                    await chn.Key.AddOverwriteAsync(await s.Guild.GetMemberAsync(s.User.Id), allow: Permissions.AccessChannels | Permissions.SendMessages | Permissions.UseApplicationCommands);
                                }
                                else if (s.User != e.CurrentUser)
                                {
                                    var t = chn.Value.SignInObjects.Find(x => x.UniqueMessageId == s.Message.Id && x.ClassAccepted == true);
                                    if(s.Emoji.Name == Config.Emoji_Role_Healer || s.Emoji.Name == Config.Emoji_Role_Damagedealer || s.Emoji.Name == Config.Emoji_Role_Tank || s.Emoji.Name == Config.Emoji_General_Key)
                                        await s.Message.DeleteReactionAsync(s.Emoji, s.User);
                                }



                            }
                            else if (s.User == chn.Value.Advertiser && s.User != e.CurrentUser)
                            {

                                if ((s.Emoji.Name == Config.Emoji_Role_Tank || s.Emoji.Name == Config.Emoji_Role_Damagedealer || s.Emoji.Name == Config.Emoji_Role_Healer) && chn.Value.SignInObjects.Find(x => x.UniqueMessageId == s.Message.Id) != null)
                                {
                                    DiscordEmbedBuilder KeyEmbed = new DiscordEmbedBuilder
                                    {
                                        Title = $"{chn.Value.SignInObjects.Find(x => x.UniqueMessageId == s.Message.Id).Member.DisplayName} wurde als Keyholder ernannt!",
                                        Color = new DiscordColor(0, 142, 68)
                                    };
                                    if (chn.Value.Key != KeyEnum.option9 && !chn.Value.KeyHolderIsSet)
                                    {
                                        await chn.Key.SendMessageAsync(embed: KeyEmbed);
                                        await chn.Key.AddOverwriteAsync(s.Guild.GetRole(Config.Id_Role_Member), allow: Permissions.SendMessages | Permissions.AccessChannels);
                                        chn.Value.KeyHolderIsSet = true;
                                        chn.Value.KeyHolder = chn.Value.SignInObjects.Find(x => x.UniqueMessageId == s.Message.Id).Member;
                                    }

                                    chn.Value.SignInObjects.Find(x => x.UniqueMessageId == s.Message.Id).ClassAccepted = true;
                                }

                                if (s.Emoji.Name == Config.Emoji_Class_Demonhunter || s.Emoji.Name == Config.Emoji_Class_Deathknight || s.Emoji.Name == Config.Emoji_Class_Druid
                                    || s.Emoji.Name == Config.Emoji_Class_Mage || s.Emoji.Name == Config.Emoji_Class_Monk || s.Emoji.Name == Config.Emoji_Class_Paladin ||
                                    s.Emoji.Name == Config.Emoji_Class_Priest || s.Emoji.Name == Config.Emoji_Class_Rogue || s.Emoji.Name == Config.Emoji_Class_Schaman ||
                                    s.Emoji.Name == Config.Emoji_Class_Warlock || s.Emoji.Name == Config.Emoji_Class_Warrior || s.Emoji.Name == Config.Emoji_Class_Hunter)
                                    chn.Value.SignInObjects.Find(x => x.UniqueMessageId == s.Message.Id).RoleAccepted = true;
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return;
        }
        private static Task OnMessageReactionRemoved(DiscordClient e, MessageReactionRemoveEventArgs s)
        {
            try
            {
                foreach (KeyValuePair<DiscordChannel, Booking> chn in SearchChannel)
                {
                    if (chn.Key.Id == s.Channel.Id)
                    {
                        if (s.User == chn.Value.Advertiser)
                        {
                            if (s.Emoji.Name == Config.Emoji_Role_Healer || s.Emoji.Name == Config.Emoji_Role_Damagedealer || s.Emoji.Name == Config.Emoji_Role_Tank)
                                chn.Value.SignInObjects.Find(x => x.UniqueMessageId == s.Message.Id).ClassAccepted = false;

                            if (s.Emoji.Name == Config.Emoji_Class_Demonhunter || s.Emoji.Name == Config.Emoji_Class_Deathknight || s.Emoji.Name == Config.Emoji_Class_Druid
                                || s.Emoji.Name == Config.Emoji_Class_Mage || s.Emoji.Name == Config.Emoji_Class_Monk || s.Emoji.Name == Config.Emoji_Class_Paladin ||
                                s.Emoji.Name == Config.Emoji_Class_Priest || s.Emoji.Name == Config.Emoji_Class_Rogue || s.Emoji.Name == Config.Emoji_Class_Schaman ||
                                s.Emoji.Name == Config.Emoji_Class_Warlock || s.Emoji.Name == Config.Emoji_Class_Warrior || s.Emoji.Name == Config.Emoji_Class_Hunter)
                                chn.Value.SignInObjects.Find(x => x.UniqueMessageId == s.Message.Id).RoleAccepted = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return Task.CompletedTask;
        }


    }
}
