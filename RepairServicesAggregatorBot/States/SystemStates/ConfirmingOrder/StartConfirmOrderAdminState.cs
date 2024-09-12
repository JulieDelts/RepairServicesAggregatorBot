using System.Text;
using System.Text.RegularExpressions;
using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesProviderBot.Core.OutputModels;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates.ConfirmingOrder
{
    public class StartConfirmOrderAdminState : AbstractState
    {
        public InitialOrderOutputModel InitialOrderOutputModel { get; set; }

        private int _messageId;

        private bool _isIdError;

        public StartConfirmOrderAdminState(int messageId, InitialOrderOutputModel initialOrderOutputModel)
        {
            InitialOrderOutputModel = initialOrderOutputModel;

            _messageId = messageId;

            _isIdError = false;
        }

        public override async void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            if (IsIdValid(message.Text))
            {
                try
                {
                    int id = Convert.ToInt32(message.Text);

                    ServiceTypeService serviceTypeService = new();

                    var serviceType = serviceTypeService.GetServiceTypeById(id);

                    ContractorService contractorService = new();

                    var contractors = contractorService.GetContractorsByServiceTypeId(id);

                    OrderService orderService = new();

                    var order = orderService.GetOrderSystemInfoById(InitialOrderOutputModel.Id);

                    var updatedOrder = new ExtendedOrderInputModel()
                    {
                        Id = order.Id,
                        ClientId = order.ClientId,
                        ContractorId = order.ContractorId,
                        AdminId = context.Id,
                        StatusId = 1,
                        ServiceTypeId = id,
                        Date = order.Date,
                        OrderDescription = order.OrderDescription,
                        Address = order.Address,
                        IsDeleted = order.IsDeleted
                    };

                    context.State = new CompleteConfirmOrderAdminState(updatedOrder, contractors);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    _isIdError = true;
                }
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            if (_isIdError)
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "ID введен некорректно.");
            }
            else
            {
                ServiceTypeService serviceTypeService = new();

                var services = serviceTypeService.GetAvailableServices();

                StringBuilder servicesDescription = new("Услуги:\n");

                for (int i = 0; i < services.Count; i++)
                {
                    if (services[i].IsDeleted == true)
                    {
                        continue;
                    }

                    servicesDescription.Append($"{i + 1}. {services[i].ServiceTypeDescription}\nID: {services[i].Id}\n");
                }

                var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, $"{servicesDescription}\nВведите ID услуги: ");
            }
        }

        private bool IsIdValid(string id)
        {
            return Regex.IsMatch(id, @"[0-9]+");
        }
    }
}
