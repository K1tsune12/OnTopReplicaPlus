using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Forms;

namespace OnTopReplica.Properties {

    /// <summary>
    /// Application settings, persisted as a human-readable JSON file under
    /// <c>%LOCALAPPDATA%\OnTopReplicaPlus\settings.json</c>.
    /// </summary>
    /// <remarks>
    /// This is a drop-in replacement for the old auto-generated
    /// <c>ApplicationSettingsBase</c> settings. It keeps the exact same public
    /// surface (<see cref="Default"/>, the same properties, <see cref="Save"/>
    /// and <see cref="Upgrade"/>) so that all existing <c>Settings.Default.Xxx</c>
    /// call sites keep compiling and working unchanged — but the data now lives
    /// in a single, portable JSON file instead of the framework's user.config.
    /// </remarks>
    sealed class Settings {

        const string AppFolderName = "OnTopReplicaPlus";
        const string SettingsFileName = "settings.json";

        static readonly Settings _instance = new Settings();

        /// <summary>
        /// Gets the singleton settings instance.
        /// </summary>
        public static Settings Default {
            get { return _instance; }
        }

        SettingsData _data;
        StoredRegionArray _savedRegions;

        private Settings() {
            Load();
        }

        #region Simple persisted properties

        public byte Opacity {
            get { return _data.Opacity; }
            set { _data.Opacity = value; }
        }

        public CultureInfo Language {
            get {
                if (string.IsNullOrEmpty(_data.Language))
                    return CultureInfo.InvariantCulture;
                try {
                    return CultureInfo.GetCultureInfo(_data.Language);
                }
                catch (CultureNotFoundException) {
                    return CultureInfo.InvariantCulture;
                }
            }
            set { _data.Language = (value == null) ? null : value.Name; }
        }

        public bool MustUpdate {
            get { return _data.MustUpdate; }
            set { _data.MustUpdate = value; }
        }

        public bool ClickThrough {
            get { return _data.ClickThrough; }
            set { _data.ClickThrough = value; }
        }

        public bool FirstTimeClickThrough {
            get { return _data.FirstTimeClickThrough; }
            set { _data.FirstTimeClickThrough = value; }
        }

        public bool FirstTimeClickForwarding {
            get { return _data.FirstTimeClickForwarding; }
            set { _data.FirstTimeClickForwarding = value; }
        }

        public bool FullscreenAlwaysOnTop {
            get { return _data.FullscreenAlwaysOnTop; }
            set { _data.FullscreenAlwaysOnTop = value; }
        }

        public bool RestoreSizeAndPosition {
            get { return _data.RestoreSizeAndPosition; }
            set { _data.RestoreSizeAndPosition = value; }
        }

        public Size RestoreLastSize {
            get { return new Size(_data.RestoreLastSizeWidth, _data.RestoreLastSizeHeight); }
            set {
                _data.RestoreLastSizeWidth = value.Width;
                _data.RestoreLastSizeHeight = value.Height;
            }
        }

        public Point RestoreLastPosition {
            get { return new Point(_data.RestoreLastPositionX, _data.RestoreLastPositionY); }
            set {
                _data.RestoreLastPositionX = value.X;
                _data.RestoreLastPositionY = value.Y;
            }
        }

        public bool RestoreLastWindow {
            get { return _data.RestoreLastWindow; }
            set { _data.RestoreLastWindow = value; }
        }

        public string RestoreLastWindowClass {
            get { return _data.RestoreLastWindowClass; }
            set { _data.RestoreLastWindowClass = value; }
        }

        public string RestoreLastWindowTitle {
            get { return _data.RestoreLastWindowTitle; }
            set { _data.RestoreLastWindowTitle = value; }
        }

        public long RestoreLastWindowHwnd {
            get { return _data.RestoreLastWindowHwnd; }
            set { _data.RestoreLastWindowHwnd = value; }
        }

