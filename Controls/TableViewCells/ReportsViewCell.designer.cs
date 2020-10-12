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
    [Register ("ReportsViewCell")]
    partial class ReportsViewCell
    {
        [Outlet]
        UIKit.UILabel businessSectorLabel { get; set; }


        [Outlet]
        UIKit.UILabel dateLabel { get; set; }


        [Outlet]
        UIKit.UIButton deleteButton { get; set; }


        [Outlet]
        UIKit.UIButton editButton { get; set; }


        [Outlet]
        UIKit.UILabel storeLabel { get; set; }


        [Outlet]
        UIKit.UIButton uploadReportButton { get; set; }


        [Outlet]
        UIKit.UIButton viewButton { get; set; }


        [Outlet]
        UIKit.UIButton viewUploadErrorButton { get; set; }


        [Action ("deleteButtonAction:")]
        partial void deleteButtonAction (Foundation.NSObject sender);


        [Action ("editButtonAction:")]
        partial void editButtonAction (Foundation.NSObject sender);


        [Action ("uploadButtonAction:")]
        partial void uploadButtonAction (Foundation.NSObject sender);


        [Action ("viewButtonAction:")]
        partial void viewButtonAction (Foundation.NSObject sender);


        [Action ("viewUploadErrorButtonAction:")]
        partial void viewUploadErrorButtonAction (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
        }
    }
}