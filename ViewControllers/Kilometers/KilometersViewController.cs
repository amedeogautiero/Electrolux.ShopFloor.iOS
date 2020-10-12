using System;
using CoreGraphics;
using Electrolux.ShopFloor.iOS.ViewControllers;
using Electrolux.ShopFloor.Middleware.Manager;
using Electrolux.ShopFloor.Mvvm.ViewModels.EditingAreas;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using GalaSoft.MvvmLight.Helpers;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class KilometersViewController : DetailBaseViewController<KilometersViewModel>
	{

		public KilometersViewController(IntPtr handle) : base(handle)
		{
		}

		public override void Translations()
		{
			this.travelTimeLabel.Text = TranslatorManager.GetInstance().GetString("Travel Time (Km)");
			this.orderNumberLabel.Text = TranslatorManager.GetInstance().GetString("Order Number");
			this.visitTimeFromLabel.Text = TranslatorManager.GetInstance().GetString("Visit Time From");
			this.visitTimeToLabel.Text = TranslatorManager.GetInstance().GetString("Visit Time To");

			this.travelTimeTextField.Placeholder = TranslatorManager.GetInstance().GetString("Mandatory");
			this.orderNumberTextField.Placeholder = TranslatorManager.GetInstance().GetString("Mandatory");
			this.visitTimeFromTextField.Placeholder = TranslatorManager.GetInstance().GetString("Mandatory");
			this.visitTimeToTextField.Placeholder = TranslatorManager.GetInstance().GetString("Mandatory");
		}

		public override void ConfigureArea()
		{
			base.ConfigureArea();

			this.visitTimeFromTextField.ShouldBeginEditing += (UITextField textField) =>
			{
				ShowPopover(textField, (TimeSpan timeSelected) =>
				{
					this.AreaViewModel.BeginTime = timeSelected;
					this.AreaViewModel.BeginTimeText = timeSelected.ToString("hh\\:mm");
					DismissPopover();

				});
				return false;
			};

			this.visitTimeToTextField.ShouldBeginEditing += (UITextField textField) =>
			{
				ShowPopover(textField, (TimeSpan timeSelected) =>
				{
					this.AreaViewModel.EndTime = timeSelected;
					this.AreaViewModel.EndTimeText = timeSelected.ToString("hh\\:mm");
					DismissPopover();
				});
				return false;
			};

		}

		public override void RegisterBindingsLocal()
		{
			base.RegisterBindingsLocal();

			if (this.AreaViewModel == null)
				return;

			KeepBindingInMemoryLocal(new Binding<string, string>(
				this.AreaViewModel,
				() => this.AreaViewModel.BeginTimeText,
				this,
				() => this.visitTimeFromTextField.Text,
				BindingMode.OneWay));

			KeepBindingInMemoryLocal(new Binding<string, string>(
				this.AreaViewModel,
				() => this.AreaViewModel.EndTimeText,
				this,
				() => this.visitTimeToTextField.Text,
				BindingMode.OneWay));

			KeepBindingInMemoryLocal(new Binding<string, string>(
				this.AreaViewModel,
				() => this.AreaViewModel.OrderNumber,
				this,
				() => this.orderNumberTextField.Text,
				BindingMode.TwoWay));

			KeepBindingInMemoryLocal(new Binding<string, string>(
				this.AreaViewModel,
				() => this.AreaViewModel.Distance,
				this,
				() => this.travelTimeTextField.Text,
				BindingMode.TwoWay));

			KeepBindingInMemoryLocal(new Binding<string, string>(
				this.AreaViewModel,
				() => this.AreaViewModel.EndErrorMessage,
				this,
				() => this.visitTimeToMessageLabel.Text,
				BindingMode.OneWay));

			KeepBindingInMemoryLocal(new Binding<string, string>(
				this.AreaViewModel,
				() => this.AreaViewModel.BeginErrorMessage,
				this,
				() => this.visitTimeFromMessageLabel.Text,
				BindingMode.OneWay));

			KeepBindingInMemoryLocal(new Binding<string, string>(
				this.AreaViewModel,
				() => this.AreaViewModel.OrderErrorMessage,
				this,
				() => this.orderNumberMessageLabel.Text,
				BindingMode.OneWay));

			KeepBindingInMemoryLocal(new Binding<string, string>(
				this.AreaViewModel,
				() => this.AreaViewModel.DistanceErrorMessage,
				this,
				() => this.travelTimeMessageLabel.Text,
				BindingMode.OneWay));

		}

		public void DismissPopover()
		{
			this.DismissViewController(false, null);
		}

		public void ShowPopover(UITextField textField, Action<TimeSpan> timeSlected)
		{
			var storyboard = AppDelegate.NavigationController.Storyboard;
			var controller = (TimePickerViewController)storyboard.InstantiateViewController("TimePickerViewController");
			controller.TimeSelected = timeSlected;

			controller.ModalPresentationStyle = UIModalPresentationStyle.Popover;

			this.PresentViewController(controller, true, () =>
			{
				controller.PreferredContentSize = new CGSize(controller.ContentRect.Width, 280f);
			});

			UIPopoverPresentationController popController = controller.PopoverPresentationController;
			popController.PermittedArrowDirections = UIPopoverArrowDirection.Any;
			popController.SourceView = this.View;
			popController.SourceRect = textField.Frame;
		}
	}
}
