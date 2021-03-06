﻿using HPC.DAL.AdoNet;
using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace HPC.DAL.Impls.Base
{
    internal abstract class ImplerAsync
        : Impler
    {
        protected ImplerAsync(Context dc)
            : base(dc)
        {
            DSA = new DataSourceAsync(dc);
        }

        internal DataSourceAsync DSA { get; private set; }

    }
}
