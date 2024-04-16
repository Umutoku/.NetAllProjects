﻿using SharedSaga.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedSaga.Events
{
    public class OrderRequestFailedEvent : IOrderRequestFailedEvent
    {
        public int OrderId { get; set; }
        public string Reason { get; set; }
    }
}