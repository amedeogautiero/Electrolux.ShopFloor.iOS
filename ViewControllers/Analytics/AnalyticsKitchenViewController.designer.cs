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
    [Register ("AnalyticsKitchenViewController")]
    partial class AnalyticsKitchenViewController
    {
        [Outlet]
        UIKit.UILabel comparisonLabel { get; set; }


        [Outlet]
        UIKit.UIView comparisonNoDataView { get; set; }


        [Outlet]
        UIKit.UILabel comparisonNoDatLabel { get; set; }


        [Outlet]
        UIKit.UIButton comparisonShowDetailsButton { get; set; }


        [Outlet]
        UIKit.UITableView comparisonTableView { get; set; }


        [Outlet]
        UIKit.UIView comparisonView { get; set; }


        [Outlet]
        UIKit.UILabel inheritedListLabel { get; set; }


        [Outlet]
        UIKit.UILabel inheritedListMessageLabel { get; set; }


        [Outlet]
        UIKit.UISegmentedControl inheritedListSegmentedControl { get; set; }


        [Outlet]
        UIKit.UIBarButtonItem modelVariationsBarButtonAction { get; set; }


        [Outlet]
        UIKit.UILabel previousReportLabel { get; set; }


        [Outlet]
        UIKit.UILabel previousReportNoDataLabel { get; set; }


        [Outlet]
        UIKit.UIView previousReportNoDataView { get; set; }


        [Outlet]
        UIKit.UIButton previousReportShowDetailsButton { get; set; }


        [Outlet]
        UIKit.UITableView previousReportTableView { get; set; }


        [Outlet]
        UIKit.UIView previousReportView { get; set; }


        [Outlet]
        UIKit.UILabel productTypologyLabel { get; set; }


        [Outlet]
        UIKit.UILabel productTypologyMessageLabel { get; set; }


        [Outlet]
        UIKit.UISegmentedControl productTypologySegmentedControl { get; set; }


        [Outlet]
        UIKit.UILabel storeLabel { get; set; }


        [Outlet]
        UIKit.UILabel storeMessageLabel { get; set; }


        [Outlet]
        UIKit.UITextField storeTextField { get; set; }


        [Action ("comparisonShowDetailsAction:")]
        partial void comparisonShowDetailsAction (Foundation.NSObject sender);


        [Action ("inheritedListValueChanged:")]
        partial void inheritedListValueChanged (Foundation.NSObject sender);


        [Action ("modelVariationsAction:")]
        partial void modelVariationsAction (Foundation.NSObject sender);


        [Action ("previousReportShowDetailsAction:")]
        partial void previousReportShowDetailsAction (Foundation.NSObject sender);


        [Action ("productTypologyValueChanged:")]
        partial void productTypologyValueChanged (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
        }
    }
}