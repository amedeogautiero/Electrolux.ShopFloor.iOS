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
	[Register ("FloorspaceCompetitorsViewController")]
	partial class FloorspaceCompetitorsViewController
	{
		[Outlet]
		UIKit.UILabel brandHeaderLabel { get; set; }

		[Outlet]
		UIKit.UILabel categoryHeaderLabel { get; set; }

		[Outlet]
		UIKit.UILabel modelHeaderLabel { get; set; }

		[Outlet]
		UIKit.UILabel oldEnergyClassHeaderLabel { get; set; }

		[Outlet]
		UIKit.UILabel priceHeaderLabel { get; set; }

		[Outlet]
		UIKit.UILabel qtyOnDisplayHeaderLabel { get; set; }

		[Outlet]
		UIKit.UILabel updateEnergyClassHeaderLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (brandHeaderLabel != null) {
				brandHeaderLabel.Dispose ();
				brandHeaderLabel = null;
			}

			if (modelHeaderLabel != null) {
				modelHeaderLabel.Dispose ();
				modelHeaderLabel = null;
			}

			if (priceHeaderLabel != null) {
				priceHeaderLabel.Dispose ();
				priceHeaderLabel = null;
			}

			if (qtyOnDisplayHeaderLabel != null) {
				qtyOnDisplayHeaderLabel.Dispose ();
				qtyOnDisplayHeaderLabel = null;
			}

			if (categoryHeaderLabel != null) {
				categoryHeaderLabel.Dispose ();
				categoryHeaderLabel = null;
			}

			if (oldEnergyClassHeaderLabel != null) {
				oldEnergyClassHeaderLabel.Dispose ();
				oldEnergyClassHeaderLabel = null;
			}

			if (updateEnergyClassHeaderLabel != null) {
				updateEnergyClassHeaderLabel.Dispose ();
				updateEnergyClassHeaderLabel = null;
			}
		}
	}
}
