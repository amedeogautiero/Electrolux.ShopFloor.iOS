using System;
using CoreGraphics;
using Electrolux.ShopFloor.iOS.ViewControllers;
using Electrolux.ShopFloor.Mvvm.ViewModels.EditingAreas;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using GalaSoft.MvvmLight.Helpers;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{

	public class BasePopoverController : BaseViewController
	{
		protected UIPopoverArrowDirection direction;

		public UIPopoverController DetailViewPopover { get; set; }
		public bool IsPopoverShown { get; set; }

		public BasePopoverController(IntPtr handle) : base(handle)
		{
		}

		public BasePopoverController(UIPopoverArrowDirection direction) : base()
		{
			this.direction = direction;
		}

		public virtual void ShowPopover(UIView sender, CGSize size)
		{
			this.IsPopoverShown = true;
		}

		public virtual void DismissPopover()
		{
			DetailViewPopover.Dismiss(true);
			this.IsPopoverShown = false;
		}
	}
}
