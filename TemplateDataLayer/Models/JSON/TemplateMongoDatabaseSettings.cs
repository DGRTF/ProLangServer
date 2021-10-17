namespace TemplateDataLayer.Models.JSON
{
    public class TemplateMongoDatabaseSettings : ITemplateMongoDatabaseSettings
    {
        public string OrdersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
