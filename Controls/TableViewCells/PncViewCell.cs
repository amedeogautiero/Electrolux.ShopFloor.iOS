using System;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class PncViewCell : UITableViewCell, ICellBinding<PncModelUnit>
	{
		public static readonly NSString Key = new NSString("PncViewCell");
		public static readonly UINib Nib;

		static PncViewCell()
		{
			Nib = UINib.FromName("PncViewCell", NSBundle.MainBundle);
		}

		protected PncViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public PncModelUnit Item { get; set; }

		public void BindCell(PncModelUnit item)
		{
			this.Item = item;

			this.TextLabel.Text = item.Text;
		}
	}
}
