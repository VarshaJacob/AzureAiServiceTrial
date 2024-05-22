using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Microsoft.Azure.Search;
using System.ComponentModel.DataAnnotations;

namespace AzureAIServiceTrialApi
{
    public class IndexFields
    {
        [SearchableField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string id { get; set; }

        [SearchableField]
        public string title { get; set; }


        [SearchableField]
        public string content { get; set; }

        [SearchableField]
        public string contentVector { get; set; }

        [SearchableField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string parent_id { get; set; }

        public string url { get; set; }

        public string filepath { get; set; }

        [SearchableField(IsKey = true, IsFilterable = true, IsSortable = true, IsFacetable = true, AnalyzerName = LexicalAnalyzerName.Values.Keyword)]
        public string chunk_id { get; set; }
        public string last_updated { get; set; }

        //public string metadata_storage_name { get; set; }
        //public string metadata_storage_path { get; set; }
        //public string metadata_storage_last_modified { get; set; }

    }
}
