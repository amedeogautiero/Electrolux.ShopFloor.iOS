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
    [Register ("ReportsFiltersViewController")]
    partial class ReportsFiltersViewController
    {
        [Outlet]
        UIKit.UIBarButtonItem applyButton { get; set; }


        [Outlet]
        UIKit.UIButton cityButton { get; set; }


        [Outlet]
        UIKit.UIView cityView { get; set; }


        [Outlet]
        UIKit.UIButton datesButton { get; set; }


        [Outlet]
        UIKit.UIView datesView { get; set; }


        [Outlet]
        UIKit.UIView filterView { get; set; }


        [Outlet]
        UIKit.UIBarButtonItem resetButton { get; set; }


        [Outlet]
        UIKit.UIButton statusButton { get; set; }


        [Outlet]
        UIKit.UIView statusView { get; set; }


        [Outlet]
        UIKit.UIButton storeButton { get; set; }


        [Outlet]
        UIKit.UIView storeView { get; set; }


        [Action ("applyButtonAction:")]
        partial void applyButtonAction (Foundation.NSObject sender);


        [Action ("cityButtonAction:")]
        partial void cityButtonAction (Foundation.NSObject sender);


        [Action ("datesButtonAction:")]
        partial void datesButtonAction (Foundation.NSObject sender);


        [Action ("resetButtonAction:")]
        partial void resetButtonAction (Foundation.NSObject sender);


        [Action ("statusButtonAction:")]
        partial void statusButtonAction (Foundation.NSObject sender);


        [Action ("storeButtonAction:")]
        partial void storeButtonAction (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
        }
    }
}