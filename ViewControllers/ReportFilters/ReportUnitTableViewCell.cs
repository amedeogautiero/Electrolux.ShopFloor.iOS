using System;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class ReportUnitTableViewCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString("ReportUnitTableViewCell");
		public static readonly UINib Nib;

		static ReportUnitTableViewCell()
		{
			Nib = UINib.FromName("ReportUnitTableViewCell", NSBundle.MainBundle);
		}

		protected ReportUnitTableViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

	}

}