using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UIKit;
using System.Linq;
using Foundation;

namespace Electrolux.ShopFloor.iOS
{
	public class BaseTableViewSource<T> : UITableViewSource //where T : SelectableUnit
	{
		private Action<UITableViewCell> itemSelected;
		private Action collectionChanged;

		protected ObservableCollection<T> dataSource;
		protected UITableView tableView;
		protected string cellName;

		public Action<UITableViewCell> ItemSelected
		{
			get { return this.itemSelected; }
			set { this.itemSelected = value; }
		}

		public Action CollectionChanged
		{
			get { return this.collectionChanged; }
			set { this.collectionChanged = value; }
		}

		public BaseTableViewSource()
		{
		}

		public BaseTableViewSource(ObservableCollection<T> dataSource, UITableView tableView, string cellName)
		{
			this.dataSource = dataSource;
			this.tableView = tableView;
			this.cellName = cellName;

			if (dataSource is INotifyCollectionChanged)
			{
				((INotifyCollectionChanged)dataSource).CollectionChanged += DataSource_CollectionChanged;
			}
		}

		private void DataSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (this.collectionChanged != null)
			{
				this.CollectionChanged();
			}
			this.tableView.ReloadData();
			if (e.Action == NotifyCollectionChangedAction.Add)
			{
			}
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return this.dataSource.Count();
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(this.cellName, indexPath);
			cell.BackgroundColor = (indexPath.Row % 2 == 0) ? UIColor.FromRGB(243f / 255f, 243f / 255f, 243f / 255f) : UIColor.White;
			if ((cell as ICellBinding<T>) != null)
			{
				((ICellBinding<T>)(cell)).BindCell(this.dataSource.ToList()[indexPath.Row]);
			}
			return cell;
		}

		public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
		{
			return false;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var selectedRow = indexPath.Row;
			this.itemSelected(tableView.CellAt(indexPath));
		}

        public override NSIndexPath WillSelectRow(UITableView tableView, NSIndexPath indexPath)
        {
            if (tableView.CellAt(indexPath).SelectionStyle != UITableViewCellSelectionStyle.None)
            {
                return indexPath;
            }
            return null;
        }
	}
}
