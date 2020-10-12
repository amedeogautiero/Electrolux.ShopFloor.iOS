using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CoreGraphics;
using Electrolux.ShopFloor.iOS;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public class PopoverViewController<T> : BasePopoverController
	{
		private ObservableCollection<T> dataSource;
		private nfloat popoverMaxHeight;
		private CGSize size;
		private CGSize popoverContentSize;

		public PopoverContentViewController<T> Content { get; set; }

		public PopoverViewController(IntPtr handle) : base(handle)
		{
		}

		public PopoverViewController(ObservableCollection<T> dataSource, CGSize size, string cellName, UIPopoverArrowDirection direction, Action<UITableViewCell> itemSelected = null) : base(direction)
		{
			this.dataSource = dataSource;
			this.size = size;

			Content = new PopoverContentViewController<T>(size, this.dataSource, cellName, itemSelected);
			DetailViewPopover = new UIPopoverController(Content);
			this.direction = direction;
		}

		private void DataSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (this.dataSource.Count >= 0)
			{
				nfloat w = DetailViewPopover.PopoverContentSize.Width;
				nfloat h = (nfloat)Math.Min(44.0f * Math.Max(this.dataSource.Count, 1) + 30.0f, this.popoverMaxHeight);
				CGSize size = new CGSize(w, h);
				DetailViewPopover.SetPopoverContentSize(size, true);
				Content.PreferredContentSize = size;
			}
		}

		public void ShowPopover(UIView sender)
		{
			ShowPopover(sender, false);
		}

		public void ShowPopover(UIView sender, bool forceWidth)
		{
			((INotifyCollectionChanged)this.dataSource).CollectionChanged += DataSource_CollectionChanged;
			this.ShowPopover(sender, this.size);

			// Present the popover from the button that was tapped in the detail view.
			CGPoint sourcePoint = new CGPoint(0, sender.Frame.Location.Y + sender.Frame.Size.Height);
			CGPoint targetPoint = sender.Superview.ConvertPointToView(sourcePoint, UIApplication.SharedApplication.KeyWindow);
			popoverMaxHeight = UIApplication.SharedApplication.KeyWindow.Frame.Height - targetPoint.Y;
			nfloat h = (nfloat)Math.Min(44.0f * Math.Max(this.dataSource.Count, 1) + 30.0f, popoverMaxHeight);
			popoverContentSize = new CGSize((forceWidth) ? this.size.Width : sender.Frame.Size.Width, h);
			DetailViewPopover.SetPopoverContentSize(popoverContentSize, true);
			Content.PreferredContentSize = DetailViewPopover.PopoverContentSize;
			DetailViewPopover.PresentFromRect(sender.Frame, sender.Superview, this.direction, true);
		}

		public override void DismissPopover()
		{
			((INotifyCollectionChanged)this.dataSource).CollectionChanged -= DataSource_CollectionChanged;
			base.DismissPopover();
		}

		override protected void keyboardWillHide(NSNotification obj)
		{
			base.keyboardWillHide(obj);

			DetailViewPopover.SetPopoverContentSize(size, true);
			Content.PreferredContentSize = size;
		}
	}

	public class PopoverContentViewController<T> : UITableViewController
	{
		public PopoverContentViewController(CGSize size, ObservableCollection<T> dataSource, string cellName, Action<UITableViewCell> itemSelected = null) : base()
		{
			TableView = new UITableView(new CGRect(new CGPoint(0f, 0f), size), UITableViewStyle.Plain);
			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;

			BaseTableViewSource<T> source = new BaseTableViewSource<T>(dataSource, TableView, cellName);
			source.ItemSelected = itemSelected;
			TableView.Source = source;

			TableView.RegisterNibForCellReuse(UINib.FromName(cellName, NSBundle.MainBundle), cellName);
			TableView.ReloadData();
		}

		public PopoverContentViewController(IntPtr handle) : base(handle)
		{
		}

		//loads the PopoverContentViewController.xib file and connects it to this object
		public PopoverContentViewController() : base()
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.View.AddSubview(TableView);
		}
	}
}

