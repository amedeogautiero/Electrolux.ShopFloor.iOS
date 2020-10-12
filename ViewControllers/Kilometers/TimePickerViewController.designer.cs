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
    [Register ("TimePickerViewController")]
    partial class TimePickerViewController
    {
        [Outlet]
        UIKit.UIDatePicker timePicker { get; set; }


        [Action ("confirmButton:")]
        partial void confirmButton (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
        }
    }
}