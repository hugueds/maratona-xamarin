using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using File = System.IO.File;
using Microsoft.Owin.Hosting;

namespace ConsoleWebServer
{

    static class Bot
    {
        public static readonly TelegramBotClient Api = new TelegramBotClient("272213485:AAHLjof79Pt2buM5UyoVsKgYpZyHx8VA40c");
    }
    public class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("https://+:5000"))
            {
                Bot.Api.SetWebhookAsync("http://localhost:5000/WebHook");
                Console.WriteLine("Server has started!");

                Console.ReadLine();

                Bot.Api.SetWebhookAsync();
            } 
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();

            configuration.Routes.MapHttpRoute("WebHook", "{controller}");

            app.UseWebApi(configuration);
        }
    }


    public class WebHookController : ApiController
    {
        public async Task<IHttpActionResult> Post(Update update)
        {
            var message = update.Message;

            Console.WriteLine("Received Message from {0}", message.Chat.Id);

            if (message.Type == MessageType.TextMessage)
            {
                // Echo each Message
                await Bot.Api.SendTextMessageAsync(message.Chat.Id, message.Text);
            }
            else if (message.Type == MessageType.PhotoMessage)
            {
                // Download Photo
                var file = await Bot.Api.GetFileAsync(message.Photo.LastOrDefault()?.FileId);

                var filename = file.FileId + "." + file.FilePath.Split('.').Last();

                using (var saveImageStream = File.Open(filename, FileMode.Create))
                {
                    await file.FileStream.CopyToAsync(saveImageStream);
                }

                await Bot.Api.SendTextMessageAsync(message.Chat.Id, "Thx for the Pics");
            }

            return Ok();
        }
    }
}
