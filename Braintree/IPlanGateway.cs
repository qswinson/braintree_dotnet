﻿using System;
using System.Collections.Generic;

namespace Braintree
{
    public interface IPlanGateway
    {
        List<IPlan> All();
    }
}