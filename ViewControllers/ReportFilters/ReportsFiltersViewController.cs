using System;
using System.Collections.ObjectModel;
using System.Linq;
using CoreGraphics;
using Electrolux.ShopFloor.iOS;
using Electrolux.ShopFloor.iOS.ViewControllers;
using Electrolux.ShopFloor.Middleware.Manager;
using Electrolux.ShopFloor.Mvvm.ViewModels;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using GalaSoft.MvvmLight.Helpers;
using Syncfusion.Data.Extensions;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class ReportsFiltersViewController : BaseFilterViewController
	{
		#region Events

		partial void statusButtonAction(NSObject sender)
		{

			this.storeButton.Selected = false;
			this.cityButton.Selected = false;
			this.datesButton.Selected = false;
			this.statusButton.Selected = true;

			InitController("FilterStatus");

			this.filterView.BringSubviewToFront(this.statusView);
		}

		partial void cityButtonAction(NSObject sender)
		{

			this.storeButton.Selected = false;
			this.cityButton.Selected = true;
			this.datesButton.Selected = false;
			this.statusButton.Selected = false;

			this.filterView.BringSubviewToFront(this.cityView);

			InitController("FilterCity");
		}

		partial void datesButtonAction(NSObject sender)
		{

			this.storeButton.Selected = false;
			this.cityButton.Selected = false;
			this.datesButton.Selected = true;
			this.statusButton.Selected = false;

			this.filterView.BringSubviewToFront(this.datesView);

			InitController("FilterDates");

		}

		partial void storeButtonAction(NSObject sender)
		{

			this.storeButton.Selected = true;
			this.cityButton.Selected = false;
			this.datesButton.Selected = false;
			this.statusButton.Selected = false;

			this.filterView.BringSubviewToFront(this.storeView);

			InitController("FilterStore");

		}

		partial void applyButtonAction(NSObject sender)
		{
			if (this.ViewModel.ApplyFiltersCommand.CanExecute(sender) == false)
				return;

			this.ViewModel.ApplyFiltersCommand.Execute(sender);
		}

		partial void resetButtonAction(NSObject sender)
		{
			if (this.ViewModel.ResetFiltersCommand.CanExecute(sender) == false)
				return;

			this.ViewModel.ResetFiltersCommand.Execute(sender);
		}

		#endregion

		void InitController(string restorationID)
		{

			var currentController = this.ChildViewControllers.GetByRestorationID(restorationID) as BaseFilterViewController;
			if (currentController == null)
				throw new Exception("Filter controller not found");

			currentController.ConfigureArea();
		}

		public override void Translations()
		{
			this.storeButton.SetTitleColor(UIColor.White, UIControlState.Selected);
			this.statusButton.SetTitleColor(UIColor.White, UIControlState.Selected);
			this.cityButton.SetTitleColor(UIColor.White, UIControlState.Selected);
			this.datesButton.SetTitleColor(UIColor.White, UIControlState.Selected);

			this.storeButton.SetBackgroundColor(ElectroluxColors.ElectroluxButtonSelected, UIControlState.Selected);
			this.statusButton.SetBackgroundColor(ElectroluxColors.ElectroluxButtonSelected, UIControlState.Selected);
			this.datesButton.SetBackgroundColor(ElectroluxColors.ElectroluxButtonSelected, UIControlState.Selected);
			this.cityButton.SetBackgroundColor(ElectroluxColors.ElectroluxButtonSelected, UIControlState.Selected);

			this.applyButton.Title = TranslatorManager.GetInstance().GetString("Apply");
			this.resetButton.Title = TranslatorManager.GetInstance().GetString("Reset");

			this.storeButton.SetTitle(TranslatorManager.GetInstance().GetString("Store"), UIControlState.Normal);
			this.statusButton.SetTitle(TranslatorManager.GetInstance().GetString("Status"), UIControlState.Normal);
			this.datesButton.SetTitle(TranslatorManager.GetInstance().GetString("Dates"), UIControlState.Normal);
			this.cityButton.SetTitle(TranslatorManager.GetInstance().GetString("City"), UIControlState.Normal);
		}

		public override void RegisterCommands()
		{
			base.RegisterCommands();

			this.storeButtonAction(null);
		}

		public ReportsFiltersViewController(IntPtr handle) : base(handle)
		{
		}
	}
}
