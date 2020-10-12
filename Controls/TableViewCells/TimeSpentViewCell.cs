using System;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class TimeSpentViewCell : UITableViewCell, ICellBinding<TimeSpanModelUnit>
	{
		public TimeSpanModelUnit Item
		{
			get;
			set;
		}

		public static readonly NSString Key = new NSString("TimeSpentViewCell");
		public static readonly UINib Nib;

		static TimeSpentViewCell()
		{
			Nib = UINib.FromName("TimeSpentViewCell", NSBundle.MainBundle);
		}

		protected TimeSpentViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public void BindCell(TimeSpanModelUnit item)
		{
			this.Item = item;
			this.TextLabel.Text = item.Text;
		}
	}
}
