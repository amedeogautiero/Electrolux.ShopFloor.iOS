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
	[Register ("FSCompetitorByBrandProductGroupsCell")]
	partial class FSCompetitorByBrandProductGroupsCell
	{
		[Outlet]
		UIKit.UIButton checkProductGroupButton { get; set; }

		[Outlet]
		UIKit.UIImageView isSelectedImageView { get; set; }

		[Outlet]
		UIKit.UILabel productGroupLabel { get; set; }

		[Action ("checkProductGroupButtonAction:")]
		partial void checkProductGroupButtonAction (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (productGroupLabel != null) {
				productGroupLabel.Dispose ();
				productGroupLabel = null;
			}

			if (isSelectedImageView != null) {
				isSelectedImageView.Dispose ();
				isSelectedImageView = null;
			}

			if (checkProductGroupButton != null) {
				checkProductGroupButton.Dispose ();
				checkProductGroupButton = null;
			}
		}
	}
}
