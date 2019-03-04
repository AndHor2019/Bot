using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        private static DiscordSocketClient Client;
        private CommandService Commands;

        public static int messages = 0;
        public static int commands = 0;
        public string log = "";

        static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            Console.WriteLine("3");
            Thread.Sleep(1000);
            Console.WriteLine("2");
            Thread.Sleep(1000);
            Console.WriteLine("1");
            Thread.Sleep(1000);
            Console.WriteLine("0");
            Thread.Sleep(1000);
            Console.WriteLine("Start!");

            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug,
                TotalShards = 2
            });
            Commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
            });


            Client.MessageReceived += Client_MessageReceived;
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly());

            Client.Ready += Client_Ready;
            Client.Log += Client_Log;

            string Token = "";
            using (var Stream = new FileStream(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location).Replace(@"bin\Debug\netcoreapp2.1", @"Data\Token.txt"), FileMode.Open, FileAccess.Read))
            using (var ReadToken = new StreamReader(@"Data\Token.txt"))
            {
                Token = ReadToken.ReadToEnd();
            }
            /*StreamReader readToken = new StreamReader("Data\\Token.txt");
            Token = readToken.ReadLine();
            readToken.Close();*/

            await Client.LoginAsync(TokenType.Bot, Token);
            await Client.StartAsync();


            System.Timers.Timer gameChanger = new System.Timers.Timer();
            gameChanger.Elapsed += new System.Timers.ElapsedEventHandler(GameChanger);
            //gameChanger.Interval = 900000;
            gameChanger.Interval = 300000;
            gameChanger.Enabled = true;

            await Task.Delay(-1);
        }

        public static DateTime start;

        public static void GameChanger(object source, System.Timers.ElapsedEventArgs e)
        {
            Random rand = new Random();
            string[] games = { "everything", "with you", "with my CPU", "with my friends", "lol", "skap!help", "skap!invite will work in future", "with my PC" };

            int hehe = rand.Next(0, 9);

            switch (hehe)
            {
                case 0:
                    Client.SetGameAsync(games[0]);
                    string logCommand = ($"[" + DateTime.Now + $" at Bot's game] Skap!#0734 changed playing status to {games[0]}");
                    Console.WriteLine(logCommand);
                    CommandsLog(logCommand);
                    break;
                case 1:
                    Client.SetGameAsync(games[1]);
                    string logCommand1 = ($"[" + DateTime.Now + $" at Bot's game] Skap!#0734 changed playing status to {games[1]}");
                    Console.WriteLine(logCommand1);
                    CommandsLog(logCommand1);
                    break;
                case 2:
                    Client.SetGameAsync(games[2]);
                    string logCommand2 = ($"[" + DateTime.Now + $" at Bot's game] Skap!#0734 changed playing status to {games[2]}");
                    Console.WriteLine(logCommand2);
                    CommandsLog(logCommand2);
                    break;
                case 3:
                    Client.SetGameAsync(games[3]);
                    string logCommand3 = ($"[" + DateTime.Now + $" at Bot's game] Skap!#0734 changed playing status to {games[3]}");
                    Console.WriteLine(logCommand3);
                    CommandsLog(logCommand3);
                    break;
                case 4:
                    Client.SetGameAsync(games[4]);
                    string logCommand4 = ($"[" + DateTime.Now + $" at Bot's game] Skap!#0734 changed playing status to {games[4]}");
                    Console.WriteLine(logCommand4);
                    CommandsLog(logCommand4);
                    break;
                case 5:
                    Client.SetGameAsync(games[5]);
                    string logCommand5 = ($"[" + DateTime.Now + $" at Bot's game] Skap!#0734 changed playing status to {games[5]}");
                    Console.WriteLine(logCommand5);
                    CommandsLog(logCommand5);
                    break;
                case 6:
                    Client.SetGameAsync(games[6]);
                    string logCommand6 = ($"[" + DateTime.Now + $" at Bot's game] Skap!#0734 changed playing status to {games[6]}");
                    Console.WriteLine(logCommand6);
                    CommandsLog(logCommand6);
                    break;
                case 7:
                    Client.SetGameAsync(games[7]);
                    string logCommand7 = ($"[" + DateTime.Now + $" at Bot's game] Skap!#0734 changed playing status to {games[7]}");
                    Console.WriteLine(logCommand7);
                    CommandsLog(logCommand7);
                    break;
                case 8:
                    string game8 = ($"started at {start} CEST");
                    Client.SetGameAsync(game8);
                    string logCommand8 = ($"[" + DateTime.Now + $" at Bot's game] Skap!#0734 changed playing status to {game8}");
                    Console.WriteLine(logCommand8);
                    CommandsLog(logCommand8);
                    break;
                default:
                    break;
            }
        }

        public static DateTime dd = DateTime.Now;
        public static DateTime dirDate = dd;

        private async Task Client_Log(LogMessage Message)
        {
            Console.WriteLine($"[{DateTime.Now} at {Message.Source}] {Message.Message}");
            //log += ($"[{DateTime.Now} at {Message.Source}] {Message.Message}\n");
            try
            {
                if (dd.Hour != DateTime.Now.Hour)
                {
                    dd = DateTime.Now;
                }
                if(dirDate.Day != DateTime.Now.Day)
                {
                    dirDate = DateTime.Now;
                }
                string botLog = "";
                string logmsg = "";

                string dirPath = $@"Logs\{dirDate.Day}-{dirDate.Month}-{dirDate.Year}";
                string path = $@"{dirPath}\Bot_Log_{dd.Day}-{dd.Month}-{dd.Year}_{dd.Hour}-{dd.Minute}-{dd.Second}.log";

                logmsg = $"[{DateTime.Now} at {Message.Source}] {Message.Message}";


                if (Directory.Exists(dirPath))
                {
                    if (File.Exists(path))
                    {
                        StreamReader r = new StreamReader(path);
                        botLog = r.ReadToEnd();
                        r.Close();
                        botLog = botLog + (string)logmsg;
                    }
                    else
                    {
                        botLog = botLog + (string)logmsg;
                        StreamWriter w = new StreamWriter(path);
                        w.Write(logmsg + "\n");
                        w.Close();
                        return;
                    }
                    StreamWriter w1 = new StreamWriter(path);
                    w1.WriteLine(botLog);
                    w1.Close();
                }
                else
                {
                    Directory.CreateDirectory(dirPath);

                    if (File.Exists(path))
                    {
                        StreamReader r = new StreamReader(path);
                        botLog = r.ReadToEnd();
                        r.Close();
                        botLog = botLog + (string)logmsg;
                    }
                    else
                    {
                        botLog = botLog + (string)logmsg;
                        StreamWriter w = new StreamWriter(path);
                        w.Write(logmsg + "\n");
                        w.Close();
                        return;
                    }
                    StreamWriter w1 = new StreamWriter(path);
                    w1.WriteLine(botLog);
                    w1.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void CommandsLog(string logCommand)
        {
            try
            {
                if (dd.Hour != DateTime.Now.Hour)
                {
                    dd = DateTime.Now;
                }
                if (dirDate.Day != DateTime.Now.Day)
                {
                    dirDate = DateTime.Now;
                }
                string botLog = "";
                string logmsg = logCommand;

                string dirPath = $@"Logs\{dirDate.Day}-{dirDate.Month}-{dirDate.Year}";
                string path = $@"{dirPath}\Commands_Log_{dd.Day}-{dd.Month}-{dd.Year}_{dd.Hour}-{dd.Minute}-{dd.Second}.log";

                if (Directory.Exists(dirPath))
                {
                    if (File.Exists(path))
                    {
                        StreamReader r = new StreamReader(path);
                        botLog = r.ReadToEnd();
                        r.Close();
                        botLog = botLog + logmsg;
                    }
                    else
                    {
                        botLog = botLog + logmsg;
                        StreamWriter w = new StreamWriter(path);
                        w.Write(botLog + "\n");
                        w.Close();
                        return;
                    }
                    StreamWriter w1 = new StreamWriter(path);
                    w1.WriteLine(botLog);
                    w1.Close();
                }
                else
                {
                    Directory.CreateDirectory(dirPath);

                    if (File.Exists(path))
                    {
                        StreamReader r = new StreamReader(path);
                        botLog = r.ReadToEnd();
                        r.Close();
                        botLog = botLog + logmsg;
                    }
                    else
                    {
                        botLog = botLog + logmsg;
                        StreamWriter w = new StreamWriter(path);
                        w.Write(botLog + "\n");
                        w.Close();
                        return;
                    }
                    StreamWriter w1 = new StreamWriter(path);
                    w1.WriteLine(botLog);
                    w1.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async Task Client_Ready()
        {
            start = DateTime.Now;
            await Client.SetGameAsync($"started at {start} CEST");
        }

        private async Task Client_MessageReceived(SocketMessage MessageParam)
        {
            var Message = MessageParam as SocketUserMessage;
            var Context = new SocketCommandContext(Client, Message);

            if (Context.Message == null || Context.Message.Content == "") return;
            if (Context.User.IsBot) return;

            int ArgPos = 0;
            if (Message.HasStringPrefix("skap!", ref ArgPos) || Message.HasMentionPrefix(Client.CurrentUser, ref ArgPos))
            {
                commands++;
                var Result = Commands.ExecuteAsync(Context, ArgPos);
                if (Result.IsCanceled)
                    Console.WriteLine($"[{DateTime.Now} at Commands] went wrong with executting a command. Text: {Context.Message.Content} | Result: {Result.Result} | Exception: {Result.Exception} | Status: {Result.Status}");
            }
            else
            {
                messages++;
                return;
            }
        }

        public async Task test()
        {
            await Client.SetGameAsync("Shutting down...");
        }
    }
}
