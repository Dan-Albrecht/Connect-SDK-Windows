﻿using Windows.Data.Json;
using ConnectSdk.Windows.Core;

namespace ConnectSdk.Windows.Device.Netcast
{
    public class NetcastChannelParser
    {
        public JsonArray ChannelArray;
        public JsonObject Channel;

        public string Value;

        public string ChannelType = "chtype";
        public string Major = "major";
        public string Minor = "minor";
        public string DisplayMajor = "displayMajor";
        public string DisplayMinor = "displayMinor";
        public string SourceIndex = "sourceIndex";
        public string PhysicalNum = "physicalNum";
        public string ChannelName = "chname";
        public string ProgramName = "progName";
        public string AudioChannel = "audioCh";
        public string InputSourceName = "inputSourceName";
        public string InputSourceType = "inputSourceType";
        public string LabelName = "labelName";
        public string InputSourceIndex = "inputSourceIdx";

        public NetcastChannelParser()
        {
            ChannelArray = new JsonArray();
            Value = null;
        }

        public void Characters(char[] ch, int start, int length)
        {
            Value = new string(ch, start, length);
        }

        public JsonArray GetJsonChannelArray()
        {
            return ChannelArray;
        }

        public static ChannelInfo ParseRawChannelData(JsonObject channelRawData)
        {
            string channelName = null;
            string channelId = null;
            var minorNumber = 0;
            var majorNumber = 0;

            var channelInfo = new ChannelInfo {RawData = channelRawData};

            try
            {
                if (!channelRawData.ContainsKey("channelName"))
                    channelName = channelRawData.GetNamedString("channelName");

                if (!channelRawData.ContainsKey("channelId"))
                    channelId = channelRawData.GetNamedString("channelId");

                if (!channelRawData.ContainsKey("majorNumber"))
                    majorNumber = (int)channelRawData.GetNamedNumber("majorNumber");

                if (!channelRawData.ContainsKey("minorNumber"))
                    minorNumber = (int)channelRawData.GetNamedNumber("minorNumber");

                string channelNumber = !channelRawData.ContainsKey("channelNumber") 
                    ? channelRawData.GetNamedString("channelNumber") 
                    : string.Format("{0}-{1}", majorNumber, minorNumber);

                channelInfo.ChannelName = channelName;
                channelInfo.ChannelId = channelId;
                channelInfo.ChannelNumber = channelNumber;
                channelInfo.MajorNumber = majorNumber;
                channelInfo.MinorNumber = minorNumber;

            }
            catch
            {
                //TODO: get some analysis here
                throw;
            }

            return channelInfo;
        }
    }
}