using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TemplateDataLayer.Models
{
    public class Order
    {
        /// <summary>
        /// id заказа
        /// </summary>
        [BsonId]
        public Guid Id { get; set; }

        /// <summary>
        /// дата создания
        /// </summary>
        [BsonId]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// ФИО заказчика
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// описание заказа
        /// </summary>
        public string Ddescription { get; set; }

        /// <summary>
        /// стоимость заказа
        /// </summary>
        public double Costs { get; set; }
    }
}
