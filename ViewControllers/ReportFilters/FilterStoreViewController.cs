using System;
using System.Collections.Generic;
using System.Linq;
using Electrolux.ShopFloor.iOS.ViewControllers;
using Electrolux.ShopFloor.Middleware.Manager;
using Electrolux.ShopFloor.Mvvm.ViewModels;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using GalaSoft.MvvmLight.Helpers;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class FilterStoreViewController : BaseFilterViewController
	{
		public FilterStoreViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ConfigureArea()
		{
			base.ConfigureArea();

			this.searchBar.Placeholder = TranslatorManager.GetInstance().GetString("Store");

			if (this.tableView.Source == null)
			{
				this.tableView.Source = GetItems();
			}

			this.searchBar.ShouldChangeTextInRange += (control, range, text) =>
			{
				var stringSearch = new NSString(control.Text).Replace(range, new NSString(text)).ToString();
				if (stringSearch.Length > this.ViewModel.ApplicationController.SearchThreshold)
				{
					ViewModel.ShopName = stringSearch;
				}

				return true;
			};
		}

		public override void RegisterBindingsLocal()
		{
			base.RegisterBindingsLocal();

			KeepBindingInMemoryLocalArea(this.SetBinding(() => this.ViewModel.ShopName).WhenSourceChanges(() =>
			{
				this.tableView.ReloadData();
			}));


			KeepBindingInMemoryLocalArea(this.SetBinding(() => this.ViewModel.SelectedShop).WhenSourceChanges(() =>
			{
				this.tableView.ReloadData();
			}));

			this.KeepBindingInMemoryLocal(this.SetBinding(() => this.ViewModel.HasFiltersDidReset).WhenSourceChanges(() =>
			{
				this.tableView.ReloadData();

			}));
		}

		private ObservableTableViewSource<ReportUnit> GetItems()
		{
			var shops = ViewModel.Shops;

			var dataSource = shops.GetTableViewSource<ReportUnit>(
					createCellDelegate: CreateTaskCell,
					bindCellDelegate: BindTaskCell,
					reuseId: "ReportUnitTableViewCell",
					factory: () => new ShopFiltersTableSource(this.ViewModel)
			);

			return dataSource;
		}

		private void BindTaskCell(UITableViewCell cell, ReportUnit item, NSIndexPath path)
		{
			ReportUnitTableViewCell reportUnitTableViewCell = cell as ReportUnitTableViewCell;

			reportUnitTableViewCell.TextLabel.Text = item.Text;
			reportUnitTableViewCell.Selected = item.IsSelected;
			if (reportUnitTableViewCell.Selected)
			{
				reportUnitTableViewCell.BackgroundView = new UIView { BackgroundColor = ElectroluxColors.ElectroluxRowSelected };
			}
			else
			{
				reportUnitTableViewCell.BackgroundView = new UIView { BackgroundColor = UIColor.White };
			}
		}

		private UITableViewCell CreateTaskCell(NSString arg)
		{
			var cell = new UITableViewCell(UITableViewCellStyle.Default, "ReportUnitTableViewCell");

			return cell;
		}

	}

	public class ShopFiltersTableSource : ObservableTableViewSource<ReportUnit>
	{
		private ReportsViewModel viewModel;

		public ShopFiltersTableSource(ReportsViewModel viewModel) : base()
		{
			this.viewModel = viewModel;
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return this.viewModel.Shops.Count;
		}

		public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
		{
			return false;
		}

		public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
		}

		public override UITableViewCellEditingStyle EditingStyleForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return UITableViewCellEditingStyle.None;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			this.viewModel.SelectedShop = this.GetItem(indexPath);
		}
	}

}
