using System;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class WeatherViewCell : UITableViewCell, ICellBinding<WeatherUnit>
	{
		public static readonly NSString Key = new NSString("WeatherViewCell");
		public static readonly UINib Nib;

		static WeatherViewCell()
		{
			Nib = UINib.FromName("WeatherViewCell", NSBundle.MainBundle);
		}

		protected WeatherViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public WeatherUnit Item { get; set; }

		public void BindCell(WeatherUnit item)
		{
			this.Item = item;
			this.TextLabel.Text = item.Text;
		}
	}
}
