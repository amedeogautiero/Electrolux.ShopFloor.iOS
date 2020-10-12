using Electrolux.ShopFloor.Middleware.Contract;
using Electrolux.ShopFloor.Mvvm.Infrastructure;
using Electrolux.ShopFloor.iOS.Services.Providers;
using Foundation;
using GalaSoft.MvvmLight.Views;
using System;
using System.Threading.Tasks;
using UIKit;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace Electrolux.ShopFloor.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		public static UINavigationController NavigationController { get; set; }

        public static bool IsInitialized { get; set; }

        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
			UIApplication.CheckForEventAndDelegateMismatches = false;
			GlobalSettings();
			RegisterBaseServices();

            //App Center
#if PRODUCTION
            AppCenter.Start("6cfc6b60-7612-4cd5-89df-6d17e09850af", typeof(Analytics), typeof(Crashes));
#else
            AppCenter.Start("1cd9fc9e-0a59-4a8b-a7e2-0e89ba1a40bb", typeof(Analytics), typeof(Crashes));
#endif

            #region HockeyApp (OLD)
            //AppConfig.Instance.InitHockeyApp();
            //#if !PROD
            //			InitHockeyApp("97f787db94154859a898fa934571b725");
            //			Xamarin.Calabash.Start();
            //#endif
            #endregion

            return true;
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }

		private void GlobalSettings()
		{
			/*
			var textFieldAppearance = UITextField.AppearanceWhenContainedIn(typeof(UISearchBar));
			textFieldAppearance
			[[UITextField appearanceWhenContainedIn:[UISearchBar class], nil] setDefaultTextAttributes:@{
            	NSFontAttributeName: [UIFont fontWithName:@"Helvetica" size:20],
      		}];
      		*/
			var textAttributes = new UITextAttributes()
			{
				Font = UIFont.FromName("ElectroluxSans-Regular", 17)
			};

			UIBarButtonItem.Appearance.SetTitleTextAttributes( textAttributes, UIControlState.Normal);
		}

        private void RegisterBaseServices()
        {
            RegistryService.Instance.Register<IAppConfig>(AppConfig.GetInstance());
            RegistryService.Instance.Register<IFileStorage>(FileStorageProvider.Instance);
            //RegistryService.GetInstance().Register<INetworkProvider>(new NetworkProvider());
        }

        private static ViewModelLocator _locator;
        //private NavigationService _navigationService;

        public static ViewModelLocator Locator
        {
            get
            {
                return _locator;
            }
        }

        public static async Task CreateLocatorAsync(UINavigationController navigationController)
        {
            var locator = new ViewModelLocator();

            NavigationService _navigationService = new NavigationService();

            _navigationService.Configure(ViewModelLocator.SplashViewModelKey, "SplashView");
            _navigationService.Configure(ViewModelLocator.LoginViewModelKey, "LoginView");
            _navigationService.Configure(ViewModelLocator.HomeViewModelKey, "HomeView");
            _navigationService.Configure(ViewModelLocator.ReportsViewModelKey, "ReportsView");
			_navigationService.Configure(ViewModelLocator.ReportEditorViewModelKey, "ReportEditorView");
			_navigationService.Configure(ViewModelLocator.SettingsViewModelKey, "SettingsView");
			_navigationService.Configure(ViewModelLocator.AnalyticsFloorspaceViewModelKey, "AnalyticsFloorspaceView");
			_navigationService.Configure(ViewModelLocator.AnalyticsKitchenViewModelKey, "AnalyticsKitchenView");
			_navigationService.Configure(ViewModelLocator.AnalyticsModelVariationsViewModelKey, "AnalyticsModelVariationsView");
			_navigationService.Configure(ViewModelLocator.AnalyticsDetailViewModelKey, "AnalyticsFloorspaceDetailsView");

			DialogService dialogService = new DialogService();
            IAppConfig appConfig = AppConfig.Instance;
            await locator.InitAsync(_navigationService, dialogService, appConfig, new NetworkProvider(), new UIServiceProvider(navigationController));

            AppDelegate.IsInitialized = true;
            _locator = locator;
        }

        public static void InitializeNavigationService(UINavigationController navigationController)
        {
            var navigationService = AppDelegate.Locator.NavigationService as NavigationService;

			AppDelegate.NavigationController = navigationController;

            if (navigationService == null || Locator == null)
            {
                throw new ApplicationException("Navigation Service Not Found or Applicaiton.InitLocator not called");
            }
            else
            {
                navigationService.Initialize(navigationController);
            }
        }

        public static void InitApp(UINavigationController navigationController)
        {
            navigationController.NavigationBar.TintColor = UIColor.White;

            var keys = new object[] { UIStringAttributeKey.ForegroundColor };
            var values = new object[] { UIColor.White };
            var dict = NSDictionary.FromObjectsAndKeys(values, keys);
            navigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes(dict);

            navigationController.NavigationBar.Translucent = false;
            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);

            navigationController.NavigationBar.BarTintColor = AppDelegate.ElectroluxBlue;
        }

        public static UIColor ElectroluxBlue
        {
            get
            {
                return new UIColor((nfloat)29/255, (nfloat)32 /255, (nfloat)81 /255, 1);
            }
        }
    }
}


