﻿using HPC.DAL.Core.Bases;
using System;

namespace HPC.DAL.Core.Helper
{
    internal class ToStringHelper
    {
        private Context DC { get; set; }
        internal ToStringHelper(Context dc)
        {
            DC = dc;
        }

        /******************************************************************************************************************************************/

        internal string DateTime(string format)
        {
            var fcs = format.Trim();
            if ("yyyy-MM-dd".Equals(fcs, StringComparison.OrdinalIgnoreCase))
            {
                return "%Y-%m-%d";
            }
            else if ("yyyy-MM".Equals(fcs, StringComparison.OrdinalIgnoreCase))
            {
                return "%Y-%m";
            }
            else if ("yyyy".Equals(fcs, StringComparison.OrdinalIgnoreCase))
            {
                return "%Y";
            }
            else
            {
                throw DC.Exception(XConfig.EC._001, fcs);
            }
        }
    }
}
