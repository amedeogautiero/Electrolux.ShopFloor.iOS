using System;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class ActivityViewCell : UITableViewCell,ICellBinding<ActivityModelUnit>
	{
		public ActivityModelUnit Item { get; set; }

		public static readonly NSString Key = new NSString("ActivityViewCell");
		public static readonly UINib Nib;

		static ActivityViewCell()
		{
			Nib = UINib.FromName("ActivityViewCell", NSBundle.MainBundle);
		}

		protected ActivityViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public void BindCell(ActivityModelUnit item)
		{
			this.Item = item;
			this.TextLabel.Text = item.Text;
            this.TextLabel.Enabled = item.IsEnabled;
            if (!item.IsEnabled)
            {
                this.SelectionStyle = UITableViewCellSelectionStyle.None;
            } else {
                this.SelectionStyle = UITableViewCellSelectionStyle.Default;
            }
		}
	}
}
