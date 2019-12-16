﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectSdk
{
    public class DoublePoint
    {
        public DoublePoint(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }
    }
}