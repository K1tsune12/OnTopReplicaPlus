using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OnTopReplica {
    public static class AppPaths {

        const string AppDataFolder = "OnTopReplicaPlus";

        public static void SetupPaths() {
            //Settings, logs and crash dumps all live together under
            //%LOCALAPPDATA%\OnTopReplicaPlus (local, non-roaming, portable).
            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var localAppDataApplicationPath = Path.Combine(localAppData, AppDataFolder);

            if (!Directory.Exists(localAppDataApplicationPath)) {
                Directory.CreateDirectory(localAppDataApplicationPath);
            }
            PrivateDataFolderPath = localAppDataApplicationPath;
        }

        public static string PrivateDataFolderPath { get; private set; }

        public static string GenerateCrashDumpPath() {
            var now = DateTime.Now;

            string dump = string.Format("OnTopReplicaPlus-dump-{0}{1}{2}-{3}{4}.txt",
                now.Year, now.Month, now.Day,
                now.Hour, now.Minute);

            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), dump);
        }
    }
}
