using System;
using System.Collections.ObjectModel;
using System.Linq;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public interface IBindableViewController<K>
	{
		void BindTaskCell(UITableViewCell cell, K item, NSIndexPath path);
	}

	public class BindableBaseTableViewSource<T> : BaseTableViewSource<T> //where T : SelectableUnit
	{
		protected IBindableViewController<T> OwnerController { get { return wController.Target as IBindableViewController<T>; } }

		private WeakReference wController;

		public BindableBaseTableViewSource()
		{
		}

		public BindableBaseTableViewSource(ObservableCollection<T> dataSource, UITableView tableView, string cellName, IBindableViewController<T> ownerController)
			: base(dataSource, tableView, cellName)
		{
			this.wController = new WeakReference(ownerController);
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = base.GetCell(tableView, indexPath);
			return BindCell(cell, this.dataSource.ToList()[indexPath.Row], indexPath);
		}

		public virtual UITableViewCell BindCell(UITableViewCell cell, T item, NSIndexPath indexPath)
		{
			OwnerController.BindTaskCell(cell, item, indexPath);
			return cell;
		}
	}
}