        public string HotKeyCloneCurrent {
            get { return _data.HotKeyCloneCurrent; }
            set { _data.HotKeyCloneCurrent = value; }
        }

        public string HotKeyShowHide {
            get { return _data.HotKeyShowHide; }
            set { _data.HotKeyShowHide = value; }
        }

        public string HotKeyCycleSavedRegion {
            get { return _data.HotKeyCycleSavedRegion; }
            set { _data.HotKeyCycleSavedRegion = value; }
        }

        public string FullscreenMode {
            get { return _data.FullscreenMode; }
            set { _data.FullscreenMode = value; }
        }

        public bool RestoreLastShowChrome {
            get { return _data.RestoreLastShowChrome; }
            set { _data.RestoreLastShowChrome = value; }
        }

        #endregion

        #region Saved regions

        /// <summary>
        /// Gets or sets the list of stored regions. The returned array is the
        /// live instance: callers may mutate it (Add/RemoveAt) and the changes
        /// are persisted on the next <see cref="Save"/>.
        /// </summary>
        public StoredRegionArray SavedRegions {
            get { return _savedRegions; }
            set { _savedRegions = value ?? new StoredRegionArray(); }
        }

        #endregion

        #region Load / Save

        static string GetSettingsFolderPath() {
            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(localAppData, AppFolderName);
        }

        static string GetSettingsFilePath() {
            return Path.Combine(GetSettingsFolderPath(), SettingsFileName);
        }

        void Load() {
            _data = null;

            try {
                var path = GetSettingsFilePath();
                if (File.Exists(path)) {
                    using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                        var serializer = new DataContractJsonSerializer(typeof(SettingsData));
                        _data = (SettingsData)serializer.ReadObject(fs);
                    }
                }
            }
            catch (Exception ex) {
                Log.WriteException("Failed to load settings, reverting to defaults", ex);
                _data = null;
            }

            if (_data == null)
                _data = new SettingsData();

            //Rebuild the live region array from the serialized data
            _savedRegions = new StoredRegionArray();
            if (_data.SavedRegions != null) {
                foreach (var r in _data.SavedRegions) {
                    if (r == null || string.IsNullOrEmpty(r.Name))
                        continue;

                    ThumbnailRegion region = r.Relative
                        ? new ThumbnailRegion(new Padding(r.X, r.Y, r.Width, r.Height))
                        : new ThumbnailRegion(new Rectangle(r.X, r.Y, r.Width, r.Height));
                    _savedRegions.Add(new StoredRegion(region, r.Name));
                }
            }
        }

        /// <summary>
        /// Persists all settings to the JSON file. Failures are logged but never
        /// throw, so that they cannot prevent the application from shutting down.
        /// </summary>
        public void Save() {
            try {
                //Flush the live region array back into the serializable data
                _data.SavedRegions = SerializeRegions();

                var folder = GetSettingsFolderPath();
                Directory.CreateDirectory(folder);

                var path = GetSettingsFilePath();
                var tempPath = path + ".tmp";

                var serializer = new DataContractJsonSerializer(typeof(SettingsData));
                using (var fs = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
                using (var writer = JsonReaderWriterFactory.CreateJsonWriter(fs, Encoding.UTF8, false, true)) {
                    serializer.WriteObject(writer, _data);
                }

                //Atomic-ish replace so a crash mid-write can never corrupt the file
                if (File.Exists(path))
                    File.Delete(path);
                File.Move(tempPath, path);
            }
            catch (Exception ex) {
                Log.WriteException("Failed to save settings", ex);
            }
        }

        /// <summary>
        /// No-op kept for compatibility with the previous settings API. The JSON
        /// store uses a fixed, version-independent path, so there is nothing to
        /// upgrade between application versions.
        /// </summary>
        public void Upgrade() {
        }

        List<SerializableRegion> SerializeRegions() {
            var list = new List<SerializableRegion>();
            if (_savedRegions == null)
                return list;

            foreach (var stored in _savedRegions) {
                var data = new SerializableRegion {
                    Name = stored.Name,
                    Relative = stored.Region.Relative
                };

                if (stored.Region.Relative) {
                    var padding = stored.Region.BoundsAsPadding;
                    data.X = padding.Left;
                    data.Y = padding.Top;
                    data.Width = padding.Right;
                    data.Height = padding.Bottom;
                }
                else {
                    var bounds = stored.Region.Bounds;
                    data.X = bounds.X;
                    data.Y = bounds.Y;
                    data.Width = bounds.Width;
                    data.Height = bounds.Height;
                }

                list.Add(data);
            }

            return list;
        }

        #endregion

    }

