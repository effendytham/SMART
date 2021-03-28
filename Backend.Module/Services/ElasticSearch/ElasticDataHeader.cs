namespace Backend.Module.Services.ElasticSearch
{
    public class ElasticDataHeader
    {
        public ElasticDataHeader(string prmIndex, string prmId) {
            index = new ElasticDataHeaderIndex();
            index._index = prmIndex;
            index._id = prmId;
        }
        public ElasticDataHeaderIndex index { get; set; }
    }
}