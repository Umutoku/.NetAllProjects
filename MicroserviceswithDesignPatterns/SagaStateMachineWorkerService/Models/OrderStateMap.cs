﻿using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaStateMachineWorkerService.Models
{
    public class OrderStateMap : SagaClassMap<OrderStateInstance>
    {
        protected override void Configure(EntityTypeBuilder<OrderStateInstance> entity, ModelBuilder model)
        {
            entity.Property(x => x.CurrentState).HasMaxLength(64);
            entity.Property(x => x.BuyerId).HasMaxLength(64);
            entity.Property(x => x.CardName).HasMaxLength(64);
            entity.Property(x => x.CardNumber).HasMaxLength(64);
            entity.Property(x => x.CardType).HasMaxLength(64);

        }
    }
}
