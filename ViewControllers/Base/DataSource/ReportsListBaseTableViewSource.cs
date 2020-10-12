using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Electrolux.ShopFloor.iOS.ViewControllers;
using Electrolux.ShopFloor.Mvvm.ViewModels;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public class ReportsListBaseTableViewSource<K, T> : BindableBaseTableViewSource<T> where K : AppViewModelBase where T : IndexedUnitCore
	{
		protected K viewModel;

		private BaseViewController<K, T> ownerController { get { return this.OwnerController as BaseViewController<K, T>; } }

		public ReportsListBaseTableViewSource()
		{
		}

		public ReportsListBaseTableViewSource(ObservableCollection<T> dataSource, UITableView tableView, string cellName, BaseViewController<K, T> ownerController)
			: base(dataSource, tableView, cellName, ownerController)
		{
			this.viewModel = this.ownerController.ViewModel;
		}
	}
}
