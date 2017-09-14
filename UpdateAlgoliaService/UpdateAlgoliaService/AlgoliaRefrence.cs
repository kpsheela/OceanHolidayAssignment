using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algolia.Search;

namespace UpdateAlgoliaService
{
    public class AlgoliaRefrence
    {
        public static string _testApplicationID = "";
        public static string _testApiKey = "";
        private static System.Collections.Specialized.NameValueCollection AppSettings { get { return System.Configuration.ConfigurationManager.AppSettings; } }

        public AlgoliaClient _client;
        public Index _index;
        public IndexHelper<AccomidationModel> _indexHelper;

        public AlgoliaRefrence()
        {
            _testApiKey = ALGOLIA_API_KEY;
            _testApplicationID = ALGOLIA_APPLICATION_ID;
            _client = new AlgoliaClient(_testApplicationID, _testApiKey);
            _index = _client.InitIndex(ALGOLIA_INDEX_NAME);
            _indexHelper = new IndexHelper<AccomidationModel>(_client, ALGOLIA_INDEX_NAME);
        }

        /// <summary>
        /// ALGOLIA_API_KEY
        /// </summary>
        internal static string ALGOLIA_API_KEY
        {
            get
            {
                if (AppSettings == null) return string.Empty;
                else
                {
                    string value = AppSettings["ALGOLIA_API_KEY"];
                    if (string.IsNullOrEmpty(value)) return string.Empty;
                    else return value;
                }
            }
        }

        /// <summary>
        /// ALGOLIA_APPLICATION_ID
        /// </summary>
        internal static string ALGOLIA_APPLICATION_ID
        {
            get
            {
                if (AppSettings == null) return string.Empty;
                else
                {
                    string value = AppSettings["ALGOLIA_APPLICATION_ID"];
                    if (string.IsNullOrEmpty(value)) return string.Empty;
                    else return value;
                }
            }
        }

        /// <summary>
        /// ALGOLIA_APPLICATION_ID
        /// </summary>
        public static string ALGOLIA_INDEX_NAME
        {
            get
            {
                if (AppSettings == null) return string.Empty;
                else
                {
                    string value = AppSettings["ALGOLIA_INDEX_NAME"];
                    if (string.IsNullOrEmpty(value)) return string.Empty;
                    else return value;
                }
            }
        }

        /// <summary>
        /// ALGOLIA_APPLICATION_ID
        /// </summary>
        public static string ALGOLIA_DB_CONNSTR
        {
            get
            {
                if (AppSettings == null) return string.Empty;
                else
                {
                    string value = AppSettings["ALGOLIA_DB_CONNSTR"];
                    if (string.IsNullOrEmpty(value)) return string.Empty;
                    else return value;
                }
            }
        }
    }
    public class AccomidationModel
    {
        public string objectID { get; set; }
        public string Destination { get; set; }
        public string AccommodationType { get; set; }
        public string Name { get; set; }
        public string Features { get; set; }
        public string ImageUrl { get; set; }
        public string Price { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string CreateDate { get; set; }
        public bool Active { get; set; }
    }
}
