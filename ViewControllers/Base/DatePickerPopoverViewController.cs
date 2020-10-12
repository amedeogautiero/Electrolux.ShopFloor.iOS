using System;
using System.Collections.ObjectModel;
using CoreGraphics;
using UIKit;
using FSCalendar;
using Foundation;
//using Factorymind.Components;

namespace Electrolux.ShopFloor.iOS
{
	public class DatePickerPopoverViewController : UIViewController
	{
		private DatePickerPopoverContentViewController content;
		private UIPopoverArrowDirection direction;
		private CGSize contentSize;

		public UIPopoverController DetailViewPopover { get; set; }

		public DatePickerPopoverViewController(IntPtr handle) : base(handle)
		{
		}

		//loads the PopoverViewController.xib file and connects it to this object
		public DatePickerPopoverViewController() : base("DatePickerPopoverViewController", null)
		{
		}

		public DatePickerPopoverViewController(CGSize size, UIPopoverArrowDirection direction, Action<DateTime> itemSelected = null) : base()
		{
			contentSize = size;
			content = new DatePickerPopoverContentViewController(size, itemSelected);
			DetailViewPopover = new UIPopoverController(content);
			this.direction = direction;
		}

		public void ShowPopover(UIView sender)
		{
			// Present the popover from the button that was tapped in the detail view.
			DetailViewPopover.SetPopoverContentSize(contentSize, true);
			DetailViewPopover.PresentFromRect(sender.Frame, sender.Superview, this.direction, true);
		}

		public void DismissPopover()
		{
			DetailViewPopover.Dismiss(true);
		}

	}

    class DatePickerPopoverContentViewController : UIViewController, IFSCalendarDelegate, IFSCalendarDataSource, IFSCalendarDelegateAppearance
	{
		//private FMCalendar fmCalendar;
        private FSCalendarView fsCalendar;
        private readonly Action<DateTime> itemSelectedForDelegateCall;

        public DatePickerPopoverContentViewController(CGSize size, Action<DateTime> itemSelected = null) : base()
		{
            itemSelectedForDelegateCall = itemSelected;

            fsCalendar = new FSCalendarView()
            {
                Frame = new CGRect(new CGPoint(0f, 0f), size),
                DataSource = this,
                WeakDelegate = this,
                AllowsMultipleSelection = false
            };

            View.BackgroundColor = UIColor.White;

            /*
			fmCalendar = new FMCalendar(new CGRect(new CGPoint(0f, 0f), size));

			// Specify selection color
			fmCalendar.SelectionColor = UIColor.Red;

			// Specify today circle Color
			fmCalendar.TodayCircleColor = UIColor.Red;

			// Customizing appearance
			fmCalendar.LeftArrow = UIImage.FromFile("leftArrow.png");
			fmCalendar.RightArrow = UIImage.FromFile("rightArrow.png");

			fmCalendar.MonthFormatString = "MMMM yyyy";

			// Shows Sunday as last day of the week
			fmCalendar.SundayFirst = false;

			// Mark with a dot dates that fulfill the predicate
			fmCalendar.IsDayMarkedDelegate = (date) =>
			{
				return date.Day % 2 == 1;
			};

			// Turn gray dates that fulfill the predicate
			fmCalendar.IsDateAvailable = (date) =>
			{
				return (date >= DateTime.Today);
			};

			fmCalendar.MonthChanged = (date) =>
			{
				Console.WriteLine("Month changed {0}", date.Date);
			};

			fmCalendar.DateSelected += (date) =>
			{
				itemSelected(date);
				Console.WriteLine("Date selected: {0}", date);
			};
			*/
 		}

		public DatePickerPopoverContentViewController(IntPtr handle) : base(handle)
		{
		}

		//loads the PopoverContentViewController.xib file and connects it to this object
		public DatePickerPopoverContentViewController() : base()
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			//this.View.AddSubview(fmCalendar);
            this.View.AddSubview(fsCalendar);
		}

        [Export("calendar:didSelectDate:atMonthPosition:")]
        void CalendarDidSelectDate(FSCalendarView calendar, NSDate date, FSCalendarMonthPosition monthPosition)
        {
            itemSelectedForDelegateCall(NSDateExtensions.ToDateTime(date));
            Console.WriteLine("Date selected: {0}", date);
        }
   	}
}

