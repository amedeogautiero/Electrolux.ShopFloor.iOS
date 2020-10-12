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
	public partial class FilterStatusViewController : BaseFilterViewController
	{
		public FilterStatusViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ConfigureArea()
		{
			base.ConfigureArea();

			if (this.tableView.Source == null)
			{
				this.tableView.Source = GetItems();
				this.tableView.ReloadData();
			}
		}

		public override void RegisterBindingsLocal()
		{
			base.RegisterBindingsLocal();

			KeepBindingInMemoryLocalArea(this.SetBinding(() => this.ViewModel.SelectedReportStatus).WhenSourceChanges(() =>
			{
				this.tableView.ReloadData();
			}));

			this.KeepBindingInMemoryLocal(this.SetBinding(() => this.ViewModel.HasFiltersDidReset).WhenSourceChanges(() =>
			{
				this.tableView.ReloadData();

			}));
		}

		private ObservableTableViewSource<FilterUnit> GetItems()
		{
			var statuses = ViewModel.ReportStatus;

			var dataSource = statuses.GetTableViewSource<FilterUnit>(
					createCellDelegate: CreateTaskCell,
					bindCellDelegate: BindTaskCell,
					reuseId: "ReportUnitTableViewCell",
					factory: () => new StatusesFiltersTableSource(this.ViewModel)
			);

			return dataSource;
		}

		private void BindTaskCell(UITableViewCell cell, FilterUnit item, NSIndexPath path)
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


	public class StatusesFiltersTableSource : ObservableTableViewSource<FilterUnit>
	{
		private ReportsViewModel viewModel;

		public StatusesFiltersTableSource(ReportsViewModel viewModel) : base()
		{
			this.viewModel = viewModel;
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return this.viewModel.ReportStatus.Count;
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
			this.viewModel.SelectedReportStatus = this.GetItem(indexPath);
		}
	}
}
