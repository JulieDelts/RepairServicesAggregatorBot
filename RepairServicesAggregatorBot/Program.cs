using RepairServicesProviderBot.DAL;
using RepairServicesProviderBot.Core.DTOs;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using RepairServicesAggregatorBot.Bot;


namespace RepairServicesAggregatorBot
{
    public class Program
    {
        public static Dictionary<long, Context> Clients { get; set; }

        static void Main(string[] args)
        {
            Clients = new Dictionary<long, Context>();

            ITelegramBotClient bot = new TelegramBotClient("7469931637:AAFYfhGyHSnVwc6XCKv8v1iy5HFxpnKlYPo");
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }
            };

            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );

            Console.WriteLine("ЗАРАБОТАЛО!!!");

            Console.ReadLine();

        }
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message)
            {

                var message = update.Message;

                Context crntClient;

                if (Clients.ContainsKey(message.Chat.Id))
                {
                    crntClient = Clients[message.Chat.Id];
                }
                else
                {
                    //Сохраняем его в базку или загружаем
                    crntClient = new Context();
                    crntClient.ChatId = message.Chat.Id;
                    Clients.Add(message.Chat.Id, crntClient);
                }


                if (message.Text.ToLower() == "/start")
                {
                    crntClient.State = new LoginSystemState();
                }
                else
                {
                    crntClient.HandleMessage(update);
                }




                crntClient.ReactInBot(botClient);
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.ToString());
        }
    }
}