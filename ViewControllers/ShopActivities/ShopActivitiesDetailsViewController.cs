using System;
using CoreGraphics;
using Electrolux.ShopFloor.iOS.ViewControllers;
using Electrolux.ShopFloor.Middleware.Manager;
using Electrolux.ShopFloor.Mvvm.ViewModels;
using Electrolux.ShopFloor.Mvvm.ViewModels.EditingAreas;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using GalaSoft.MvvmLight.Helpers;
using UIKit;
using System.Collections.Generic;
using Electrolux.ShopFloor.iOS;

namespace Electrolux.ShopFloor.iOS
{
	public partial class ShopActivitiesDetailsViewController : ListDetailBaseViewController<ShopActivitiesViewModel>
	{
		private PopoverViewController<ModelCategoryUnit> modelCategoryPopoverController;
		private PopoverViewController<BrandUnit> brandPopoverController;
		private PopoverViewController<ActivityModelUnit> activityPopoverController;
		private PopoverViewController<ReasonModelUnit> reasonPopoverController;
		private PopoverViewController<TimeSpanModelUnit> timeSpentPopoverController;

		protected override UIKit.UITextView[] TextViewArray()
		{
			return new UITextView[] { this.DescriptionTextView };
		}

		public override void RegisterBindingsLocal()
		{
			base.RegisterBindingsLocal();

			KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.Model, this, () => this.ModelCategoryTextField.Text, BindingMode.TwoWay));
			KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.ModelErrorMessage, this, () => this.ModelCategoryErrorLabel.Text));
			KeepBindingInMemoryLocal(this.SetBinding(() => this.AreaViewModel.ModelWarnMessage).WhenSourceChanges(() =>
			{
				if (!String.IsNullOrWhiteSpace(this.AreaViewModel.ModelWarnMessage))
				{
					this.ModelCategoryErrorLabel.Text = this.AreaViewModel.ModelWarnMessage;
				}
			}));


			KeepBindingInMemoryLocal(new Binding<bool, bool>(this.AreaViewModel, () => this.AreaViewModel.CanEditBrandModel, this, () => this.BrandTextField.UserInteractionEnabled, BindingMode.TwoWay));

			KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.BrandName, this, () => this.BrandTextField.Text, BindingMode.TwoWay));
			KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.BrandErrorMessage, this, () => this.BrandErrorLabel.Text));

			KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.SelectedActivity.Text, this, () => this.ActivityTextField.Text, BindingMode.OneWay));
			KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.ActivityErrorMessage, this, () => this.ActivityErrorLabel.Text));

			KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.SelectedReason.Text, this, () => this.ReasonTextField.Text, BindingMode.OneWay));
			KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.ReasonErrorMessage, this, () => this.ReasonErrorLabel.Text));

			KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.SelectedSpentTime.Text, this, () => this.TimeSpentTextField.Text, BindingMode.OneWay));
			KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.SpentTimeErrorMessage, this, () => this.TimeSpentErrorLabel.Text));

			KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.Description, this, () => this.DescriptionTextView.Text, BindingMode.TwoWay));

		}

		public override void Translations()
		{
			this.ModelCategoryLabel.Text = TranslatorManager.GetInstance().GetString("Model/Category");
			this.BrandLabel.Text = TranslatorManager.GetInstance().GetString("Brand");
			this.ActivityLabel.Text = TranslatorManager.GetInstance().GetString("Shop Activities");
			this.ReasonLabel.Text = TranslatorManager.GetInstance().GetString("Store Reason");
			this.TimeSpentLabel.Text = TranslatorManager.GetInstance().GetString("Time Spent");
			this.DescriptionLabel.Text = TranslatorManager.GetInstance().GetString("Description");

			this.ModelCategoryTextField.Placeholder = TranslatorManager.GetInstance().GetString("Mandatory");
			this.BrandTextField.Placeholder = TranslatorManager.GetInstance().GetString("Mandatory");
			this.ActivityTextField.Placeholder = TranslatorManager.GetInstance().GetString("Mandatory");
			this.ReasonTextField.Placeholder = TranslatorManager.GetInstance().GetString("Mandatory");
			this.TimeSpentTextField.Placeholder = TranslatorManager.GetInstance().GetString("Mandatory");
		}

		public override void ConfigureArea()
		{
			base.ConfigureArea();

			#region Behaviors

			//this.DescriptionTextView.Delegate = this;

			#region Model/Category

			this.ModelCategoryTextField.ShouldChangeCharacters += (textField, range, replacementString) =>
			{
				var newContent = new NSString(textField.Text).Replace(range, new NSString(replacementString)).ToString();
				if (newContent.Length > this.AreaViewModel.ApplicationController.SearchThreshold)
				{
					modelCategoryPopoverController.ShowPopover(this.ModelCategoryTextField);
				}
				else
				{
					modelCategoryPopoverController.DismissPopover();
				}
				return true;
			};

			modelCategoryPopoverController = new PopoverViewController<ModelCategoryUnit>(
				this.AreaViewModel.Models,
				new CGSize(this.ModelCategoryTextField.Frame.Size.Width, 320f),
				"ModelCategoryTableViewCell",
				UIPopoverArrowDirection.Left,
				(UITableViewCell cell) =>
				{
					if (cell is ModelCategoryTableViewCell)
					{
						ModelCategoryUnit item = ((ModelCategoryTableViewCell)cell).Item;
						this.AreaViewModel.Model = item.Text;
						this.AreaViewModel.SelectedModel = item;
						this.ModelCategoryTextField.ResignFirstResponder();
					}
					modelCategoryPopoverController.DismissPopover();
				}
			);

			#endregion

			#region Brand 

			this.BrandTextField.ShouldChangeCharacters += (textField, range, replacementString) =>
			{
				var newContent = new NSString(textField.Text).Replace(range, new NSString(replacementString)).ToString();
				if (newContent.Length > this.AreaViewModel.ApplicationController.SearchThreshold)
				{
					brandPopoverController.ShowPopover(this.BrandTextField);
				}
				else
				{
					brandPopoverController.DismissPopover();
				}
				return true;
			};

			brandPopoverController = new PopoverViewController<BrandUnit>(
				this.AreaViewModel.Brands,
				new CGSize(this.BrandTextField.Frame.Size.Width, 320f),
				"BrandTableViewCell",
				UIPopoverArrowDirection.Right,
				(UITableViewCell cell) =>
				{
					if (cell is BrandTableViewCell)
					{
						BrandUnit item = ((BrandTableViewCell)cell).Item;
						this.AreaViewModel.BrandName = item.Text;
						this.AreaViewModel.SelectedBrandModel = item;
						this.BrandTextField.ResignFirstResponder();
					}
					brandPopoverController.DismissPopover();
				}
			);

			#endregion

			#region Activity


			this.ActivityTextField.ShouldBeginEditing += (UITextField textField) =>
			{
				activityPopoverController.ShowPopover(textField);
				return false;
			};

			activityPopoverController = new PopoverViewController<ActivityModelUnit>(
				this.AreaViewModel.Activities,
				new CGSize(this.ActivityTextField.Frame.Size.Width, 320f),
				"ActivityViewCell",
				UIPopoverArrowDirection.Any,
				(UITableViewCell cell) =>
				{
					if (cell is ActivityViewCell)
					{
						this.AreaViewModel.SelectedActivity = ((ActivityViewCell)cell).Item;

						activityPopoverController.DismissPopover();
					}
				}
			);

			#endregion

			#region Reason

			this.ReasonTextField.ShouldBeginEditing += (UITextField textField) =>
			{
				reasonPopoverController.ShowPopover(textField);
				return false;
			};

			reasonPopoverController = new PopoverViewController<ReasonModelUnit>(
				this.AreaViewModel.Reasons,
				new CGSize(this.ReasonTextField.Frame.Size.Width, 320f),
				"ReasonViewCell",
				UIPopoverArrowDirection.Any,
				(UITableViewCell cell) =>
				{
					if (cell is ReasonViewCell)
					{
						this.AreaViewModel.SelectedReason = ((ReasonViewCell)cell).Item;

						reasonPopoverController.DismissPopover();
					}
				}
			);

			#endregion

			#region TimeSpent

			this.TimeSpentTextField.ShouldBeginEditing += (UITextField textField) =>
			{
				timeSpentPopoverController.ShowPopover(textField);
				return false;
			};

			timeSpentPopoverController = new PopoverViewController<TimeSpanModelUnit>(
				this.AreaViewModel.SpentTimes,
				new CGSize(this.TimeSpentTextField.Frame.Size.Width, 320f),
				"TimeSpentViewCell",
				UIPopoverArrowDirection.Any,
				(UITableViewCell cell) =>
				{
					if (cell is TimeSpentViewCell)
					{
						this.AreaViewModel.SelectedSpentTime = ((TimeSpentViewCell)cell).Item;

						timeSpentPopoverController.DismissPopover();
					}
				}
			);

			#endregion

			#endregion
		}

		#region .ctor
		public ShopActivitiesDetailsViewController(IntPtr handle) : base(handle)
		{
		}
		#endregion
	}
}
