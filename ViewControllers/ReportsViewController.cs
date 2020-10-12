using Electrolux.ShopFloor.iOS.ViewControllers;
using Electrolux.ShopFloor.Middleware.Manager;
using Electrolux.ShopFloor.Mvvm.ViewModels;
using System;
using GalaSoft.MvvmLight.Helpers;
using CoreGraphics;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using UIKit;
using Foundation;
using Electrolux.ShopFloor.Middleware.Model;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using Electrolux.ShopFloor.Middleware.Contract;
//using HockeyApp.iOS;

namespace Electrolux.ShopFloor.iOS
{
	public partial class ReportsViewController : BaseViewController<ReportsViewModel, ReportUnit>
	{
		private bool isUploading;

		public bool IsUploading
		{
			get { return this.isUploading; }
			set
			{
				this.isUploading = value;
				if (this.isUploading)
				{
					this.StartAsync();
				}
				else {
					tableView.ReloadData();
					this.EndAsync();
				}
			}
		}

		public ReportsViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewWillDisappear(bool animated)
		{
			this.NavigationController.SetNavigationBarHidden(true, true);
			ViewModel.Items.Clear();

#if !PROD
			Messenger.Default.Unregister<ReportUploadResultMessage>(this, HandleStatusMessage);
#endif
			base.ViewWillDisappear(animated);
		}

		public override void ViewWillAppear(bool animated)
		{
			FilterViewHide();

			this.NavigationController.SetNavigationBarHidden(false, false);

			//Debug.WriteLine("ReportsViewController.GetTableViewSource before");
			tableView.Source = new ReportsListTableViewSource(ViewModel.Items, tableView, "ReportsViewCell", this);

#if !PROD
			Messenger.Default.Register<ReportUploadResultMessage>(this, HandleStatusMessage);
#endif
			base.ViewWillAppear(animated);
		}

		private void HandleStatusMessage(ReportUploadResultMessage msg)
		{
			//IAppConfig config = RegistryService.Instance.Resolve<IAppConfig>();
   //         if (config.IsHockeyAppActive)
   //         {
			//	var manager = BITHockeyManager.SharedHockeyManager;
				//if (msg.status != eReportStatus.UploadError)
				//{
				//	manager.MetricsManager.TrackEvent("ReportUploadSuccess");
				//}
				//else
				//{
					//var properties = new NSDictionary("ReportXML", msg.reportContent);
					//var measurements = new NSDictionary();

				//	manager.MetricsManager.TrackEvent("ReportUploadFailure", properties, measurements);
				//}
			//}
		}

		public override void Translations()
		{
			this.headerLabelDate.Text = TranslatorManager.GetInstance().GetString("Date");
			this.headerLabelStatus.Text = TranslatorManager.GetInstance().GetString("Status");
			this.headerLabelStore.Text = TranslatorManager.GetInstance().GetString("Store");
            this.headerLabelSector.Text = TranslatorManager.GetInstance().GetString("Sector");
			this.filtersButton.Title = TranslatorManager.GetInstance().GetString("Filters");
		}

		public override void RegisterBindings()
		{
			base.RegisterBindings();

			this.KeepBindingInMemoryLocal(new Binding<bool, bool>(ViewModel, () => ViewModel.IsUploading, this, () => this.IsUploading));
			this.KeepBindingInMemoryLocal(this.SetBinding(() => ViewModel.IsFiltersPanelVisible).WhenSourceChanges(() =>
			{
				if (ViewModel.IsFiltersPanelVisible)
				{
					FilterViewShow();
				}
				else {
					FilterViewHide();
				}
			}));

			this.KeepBindingInMemoryLocal(new Binding<bool, bool>(ViewModel, () => ViewModel.IsBusy)
			  .WhenSourceChanges(() =>
				{
					if (ViewModel.IsBusy)
						this.StartAsync();
					else
						this.EndAsync();
				}));
		}

		partial void filtersButtonAction(Foundation.NSObject sender)
		{
			if (this.ViewModel.FiltersCommand.CanExecute(null))
			{
				this.ViewModel.FiltersCommand.Execute(null);
			}
		}

