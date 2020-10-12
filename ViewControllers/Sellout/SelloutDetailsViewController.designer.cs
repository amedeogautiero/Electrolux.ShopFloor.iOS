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
    [Register ("SelloutDetailsViewController")]
    partial class SelloutDetailsViewController
    {
        [Outlet]
        UIKit.UILabel generalMessageLabel { get; set; }


        [Outlet]
        UIKit.UILabel inEvidenceLabel { get; set; }


        [Outlet]
        UIKit.UISegmentedControl inEvidenceSegmentedControl { get; set; }


        [Outlet]
        UIKit.UILabel inStockLabel { get; set; }


        [Outlet]
        UIKit.UISegmentedControl inStockSegmentedControl { get; set; }


        [Outlet]
        UIKit.UILabel modelProductSetLabel { get; set; }


        [Outlet]
        UIKit.UILabel modelProductSetMessageLabel { get; set; }


        [Outlet]
        UIKit.UITextField modelProductSetTextField { get; set; }


        [Outlet]
        UIKit.UILabel pncLabel { get; set; }


        [Outlet]
        UIKit.UILabel pncMessageLabel { get; set; }


        [Outlet]
        UIKit.UITextField pncTextField { get; set; }


        [Outlet]
        UIKit.UILabel priceLabel { get; set; }


        [Outlet]
        UIKit.UILabel priceMessageLabel { get; set; }


        [Outlet]
        UIKit.UITextField priceTextField { get; set; }


        [Outlet]
        UIKit.UILabel quantityLabel { get; set; }


        [Outlet]
        UIKit.UILabel quantityMessageLabel { get; set; }


        [Outlet]
        UIKit.UITextField quantityTextField { get; set; }


        [Outlet]
        UIKit.UILabel soldUsingIPadLabel { get; set; }


        [Outlet]
        UIKit.UISegmentedControl soldUsingIPadSegmentedControl { get; set; }


        [Action ("inEvidenceValueChanged:")]
        partial void inEvidenceValueChanged (Foundation.NSObject sender);


        [Action ("inStockValueChanged:")]
        partial void inStockValueChanged (Foundation.NSObject sender);


        [Action ("soldUsingIPadValueChanged:")]
        partial void soldUsingIPadValueChanged (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
        }
    }
}