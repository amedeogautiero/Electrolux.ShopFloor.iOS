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
    [Register ("FilterDatesViewController")]
    partial class FilterDatesViewController
    {
        [Outlet]
        UIKit.UIButton dateFromButton { get; set; }


        [Outlet]
        UIKit.UIButton dateToButton { get; set; }


        [Action ("dateFromAction:")]
        partial void dateFromAction (Foundation.NSObject sender);


        [Action ("dateToAction:")]
        partial void dateToAction (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
        }
    }
}