		private void FilterViewShow()
		{
			this.View.BringSubviewToFront(this.filtersView);

			this.filtersView.Frame = new CGRect(new CGPoint(393f, 0), this.filtersView.Frame.Size);
		}

		private void FilterViewHide()
		{
			this.filtersView.Frame = new CGRect(new CGPoint(1024f, 0), this.filtersView.Frame.Size);

			this.View.SendSubviewToBack(this.filtersView);
		}

		public override void BindTaskCell(UITableViewCell cell, ReportUnit item, NSIndexPath path)
		{
			//DateTime startTime = DateTime.Now;

			//Debug.WriteLine("After ReportsViewController.BindTaskCell.Start " + (DateTime.Now - startTime).TotalSeconds.ToString());
			//startTime = DateTime.Now;

			ReportsViewCell reportsCell = cell as ReportsViewCell;

			reportsCell.StoreLabel.Text = item.Text;
			reportsCell.DateLabel.Text = item.Date;
            reportsCell.BusinessSectorLabel.Text = item.BusinessSectorID;

			KeepBindingInMemoryLocal(new Binding<string, bool>(item, () => item.Status, reportsCell, () => reportsCell.UploadReportButton.Enabled)
						 .ConvertSourceToTarget((arg) => 
			{
				return (item.GeneralInfo.ReportStatus == eReportStatus.Saved || item.GeneralInfo.ReportStatus == eReportStatus.UploadError);
			}));

			//KeepBindingInMemoryLocal(new Binding<string, bool>(item, () => item.Status, reportsCell, () => reportsCell.DeleteButton.Enabled)
			//						 .ConvertSourceToTarget((arg) => (item.GeneralInfo.ReportStatus != eReportStatus.Uploaded)));

			KeepBindingInMemoryLocal(new Binding<bool, bool>(item, () => item.IsReadOnly, reportsCell, () => reportsCell.EditButton.Enabled)
									 .ConvertSourceToTarget((arg) => !arg));
			KeepBindingInMemoryLocal(new Binding<bool, bool>(item, () => item.IsReadOnly, reportsCell, () => reportsCell.ViewButton.Enabled));

			KeepBindingInMemoryLocal(new Binding<string, string>(item, () => item.Status).WhenSourceChanges(() =>
			{
				reportsCell.ViewUploadErrorButton.SetTitle(item.Status, UIControlState.Normal);
			}));

			KeepBindingInMemoryLocal(new Binding<bool, bool>(ViewModel, () => ViewModel.IsUploading)
									 .WhenSourceChanges(() =>
			{
				if (ViewModel.IsUploading)
				{
					reportsCell.UploadReportButton.Enabled = false;
				} else
				{
					reportsCell.UploadReportButton.Enabled = (item.GeneralInfo.ReportStatus == eReportStatus.Saved || item.GeneralInfo.ReportStatus == eReportStatus.UploadError);
				}
			}));

			reportsCell.EditButtonAction = (UIView sender) =>
			{
				if (ViewModel.InfoCommand.CanExecute(item))
				{
					ViewModel.InfoCommand.Execute(item);
				}
			};

			reportsCell.UploadButtonAction = (UIView sender) =>
			{
				if (ViewModel.UploadCommand.CanExecute(item))
				{
					ViewModel.UploadCommand.Execute(item);
				}
			};

			reportsCell.DeleteButtonAction = (UIView sender) =>
			{
				if (ViewModel.DeleteCommand.CanExecute(item))
				{
					ViewModel.DeleteCommand.Execute(item);
				}
			};

			reportsCell.ViewButtonAction = (UIView sender) =>
			{
				if (ViewModel.ViewCommand.CanExecute(item))
				{
					ViewModel.ViewCommand.Execute(item);
				}
			};

			reportsCell.ViewUploadErrorButtonAction = (UIView sender) =>
			{
				if (ViewModel.OpenStatusPanelCommand.CanExecute(item))
				{
					ViewModel.OpenStatusPanelCommand.Execute(item);
				}
			};
			//Debug.WriteLine("After ReportsViewController.BindTaskCell.End " + (DateTime.Now - startTime).TotalSeconds.ToString());
			//startTime = DateTime.Now;
		}
	}
}