// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using GalaSoft.MvvmLight.Helpers;
using Electrolux.ShopFloor.Mvvm.ViewModels;
using Electrolux.ShopFloor.Middleware.Manager;

namespace Electrolux.ShopFloor.iOS.ViewControllers
{
    public partial class HomeViewController : BaseViewController<HomeViewModel>
    {
        public HomeViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
			ChangeSyncIcon(this.ViewModel.IsSyncRequired);
		}

		public override void Translations()
		{
			this.settingsLabel.Text = TranslatorManager.GetInstance().GetString("Settings");
			this.logoutLabel.Text = TranslatorManager.GetInstance().GetString("Logout");
			this.welcomeLabel.Text = TranslatorManager.GetInstance().GetString("Welcome");
			this.addNewReportButton.SetTitle(TranslatorManager.GetInstance().GetString("Add New Report"), UIControlState.Normal);
			this.manageReportButton.SetTitle(TranslatorManager.GetInstance().GetString("Manage Reports"), UIControlState.Normal);
			this.analyticsButton.SetTitle(TranslatorManager.GetInstance().GetString("Analytics"), UIControlState.Normal);
		}

        public override void RegisterBindings()
        {
			base.RegisterBindings();
			this.KeepBindingInMemory(this.SetBinding(() => this.ViewModel.Username, () => this.usernameLabel.Text));
            this.KeepBindingInMemory(this.SetBinding(() => this.ViewModel.CachePreloadMessage, () => this.cachePreloadLabel.Text));
            this.KeepBindingInMemory(this.SetBinding(() => this.ViewModel.CachePreloadProgress, () => this.cachePreloadProgress.Progress));
			this.KeepBindingInMemory(this.SetBinding(() => this.ViewModel.IsSyncRequired).WhenSourceChanges(() =>
			{
				ChangeSyncIcon(this.ViewModel.IsSyncRequired);
			}));
            this.KeepBindingInMemory(this.SetBinding(() => this.ViewModel.IsBusy).WhenSourceChanges(() => 
            {
                if (this.ViewModel.IsBusy)
                {
                    this.StartAsync();
                } else {
                    this.EndAsync();
                }
            }));
            this.KeepBindingInMemory(this.SetBinding(() => this.ViewModel.IsPreloadingCache).WhenSourceChanges(() =>
			{
				if (this.ViewModel.IsPreloadingCache)
				{
                    this.cachePreloadLabel.Hidden = this.cachePreloadProgress.Hidden = cachePreloadActivityIndicator.Hidden = false;
                    this.logoutButton.Enabled = false;
					this.settingsButton.Enabled = false;
					this.addNewReportButton.Enabled = false;
					this.manageReportButton.Enabled = false;
					this.analyticsButton.Enabled = false;
				}
				else
				{
					this.cachePreloadLabel.Hidden = this.cachePreloadProgress.Hidden = cachePreloadActivityIndicator.Hidden = true;
					this.logoutButton.Enabled = true;
					this.settingsButton.Enabled = true;
					this.addNewReportButton.Enabled = true;
					this.manageReportButton.Enabled = true;
					this.analyticsButton.Enabled = true;
				}
			}));
		}

		public override void RegisterCommands()
		{
			this.logoutButton.SetCommand("TouchUpInside", this.ViewModel.LogoutCommand);
			this.settingsButton.SetCommand("TouchUpInside", this.ViewModel.SettingsCommand);
			this.addNewReportButton.SetCommand("TouchUpInside", this.ViewModel.AddNewCommand);
			this.manageReportButton.SetCommand("TouchUpInside", this.ViewModel.ManageReportsCommand);
			this.analyticsButton.SetCommand("TouchUpInside", this.ViewModel.AnalyticsCommand);
		}

		void ChangeSyncIcon(bool isSyncRequired)
		{
			if (isSyncRequired)
			{
				this.settingsButton.SetBackgroundImage(UIImage.FromBundle("btn-settings-alert"), UIControlState.Normal);
			}
			else {
				this.settingsButton.SetBackgroundImage(UIImage.FromBundle("btn-settings"), UIControlState.Normal);
			}
		}
	}
}
