using RepairServicesAggregatorBot.Bot;
using RepairServicesAggregatorBot.Bot.States.AdminStates;
using RepairServicesAggregatorBot.Bot.States.ClientStates;
using RepairServicesAggregatorBot.Bot.States.SystemStates.RegisteringUser;
using RepairServicesProviderBot.BLL;
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

            ITelegramBotClient bot = new TelegramBotClient("7500546786:AAF-jAJEnauxeKOIrNA1MVhoGFuFjSxOzF0");
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
                    crntClient = new Context();
                    crntClient.ChatId = message.Chat.Id;
                    try
                    {
                        UserService userService = new UserService();
                        var clientModel = userService.GetUserByChatId(message.Chat.Id);
                        crntClient.Id = clientModel.Id;
                        crntClient.RoleId = clientModel.RoleId;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }

                    Clients.Add(message.Chat.Id, crntClient);
                }


                if (message.Text.ToLower() == "/start")
                {
                    SetBaseState(crntClient);
                }
                else
                {
                    crntClient.HandleMessage(update, botClient);
                }

                crntClient.ReactInBot(botClient);

            }
            else if (update.Type == UpdateType.CallbackQuery)
            {
                var callback = update.CallbackQuery;

                Context crntClient;

                if (Clients.ContainsKey(callback.From.Id))
                {
                    crntClient = Clients[callback.From.Id];
                }
                else
                {
                    crntClient = new Context();
                    crntClient.ChatId = callback.From.Id;
                    try
                    {
                        UserService userService = new UserService();
                        var clientModel = userService.GetUserByChatId(callback.From.Id);
                        crntClient.Id = clientModel.Id;
                        crntClient.RoleId = clientModel.RoleId;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }

                    Clients.Add(callback.From.Id, crntClient);
                }

                crntClient.HandleMessage(update, botClient);

                crntClient.ReactInBot(botClient);
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Error!!!! {exception.ToString()}");
        }

        private static Context SetBaseState(Context context)
        {
            if (context.Id == 0)
            {
                context.State = new StartRegistrationSystemState();
            }
            else if (context.RoleId == 1)
            {
                context.State = new ClientMenuState();
            }
            else if (context.RoleId == 2)
            {
                //contractor menu
            }
            else if (context.RoleId == 3)
            {
                context.State = new AdminMenuState();
            }

            return context;
        }
    }
}