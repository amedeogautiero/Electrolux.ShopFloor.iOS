using System;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class ProductGroupBrandTableViewCell : UITableViewCell, ICellBinding<BrandProductGroupUnit>
	{
		public static readonly NSString Key = new NSString("ProductGroupBrandTableViewCell");
		public static readonly UINib Nib;

		static ProductGroupBrandTableViewCell()
		{
			Nib = UINib.FromName("ProductGroupBrandTableViewCell", NSBundle.MainBundle);
		}

		protected ProductGroupBrandTableViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public BrandProductGroupUnit Item { get; set; }

		public void BindCell(BrandProductGroupUnit item)
		{
			this.Item = item;
			if (item == null)
				return;

			this.TextLabel.Text = item.Text;
			this.DetailTextLabel.Text = item.Text2;
		}
	}
}
