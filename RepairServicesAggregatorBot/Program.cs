using RepairServicesAggregatorBot.Bot;
using RepairServicesAggregatorBot.Bot.States.OrderStates.CreatingOrderStates;
using RepairServicesAggregatorBot.Bot.States.SystemStates;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


namespace RepairServicesAggregatorBot
{
    public class Program
    {
        public static Dictionary<long, Context> Clients { get; set; }

        static async Task Main(string[] args)
        {
            Clients = new Dictionary<long, Context>();

            ITelegramBotClient bot = new TelegramBotClient("7038260400:AAF1gzbDgTjtrlP03nsLvmg3KplbTF9mzrE");
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

            await Task.Delay(-1);

        }
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update.Message.Text != null)
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


                if (message.Text == "/start")
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