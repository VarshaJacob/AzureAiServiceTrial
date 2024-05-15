using Azure.Search.Documents.Indexes;
using Microsoft.Azure.Search;
using System.ComponentModel.DataAnnotations;

namespace AzureAIServiceTrialApi
{
    public class IndexFields
    {
        [Key]
        [IsFilterable, IsSortable]
        public string id { get; set; }

        [SearchableField]
        public string title { get; set; }


        [SearchableField]
        public string content { get; set; }

        public string url { get; set; }

        public string filepath { get; set; }
        public string chunk_id { get; set; }
        public string last_updated { get; set; }

        //public string metadata_storage_name { get; set; }
        //public string metadata_storage_path { get; set; }
        //public string metadata_storage_last_modified { get; set; }

    }
}
