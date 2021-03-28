using System;

namespace Backend.Module
{
   public interface IDataHandler {
      string ParseToElasticBulkFormat(string bulkJson, string idProperty, string index);
   }
}