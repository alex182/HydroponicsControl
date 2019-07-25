﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HydroponicsControl.Controllers.Common.Processor
{
    public interface IProcessorFactory
    {
        IProcessor Create(IProcessorRequest request);
    }
}
