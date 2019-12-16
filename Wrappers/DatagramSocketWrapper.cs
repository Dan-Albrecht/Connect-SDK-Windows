#region Copyright Notice
/*
 * ConnectSdk.Windows
 * DatagramSocketWrapper.cs
 * 
 * Copyright (c) 2015, https://github.com/sdaemon
 * Created by Sorin S. Serban on 8-5-2015,
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
#endregion
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using ConnectSdk.Windows.Fakes;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ConnectSdk.Windows.Wrappers
{
    // BUGBUG: Finish cleaning this up
    public class DatagramSocketWrapper
    {
        private readonly UdpClient client;
        //IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);

        public event EventHandler<string> MessageReceived;

        public DatagramSocketWrapper(IPEndPoint adapter)
        {
            if (MessageFakeFactory.Instance != null)
            {
                MessageFakeFactory.Instance.NewDatagraMessage += (sender, s) =>
                {
                    if (MessageReceived != null)
                        MessageReceived(this, s);
                };
            }
            else
            {
                this.client = new UdpClient(adapter);
                Task.Run(this.Receive);
            }
        }

        public void JoinMulticastGroup(IPAddress host)
        {
            if (MessageFakeFactory.Instance != null)
            {
                MessageFakeFactory.Instance.StartJoinMulticastGroup();
            }
            else
            {
                this.client.JoinMulticastGroup(host);
            }
        }

        private async Task Receive()
        {
            // BUGBUG: This seems wrong, what is the intent of this class
            while (true)
            {
                UdpReceiveResult result = await this.client.ReceiveAsync();

                var currentReceive = this.MessageReceived;

                if (currentReceive != null)
                {
                    string data = Encoding.UTF8.GetString(result.Buffer);
                    Console.WriteLine($"From {result.RemoteEndPoint} got: {data}");
                    currentReceive(this, data);
                }
            }
        }

        private static bool once = false;

        internal Task<int> Send(IPEndPoint target, byte[] reqBuff)
        {
            if (!once)
            {
                lock (this)
                {
                    if (!once)
                    {
                        once = true;
                        this.JoinMulticastGroup(target.Address);
                    }
                }
            }
            
            return this.client.SendAsync(reqBuff, reqBuff.Length, target);
        }
    }
}