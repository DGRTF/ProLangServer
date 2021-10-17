using System.Collections.Generic;

namespace TemplateDataLayer.Models.ResponseModels
{
    public class FindOrdersResponseModel
    {
        public FindOrdersResponseModel(List<Order> orders, long count)
        {
            Orders = orders;
            Count = count;
        }
        public List<Order> Orders { get; set; }
        public long Count { get; set; }
    }
}
