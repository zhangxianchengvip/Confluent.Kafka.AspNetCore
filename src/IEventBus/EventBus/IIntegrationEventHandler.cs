﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public interface ISubscribeHandler<TValue>
    {
        Task Handle(TValue value);
    }
}

