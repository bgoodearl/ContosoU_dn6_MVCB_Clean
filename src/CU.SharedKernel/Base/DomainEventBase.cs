﻿using MediatR;

namespace CU.SharedKernel.Base
{
    public abstract class DomainEventBase : INotification
    {
        public DateTime WhenOccurred { get; protected set; } = DateTime.UtcNow;
    }
}