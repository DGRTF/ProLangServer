using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Template.Models.RequestModels
{
    public class FindOrdersModel : GetOrdersModel
    {
        public string Customer { get; set; }
    }
}