    /// <summary>
    /// Plain serializable container backing <see cref="Settings"/>. Uses simple
    /// primitive members so the resulting JSON is stable and human-readable.
    /// </summary>
    [DataContract(Name = "Settings", Namespace = "")]
    sealed class SettingsData {

        public SettingsData() {
            SetDefaults();
        }

        [OnDeserializing]
        void OnDeserializing(StreamingContext context) {
            //Ensure members absent from the JSON keep their defaults instead of
            //the zero/null values left by the serializer.
            SetDefaults();
        }

        void SetDefaults() {
            Opacity = 255;
            Language = null;
            MustUpdate = true;
            ClickThrough = true;
            FirstTimeClickThrough = true;
            FirstTimeClickForwarding = true;
            FullscreenAlwaysOnTop = false;
            RestoreSizeAndPosition = true;
            RestoreLastSizeWidth = 0;
            RestoreLastSizeHeight = 0;
            RestoreLastPositionX = 0;
            RestoreLastPositionY = 0;
            RestoreLastWindow = true;
            RestoreLastWindowClass = string.Empty;
            RestoreLastWindowTitle = string.Empty;
            RestoreLastWindowHwnd = 0;
            HotKeyCloneCurrent = "[CTRL]+[SHIFT]+C";
            HotKeyShowHide = "[CTRL]+[SHIFT]+O";
            HotKeyCycleSavedRegion = "[CTRL]+[SHIFT]+R";
            FullscreenMode = "Standard";
            RestoreLastShowChrome = true;
            SavedRegions = new List<SerializableRegion>();
        }

        [DataMember] public byte Opacity;
        [DataMember] public string Language;
        [DataMember] public bool MustUpdate;
        [DataMember] public bool ClickThrough;
        [DataMember] public bool FirstTimeClickThrough;
        [DataMember] public bool FirstTimeClickForwarding;
        [DataMember] public bool FullscreenAlwaysOnTop;
        [DataMember] public bool RestoreSizeAndPosition;
        [DataMember] public int RestoreLastSizeWidth;
        [DataMember] public int RestoreLastSizeHeight;
        [DataMember] public int RestoreLastPositionX;
        [DataMember] public int RestoreLastPositionY;
        [DataMember] public bool RestoreLastWindow;
        [DataMember] public string RestoreLastWindowClass;
        [DataMember] public string RestoreLastWindowTitle;
        [DataMember] public long RestoreLastWindowHwnd;
        [DataMember] public string HotKeyCloneCurrent;
        [DataMember] public string HotKeyShowHide;
        [DataMember] public string HotKeyCycleSavedRegion;
        [DataMember] public string FullscreenMode;
        [DataMember] public bool RestoreLastShowChrome;
        [DataMember] public List<SerializableRegion> SavedRegions;

    }

    /// <summary>
    /// Serializable representation of a single stored region.
    /// </summary>
    [DataContract(Name = "Region", Namespace = "")]
    sealed class SerializableRegion {
        [DataMember] public string Name;
        [DataMember] public bool Relative;
        [DataMember] public int X;
        [DataMember] public int Y;
        [DataMember] public int Width;
        [DataMember] public int Height;
    }

}
