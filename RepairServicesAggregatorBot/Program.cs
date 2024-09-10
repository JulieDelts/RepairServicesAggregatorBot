using Microsoft.VisualBasic;
using RepairServicesAggregatorBot.Bot;
using RepairServicesAggregatorBot.Bot.States.AdminStates;
using RepairServicesAggregatorBot.Bot.States.ClientStates;
using RepairServicesAggregatorBot.Bot.States.ContractorStates;
using RepairServicesAggregatorBot.Bot.States.SystemStates.RegisteringUser;
using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.DAL;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


namespace RepairServicesAggregatorBot
{
    public class Program
    {
        public static Dictionary<long, Context> Users { get; set; } /// посылать уведомления. принудительно отправить письмо (вызвать реакт)

        static async Task Main(string[] args)
        {
            Users = new Dictionary<long, Context>();

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

            Console.WriteLine("The bot is functioning.");

            await Task.Delay(-1);
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message)
            {
                HandleMessage(update, botClient);
            }
            else if (update.Type == UpdateType.CallbackQuery)
            {
                HandleCallback(update, botClient);
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Error! {exception.Message}");
        }

        private static void HandleMessage(Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            Context currentUser = GetUserFromMessage(message);

            if (message.Text != null && message.Text.ToLower() == "/start")
            {
                SetBaseState(currentUser);
            }
            else
            {
                currentUser.HandleMessage(update, botClient);
            }

            currentUser.ReactInBot(botClient);
        }

        private static Context GetUserFromMessage(Message message)
        {
            Context currentUser;

            if (Users.ContainsKey(message.Chat.Id))
            {
                currentUser = Users[message.Chat.Id];
            }
            else
            {
                currentUser = new Context();

                currentUser.ChatId = message.Chat.Id;

                try
                {
                    UserService userService = new UserService();

                    var clientModel = userService.GetUserByChatId(message.Chat.Id);

                    currentUser.Id = clientModel.Id;
                    currentUser.RoleId = clientModel.RoleId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Users.Add(message.Chat.Id, currentUser);
            }

            return currentUser;
        }

        private static void HandleCallback(Update update, ITelegramBotClient botClient)
        {
            var callback = update.CallbackQuery;

            Context currentUser = GetUserFromCallback(callback);

            currentUser.HandleCallbackQuery(update, botClient);

            currentUser.ReactInBot(botClient);
        }

        private static Context GetUserFromCallback(CallbackQuery callback)
        {
            Context currentUser;

            if (Users.ContainsKey(callback.From.Id))
            {
                currentUser = Users[callback.From.Id];
            }
            else
            {
                currentUser = new Context();

                currentUser.ChatId = callback.From.Id;

                try
                {
                    UserService userService = new UserService();

                    var clientModel = userService.GetUserByChatId(callback.From.Id);

                    currentUser.Id = clientModel.Id;
                    currentUser.RoleId = clientModel.RoleId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Users.Add(callback.From.Id, currentUser);
            }

            return currentUser;
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
                context.State = new ContractorMenuState();
            }
            else if (context.RoleId == 3)
            {
                context.State = new AdminMenuState();
            }

            return context;
        }
    }
}
