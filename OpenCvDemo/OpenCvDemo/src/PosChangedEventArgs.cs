using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageExDemo
{

    public class PosChangedEventArgs : EventArgs
    {
        public Point Pos { get; protected set; }

        public object[] ChannelValues { get; protected set; }


        public PosChangedEventArgs(Point pos, object[] channelsValue)
        {
            Pos = pos;
            ChannelValues = channelsValue;
        }

    }
}
