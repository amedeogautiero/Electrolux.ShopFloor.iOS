// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Electrolux.ShopFloor.iOS.ViewControllers
{
	[Register ("ListAppBaseViewController")]
	partial class ListBaseViewController<T, K>
	{
		[Outlet]
		UIKit.UIView DetailsView { get; set; }

		[Outlet]
		UIKit.UITableView TableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DetailsView != null) {
				DetailsView.Dispose ();
				DetailsView = null;
			}

			if (TableView != null) {
				TableView.Dispose ();
				TableView = null;
			}
		}
	}
}
