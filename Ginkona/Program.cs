using DSharpPlus;
using DSharpPlus.SlashCommands;
using DSharpPlus.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using DSharpPlus.SlashCommands.EventArgs;
using System.Diagnostics;

namespace Ginkona
{

	class Program
	{
		static void Main(string[] args)
		{
			Version version = Assembly.GetExecutingAssembly().GetName().Version;
			DateTime buildDate = new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.Revision * 2);
			string Version = $"{version} \n    Build Date: ({buildDate})";

			Console.Title = $"Ginkona {Version}";

			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine($@"   _____ _       _                     
  / ____(_)     | |                    
 | |  __ _ _ __ | | _____  _ __   __ _ 
 | | |_ | | '_ \| |/ / _ \| '_ \ / _` |
 | |__| | | | | |   < (_) | | | | (_| |
  \_____|_|_| |_|_|\_\___/|_| |_|\__,_|
           Programmed by Lemix
          Powered by DSharpPlus
           Version: {Version}
");
			Console.ForegroundColor = ConsoleColor.Gray;

			MainAsync().GetAwaiter().GetResult();
		}


		static async Task MainAsync() {
			Stopwatch watch = Stopwatch.StartNew();
			var client = new DiscordShardedClient(new DiscordConfiguration()
			{
				Token = "apikey",
				TokenType = TokenType.Bot,
				Intents = DiscordIntents.All,
				MinimumLogLevel = LogLevel.Debug,
				LogTimestampFormat = "dd-MM-yyyy HH:mm:ss",
				AutoReconnect = true
			});

			var slash = await client.UseSlashCommandsAsync();
			foreach (KeyValuePair<int, SlashCommandsExtension> entry in slash)
			{

				entry.Value.RegisterCommands<Commands>();
				entry.Value.SlashCommandExecuted += async (s, e) => { await OnCommandExecuted(s, e); }; ;
				entry.Value.SlashCommandErrored += async (s, e) => { await OnCommandError(s, e); }; ;
				
				
			}

			await client.StartAsync();
			
			client.Logger.LogInformation(new EventId(1, "Startup"), $"Completed Bot initialization in {watch.ElapsedMilliseconds} ms");
			await Task.Delay(5000);
			await client.UpdateStatusAsync(new DiscordActivity("depleted den Tolostack nicht", ActivityType.Playing), UserStatus.Online);
			client.Logger.LogInformation(new EventId(2, "UpdateStatus"), $"Updated Bot Status");
			

			await Task.Delay(-1);

		}

        private static Task OnCommandExecuted(SlashCommandsExtension s, SlashCommandExecutedEventArgs e)
		{
			return Task.CompletedTask;
		}

		private static Task OnCommandError(SlashCommandsExtension s, SlashCommandErrorEventArgs e)
		{
			return Task.CompletedTask;
		}

	}
}
