﻿#region Copyright Notice
/*
 * ConnectSdk.Windows
 * DlnaHttpServer.cs
 * 
 * Copyright (c) 2015, https://github.com/sdaemon
 * Created by Sorin S. Serban on 11-6-2015,
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
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ConnectSdk.Windows.Service.Command;
using System.Net;

namespace ConnectSdk.Windows.Service.Upnp
{
    // BUGBUG: This class isn't finished or vaidated
    public class DlnaHttpServer
    {
        public int Port = 49291;
        private const uint BufferSize = 8192;

        private HttpListener listener;
        private List<UrlServiceSubscription> subscriptions;

        public DlnaHttpServer()
        {
            Subscriptions = new List<UrlServiceSubscription>();
        }

        public bool IsRunning { get; set; }

        public List<UrlServiceSubscription> Subscriptions
        {
            get { return subscriptions; }
            set { subscriptions = value; }
        }

        private async Task ProcessRequestAsync(object socket)
        {
            await Task.Yield();
            /*
            var body = "";

            var request = new StringBuilder();
            using (var input = socket.InputStream)
            {
                var data = new byte[BufferSize];
                var buffer = data.AsBuffer();
                var dataRead = BufferSize;
                while (dataRead == BufferSize)
                {
                    await input.ReadAsync(buffer, BufferSize, InputStreamOptions.Partial);
                    body = Encoding.UTF8.GetString(data, 0, data.Length);
                    request.Append(body);
                    dataRead = buffer.Length;
                }
            }

            using (var output = socket.OutputStream)
            {
                DataWriter dr = null;
                try
                {
                    dr = new DataWriter(socket.OutputStream);
                    var message = new StringBuilder();
                    message.AppendLine("HTTP/1.1 200 OK");
                    message.AppendLine("Connection: Close");
                    message.AppendLine("Content-Length: 0");
                    dr.WriteString(message.ToString());
                    dr.StoreAsync();
                }
                catch
                {

                }
                finally
                {
                    if (dr != null)
                    {
                        dr.DetachStream();
                        dr.Dispose();
                    }
                }
            }

            if (body == null) return;

            //todo: add processing here*/
        }

        private async Task WriteResponseAsync(string path, object os)
        {
            using (Stream resp = (Stream)os)
            {
                bool exists = true;
                try
                {
                    // Look in the Data subdirectory of the app package
                    string filePath = "Data" + path.Replace('/', '\\');
                    //using (Stream fs = await LocalFolder.OpenStreamForReadAsync(filePath))
                    //{
                    //    string header = String.Format("HTTP/1.1 200 OK\r\n" +
                    //                                  "Content-Length: {0}\r\n" +
                    //                                  "Connection: close\r\n\r\n",
                    //        fs.Length);
                    //    byte[] headerArray = Encoding.UTF8.GetBytes(header);
                    //    await resp.WriteAsync(headerArray, 0, headerArray.Length);
                    //    await fs.CopyToAsync(resp);
                    //}
                }
                catch (FileNotFoundException)
                {
                    exists = false;
                }

                if (!exists)
                {
                    byte[] headerArray = Encoding.UTF8.GetBytes(
                        "HTTP/1.1 404 Not Found\r\n" +
                        "Content-Length:0\r\n" +
                        "Connection: close\r\n\r\n");
                    await resp.WriteAsync(headerArray, 0, headerArray.Length);
                }

                await resp.FlushAsync();
            }
        }

        public void Start()
        {
            if (IsRunning)
            {
                return;
            }
            this.listener = new HttpListener();
            this.listener.Prefixes.Add($"http://*:{this.Port}/");

            IsRunning = true;
        }

        public void Stop()
        {
            if (!IsRunning)
            {
                return;
            }

            foreach (UrlServiceSubscription sub in Subscriptions)
            {
                sub.Unsubscribe();
            }
            Subscriptions.Clear();
            IsRunning = false;
        }
    }
}
