using System;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class BrandTableViewCell : UITableViewCell, ICellBinding<BrandUnit>
	{
		public BrandUnit Item { get; set; }

		public static readonly NSString Key = new NSString("BrandTableViewCell");
		public static readonly UINib Nib;

		static BrandTableViewCell()
		{
			Nib = UINib.FromName("BrandTableViewCell", NSBundle.MainBundle);
		}

		protected BrandTableViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public void BindCell(BrandUnit item)
		{
			this.Item = item;
			this.TextLabel.Text = item.Text;
			//			this.DetailTextLabel.Text = item.Text2;
		}
	}
}
