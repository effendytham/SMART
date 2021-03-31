namespace Backend.Module.Services
{
    public class AppSettings
    {
        public AppSettings()
        {
            ElasticSearch = new ElasticSearchSettings();
        }
        public ElasticSearchSettings ElasticSearch { get; set; }
    }
}