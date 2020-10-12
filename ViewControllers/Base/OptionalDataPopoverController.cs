using System;
using CoreGraphics;
using Electrolux.ShopFloor.iOS.ViewControllers;
using Electrolux.ShopFloor.Mvvm.ViewModels.EditingAreas;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using GalaSoft.MvvmLight.Helpers;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public class OptionalDataPopoverController<T, K, M> : BasePopoverController where T : OptionalDataBaseViewController<K, M> where K : IndexedUnitCore where M : AreaViewModelBase
	{
		private T content;

		public OptionalDataPopoverController(IntPtr handle) : base(handle)
		{
		}

		public OptionalDataPopoverController(UIPopoverArrowDirection direction, M areaViewModel) : base(direction)
		{
			var sb = AppDelegate.NavigationController.Storyboard;
			// the storyboard id MUST be equal to the class name of the controller for this to work
			content = (T)sb.InstantiateViewController(typeof(T).Name);
			content.Owner = this;
			content.AreaViewModel = areaViewModel;
			DetailViewPopover = new UIPopoverController(content);
		}

		public void ShowPopover(UIView sender, CGSize size, K item)
		{
			this.ShowPopover(sender, size);

			// Present the popover from the button that was tapped in the detail view.
			content.Item = item;

			DetailViewPopover.SetPopoverContentSize(size, true);
			DetailViewPopover.PresentFromRect(sender.Frame, sender.Superview, this.direction, true);
		}
	}
}
