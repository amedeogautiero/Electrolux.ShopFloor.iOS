using System;
using Foundation;

namespace Electrolux.ShopFloor.iOS
{
	public static class Utility
	{
		public static DateTime NSDateToDateTime(this NSDate date)
		{
			var dateCurrentTiemZone = date.toCurrentTimeZone();

			DateTime reference = new DateTime(2001, 1, 1, 0, 0, 0);

			return reference.AddSeconds(dateCurrentTiemZone.SecondsSinceReferenceDate);

		}

		private static NSDate toCurrentTimeZone(this NSDate sourceDate)
		{
			NSTimeZone sourceTimeZone = new NSTimeZone("GMT");

			NSTimeZone destinationTimeZone = NSTimeZone.SystemTimeZone;

			var sourceGMTOffset = sourceTimeZone.SecondsFromGMT(sourceDate);
			var destinationGMTOffset = destinationTimeZone.SecondsFromGMT(sourceDate);
			var interval = destinationGMTOffset - sourceGMTOffset;

			return sourceDate.AddSeconds(interval);
		}

	}
}

