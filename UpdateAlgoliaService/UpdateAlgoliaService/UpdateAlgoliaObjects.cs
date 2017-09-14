using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace UpdateAlgoliaService
{
   public class UpdateAlgoliaObjects: AlgoliaRefrence
    {
        public static void WriteErrorLog(Exception ex)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + ex.Source.ToString().Trim() + "; " + ex.Message.ToString().Trim());
                sw.Flush();
                sw.Close();
            }
            catch (Exception exsw)
            {
                throw exsw;
            }
        }

        public static void WriteErrorLog(string Message)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + Message);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex);
            }
        }

        /// <summary>
        /// Add new data..
        /// </summary>
        private void AddJsonSQLObjects()
        {
            try
            {
                string queryString = "SELECT A.[Id] objectID, D.Name [Destination] ,ATY.EnumName[AccommodationType] ,A.[Name]" +
                    ",[Features] ,[ImageUrl] ,[Price] ,CAST([LatitudeLongitude].Lat as nvarchar(20)) Latitude" +
                    ",CAST([LatitudeLongitude].Long as nvarchar(20)) Longitude ,[CreateDate] ,A.[Active] FROM[algoliasearch].[dbo].[Accommodation] A " +
                    "INNER JOIN[algoliasearch].[dbo].[AccommodationType] ATY ON ATY.id = A.AccommodationTypeId " +
                    "INNER JOIN[algoliasearch].[dbo].[Destination] D ON D.id = A.DestinationId";

                List<AccomidationModel> searchresults = new List<AccomidationModel>();
                List<JObject> Uploadobjs = new List<JObject>();

                //string connectionString = "Data Source=GRACEBENNETT;Initial Catalog = algoliasearch; Integrated Security=SSPI";
                using (SqlConnection connection = new SqlConnection(ALGOLIA_DB_CONNSTR))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    //command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {

                        while (reader.Read())
                        {
                            AccomidationModel ACM = new AccomidationModel();

                            ACM.objectID = reader["objectID"].ToString();
                            ACM.Destination = reader["Destination"].ToString();
                            ACM.AccommodationType = reader["AccommodationType"].ToString();
                            ACM.Name = reader["Name"].ToString();
                            ACM.Features = reader["Features"].ToString();
                            ACM.ImageUrl = reader["ImageUrl"].ToString();
                            ACM.Price = reader["Price"].ToString();
                            ACM.Latitude = reader["Latitude"].ToString();
                            ACM.Longitude = reader["Longitude"].ToString();
                            ACM.CreateDate = reader["CreateDate"].ToString();
                            ACM.Active = (bool)reader["Active"];

                            searchresults.Add(ACM);
                            JObject JOBJ = (JObject)JToken.FromObject(ACM);
                            Uploadobjs.Add(JOBJ);
                        }
                    }
                    finally
                    {
                        connection.Close();
                    }
                }

                var json = JsonConvert.SerializeObject(new { Hotels = searchresults });

                var task = _index.AddObjects(Uploadobjs);
                _index.WaitTask(task["taskID"].ToString());

                //recon purpose un comment below while testing or debugging
                //var res = _index.Search(new Query(""));
                //var ret = res["nbHits"].ToObject<int>();
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex);
            }
        }

        public static void UpdateAlgoliaObject()
        {
            //UpdateJsonSQLObjects();
        }
        /// <summary>
        /// Update data 
        /// </summary>
        public void UpdateJsonSQLObjects()
        {
            try
            {
                string queryString = "SELECT A.[Id] objectID, D.Name [Destination] ,ATY.EnumName[AccommodationType] ,A.[Name]" +
                    ",[Features] ,[ImageUrl] ,[Price] ,CAST([LatitudeLongitude].Lat as nvarchar(20)) Latitude" +
                    ",CAST([LatitudeLongitude].Long as nvarchar(20)) Longitude ,[CreateDate] ,A.[Active] FROM[algoliasearch].[dbo].[Accommodation] A " +
                    "INNER JOIN[algoliasearch].[dbo].[AccommodationType] ATY ON ATY.id = A.AccommodationTypeId " +
                    "INNER JOIN[algoliasearch].[dbo].[Destination] D ON D.id = A.DestinationId";

                List<AccomidationModel> searchresults = new List<AccomidationModel>();
                List<JObject> Uploadobjs = new List<JObject>();

                //string connectionString = "Data Source=GRACEBENNETT;Initial Catalog = algoliasearch; Integrated Security=SSPI";
                using (SqlConnection connection = new SqlConnection(ALGOLIA_DB_CONNSTR))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    //command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {

                        while (reader.Read())
                        {
                            AccomidationModel ACM = new AccomidationModel();

                            ACM.objectID = reader["objectID"].ToString();
                            ACM.Destination = reader["Destination"].ToString();
                            ACM.AccommodationType = reader["AccommodationType"].ToString();
                            ACM.Name = reader["Name"].ToString();
                            ACM.Features = reader["Features"].ToString();
                            ACM.ImageUrl = reader["ImageUrl"].ToString();
                            ACM.Price = reader["Price"].ToString();
                            ACM.Latitude = reader["Latitude"].ToString();
                            ACM.Longitude = reader["Longitude"].ToString();
                            ACM.CreateDate = reader["CreateDate"].ToString();
                            ACM.Active = (bool)reader["Active"];

                            searchresults.Add(ACM);
                            JObject JOBJ = (JObject)JToken.FromObject(ACM);
                            Uploadobjs.Add(JOBJ);
                        }
                    }
                    finally
                    {
                        connection.Close();
                    }
                }

                var json = JsonConvert.SerializeObject(new { Hotels = searchresults });

                var task = _index.SaveObjects(Uploadobjs);
                _index.WaitTask(task["taskID"].ToString());

                //recon purpose un comment below while testing or debugging
                //var res = _index.Search(new Query(""));
                //var ret = res["nbHits"].ToObject<int>();
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex);
            }
        }
    }
}
