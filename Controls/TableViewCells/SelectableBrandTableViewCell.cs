using System;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class SelectableBrandTableViewCell : UITableViewCell, ICellBinding<SelectableBrandUnit>
	{
		public SelectableBrandUnit Item { get; set; }

		public static readonly NSString Key = new NSString("SelectableBrandTableViewCell");
		public static readonly UINib Nib;

		static SelectableBrandTableViewCell()
		{
			Nib = UINib.FromName("SelectableBrandTableViewCell", NSBundle.MainBundle);
		}

		protected SelectableBrandTableViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public void BindCell(SelectableBrandUnit item)
		{
			this.Item = item;
			this.TextLabel.Text = item.Text;
//			this.DetailTextLabel.Text = item.Text2;
		}
	}
}
