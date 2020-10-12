using System;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class KitchenTableViewCell : UITableViewCell, ICellBinding<KitchenUnit>
	{
		public static readonly NSString Key = new NSString("KitchenTableViewCell");
		public static readonly UINib Nib;

		public KitchenUnit Item
		{
			get;
			set;
		}

		static KitchenTableViewCell()
		{
			Nib = UINib.FromName("KitchenTableViewCell", NSBundle.MainBundle);
		}

		protected KitchenTableViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public void BindCell(KitchenUnit item)
		{
			this.Item = item;
			this.TextLabel.Text = item.Text;
		}
	}
}
