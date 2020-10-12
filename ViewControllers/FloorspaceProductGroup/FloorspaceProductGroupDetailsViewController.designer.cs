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
    [Register ("FloorspaceProductGroupDetailsViewController")]
    partial class FloorspaceProductGroupDetailsViewController
    {
        [Outlet]
        UIKit.UIButton addBrandButton { get; set; }


        [Outlet]
        UIKit.UILabel brandHeaderLabel { get; set; }


        [Outlet]
        UIKit.UILabel brandLabel { get; set; }


        [Outlet]
        UIKit.UILabel brandMessageLabel { get; set; }


        [Outlet]
        UIKit.UITextField brandTextField { get; set; }


        [Outlet]
        UIKit.UIButton optionalDataButton { get; set; }


        [Outlet]
        UIKit.UILabel productGroupLabel { get; set; }


        [Outlet]
        UIKit.UILabel productGroupMessageLabel { get; set; }


        [Outlet]
        UIKit.UITextField productGroupTextField { get; set; }


        [Outlet]
        UIKit.UILabel qtyHeaderLabel { get; set; }


        [Outlet]
        UIKit.UILabel qtyLabel { get; set; }


        [Outlet]
        UIKit.UILabel qtyPOSHeaderLabel { get; set; }


        [Outlet]
        UIKit.UILabel qtyPOSLabel { get; set; }


        [Outlet]
        UIKit.UITextField qtyPOSTextField { get; set; }


        [Outlet]
        UIKit.UILabel qtyPromoHeaderLabel { get; set; }


        [Outlet]
        UIKit.UILabel qtyPromoLabel { get; set; }


        [Outlet]
        UIKit.UITextField qtyPromoTextField { get; set; }


        [Outlet]
        UIKit.UILabel qtySpecialHeaderLabel { get; set; }


        [Outlet]
        UIKit.UILabel qtySpecialLabel { get; set; }


        [Outlet]
        UIKit.UITextField qtySpecialTextField { get; set; }


        [Outlet]
        UIKit.UITextField qtyTextField { get; set; }


        [Outlet]
        UIKit.UITableView tableView { get; set; }


        [Action ("addBrandAction:")]
        partial void addBrandAction (Foundation.NSObject sender);


        [Action ("optionalDataAction:")]
        partial void optionalDataAction (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
        }
    }
}