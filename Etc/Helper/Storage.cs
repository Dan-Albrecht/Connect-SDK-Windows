#region Copyright Notice
/*
 * ConnectSdk.Windows
 * storage.cs
 * 
 * Copyright (c) 2015, https://github.com/sdaemon
 * Created by Sorin S. Serban on 22-4-2015,
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
using ConnectSdk.Windows.Core;
using System.Collections.Concurrent;

namespace ConnectSdk.Windows.Etc.Helper
{
    public class Storage
    {
        // BUGBUG: Need to persit this as some point
        private readonly ConcurrentDictionary<string, object> settings = new ConcurrentDictionary<string, object>();

        public const string StoredDevicesKeyName = "StoredDevices";
        public const string StoredKeysKeyName = "StoredKeys";
        public const string StoredVibrationKeyName = "VibrationSetting";

        /// <summary>
        /// Constructor that gets the application settings.
        /// </summary>
        public Storage()
        {
        }

        /// <summary>
        /// Update a setting value for our application. If the setting does not
        /// exist, then add the setting.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool AddOrUpdateValue(string key, Object value)
        {
            if (this.settings.TryGetValue(key, out object existingValue) && existingValue == value)
            {
                return false;
            }

            if (value is string valueString)
            {
                value = StringCompressor.CompressString(valueString);
            }

            this.settings[key] = value;

            return true;
        }

        /// <summary>
        /// Get the current value of the setting, or if it is not found, set the 
        /// setting to the default setting.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T GetValueOrDefault<T>(string key, T defaultValue)
        {
            if (this.settings.TryGetValue(key, out object value))
            {
                if (value.GetType() == typeof(string))
                {
                    string stringValue = (string)value;
                    stringValue = StringCompressor.DecompressString(stringValue);
                    return (T)Convert.ChangeType(stringValue, typeof(T));
                }
                else
                {
                    return (T)value;
                }
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Save the settings.
        /// </summary>
        public void Save()
        {
            //settings.Save();
        }


        private static Storage current;

        public static Storage Current
        {
            get { return current ?? (current = new Storage()); }
        }

        public string StoredDevices
        {
            get
            {
                return GetValueOrDefault(StoredKeysKeyName, "");
            }
        }
    }
}

