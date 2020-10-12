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
    [Register ("GeneralInfoViewController")]
    partial class GeneralInfoViewController
    {
        [Outlet]
        UIKit.UILabel dateLabel { get; set; }


        [Outlet]
        UIKit.UILabel datesErrorMessageLabel { get; set; }


        [Outlet]
        UIKit.UITextField dateTextField { get; set; }


        [Outlet]
        UIKit.UILabel hoursErrorMessageLabel { get; set; }


        [Outlet]
        UIKit.UILabel hoursLabel { get; set; }


        [Outlet]
        UIKit.UITextField hoursTextField { get; set; }


        [Outlet]
        UIKit.UILabel inheritedListLabel { get; set; }


        [Outlet]
        UIKit.UISegmentedControl inheritedListSegmentedControl { get; set; }


        [Outlet]
        UIKit.UISegmentedControl productTypeSegmentedControl { get; set; }


        [Outlet]
        UIKit.UILabel productTypologyLabel { get; set; }


        [Outlet]
        UIKit.UILabel storeErrorMessageLabel { get; set; }


        [Outlet]
        UIKit.UILabel storeLabel { get; set; }


        [Outlet]
        UIKit.UITextField storeTextField { get; set; }


        [Action ("inheritedListValueChanged:")]
        partial void inheritedListValueChanged (Foundation.NSObject sender);


        [Action ("pickdateButtonAction:")]
        partial void pickdateButtonAction (Foundation.NSObject sender);


        [Action ("productTypeValueChanged:")]
        partial void productTypeValueChanged (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
        }
    }
}