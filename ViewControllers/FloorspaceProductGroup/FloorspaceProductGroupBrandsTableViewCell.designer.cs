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
    [Register ("FloorspaceProductGroupBrandsTableViewCell")]
    partial class FloorspaceProductGroupBrandsTableViewCell
    {
        [Outlet]
        UIKit.UILabel brandLabel { get; set; }


        [Outlet]
        UIKit.UILabel qtyLabel { get; set; }


        [Outlet]
        UIKit.UILabel qtyPOSLabel { get; set; }


        [Outlet]
        UIKit.UILabel qtyPromoLabel { get; set; }


        [Outlet]
        UIKit.UILabel qtySpecialLabel { get; set; }


        [Action ("qtyMinusAction:")]
        partial void qtyMinusAction (Foundation.NSObject sender);


        [Action ("qtyPlusAction:")]
        partial void qtyPlusAction (Foundation.NSObject sender);


        [Action ("qtyPOSMinusAction:")]
        partial void qtyPOSMinusAction (Foundation.NSObject sender);


        [Action ("qtyPOSPlusAction:")]
        partial void qtyPOSPlusAction (Foundation.NSObject sender);


        [Action ("qtyPromoMinusAction:")]
        partial void qtyPromoMinusAction (Foundation.NSObject sender);


        [Action ("qtyPromoPlusAction:")]
        partial void qtyPromoPlusAction (Foundation.NSObject sender);


        [Action ("qtySpecialMinusAction:")]
        partial void qtySpecialMinusAction (Foundation.NSObject sender);


        [Action ("qtySpecialPlusAction:")]
        partial void qtySpecialPlusAction (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
        }
    }
}