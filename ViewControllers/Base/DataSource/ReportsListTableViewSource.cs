using System.Collections.ObjectModel;
using Electrolux.ShopFloor.iOS.ViewControllers;
using Electrolux.ShopFloor.Middleware.Model;
using Electrolux.ShopFloor.Mvvm.ViewModels;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using GalaSoft.MvvmLight.Helpers;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public class ReportsListTableViewSource : ReportsListBaseTableViewSource<ReportsViewModel, ReportUnit>
	{
		public ReportsListTableViewSource(ObservableCollection<ReportUnit> dataSource, UITableView tableView, string cellName, BaseViewController<ReportsViewModel, ReportUnit> ownerController)
			: base(dataSource, tableView, cellName, ownerController)
		{
		}
	}
}
