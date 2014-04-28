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
        public void Post(MessagingModel message)
        {
            new MessagingDataMapper().SaveData(message);
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
    }

    public class MessagingDataMapper
    {
        private IDatabaseHelper _DbHelper;
        public MessagingDataMapper()
        {
            _DbHelper = new DatabaseHelper(ConfigurationManager.AppSettings["conn"]);
        }
        
        public void SaveData(MessagingModel message)
        {
            DataParam[] dataParams = {
                            new DataParam("@Id",message.Id),
                            new DataParam("@Name", message.Name),
                            new DataParam("@Message", message.Message)
                    };

            _DbHelper.ExecuteNonQuery("SaveData", dataParams);
        }

        public MessagingModel GetData()
        {
             DataParam[] dataParams = {};
             DataHelperReturn dataHelper = _DbHelper.GetDataSet("GetData", dataParams);

             MessagingModel model = new MessagingModel();

             foreach (DataRow row in dataHelper.ReturnDataSet.Tables[0].Rows)
             {
                 model.Id = Convert.ToInt32(row["Id"]);
                 model.Name = row["Name"].ToString();
                 model.Message = row["Message"].ToString();
             }
             return model;
        }
    }
}