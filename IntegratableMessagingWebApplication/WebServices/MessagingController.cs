using Newtonsoft.Json;
using SoftwarePeople.General.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IntegratableMessagingWebApplication.WebServices
{
    public class MessagingController : ApiController
    {
        // GET api/<controller>
        public MessagingModel Get()
        {
            return new MessagingDataMapper().GetData();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
            new MessagingDataMapper().SaveData(value);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {

        }

    }

    public class MessagingModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ReplyOptions { get; set; }
        public string ReplyEndpoint { get; set; }
    }

    public class MessagingDataMapper
    {
        private IDatabaseHelper _DbHelper;
        public MessagingDataMapper()
        {
            _DbHelper = new DatabaseHelper(ConfigurationManager.AppSettings["conn"]);
        }

        public MessagingModel GetData()
        {
            DataParam[] dataParams = { };
            DataHelperReturn dataHelper = _DbHelper.GetDataSet("GetData", dataParams);

            MessagingModel model = new MessagingModel();

            foreach (DataRow row in dataHelper.ReturnDataSet.Tables[0].Rows)
            {
                //model.Id = Convert.ToInt32(row["Id"]);
                model.Name = row["Name"].ToString();
                model.Message = row["Message"].ToString();
                //model.Type = row["Type"].ToString();
                //model.ReplyOptions = row["ReplyOptions"].ToString();
                //model.ReplyEndpoint = row["ReplyEndpoint"].ToString();
            }
            return model;
        }

        internal void SaveData(string value)
        {
            Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(value);
            MessagingModel message = new MessagingModel
            {
                Name = values["Name"].ToString(),
                Message = values["Message"].ToString()
                //Type = values["Type"].ToString(),
                //ReplyOptions = values["ReplyOptions"].ToString(),
                //ReplyEndpoint = values["ReplyEndpoint"].ToString()
            };

            DataParam[] dataParams = {                            
                            new DataParam("@Name", message.Name),
                            new DataParam("@Message", message.Message)
                            //,
                            //new DataParam("@Type", message.Message),
                            //new DataParam("@ReplyOptions", message.Message),
                            //new DataParam("@ReplyEndpoint", message.Message)
                    };

            _DbHelper.ExecuteNonQuery("SaveData", dataParams);
        }
    }
}