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
    [Register ("AnalyticsFloorspaceDetailsViewController")]
    partial class AnalyticsFloorspaceDetailsViewController
    {
        [Outlet]
        UIKit.UIView gridPlaceholderView { get; set; }


        [Outlet]
        UIKit.UILabel inheritedListLabel { get; set; }


        [Outlet]
        UIKit.UILabel inheritedListMessageLabel { get; set; }


        [Outlet]
        UIKit.UISegmentedControl inheritedListSegmentedControl { get; set; }


        [Outlet]
        UIKit.UILabel productTypologyLabel { get; set; }


        [Outlet]
        UIKit.UILabel productTypologyMessageLabel { get; set; }


        [Outlet]
        UIKit.UISegmentedControl productTypologySegmentedControl { get; set; }


        [Outlet]
        UIKit.UIButton showCompetitorsBrands { get; set; }


        [Outlet]
        UIKit.UIButton showElectroluxBrands { get; set; }


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


        [Action ("showCompetitorsBrandsAction:")]
        partial void showCompetitorsBrandsAction (Foundation.NSObject sender);


        [Action ("showElectroluxBrandsAction:")]
        partial void showElectroluxBrandsAction (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
        }
    }
}