﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class DIContainer
    {
        public static IServiceProvider ServiceProvider { get; set; } = default!;
    }
}
