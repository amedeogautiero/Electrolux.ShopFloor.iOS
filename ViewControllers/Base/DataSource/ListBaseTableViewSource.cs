using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Electrolux.ShopFloor.iOS.ViewControllers;
using Electrolux.ShopFloor.Mvvm.ViewModels.EditingAreas;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public class ListBaseTableViewSource<T, K> : BindableBaseTableViewSource<T> where T : IndexedUnitCore where K : AreaListViewModelBase<T>
	{
		protected K viewModel;

		private ListBaseViewController<K, T> ownerController { get { return this.OwnerController as ListBaseViewController<K, T>; } }

		public ListBaseTableViewSource()
		{
		}

		public ListBaseTableViewSource(ObservableCollection<T> dataSource,
												   UITableView tableView,
												   string cellName,
												   ListBaseViewController<K, T> ownerController) : base(dataSource, tableView, cellName, ownerController)
		{
			this.viewModel = this.ownerController.AreaViewModel;
		}

		public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
		{
			return this.ownerController.ListRowEditingEnabled;
		}

		public override void CommitEditingStyle(UITableView tableView,
												UITableViewCellEditingStyle editingStyle,
												NSIndexPath indexPath)
		{
			switch (editingStyle)
			{
				case UITableViewCellEditingStyle.Delete:
					this.viewModel.DeleteCommand.Execute(this.dataSource.ToList()[indexPath.Row]);
					//List<NSIndexPath> Rows = new List<NSIndexPath> { indexPath };
					//tableView.DeleteRows(Rows.ToArray(), UITableViewRowAnimation.Fade);
					break;
			}
		}

		public override UITableViewCellEditingStyle EditingStyleForRow(UITableView tableView,
																		NSIndexPath indexPath)
		{
			return UITableViewCellEditingStyle.Delete;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			ownerController.StartAsync(ownerController.View);
			this.viewModel.SelectedItem = this.dataSource.ElementAt(indexPath.Row);
		}
	}
}
