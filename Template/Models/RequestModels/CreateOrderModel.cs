using System;

namespace Template.Models.RequestModels
{
    public class CreateOrderModel
    {
        /// <summary>
        /// ФИО заказчика
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// описание заказа
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// стоимость заказа
        /// </summary>
        public double Costs { get; set; }
    }
}
