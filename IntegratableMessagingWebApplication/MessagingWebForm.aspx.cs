﻿using IntegratableMessagingWebApplication.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegratableMessagingWebApplication
{
    public partial class MessagingWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static MessagingModel GetMessage()
        {
            MessagingDataMapper messagingDataMapper = new MessagingDataMapper();
            return messagingDataMapper.GetData();
        }
    }
}