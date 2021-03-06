﻿using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Enums;
using HPC.DAL.Impls.Base;
using HPC.DAL.Interfaces.ISyncs;
using System.Collections.Generic;
using System.Data;

namespace HPC.DAL.Impls.ImplSyncs
{
    internal sealed class CreateImpl<M>
        : ImplerSync
        , ICreate<M>
        where M : class
    {
        public CreateImpl(Context dc)
            : base(dc)
        { }

        public int Create(M m)
        {
            DC.Action = ActionEnum.Insert;
            CreateMHandle(new List<M> { m });
            PreExecuteHandle(UiMethodEnum.Create);
            return DSS.ExecuteNonQuery();
        }
    }
}
