// This file has been autogenerated from a class added in the UI designer.

using System;
using Electrolux.ShopFloor.iOS;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class CompetitorsActivitiesTableViewCell : UITableViewCell
	{
		public CompetitorActivityUnit Item { get; set; }
		public UILabel BrandLabel { get { return brandLabel; } }
		public UILabel CompetitorsActivityLabel { get { return competitorsActivityLabel; } }
		public UILabel DescriptionLabel { get { return descriptionLabel; } }
		public UILabel ModelLabel { get { return modelLabel; } }
		public UILabel ReasonLabel { get { return reasonLabel; } }
		public UILabel TimeSpentLabel { get { return timeSpentLabel; } }

		public CompetitorsActivitiesTableViewCell (IntPtr handle) : base (handle)
		{
		}
	}
}
