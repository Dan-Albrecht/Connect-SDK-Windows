﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ConnectSdk
{
    class JsonValue
    {
        internal static string CreateStringValue(string iD)
        {
            return iD;
        }

        internal static double CreateNumberValue(double lastConnected)
        {
            return lastConnected;
        }

        internal static int CreateNumberValue(int lastConnected)
        {
            return lastConnected;
        }

        internal static bool CreateBooleanValue(bool shouldLoop)
        {
            return shouldLoop;
        }
    }
}
