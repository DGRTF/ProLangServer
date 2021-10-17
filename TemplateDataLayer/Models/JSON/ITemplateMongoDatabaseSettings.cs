using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateDataLayer.Models.JSON
{
    public interface ITemplateMongoDatabaseSettings
    {
        public string OrdersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
