using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairServicesProviderBot.Core.OutputModels
{
    public class UnConfirmedOrderOutputModel: OrderOutputModel
    {
        public List<ContractorWithServiceTypeOutputModel>? AvailableContractors { get; set; }

        public override string ToString()
        {
            return $"Заказчик {Client} Статус {Status ?? "не указано"} Дата {Date ?? "не указано"} Описание {OrderDescription ?? "не указано"} Адрес {Address ?? "не указано"}";
        }
    }
}
