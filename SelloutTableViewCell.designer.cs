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
	[Register ("SelloutTableViewCell")]
	partial class SelloutTableViewCell
	{
		[Outlet]
		UIKit.UILabel inEvidenceLabel { get; set; }

		[Outlet]
		UIKit.UILabel inStockLabel { get; set; }

		[Outlet]
		UIKit.UILabel modelProductSetLabel { get; set; }

		[Outlet]
		UIKit.UILabel pncLabel { get; set; }

		[Outlet]
		UIKit.UILabel priceLabel { get; set; }

		[Outlet]
		UIKit.UILabel quantityLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (modelProductSetLabel != null) {
				modelProductSetLabel.Dispose ();
				modelProductSetLabel = null;
			}

			if (pncLabel != null) {
				pncLabel.Dispose ();
				pncLabel = null;
			}

			if (quantityLabel != null) {
				quantityLabel.Dispose ();
				quantityLabel = null;
			}

			if (priceLabel != null) {
				priceLabel.Dispose ();
				priceLabel = null;
			}

			if (inEvidenceLabel != null) {
				inEvidenceLabel.Dispose ();
				inEvidenceLabel = null;
			}

			if (inStockLabel != null) {
				inStockLabel.Dispose ();
				inStockLabel = null;
			}
		}
	}
}
