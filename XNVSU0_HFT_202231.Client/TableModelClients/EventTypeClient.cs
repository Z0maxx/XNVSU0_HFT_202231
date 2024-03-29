﻿using System.ComponentModel;
using XNVSU0_HFT_202231.Models.TableModels;

namespace XNVSU0_HFT_202231.Client
{
    [DisplayName("Event type")]
    class EventTypeClient : TableModelClient<EventType>, IClient
    {
        public EventTypeClient(RestService rest, string[] args)
            :base(rest, args, new string[] { "Id", "Name" })
        {
        }
    }
}