﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Mubox.Extensibility;

namespace Mubox.Control
{
    public static class FileReplicationManager
    {
        public static void PerformReplication(Configuration.FileReplicationSettingCollection fileReplicationSettings)
        {
            foreach (var item in fileReplicationSettings.OfType<Configuration.FileReplicationSetting>())
            {
                try
                {
                    if (File.Exists(item.Source))
                    {
                        if (File.Exists(item.Destination))
                        {
                            File.Delete(item.Destination);
                        }
                        else if (!Directory.Exists(Path.GetDirectoryName(item.Destination)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(item.Destination));
                        }
                        File.Copy(item.Source, item.Destination, true);
                        ("ReplicationSuccess for " + item).Log();
                    }
                }
                catch (Exception ex)
                {
                    ("FileReplicationFailed for " + item).Log();
                    ex.Log();
                }
            }
        }
    }
}