﻿using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedSaga.Interfaces
{
    public interface IOrderRequestCompletedEvent 
    {
        int OrderId { get; set; }
    }
}
