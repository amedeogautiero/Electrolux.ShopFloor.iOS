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
    [Register ("FloorspaceElectroluxTableViewCell")]
    partial class FloorspaceElectroluxTableViewCell
    {
        [Outlet]
        UIKit.UILabel brandLabel { get; set; }


        [Outlet]
        UIKit.UILabel categoryLabel { get; set; }


        [Outlet]
        UIKit.UIButton checkboxButton { get; set; }


        [Outlet]
        UIKit.UILabel errorMessageLabel { get; set; }


        [Outlet]
        UIKit.UILabel isInPromoLabel { get; set; }


        [Outlet]
        UIKit.UISwitch isInPromoSwitch { get; set; }


        [Outlet]
        UIKit.UILabel isPreferredLabel { get; set; }


        [Outlet]
        UIKit.UILabel modelLabel { get; set; }


        [Outlet]
        UIKit.UIButton optionalDataButton { get; set; }


        [Outlet]
        UIKit.UILabel priceLabel { get; set; }


        [Outlet]
        UIKit.UITextField priceTextField { get; set; }


        [Outlet]
        UIKit.UILabel quantityOnDisplayLabel { get; set; }


        [Outlet]
        UIKit.UITextField quantityOnDisplayTextField { get; set; }


        [Outlet]
        UIKit.UILabel quantitySpecialPlacementLabel { get; set; }


        [Outlet]
        UIKit.UILabel quantityWithPOSMLabel { get; set; }


        [Outlet]
        UIKit.UISwitch specialPlacementSwitch { get; set; }


        [Outlet]
        UIKit.UISwitch withPOSMSwitch { get; set; }


        [Action ("checkboxButtonAction:")]
        partial void checkboxButtonAction (Foundation.NSObject sender);


        [Action ("optionalDataButtonAction:")]
        partial void optionalDataButtonAction (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
        }
    }
}