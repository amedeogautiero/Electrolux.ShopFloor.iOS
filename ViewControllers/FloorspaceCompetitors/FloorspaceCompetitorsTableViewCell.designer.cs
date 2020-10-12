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
	[Register ("FloorspaceCompetitorsTableViewCell")]
	partial class FloorspaceCompetitorsTableViewCell
	{
		[Outlet]
		UIKit.UILabel brandLabel { get; set; }

		[Outlet]
		UIKit.UILabel categoryLabel { get; set; }

		[Outlet]
		UIKit.UILabel modelLabel { get; set; }

		[Outlet]
		UIKit.UILabel oldEnergyClassLabel { get; set; }

		[Outlet]
		UIKit.UILabel priceLabel { get; set; }

		[Outlet]
		UIKit.UILabel qtyOnDisplayLabel { get; set; }

		[Outlet]
		UIKit.UILabel updateEnergyClassLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (brandLabel != null) {
				brandLabel.Dispose ();
				brandLabel = null;
			}

			if (modelLabel != null) {
				modelLabel.Dispose ();
				modelLabel = null;
			}

			if (priceLabel != null) {
				priceLabel.Dispose ();
				priceLabel = null;
			}

			if (qtyOnDisplayLabel != null) {
				qtyOnDisplayLabel.Dispose ();
				qtyOnDisplayLabel = null;
			}

			if (categoryLabel != null) {
				categoryLabel.Dispose ();
				categoryLabel = null;
			}

			if (oldEnergyClassLabel != null) {
				oldEnergyClassLabel.Dispose ();
				oldEnergyClassLabel = null;
			}

			if (updateEnergyClassLabel != null) {
				updateEnergyClassLabel.Dispose ();
				updateEnergyClassLabel = null;
			}
		}
	}
}
