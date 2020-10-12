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
    [Register ("MainCollectionCell")]
    partial class MainCollectionCell
    {
        [Outlet]
        UIKit.UILabel qtyLabel { get; set; }


        [Outlet]
        UIKit.UILabel qtyOnDisplayLabel { get; set; }


        [Outlet]
        UIKit.UILabel qtyOnStockLabel { get; set; }


        [Outlet]
        UIKit.UILabel qtyPOSLabel { get; set; }


        [Action ("qtyDisplayMinusAction:")]
        partial void qtyDisplayMinusAction (Foundation.NSObject sender);


        [Action ("qtyDisplayPlusAction:")]
        partial void qtyDisplayPlusAction (Foundation.NSObject sender);


        [Action ("qtyMinusAction:")]
        partial void qtyMinusAction (Foundation.NSObject sender);


        [Action ("qtyPlusAction:")]
        partial void qtyPlusAction (Foundation.NSObject sender);


        [Action ("qtyPOSMinusAction:")]
        partial void qtyPOSMinusAction (Foundation.NSObject sender);


        [Action ("qtyPOSPlusAction:")]
        partial void qtyPOSPlusAction (Foundation.NSObject sender);


        [Action ("qtyStockMinusAction:")]
        partial void qtyStockMinusAction (Foundation.NSObject sender);


        [Action ("qtyStockPlusAction:")]
        partial void qtyStockPlusAction (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
        }
    }
}