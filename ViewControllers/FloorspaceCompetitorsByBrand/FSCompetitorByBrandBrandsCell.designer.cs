// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Electrolux.ShopFloor.iOS
{
	[Register ("FSCompetitorByBrandBrandsCell")]
	partial class FSCompetitorByBrandBrandsCell
	{
		[Outlet]
		UIKit.UILabel brandLabel { get; set; }

		[Outlet]
		UIKit.UIButton checkBrandButton { get; set; }

		[Outlet]
		UIKit.UIImageView isSelectedImageView { get; set; }

		[Action ("checkBrandButtonAction:")]
		partial void checkBrandButtonAction (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (brandLabel != null) {
				brandLabel.Dispose ();
				brandLabel = null;
			}

			if (isSelectedImageView != null) {
				isSelectedImageView.Dispose ();
				isSelectedImageView = null;
			}

			if (checkBrandButton != null) {
				checkBrandButton.Dispose ();
				checkBrandButton = null;
			}
		}
	}
}
