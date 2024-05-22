using Azure;
using Azure.Search.Documents;
using AzureAiServiceTrial;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Azure.Search.Documents.Models;

namespace AzureAIServiceTrialApi
{
    public class AISearch
    {
        private AIOptions AIOptions;
        private BlobAccessOptions BlobAccessOptions;
        private SearchClient searchClient;
        private SearchIndexClient indexClient;
        private SearchIndexerClient indexerClient;
        public AISearch(AIOptions aiOptions, BlobAccessOptions blobAccessOptions)
        {
            AIOptions = aiOptions;
            BlobAccessOptions = blobAccessOptions;
            searchClient = new(new Uri(AIOptions.AISearchEndpoint), AIOptions.AISearchIndex, new AzureKeyCredential(AIOptions.AISearchKey));
            indexClient = new(new Uri(AIOptions.AISearchEndpoint), new AzureKeyCredential(AIOptions.AISearchKey));
            indexerClient = new(new Uri(AIOptions.AISearchEndpoint), new AzureKeyCredential(AIOptions.AISearchKey));
        }

        public async Task CreateIndex(string blobName)
        {
            try
            {
                //index
                var fieldBuidler = new FieldBuilder();
                var searchFields = fieldBuidler.Build(typeof(IndexFields));

                var index = new SearchIndex(AIOptions.AISearchIndex, searchFields);

                await indexClient.CreateOrUpdateIndexAsync(index);

                //data source
                var dataSource = new SearchIndexerDataSourceConnection(
               AIOptions.AIDataSource,
               SearchIndexerDataSourceType.AzureBlob,
               BlobAccessOptions.ConnectionString,
               new SearchIndexerDataContainer(BlobAccessOptions.ContainerName));

                await indexerClient.CreateOrUpdateDataSourceConnectionAsync(dataSource);

                //skillset
                var spiltSkill = new List<SearchIndexerSkill>
                {
                    // SplitSkill to split document content into pages
                    new SplitSkill(
                        new List<InputFieldMappingEntry>
                            {
                                new InputFieldMappingEntry("text") { Source = "/document/content" }
                            },
                        new List<OutputFieldMappingEntry>
                            {
                                new OutputFieldMappingEntry("textItems") { TargetName = "pages" }
                            }
                    )
                    {
                        Context = "/document",
                        TextSplitMode = TextSplitMode.Pages,
                        MaximumPageLength = 2000,
                        PageOverlapLength = 500
                    }
                    //,

                    //new AzureOpenAIEmbeddingSkill
                    //(
                    //    new List<InputFieldMappingEntry>
                    //    {
                    //        new InputFieldMappingEntry("text") { Source = "/document/pages/*" }
                    //    },
                    //    new List<OutputFieldMappingEntry>
                    //    {
                    //        new OutputFieldMappingEntry("embedding") { TargetName = "vector" }
                    //    }
                    //)
                    //{
                    //    Context = "/document/pages/*",
                    //    Description = "Embedding skill to generate content vector",
                    //    ResourceUri = new Uri( AIOptions.AzureOpenAIResourceUri),
                    //    DeploymentId = AIOptions.AzureOpenAIDeploymentName,
                    //    ApiKey = AIOptions.AzueOpenAIApiKey
                    //}
                };

                var skillset = new SearchIndexerSkillset(AIOptions.AISkillSet, spiltSkill)
                {
                    IndexProjections = new SearchIndexerIndexProjections(new[]
                    {
                        new SearchIndexerIndexProjectionSelector(AIOptions.AISearchIndex, parentKeyFieldName: "parent_id",
                            sourceContext: "/document/pages/*", mappings: new[]
                            {
                                new InputFieldMappingEntry("content")
                                {
                                    Source = "/document/pages/*"
                                },
                                //new InputFieldMappingEntry("contentVector")
                                //{
                                //    Source = "/document/pages/*/vector"
                                //},
                                new InputFieldMappingEntry("title")
                                {
                                    Source = "/document/metadata_storage_name"
                                },
                                 new InputFieldMappingEntry("filepath")
                                {
                                    Source = "/document/metadata_storage_name"
                                },
                                  new InputFieldMappingEntry("url")
                                {
                                    Source = "/document/metadata_storage_path"
                                },
                                  new InputFieldMappingEntry("last_updated")
                                {
                                    Source = "/document/metadata_storage_last_modified"
                                },
                            })
                    })
                    {
                        Parameters = new SearchIndexerIndexProjectionsParameters
                        {
                            ProjectionMode = IndexProjectionMode.SkipIndexingParentDocuments
                        }
                    }
                };

                await indexerClient.CreateOrUpdateSkillsetAsync(skillset);

                //indexer

                var schedule = new IndexingSchedule(TimeSpan.FromDays(1))
                {
                    StartTime = DateTimeOffset.Now
                };

                var parameters = new IndexingParameters()
                {
                    BatchSize = 100,
                    MaxFailedItems = 0,
                    MaxFailedItemsPerBatch = 0
                };

                var indexer = new SearchIndexer(AIOptions.AISearchIndexer, dataSource.Name, index.Name)
                {
                    Description = "indexer without chat playground",
                    Schedule = schedule,
                    Parameters = parameters,

                    FieldMappings =
                    {
                        new FieldMapping("metadata_storage_name") {TargetFieldName = "filepath"},
                        new FieldMapping("metadata_storage_path") {TargetFieldName = "url"},
                        new FieldMapping("metadata_storage_last_modified") {TargetFieldName = "last_updated"},
                    },
                    SkillsetName = AIOptions.AISkillSet
                };

                //can do a clean uo if required not done here

                await indexerClient.CreateOrUpdateIndexerAsync(indexer);

                await indexerClient.RunIndexerAsync(indexer.Name);

                var status = await indexerClient.GetIndexerStatusAsync(indexer.Name);

            }
            catch (Exception ex)
            {

            }

        }
    }
}
