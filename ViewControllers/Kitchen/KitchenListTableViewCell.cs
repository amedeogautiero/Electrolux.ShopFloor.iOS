// This file has been autogenerated from a class added in the UI designer.

using System;

using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class KitchenListTableViewCell : UITableViewCell
	{
		public KitchenCompleteUnit Item { get; set; }
		public UILabel KitchenNameLabel { get { return kitchenNameLabel; } }
		public UILabel TotalAppliancesLabel { get { return totalAppliancesLabel; } }
		public UILabel ElectroluxAppliancesLabel { get { return electroluxAppliancesLabel; } }
		public UILabel DescriptionLabel { get { return descriptionLabel; } }

		public KitchenListTableViewCell (IntPtr handle) : base (handle)
		{
		}
	}
}
