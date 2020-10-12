using System;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class EnergyClassViewCell : UITableViewCell, ICellBinding<EnergyLabelUnit>
	{
		public static readonly NSString Key = new NSString("EnergyClassViewCell");
		public static readonly UINib Nib;

		static EnergyClassViewCell()
		{
			Nib = UINib.FromName("EnergyClassViewCell", NSBundle.MainBundle);
		}

		protected EnergyClassViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public EnergyLabelUnit Item { get; set; }

		public void BindCell(EnergyLabelUnit item)
		{
			this.Item = item;
			this.TextLabel.Text = item.Text;
		}
	}
}
