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
using System.Diagnostics;

namespace Electrolux.ShopFloor.iOS
{
	public partial class CompetitorsActivitiesDetailsViewController : ListDetailBaseViewController<CompetitorsActivitiesViewModel>
	{
		private PopoverViewController<ModelCategoryUnit> modelCategoryPopoverController;
		private PopoverViewController<BrandUnit> brandPopoverController;
		private PopoverViewController<ActivityModelUnit> activityPopoverController;
		private PopoverViewController<ReasonModelUnit> reasonPopoverController;
		private PopoverViewController<TimeSpanModelUnit> timeSpentPopoverController;
		private PopoverViewController<ModelCategoryModelUnit> categoryPopoverController;

		protected override UIKit.UITextView[] TextViewArray()
		{
			return new UITextView[] { this.descriptionTextView };
		}

		public override void RegisterBindingsLocal()
		{
			if (this.AreaViewModel != null)
			{
				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.Model, this, () => this.modelCategoryTextField.Text, BindingMode.TwoWay));
				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.ModelErrorMessage, this, () => this.modelCategoryMessageLabel.Text));
				KeepBindingInMemoryLocal(this.SetBinding(() => this.AreaViewModel.ModelWarnMessage).WhenSourceChanges(() =>
				{
					if (!String.IsNullOrWhiteSpace(this.AreaViewModel.ModelWarnMessage))
					{
						this.modelCategoryMessageLabel.Text = this.AreaViewModel.ModelWarnMessage;
					}
				}));

				KeepBindingInMemoryLocal(new Binding<bool, bool>(this.AreaViewModel, () => this.AreaViewModel.CanEditBrandModel, this, () => this.brandTextField.UserInteractionEnabled, BindingMode.TwoWay));
				KeepBindingInMemoryLocal(new Binding<bool, bool>(this.AreaViewModel, () => this.AreaViewModel.CanEditCategory, this, () => this.categoryTextField.UserInteractionEnabled, BindingMode.TwoWay));
				KeepBindingInMemoryLocal(new Binding<bool, bool>(this.AreaViewModel, () => this.AreaViewModel.CanEditModel, this, () => this.modelCategoryTextField.UserInteractionEnabled, BindingMode.TwoWay));

				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.BrandName, this, () => this.brandTextField.Text, BindingMode.TwoWay));
				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.BrandErrorMessage, this, () => this.brandMessageLabel.Text));

				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.SelectedActivity.Text, this, () => this.activityTextField.Text, BindingMode.OneWay));
				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.ActivityErrorMessage, this, () => this.activityMessageLabel.Text));

				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.SelectedReason.Text, this, () => this.reasonTextField.Text, BindingMode.OneWay));
				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.ReasonErrorMessage, this, () => this.reasonMessageLabel.Text));

				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.SelectedSpentTime.Text, this, () => this.timeSpentTextField.Text, BindingMode.OneWay));
				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.SpentTimeErrorMessage, this, () => this.timeSpentMessageLabel.Text));

				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.SelectedCategory.Text, this, () => this.categoryTextField.Text, BindingMode.OneWay));
				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.CategoryErrorMessage, this, () => this.categoryMessageLabel.Text));

				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.Description, this, () => this.descriptionTextView.Text, BindingMode.TwoWay));
			}
		}

		public override void DetachBindingsLocal()
		{
			ClearAndDetachBindingsLocal();
		}

		public override void Translations()
		{
			this.modelCategoryLabel.Text = TranslatorManager.GetInstance().GetString("Model/Category");
			this.brandLabel.Text = TranslatorManager.GetInstance().GetString("Brand");
			this.activityLabel.Text = TranslatorManager.GetInstance().GetString("Competitors Activities");
			this.reasonLabel.Text = TranslatorManager.GetInstance().GetString("Reason");
			this.timeSpentLabel.Text = TranslatorManager.GetInstance().GetString("Time Spent");
			this.categoryLabel.Text = TranslatorManager.GetInstance().GetString("Category");
			this.descriptionLabel.Text = TranslatorManager.GetInstance().GetString("Description");

			this.modelCategoryTextField.Placeholder = TranslatorManager.GetInstance().GetString("Mandatory");
			this.brandTextField.Placeholder = TranslatorManager.GetInstance().GetString("Mandatory");
			this.activityTextField.Placeholder = TranslatorManager.GetInstance().GetString("Mandatory");
			this.reasonTextField.Placeholder = TranslatorManager.GetInstance().GetString("Mandatory");
			this.timeSpentTextField.Placeholder = TranslatorManager.GetInstance().GetString("Mandatory");
			this.categoryTextField.Placeholder = TranslatorManager.GetInstance().GetString("Mandatory");
		}

		public override void ConfigureArea()
		{
			base.ConfigureArea();

			#region Behaviors

			#region Model/Category

			this.modelCategoryTextField.ShouldChangeCharacters += (textField, range, replacementString) =>
			{
				var newContent = new NSString(textField.Text).Replace(range, new NSString(replacementString)).ToString();
				if (newContent.Length > this.AreaViewModel.ApplicationController.SearchThreshold)
				{
					modelCategoryPopoverController.ShowPopover(this.modelCategoryTextField);
				}
				else
				{
					modelCategoryPopoverController.DismissPopover();
				}
				return true;
			};

			modelCategoryPopoverController = new PopoverViewController<ModelCategoryUnit>(
				this.AreaViewModel.Models, 
				new CGSize(this.modelCategoryTextField.Frame.Size.Width, 320f), 
				"ModelCategoryTableViewCell", 
				UIPopoverArrowDirection.Left, 
				(UITableViewCell cell) =>
				{
					if (cell is ModelCategoryTableViewCell)
					{
						ModelCategoryUnit item = ((ModelCategoryTableViewCell)cell).Item;
						this.AreaViewModel.Model = item.Text;
						this.AreaViewModel.SelectedModel = item;
						this.modelCategoryTextField.ResignFirstResponder();
					}
					modelCategoryPopoverController.DismissPopover();
				}
			);

			#endregion

			#region Brand 

			this.brandTextField.ShouldChangeCharacters += (textField, range, replacementString) =>
			{
				var newContent = new NSString(textField.Text).Replace(range, new NSString(replacementString)).ToString();
				if (newContent.Length > this.AreaViewModel.ApplicationController.SearchThreshold)
				{
					brandPopoverController.ShowPopover(this.brandTextField);
				}
				else
				{
					brandPopoverController.DismissPopover();
				}
				return true;
			};

			brandPopoverController = new PopoverViewController<BrandUnit>(this.AreaViewModel.Brands, new CGSize(this.brandTextField.Frame.Size.Width, 320f), "BrandTableViewCell", UIPopoverArrowDirection.Right, (UITableViewCell cell) =>
			{
				if (cell is BrandTableViewCell)
				{
					this.AreaViewModel.SelectedBrandModel = ((BrandTableViewCell)cell).Item;
					this.brandTextField.ResignFirstResponder();
				}
				brandPopoverController.DismissPopover();
			});

			#endregion

			#region Activity


			this.activityTextField.ShouldBeginEditing += (UITextField textField) =>
			{
				activityPopoverController.ShowPopover(textField);
				return false;
			};

			activityPopoverController = new PopoverViewController<ActivityModelUnit>(
				this.AreaViewModel.Activities,
				new CGSize(this.activityTextField.Frame.Size.Width, 320f),
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

			this.reasonTextField.ShouldBeginEditing += (UITextField textField) =>
			{
				reasonPopoverController.ShowPopover(textField);
				return false;
			};

			reasonPopoverController = new PopoverViewController<ReasonModelUnit>(
				this.AreaViewModel.Reasons,
				new CGSize(this.reasonTextField.Frame.Size.Width, 320f),
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

			this.timeSpentTextField.ShouldBeginEditing += (UITextField textField) =>
			{
				timeSpentPopoverController.ShowPopover(textField);
				return false;
			};

			timeSpentPopoverController = new PopoverViewController<TimeSpanModelUnit>(
				this.AreaViewModel.SpentTimes,
				new CGSize(this.timeSpentTextField.Frame.Size.Width, 320f),
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

			#region Category

			this.categoryTextField.ShouldBeginEditing += (UITextField textField) =>
			{
				categoryPopoverController.ShowPopover(textField);
				return false;
			};

			categoryPopoverController = new PopoverViewController<ModelCategoryModelUnit>(
				this.AreaViewModel.Categories,
				new CGSize(this.categoryTextField.Frame.Size.Width, 320f),
				"CategoryViewCell",
				UIPopoverArrowDirection.Any,
				(UITableViewCell cell) =>
				{
					if (cell is CategoryViewCell)
					{
						this.AreaViewModel.SelectedCategory = ((CategoryViewCell)cell).Item;
						categoryPopoverController.DismissPopover();
					}
				}
			);

			#endregion

			#endregion
		}

		#region .ctor
		public CompetitorsActivitiesDetailsViewController(IntPtr handle) : base(handle)
		{
		}
		#endregion

	}
}
