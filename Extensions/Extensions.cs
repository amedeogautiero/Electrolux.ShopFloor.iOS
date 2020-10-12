using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Foundation;
using GalaSoft.MvvmLight.Helpers;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public static class Extensions
	{
		public static List<Binding> Bindings = new List<Binding>();

		public static Binding KeepBindingInMemory(this object p, Binding b)
		{
			Bindings.Add(b);
			return b;
		}

		public static UIViewController GetByRestorationID(this UIViewController[] viewControllerArray, string restorationID)
		{
			return viewControllerArray.FirstOrDefault(c => c.RestorationIdentifier == restorationID);
		}

		public static DateTimeOffset ToDateTimeOffset(this DateTime dt)
		{
			dt = DateTime.SpecifyKind(dt, DateTimeKind.Local);
			DateTimeOffset dto = dt;
			return
				dto;
		}

		public static string ToShortDateString(this DateTimeOffset dto)
		{
			DateTime dt = dto.DateTime;

			if (dt.Year == 1)
				return "";

			return
				dt.ToShortDateString();
		}

		public static void SetBackgroundColor(this UIButton button, UIColor color, UIControlState state)
		{
			button.SetBackgroundImage(color.GetImage(), state);
		}

		public static UIImage GetImage(this UIColor color)
		{
			CGRect rect = new CGRect(0, 0, 1, 1);
			UIGraphics.BeginImageContext(rect.Size);
			using (var context = UIGraphics.GetCurrentContext())
			{
				context.SetFillColor(color.CGColor);
				context.FillRect(rect);
				var image = UIGraphics.GetImageFromCurrentImageContext();

				UIGraphics.EndImageContext();

				return image;
			}
		}
	}

    public static class NSDateExtensions
    {
        static DateTime reference = new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime ToDateTime(this NSDate date)
        {
            var utcDateTime = reference.AddSeconds(date.SecondsSinceReferenceDate);
            var dateTime = utcDateTime.ToLocalTime();
            return dateTime;
        }

        public static NSDate ToNSDate(this DateTime datetime)
        {
            var utcDateTime = datetime.ToUniversalTime();
            var date = NSDate.FromTimeIntervalSinceReferenceDate((utcDateTime - reference).TotalSeconds);
            return date;
        }

    }

}

