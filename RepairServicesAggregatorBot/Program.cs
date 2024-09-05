using System.Security.Principal;
using RepairServicesAggregatorBot.Bot;
using RepairServicesAggregatorBot.Bot.States.ClientStates;
using RepairServicesAggregatorBot.Bot.States.OrderStates.CreatingBaseOrderStates;
using RepairServicesAggregatorBot.Bot.States.SystemStates;
using RepairServicesAggregatorBot.Bot.States.SystemStates.RegisteringUser;
using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.InputModels;
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

                try
                {
                    if (message.Text.ToLower() == "/start")
                    {
                        if (crntClient.Id == 0)
                        {
                            crntClient.State = new StartRegistrationSystemState();
                        }
                        else
                        {
                            crntClient.State = new AddingDescriptionOrderState();
                        }
                    }
                    else
                    {
                        crntClient.HandleMessage(update);
                    }
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