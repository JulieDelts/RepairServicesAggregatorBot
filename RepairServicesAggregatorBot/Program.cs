using RepairServicesAggregatorBot.Bot;
using RepairServicesAggregatorBot.Bot.States.OrderStates.CreatingOrderStates;
using RepairServicesAggregatorBot.Bot.States.SystemStates;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using RepairServicesAggregatorBot.Bot;
using RepairServicesAggregatorBot.Bot.States.OrderStates.CreatingOrderStates;


namespace RepairServicesAggregatorBot
{
    public class Program
    {
        public static Dictionary<long, Context> Clients { get; set; }

        static async Task Main(string[] args)
        {
            Clients = new Dictionary<long, Context>();

            ITelegramBotClient bot = new TelegramBotClient("7505426475:AAGJh965t27_zyj5g88IY5Mh7_3pDddP0wg");
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
                Console.WriteLine(message.Chat.Id);

                Context crntClient;

                if (Clients.ContainsKey(message.Chat.Id))
                {
                    Console.WriteLine("уже есть");
                    crntClient = Clients[message.Chat.Id];
                }
                else
                {
                    Console.WriteLine("добавил нового");
                    //Сохраняем его в базку или загружаем
                    crntClient = new Context();
                    crntClient.ChatId = message.Chat.Id;
                    Clients.Add(message.Chat.Id, crntClient);
                }

                Console.WriteLine(message.Text);
                try
                {
                    if (message.Text.ToLower() == "/start")
                    {
                        crntClient.State = new AddingDescriptionOrderState();
                    }
                    else
                    {
                        crntClient.HandleMessage(update);
                    }
                }
                catch(Exception ex)
                {

                }
                finally
                {
                    crntClient.ReactInBot(botClient);
                    await Task.CompletedTask;
                }



                
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Error!!!! {exception.ToString()}");
        }
    }
}