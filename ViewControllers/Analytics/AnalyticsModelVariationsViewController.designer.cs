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
    [Register ("AnalyticsModelVariationsViewController")]
    partial class AnalyticsModelVariationsViewController
    {
        [Outlet]
        UIKit.UILabel brandHeaderLabel { get; set; }


        [Outlet]
        UIKit.UILabel inheritedListLabel { get; set; }


        [Outlet]
        UIKit.UILabel inheritedListMessageLabel { get; set; }


        [Outlet]
        UIKit.UISegmentedControl inheritedListSegmentedControl { get; set; }


        [Outlet]
        UIKit.UILabel modelHeaderLabel { get; set; }


        [Outlet]
        UIKit.UITableView modelVariationTableView { get; set; }


        [Outlet]
        UIKit.UILabel productGroupHeaderLabel { get; set; }


        [Outlet]
        UIKit.UILabel productTypologyLabel { get; set; }


        [Outlet]
        UIKit.UILabel productTypologyMessageLabel { get; set; }


        [Outlet]
        UIKit.UISegmentedControl productTypologySegmentedControl { get; set; }


        [Outlet]
        UIKit.UILabel starProductsHeaderLabel { get; set; }


        [Outlet]
        UIKit.UILabel statusHeaderLabel { get; set; }


        [Outlet]
        UIKit.UILabel storeLabel { get; set; }


        [Outlet]
        UIKit.UILabel storeMessageLabel { get; set; }


        [Outlet]
        UIKit.UITextField storeTextField { get; set; }


        [Action ("inheritedListValueChanged:")]
        partial void inheritedListValueChanged (Foundation.NSObject sender);


        [Action ("productTypologyValueChanged:")]
        partial void productTypologyValueChanged (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
        }
    }
}