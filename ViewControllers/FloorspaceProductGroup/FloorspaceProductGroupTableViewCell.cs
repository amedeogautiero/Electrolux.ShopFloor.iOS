// This file has been autogenerated from a class added in the UI designer.

using System;

using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class FloorspaceProductGroupTableViewCell : UITableViewCell
	{
		public FloorSpaceProductGroupUnit Item { get; set; }
		public UILabel ProductGroupLabel { get { return productGroupLabel; } }
		public UILabel ItemsCountLabel { get { return itemsCountLabel; } }

		public FloorspaceProductGroupTableViewCell (IntPtr handle) : base (handle)
		{
		}
	}
}