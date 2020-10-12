// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Electrolux.ShopFloor.iOS
{
	[Register ("ReportUnitTableViewCell")]
	partial class ReportUnitTableViewCell
	{
		[Outlet]
		UIKit.UILabel reportUnitLabel { get; set; }

		[Outlet]
		UIKit.UISwitch reportUnitSelected { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (reportUnitSelected != null) {
				reportUnitSelected.Dispose ();
				reportUnitSelected = null;
			}

			if (reportUnitLabel != null) {
				reportUnitLabel.Dispose ();
				reportUnitLabel = null;
			}
		}
	}
}
