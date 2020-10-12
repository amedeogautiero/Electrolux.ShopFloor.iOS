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
	[Register ("FloorspaceCompetitorsByBrandFiltersViewController")]
	partial class FloorspaceCompetitorsByBrandFiltersViewController
	{
		[Outlet]
		UIKit.UILabel brandsHeaderLabel { get; set; }

		[Outlet]
		UIKit.UITableView brandsTableView { get; set; }

		[Outlet]
		UIKit.UIButton clearFilterButton { get; set; }

		[Outlet]
		UIKit.UIButton filterButton { get; set; }

		[Outlet]
		UIKit.UILabel filterErrorMessageLabel { get; set; }

		[Outlet]
		UIKit.UILabel productGroupsHeaderLabel { get; set; }

		[Outlet]
		UIKit.UITableView productGroupsTableView { get; set; }

		[Action ("clearFilterButtonAction:")]
		partial void clearFilterButtonAction (Foundation.NSObject sender);

		[Action ("filterButtonAction:")]
		partial void filterButtonAction (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (brandsHeaderLabel != null) {
				brandsHeaderLabel.Dispose ();
				brandsHeaderLabel = null;
			}

			if (brandsTableView != null) {
				brandsTableView.Dispose ();
				brandsTableView = null;
			}

			if (productGroupsHeaderLabel != null) {
				productGroupsHeaderLabel.Dispose ();
				productGroupsHeaderLabel = null;
			}

			if (productGroupsTableView != null) {
				productGroupsTableView.Dispose ();
				productGroupsTableView = null;
			}

			if (filterErrorMessageLabel != null) {
				filterErrorMessageLabel.Dispose ();
				filterErrorMessageLabel = null;
			}

			if (clearFilterButton != null) {
				clearFilterButton.Dispose ();
				clearFilterButton = null;
			}

			if (filterButton != null) {
				filterButton.Dispose ();
				filterButton = null;
			}
		}
	}
}
