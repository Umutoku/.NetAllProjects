﻿using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedSaga.Interfaces
{
    public interface IStockReservedEvent : CorrelatedBy<Guid>
    {
        List<OrderItemMessage> OrderItemMessages { get; set; }
    }
}
