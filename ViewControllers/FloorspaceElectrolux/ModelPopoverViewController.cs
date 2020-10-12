using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CoreGraphics;
using Electrolux.ShopFloor.iOS;
using Electrolux.ShopFloor.iOS.ViewControllers;
using Electrolux.ShopFloor.Middleware.Model.UI;
using Electrolux.ShopFloor.Mvvm.ViewModels.EditingAreas;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public class ModelPopoverViewController : BaseViewController
	{
		private ObservableCollection<ModelModel> dataSource;
		private FSElectroluxModelSearchViewController content;
		private UIPopoverArrowDirection direction;
		private CGSize size;

		public UIPopoverController DetailViewPopover { get; set; }

		public ModelPopoverViewController(IntPtr handle) : base(handle)
		{
		}

		public ModelPopoverViewController(ElectroluxFloorSpaceViewModel areaViewModel,
		                                  CGSize size, 
		                                  string cellName, 
		                                  UIPopoverArrowDirection direction, 
		                                  Action<UITableViewCell> itemSelected = null) : base()
		{
			this.dataSource = areaViewModel.FilteredModels;
			this.size = size;

			var sb = AppDelegate.NavigationController.Storyboard;
			content = (FSElectroluxModelSearchViewController)sb.InstantiateViewController("FSElectroluxModelSearchViewController");
			content.AreaViewModel = areaViewModel;
			content.ItemSelected = itemSelected;
			DetailViewPopover = new UIPopoverController(content);
			this.direction = direction;
		}

		private void DataSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
		}

		public void ShowPopover(UIView sender)
		{
			ShowPopover(sender, false);
		}

		public void ShowPopover(UIView sender, bool forceWidth)
		{
			((INotifyCollectionChanged)this.dataSource).CollectionChanged += DataSource_CollectionChanged;

			nfloat h = 250f;
			CGSize contentSize = new CGSize((forceWidth) ? this.size.Width : sender.Frame.Size.Width, h);
			DetailViewPopover.SetPopoverContentSize(contentSize, true);
			content.PreferredContentSize = contentSize;
			DetailViewPopover.PresentFromRect(sender.Frame, sender.Superview, this.direction, true);

		}

		public void DismissPopover()
		{
			((INotifyCollectionChanged)this.dataSource).CollectionChanged -= DataSource_CollectionChanged;
			DetailViewPopover.Dismiss(true);
		}
	}
}

