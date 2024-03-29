﻿using System.ComponentModel;
using XNVSU0_HFT_202231.Models.TableModels;

namespace XNVSU0_HFT_202231.Client
{
    [DisplayName("Job")]
    class JobClient : TableModelClient<Job>, IClient
    {
        public JobClient(RestService rest, string[] args)
            :base(rest, args, new string[] { "Id", "Name" })
        {
        }
    }
}