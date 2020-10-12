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

namespace Electrolux.ShopFloor.iOS
{
	public partial class ProductFeedbackDetailsViewController : ListDetailBaseViewController<FeedbackViewModel>
	{
		private PopoverViewController<ModelProductSetUnit> modelCategoryPopoverController;
		private PopoverViewController<BrandUnit> brandPopoverController;

		public ProductFeedbackDetailsViewController (IntPtr handle) : base (handle)
		{
		}

		public override void Translations()
		{
			this.modelCategoryLabel.Text = TranslatorManager.GetInstance().GetString("Model/Category");
			this.brandLabel.Text = TranslatorManager.GetInstance().GetString("Brand");
			this.descriptionLabel.Text = TranslatorManager.GetInstance().GetString("Description");

			this.modelCategoryTextField.Placeholder = TranslatorManager.GetInstance().GetString("Mandatory");
			this.brandTextField.Placeholder = TranslatorManager.GetInstance().GetString("Mandatory");
		}

		public override void ConfigureArea()
		{
			base.ConfigureArea();

			#region Behaviors

			//this.descriptionTextView.Delegate = this;

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


			this.modelCategoryPopoverController = new PopoverViewController<ModelProductSetUnit>(
				this.AreaViewModel.Models,
				new CGSize(this.modelCategoryTextField.Frame.Size.Width, 320f),
				"ModelProductSetTableViewCell",
				UIPopoverArrowDirection.Left,
				(UITableViewCell cell) =>
				{
					if (cell is ModelProductSetTableViewCell)
					{
						this.AreaViewModel.Model = ((ModelProductSetTableViewCell)cell).Item.Text;
						this.AreaViewModel.SelectedModel = ((ModelProductSetTableViewCell)cell).Item;
						this.modelCategoryTextField.ResignFirstResponder();
					}
					this.modelCategoryPopoverController.DismissPopover();
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

			this.brandPopoverController = new PopoverViewController<BrandUnit>(
				this.AreaViewModel.Brands,
				new CGSize(this.brandTextField.Frame.Size.Width, 320f),
				"BrandTableViewCell",
				UIPopoverArrowDirection.Right,
				(UITableViewCell cell) =>
				{
					if (cell is BrandTableViewCell)
					{
						this.AreaViewModel.SelectedBrand = ((BrandTableViewCell)cell).Item;
						this.modelCategoryTextField.ResignFirstResponder();

						this.brandPopoverController.DismissPopover();
					}
				}
			);

			#endregion

			#endregion
		}

		public override void RegisterBindingsLocal()
		{
			base.RegisterBindingsLocal();

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

			KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.BrandName, this, () => this.brandTextField.Text, BindingMode.TwoWay));
			KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.BrandErrorMessage, this, () => this.brandMessageLabel.Text));

			KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.Description, this, () => this.descriptionTextView.Text, BindingMode.TwoWay));
			KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.DescriptionErrorMessage, this, () => this.descriptionMessageLabel.Text));

		}
	}
}
