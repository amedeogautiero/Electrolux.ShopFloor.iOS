// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Electrolux.ShopFloor.iOS
{
    [Register ("ReportsViewController")]
    partial class ReportsViewController
    {
        [Outlet]
        UIKit.UIBarButtonItem filtersButton { get; set; }


        [Outlet]
        UIKit.UIView filtersView { get; set; }


        [Outlet]
        UIKit.UILabel headerLabelDate { get; set; }


        [Outlet]
        UIKit.UILabel headerLabelSector { get; set; }


        [Outlet]
        UIKit.UILabel headerLabelStatus { get; set; }


        [Outlet]
        UIKit.UILabel headerLabelStore { get; set; }


        [Outlet]
        UIKit.UITableView tableView { get; set; }


        [Action ("filtersButtonAction:")]
        partial void filtersButtonAction (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
        }
    }
}