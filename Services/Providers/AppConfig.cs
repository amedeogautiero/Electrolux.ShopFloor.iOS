using Electrolux.ShopFloor.Middleware;
using Electrolux.ShopFloor.Middleware.Contract;
using Foundation;
using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;
//using HockeyApp.iOS;

namespace Electrolux.ShopFloor.iOS.Services.Providers
{

    public class AppConfig : IAppConfig
    {
        static AppConfig _instance = new AppConfig();
        public static AppConfig GetInstance()
        {
            return _instance;
        }

        public string BaseURL
        {
            get
            {
                return Configuration.ServiceURL;
            }
        }

        public ISQLitePlatform Platform
        {
            get { return new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS(); }
        }

        public string InstallationPath
        {
            get
            {
                var bundlePath = NSBundle.MainBundle.BundlePath;
                return bundlePath;
            }
        }

        public string WorkingPath
        {
            get
            {
                var documentsDirectory = NSFileManager.DefaultManager.GetUrls(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User)[0].Path;
                return documentsDirectory;
            }
        }

        public string MediaPath
        {
            get { return WorkingPath; }
        }

        public string DataPath
        {
            get
            {
                return Path.Combine(WorkingPath, "APUsercontent");
            }
        }

        public string DatabaseName
        {
            get { return @"AmbassadorPortalModel"; }
        }

        public string DatabasePath
        {
            get
            {
                string retval = Path.Combine(this.DataPath, string.Format("{0}.sqlite", this.DatabaseName));
#if DEBUG
                Debug.WriteLine("Database Path " + retval);
#endif
                return retval;
            }
        }

        public string BackupDatabaseName
        {
            get { return string.Format("{0}_backup", this.DatabaseName); }
        }

        public string BackupDatabasePath
        {
            get { return Path.Combine(this.DataPath, string.Format("{0}.sqlite", this.BackupDatabaseName)); }
        }

        public string DeviceType
        {
            get { return "iOS"; }
        }

        //todo da trovare
        public string UDID
        {
            // todo implement UDID with iOS api
            get { return "IOS-UDID"; }
        }

        public string AppVersion
        {
            get
            {
                string appVersion = NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString();
                return appVersion;
            }
        }

        public string ResourceTranslationFilePath
        {
            get
            {
                return Path.Combine(Path.Combine(InstallationPath , "SharedContent"), "resource_translation.xml");
            }
        }

        public string ProductGroupPredefinedBrandsFilePath
        {
            get
            {
                return Path.Combine(Path.Combine(InstallationPath , "SharedContent"), "predefined_brands.xml");
            }
        }

        public bool IsProduction
        {
            get
            {
                return (NSBundle.MainBundle.BundleIdentifier == "com.electrolux.shopfloor");
            }
        }

        public bool IsStaging
        {
            get
            {
                return (NSBundle.MainBundle.BundleIdentifier == "it.extra.Electrolux.ShopFloor.iOS");
            }
        }

        //private bool isHockeyAppActive;
        //public bool IsHockeyAppActive
        //{
        //    get
        //    {
        //        return isHockeyAppActive;
        //    }
        //}

        public bool VersionRequiresLogout
        {
            get
            {
                return true;
            }
        }

        public bool VersionRequiresAutomaticReportUpload
        {
            get
            {
                return false;
            }
        }

        //public void InitHockeyApp()
        //{
        //    string hockeyAppId = "";
        //    if (IsProduction)
        //    {
        //        hockeyAppId = "6cfc6b6076124cd589df6d17e09850af";
        //        isHockeyAppActive = true;
        //    }
        //    else if (IsStaging)
        //    {
        //        hockeyAppId = "1cd9fc9e0a594a8ba7e20e89ba1a40bb";
        //        isHockeyAppActive = true;
        //    }
        //    else
        //    {
        //        isHockeyAppActive = false;
        //    }

        //    if (isHockeyAppActive)
        //    {
        //        InitHockeyApp(hockeyAppId);
        //        Xamarin.Calabash.Start();
        //    }
        //}

        //private void InitHockeyApp(string appID)
        //{
        //    var manager = BITHockeyManager.SharedHockeyManager;
        //    manager.Configure(appID);
        //    manager.StartManager();
        //    if (IsStaging)
        //    {
        //        manager.Authenticator.AuthenticateInstallation();
        //    }
        //}

        #region ctor

        public static AppConfig Instance { get; } = GetInstance();

        public string PredefinedFloorspaceCompBrandsFilePath => throw new NotImplementedException();

        private AppConfig()
        {
        }

        #endregion
    }
}
