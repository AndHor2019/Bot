using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

using Discord;
using Discord.Commands;
using System.Threading;
using System.Xml.Serialization;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace ConsoleApp1.Core.Commands
{
    public class HelloWorld : ModuleBase<SocketCommandContext>
    {
        [Command("hello"), Alias("helloworld", "world"), Summary("Hello world command")]
        public async Task Hello()
        {
            Console.Write($"[" + DateTime.Now + $" at Commands] ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"{Context.Message.Author.Username}#{Context.Message.Author.Discriminator}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" used command skap!hello\n");

            string logCommand = ($"[" + DateTime.Now + $" at Commands] {Context.Message.Author.Username}#{Context.Message.Author.Discriminator} used command skap!hello");
            Program.CommandsLog(logCommand);

            await Context.Channel.SendMessageAsync("Hello world");
        }

        [Command("help"), Summary("Help command")]
        public async Task Help()
        {
            Console.Write($"[" + DateTime.Now + $" at Commands] ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"{Context.Message.Author.Username}#{Context.Message.Author.Discriminator}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" used command skap!help\n");

            string logCommand = ($"[" + DateTime.Now + $" at Commands] {Context.Message.Author.Username}#{Context.Message.Author.Discriminator} used command skap!help");
            Program.CommandsLog(logCommand);

            Random rand = new Random();
            EmbedBuilder Embed = new EmbedBuilder();
            EmbedBuilder Embed2 = new EmbedBuilder();

            Embed.WithAuthor($"{Context.User.Username}#{Context.User.Discriminator} Here's a list of all my commands", Context.User.GetAvatarUrl());
            Embed.WithColor(rand.Next(255), rand.Next(255), rand.Next(255));
            Embed.WithTitle($"Prefix: mention ``@{Context.Client.CurrentUser.Username}#{Context.Client.CurrentUser.Discriminator}`` or text ``skap!``");
            Embed.WithFooter($"[{DateTime.Now}]", Context.Guild.Owner.GetAvatarUrl());
            Embed.AddField("SFW commands:", "``hello``" +
                                          "\n``help``" +
                                          "\n``server-ip``" +
                                          "\n``embed`` <user-input>" +
                                          "\n``avatar``" +
                                          "\n``calculator`` <num1> <operation> <num2>" +
                                          "\n``calculatorhelp``" +
                                          "\n``mur``" +
                                          "\n``murlist``" +
                                          "\n``stats``");
            Embed.AddField("NSFW commands:", "``yiff``" +
                                           "\n``yifflist``");

            Embed2.WithAuthor($"{Context.User.Username}#{Context.User.Discriminator} Here's a list of all my owner-only commands", Context.User.GetAvatarUrl());
            Embed2.WithColor(rand.Next(255), rand.Next(255), rand.Next(255));
            Embed2.WithTitle($"Prefix: mention ``@{Context.Client.CurrentUser.Username}#{Context.Client.CurrentUser.Discriminator}`` or text ``skap!``");
            Embed.WithFooter($"[{DateTime.Now}]", Context.Guild.Owner.GetAvatarUrl());
            Embed2.AddField("Commands:", "``test``" + 
                                       "\n``backdoor`` <guild-id>" + 
                                       "\n``customgame`` <game>" + 
                                       "\n``viewfile`` <file-name/file-path>" +
                                       "\n``fileinfo`` <file-name/file-path>" + 
                                       "\n``restart``");

            await Context.Channel.SendMessageAsync("", false, Embed.Build());
            await Context.Channel.SendMessageAsync("", false, Embed2.Build());
        }

        [Command("server-ip"), Summary("Get public server ip")]
        public async Task GetPublicIP()
        {
            Console.Write($"[" + DateTime.Now + $" at Commands] ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"{Context.Message.Author.Username}#{Context.Message.Author.Discriminator}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" used command skap!server-ip\n");

            string logCommand = ($"[" + DateTime.Now + $" at Commands] {Context.Message.Author.Username}#{Context.Message.Author.Discriminator} used command skap!server-ip");
            Program.CommandsLog(logCommand);

            string externalip = new WebClient().DownloadString("http://icanhazip.com");
            string ipip = externalip = externalip.Replace("\n", "");
            await Context.Channel.SendMessageAsync("Server ip: \n```fix\n" + ipip + ":666\n```");
            Console.WriteLine($"[{DateTime.Now} at Public IP] IP is: {ipip}");
        }

        [Command("embed"), Summary("Embed test command")]
        public async Task Embed([Remainder]string Input = "None")
        {
            Console.Write($"[" + DateTime.Now + $" at Commands] ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"{Context.Message.Author.Username}#{Context.Message.Author.Discriminator}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" used command skap!embed\n");

            string logCommand = ($"[" + DateTime.Now + $" at Commands] {Context.Message.Author.Username}#{Context.Message.Author.Discriminator} used command skap!embed {Input}");
            Program.CommandsLog(logCommand);

            Random rand = new Random();

            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithAuthor($"{Context.User.Username}#{Context.User.Discriminator} requested a test embed", Context.User.GetAvatarUrl());
            //Embed.WithColor(40, 200, 150);
            Embed.WithColor(rand.Next(255), rand.Next(255), rand.Next(255));
            Embed.WithTitle("Title of the embed");
            Embed.WithFooter($"The Footer of the Embed with guild owner icon.  [{DateTime.Now}]", Context.Guild.Owner.GetAvatarUrl());
            Embed.WithDescription("This is a **dummuy** description, with a cool __link__.\n" +
                                  "[Creator's webite](https://sites.google.com/site/andhor0001/home/)");
            Embed.AddField(":x: Message writer:", $"{Context.User.Username}#{Context.User.Discriminator}");
            Embed.AddField(":white_check_mark: Server owner:", $"{Context.Guild.Owner.Username}#{Context.Guild.Owner.Discriminator}", true);
            Embed.AddField("User input:", Input);
            Embed.WithThumbnailUrl(Context.User.GetAvatarUrl());

            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("stop"), Summary("Bot stop command")]
        public async Task StopBot()
        {
            Console.Write($"[" + DateTime.Now + $" at Commands] ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"{Context.Message.Author.Username}#{Context.Message.Author.Discriminator}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" used command skap!stop\n");

            string logCommand = ($"[" + DateTime.Now + $" at Commands] {Context.Message.Author.Username}#{Context.Message.Author.Discriminator} used command skap!stop");
            Program.CommandsLog(logCommand);

            await Context.Channel.SendMessageAsync("Command removed for better using >)");

            return;

            if (!(Context.User.Id == 365205975705190400))
            {
                await Context.Channel.SendMessageAsync(":x: You are not a bot moderator!");
                return;
            }

            await Context.Channel.SendMessageAsync(":white_check_mark: Nice! You have done this!");
            Program pro = new Program();
            pro.test();
            Thread.Sleep(5000);
            await Context.Client.SetGameAsync("Bot stopped! Don't use!");
            Environment.Exit(0);
        }

        [Command("avatar"), Alias("icon", "ava"), Summary("User avatar poster")]
        public async Task Avatar([Remainder]IUser User = null)
        {
            Console.Write($"[" + DateTime.Now + $" at Commands] ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"{Context.Message.Author.Username}#{Context.Message.Author.Discriminator}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" used command skap!avatar\n");

            string logCommand = ($"[" + DateTime.Now + $" at Commands] {Context.Message.Author.Username}#{Context.Message.Author.Discriminator} used command skap!avatar {User.Mention}");
            Program.CommandsLog(logCommand);

            if (User == null)
            {
                string AvatarUrl = Context.User.GetAvatarUrl();
                string[] parts = AvatarUrl.Split('=');

                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithTitle($"Avatar for {Context.User.Username}#{Context.User.Discriminator}");
                Embed.WithImageUrl(parts[0] + "=" + 512);
                Embed.WithFooter($"Requested at [{DateTime.Now}]");

                await Context.Channel.SendMessageAsync("", false, Embed.Build());
                return;
            }

            string AvatarUrl2 = "";
            string[] parts2;
            string finalAvatarUrl = "";
            try
            {
                AvatarUrl2 = User.GetAvatarUrl();
                parts2 = AvatarUrl2.Split('=');
                finalAvatarUrl = parts2[0] + "=" + 512;
            }
            catch (Exception)
            {
                await Context.Channel.SendMessageAsync("``User probably have default avatar``");
                return;
            }

            EmbedBuilder Embed2 = new EmbedBuilder();
            Embed2.WithTitle($"Avatar for {User.Username}#{User.Discriminator}");
            Embed2.WithImageUrl(finalAvatarUrl);
            Embed2.WithFooter($"Requested at [{DateTime.Now}]");

            await Context.Channel.SendMessageAsync("", false, Embed2.Build());
        }

        [Command("calculator"), Alias("calc"), Summary("Basic calculator")]
        public async Task Calc(float num1 = 0, string operation = "", float num2 = 0)
        {
            Console.Write($"[" + DateTime.Now + $" at Commands] ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"{Context.Message.Author.Username}#{Context.Message.Author.Discriminator}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" used command skap!calc\n");

            string logCommand = ($"[" + DateTime.Now + $" at Commands] {Context.Message.Author.Username}#{Context.Message.Author.Discriminator} used command skap!calc {num1} {operation} {num2}");
            Program.CommandsLog(logCommand);

            switch (operation)
            {
                case "+":
                    float num3 = num1 + num2;
                    await Context.Channel.SendMessageAsync(num3.ToString());
                    return;
                case "-":
                    float num4 = num1 - num2;
                    await Context.Channel.SendMessageAsync(num4.ToString());
                    return;
                case "*":
                    float num5 = num1 * num2;
                    await Context.Channel.SendMessageAsync(num5.ToString());
                    return;
                case "/":
                    float num6 = num1 / num2;
                    await Context.Channel.SendMessageAsync(num6.ToString());
                    return;
                case "%":
                    float num7 = num1 % num2;
                    await Context.Channel.SendMessageAsync(num7.ToString());
                    return;
                default:
                    await Context.Channel.SendMessageAsync("Unknown operation");
                    return;
            }
        }

        [Command("calculatorhelp"), Alias("calchelp"), Summary("Help for basic calculator")]
        public async Task HelpCalc()
        {
            Console.Write($"[" + DateTime.Now + $" at Commands] ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"{Context.Message.Author.Username}#{Context.Message.Author.Discriminator}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" used command skap!calchelp\n");

            string logCommand = ($"[" + DateTime.Now + $" at Commands] {Context.Message.Author.Username}#{Context.Message.Author.Discriminator} used command skap!calchelp");
            Program.CommandsLog(logCommand);

            EmbedBuilder Embed = new EmbedBuilder();
            Random rand = new Random();

            Embed.WithAuthor($"{Context.User.Username}#{Context.User.Discriminator}, this is help for calculator:", Context.User.GetAvatarUrl());
            //Embed.WithColor(40, 200, 150);
            Embed.WithColor(rand.Next(255), rand.Next(255), rand.Next(255));
            Embed.WithDescription("How to use:");
            Embed.WithFooter($"[{DateTime.Now}]", Context.Guild.Owner.GetAvatarUrl());
            Embed.AddField("+", $"skap!calculator 1 **+** 8");
            Embed.AddField("-", $"skap!calculator 5 **-** 2");
            Embed.AddField("*", $"skap!calculator 7 ***** 4");
            Embed.AddField("/", $"skap!calculator 3 **/** 5");
            Embed.AddField("%", $"skap!calculator 9 **%** 3");

            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }
        
        [Command("mur"), Summary("Sheri's api (mur)")]
        public async Task Mur()
        {
            Console.Write($"[" + DateTime.Now + $" at Commands] ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"{Context.Message.Author.Username}#{Context.Message.Author.Discriminator}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" used command skap!mur\n");

            string logCommand = ($"[" + DateTime.Now + $" at Commands] {Context.Message.Author.Username}#{Context.Message.Author.Discriminator} used command skap!mur");
            Program.CommandsLog(logCommand);

            #region Load/Save stats
            Int64 murRuns = 0;
            if (File.Exists(@"Data\Mur\Mur_Data.xml"))
            {
                StreamReader reader1 = new StreamReader(@"Data\Mur\Mur_Data.xml");
                XmlSerializer xml1 = new XmlSerializer(typeof(Int64));
                murRuns = (Int64)xml1.Deserialize(reader1);
                reader1.Close();
            }
            else
            {
                StreamWriter writer1 = new StreamWriter(@"Data\Mur\Mur_Data.xml");
                XmlSerializer xml2 = new XmlSerializer(typeof(Int64));
                xml2.Serialize(writer1, murRuns);
                writer1.Close();
            }
            murRuns++;
            StreamWriter writer2 = new StreamWriter(@"Data\Mur\Mur_Data.xml");
            XmlSerializer xml3 = new XmlSerializer(typeof(Int64));
            xml3.Serialize(writer2, murRuns);
            writer2.Close();
            #endregion

            EmbedBuilder Embed = new EmbedBuilder();
            Random rand = new Random();

            try
            {
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

                //string test1 = strOutputJson.Remove(0, 8);
                //string test2 = test1.Remove(35, (test1.Length - 35));

                var jObj = strOutputJson;
                var jObj2 = JObject.Parse(jObj);

                string gg = jObj2.First.ToString();

                gg = gg.Replace("\"{\"url\":\"", "");
                gg = gg.Replace("\"", "");
                gg = gg.Remove(0, 5);
                //await Context.Channel.SendMessageAsync(gg);
                int length = gg.Length;

                #region Container
                //If current image is in list
                string contains = "";

                if (File.Exists(@"Data\Mur\List\Mur_Image_List_" + length + ".txt"))
                {
                    StreamReader reader = new StreamReader(@"Data\Mur\List\Mur_Image_List_" + length + ".txt");
                    contains = reader.ReadToEnd();
                    reader.Close();
                }
                else
                {
                    StreamWriter writer = new StreamWriter(@"Data\Mur\List\Mur_Image_List_" + length + ".txt");
                    writer.Write("");
                    writer.Close();
                }
                if (!contains.Contains(gg))
                {
                    contains = contains + gg;
                    Console.Write("[" + DateTime.Now + " at Mur list (");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(length);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(")] Added ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(gg);
                    Console.ForegroundColor = ConsoleColor.Gray;

                    StreamWriter writer1 = new StreamWriter(@"Data\Mur\List\Mur_Image_List_" + length + ".txt");
                    writer1.WriteLine(contains);
                    writer1.Close();
                    Embed.AddField("Info:", "```css\nAdded " + gg + " to Mur images list [" + length + "]\n```");
                }
                else
                {
                    Console.Write("[" + DateTime.Now + " at Mur list (");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(length);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(")] Already contains ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(gg);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Embed.AddField("Info:", "```css\nMur images list [" + length + "] already contains " + gg + "\n```");
                }
                #endregion

                Embed.WithAuthor($"{Context.User.Username}#{Context.User.Discriminator}, Here is it for you:", Context.User.GetAvatarUrl());
                Embed.WithColor(rand.Next(255), rand.Next(255), rand.Next(255));
                Embed.WithDescription("No image? [Click here](" + gg + ")");
                Embed.WithImageUrl(gg);

                #region Tester if image have .jpg or longer name (THIS IS NOT USED) :":":":":":":":":":
                /*if (!test2.Contains(".jpg"))
                {
                    Console.Write("[" + DateTime.Now + " at Mur list] No .jpg/longer name! Saving original: ");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(strOutputJson);
                    Console.ForegroundColor = ConsoleColor.Gray;

                    string originals = "";

                    if (File.Exists("Originals.txt"))
                    {
                        StreamReader reader = new StreamReader("Originals.txt");
                        originals = reader.ReadToEnd();
                        reader.Close();
                    }
                    else
                    {
                        StreamWriter writer = new StreamWriter("Originals.txt");
                        writer.Write("");
                        writer.Close();
                    }

                    originals = originals + strOutputJson;

                    StreamWriter writer1 = new StreamWriter("Originals.txt");
                    writer1.WriteLine(originals);
                    writer1.Close();

                    Embed.AddField("Another image:", "```css\n" + strOutputJson + "\n```");
                }
                else
                {
                    Embed.WithImageUrl(test2);
                }*/
                #endregion

                Embed.WithFooter(murRuns + " times used total  |  [" + DateTime.Now + "]");
                await Context.Channel.SendMessageAsync("", false, Embed.Build());
            }
            catch (Exception ex)
            {
                Embed.WithAuthor($"{Context.User.Username}#{Context.User.Discriminator}, An error has occured:", Context.User.GetAvatarUrl());
                Embed.WithColor(rand.Next(255), rand.Next(255), rand.Next(255));
                Embed.WithDescription("``" + ex + "``");
                await Context.Channel.SendMessageAsync("", false, Embed.Build());
            }
        }

        [Command("murstats"), Summary("Stats for mur")] //Nedorobene
        public async Task MurStats()
        {
            Console.Write($"[" + DateTime.Now + $" at Commands] ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"{Context.Message.Author.Username}#{Context.Message.Author.Discriminator}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" used command skap!murstats\n");

            string logCommand = ($"[" + DateTime.Now + $" at Commands] {Context.Message.Author.Username}#{Context.Message.Author.Discriminator} used command skap!murstats");
            Program.CommandsLog(logCommand);

            return;

            //Nedorobene

            EmbedBuilder Embed = new EmbedBuilder();
            Random rand = new Random();

            Embed.WithAuthor($"{Context.User.Username}#{Context.User.Discriminator}, Here is mur stats:", Context.User.GetAvatarUrl());
            Embed.WithColor(rand.Next(255), rand.Next(255), rand.Next(255));
            Embed.WithDescription($"Mur stats for me, bot {Context.Client.CurrentUser.Mention}");

            await Context.Channel.SendMessageAsync("Wait before upload will complete");// + Context.Client.ShardId.ToString());

            string[] imageList = new string[100];
            int[] totalImages = new int[100];

            DirectoryInfo dd = new DirectoryInfo(@"Data\Mur\List\");
            var files = dd.GetFiles();
            int temp = 0;
            foreach (var file in files)
            {
                //Console.WriteLine(temp);
                //Console.WriteLine(file.Name);

                string text35 = "";
                StreamReader reader = new StreamReader(file.ToString());
                imageList[temp] = reader.ReadToEnd();
                reader.Close();

                if (file.Name.Contains("35"))
                {
                    totalImages[temp] = (imageList[temp].Length - 2) / 35;
                }
                if (file.Name.Contains("40"))
                {
                    totalImages[temp] = (imageList[temp].Length - 2) / 40;
                }
                if (file.Name.Contains("55"))
                {
                    totalImages[temp] = (imageList[temp].Length - 2) / 55;
                }
                //here
                temp++;
            }
            #region Old
            /*
            //Total num of images in 35
            string text35 = "";
            StreamReader reader = new StreamReader(@"Data\Mur\List\Mur_Image_List_35.txt");
            text35 = reader.ReadToEnd();
            reader.Close();
            int totalImages35 = (text35.Length - 1) / 35;
            text35 = "";

            //Total num of mur runs
            StreamReader reader1 = new StreamReader(@"Data\Mur\Mur_Data.xml");
            XmlSerializer xml1 = new XmlSerializer(typeof(Int64));
            Int64 murRuns = (Int64)xml1.Deserialize(reader1);
            reader1.Close();

            //Total num of images in 40
            string text40 = "";
            StreamReader reader2 = new StreamReader(@"Data\Mur\List\Mur_Image_List_40.txt");
            text40 = reader2.ReadToEnd();
            reader2.Close();
            int totalImages40 = (text40.Length - 1) / 40;
            text40 = "";

            //Total num of images in 55
            string text55 = "";
            StreamReader reader3 = new StreamReader(@"Data\Mur\List\Mur_Image_List_55.txt");
            text55 = reader3.ReadToEnd();
            reader3.Close();
            int totalImages55 = (text55.Length - 1) / 55;
            text55 = "";
            */
            #endregion

            int totnum = 0;

            foreach (int num in totalImages)
            {
                totnum = totnum + num;
            }

            //Total num of mur runs
            StreamReader reader1 = new StreamReader(@"Data\Mur\Mur_Data.xml");
            XmlSerializer xml1 = new XmlSerializer(typeof(Int64));
            Int64 murRuns = (Int64)xml1.Deserialize(reader1);
            reader1.Close();

            try
            {
                //here
                Embed.AddInlineField($"Total images in Mur list (35):", "``" + totalImages[0] + "``");
                Embed.AddInlineField($"Total images in Mur list (40):", "``" + totalImages[1] + "``");
                Embed.AddInlineField($"Total images in Mur list (55):", "``" + totalImages[2] + "``");
                Embed.AddInlineField($"Total images in all Mur lists:", "``" + totnum + "``");
                Embed.AddField("Total uses of skap!mur:", "``" + murRuns + "``");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("murlist"), Summary("List of mur images sender")]
        public async Task MurList()
        {
            Console.Write($"[" + DateTime.Now + $" at Commands] ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"{Context.Message.Author.Username}#{Context.Message.Author.Discriminator}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" used command skap!murlist\n");

            string logCommand = ($"[" + DateTime.Now + $" at Commands] {Context.Message.Author.Username}#{Context.Message.Author.Discriminator} used command skap!murlist");
            Program.CommandsLog(logCommand);

            Console.Write("[" + DateTime.Now + $" at Mur list] Sending lists to ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"{Context.Message.Author.Username}#{Context.Message.Author.Discriminator}");
            Console.ForegroundColor = ConsoleColor.Gray;

            await Context.Channel.SendMessageAsync("Wait to uploading competion");// + Context.Client.ShardId.ToString());
            
            DirectoryInfo dd = new DirectoryInfo(@"Data\Mur\List\");
            var files = dd.GetFiles();
            foreach (var file in files)
            {
                await Context.Channel.SendFileAsync(file.ToString());
            }
        }

        [Command("yiff"), Summary("Sheri's api (yiff)")]
        public async Task Yiff()
        {
            Console.Write($"[" + DateTime.Now + $" at Commands] ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"{Context.Message.Author.Username}#{Context.Message.Author.Discriminator}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" used command skap!yiff\n");

            string logCommand = ($"[" + DateTime.Now + $" at Commands] {Context.Message.Author.Username}#{Context.Message.Author.Discriminator} used command skap!yiff");
            Program.CommandsLog(logCommand);

            if (!Context.Channel.IsNsfw)
            {
                #region Load/Save stats
                Int64 yiffRuns = 0;
                if (File.Exists(@"Data\Yiff\Yiff_Data.xml"))
                {
                    StreamReader reader1 = new StreamReader(@"Data\Yiff\Yiff_Data.xml");
                    XmlSerializer xml1 = new XmlSerializer(typeof(Int64));
                    yiffRuns = (Int64)xml1.Deserialize(reader1);
                    reader1.Close();
                }
                else
                {
                    StreamWriter writer1 = new StreamWriter(@"Data\Yiff\Yiff_Data.xml");
                    XmlSerializer xml2 = new XmlSerializer(typeof(Int64));
                    xml2.Serialize(writer1, yiffRuns);
                    writer1.Close();
                }
                yiffRuns++;
                StreamWriter writer2 = new StreamWriter(@"Data\Yiff\Yiff_Data.xml");
                XmlSerializer xml3 = new XmlSerializer(typeof(Int64));
                xml3.Serialize(writer2, yiffRuns);
                writer2.Close();
                #endregion

                EmbedBuilder Embed = new EmbedBuilder();
                Random rand = new Random();

                try
                {
                    #region Getting yiff image
                    //Get new mur image
                    string apiUrl = "https://sheri.bot/api/v2/yiff";
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

                    //string test1 = strOutputJson.Remove(0, 8);
                    //string test2 = test1.Remove(35, (test1.Length - 35));

                    var jObj = strOutputJson;
                    var jObj2 = JObject.Parse(jObj);

                    string gg = jObj2.First.ToString();

                    gg = gg.Replace("\"{\"url\":\"", "");
                    gg = gg.Replace("\"", "");
                    gg = gg.Remove(0, 5);
                    //await Context.Channel.SendMessageAsync(gg);
                    int length = gg.Length;

                    #region Container
                    //If current image is in list
                    string contains = "";

                    if (File.Exists(@"Data\Yiff\List\Yiff_Image_List_" + length + ".txt"))
                    {
                        StreamReader reader = new StreamReader(@"Data\Yiff\List\Yiff_Image_List_" + length + ".txt");
                        contains = reader.ReadToEnd();
                        reader.Close();
                    }
                    else
                    {
                        StreamWriter writer = new StreamWriter(@"Data\Yiff\List\Yiff_Image_List_" + length + ".txt");
                        writer.Write("");
                        writer.Close();
                    }
                    if (!contains.Contains(gg))
                    {
                        contains = contains + gg;
                        Console.Write("[" + DateTime.Now + " at Yiff list (");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(length);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(")] Added ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(gg);
                        Console.ForegroundColor = ConsoleColor.Gray;

                        StreamWriter writer1 = new StreamWriter(@"Data\Yiff\List\Yiff_Image_List_" + length + ".txt");
                        writer1.WriteLine(contains);
                        writer1.Close();
                        Embed.AddField("Info:", "```css\nAdded " + gg + " to Yiff images list [" + length + "]\n```");
                    }
                    else
                    {
                        Console.Write("[" + DateTime.Now + " at Yiff list (");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(length);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(")] Already contains ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(gg);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Embed.AddField("Info:", "```css\nYiff images list [" + length + "] already contains " + gg + "\n```");
                    }
                    #endregion

                    Embed.WithAuthor($"{Context.User.Username}#{Context.User.Discriminator}, Here is it for you:", Context.User.GetAvatarUrl());
                    Embed.WithColor(rand.Next(255), rand.Next(255), rand.Next(255));
                    Embed.WithDescription("No image? [Click here](" + gg + ")");
                    Embed.WithImageUrl(gg);
                    Embed.WithFooter(yiffRuns + " times used total  |  [" + DateTime.Now + "]");
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());
                }
                catch (Exception ex)
                {
                    Embed.WithAuthor($"{Context.User.Username}#{Context.User.Discriminator}, An error has occured:", Context.User.GetAvatarUrl());
                    Embed.WithColor(rand.Next(255), rand.Next(255), rand.Next(255));
                    Embed.WithDescription("``" + ex + "``");
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());
                    return;
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync($"I can't execute nsfw command in non-nsfw channel! {!Context.Channel.IsNsfw}");
                return;
            }
        }



        [Command("yifflist"), Summary("List of yiff images sender")]
        public async Task YiffList()
        {
            Console.Write($"[" + DateTime.Now + $" at Commands] ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"{Context.Message.Author.Username}#{Context.Message.Author.Discriminator}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" used command skap!yifflist\n");

            string logCommand = ($"[" + DateTime.Now + $" at Commands] {Context.Message.Author.Username}#{Context.Message.Author.Discriminator} used command skap!yifflist");
            Program.CommandsLog(logCommand);

            Console.Write("[" + DateTime.Now + $" at Yiff list] Sending lists to ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"{Context.Message.Author.Username}#{Context.Message.Author.Discriminator}");
            Console.ForegroundColor = ConsoleColor.Gray;

            await Context.Channel.SendMessageAsync("Wait to uploading competion");// + Context.Client.ShardId.ToString());

            DirectoryInfo dd = new DirectoryInfo(@"Data\Yiff\List\");
            var files = dd.GetFiles();
            foreach (var file in files)
            {
                await Context.Channel.SendFileAsync(file.ToString());
            }
        }
    }
}
