using System;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class DailyTrafficViewCell : UITableViewCell, ICellBinding<TrafficUnit>
	{
		public static readonly NSString Key = new NSString("DailyTrafficViewCell");
		public static readonly UINib Nib;

		static DailyTrafficViewCell()
		{
			Nib = UINib.FromName("DailyTrafficViewCell", NSBundle.MainBundle);
		}

		protected DailyTrafficViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public TrafficUnit Item { get; set;}

		public void BindCell(TrafficUnit item)
		{
			this.Item = item;
			this.TextLabel.Text = item.Text;
		}
	}
}
