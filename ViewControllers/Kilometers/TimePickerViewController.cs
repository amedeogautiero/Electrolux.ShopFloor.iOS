// This file has been autogenerated from a class added in the UI designer.

using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class TimePickerViewController : UIViewController
	{
		public CGRect ContentRect
		{
			get { return this.timePicker.Frame; }
		}

		public TimePickerViewController(IntPtr handle) : base(handle)
		{

		}

		public Action<TimeSpan> TimeSelected;

		partial void confirmButton(NSObject sender)
		{
			var dt = timePicker.Date.NSDateToDateTime();

			TimeSelected(new TimeSpan(dt.Hour, dt.Minute, 0));
		}

	}



}
