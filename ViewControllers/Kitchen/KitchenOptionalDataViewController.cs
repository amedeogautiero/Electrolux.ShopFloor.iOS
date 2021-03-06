using System;
using System.Diagnostics;
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
	public partial class KitchenOptionalDataViewController : OptionalDataBaseViewController<KitchenCompleteUnit, KitchenViewModel>
	{
		public KitchenOptionalDataViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.modelLabel.Text = TranslatorManager.GetInstance().GetString("Model/Category");
			this.qtyLabel.Text = TranslatorManager.GetInstance().GetString("Quantity On Display");
			this.qtyPOSLabel.Text = TranslatorManager.GetInstance().GetString("Quantity With POS Material");
			this.qtySpecialLabel.Text = TranslatorManager.GetInstance().GetString("Quantity Functional Kitchen");
			this.inPromoLabel.Text = TranslatorManager.GetInstance().GetString("Is In Promotion");
			this.priceLabel.Text = TranslatorManager.GetInstance().GetString("Price");
			this.qtyStockLabel.Text = TranslatorManager.GetInstance().GetString("Quantity On Stock");

			this.saveButton.SetTitle(TranslatorManager.GetInstance().GetString("OK"), UIControlState.Normal);
			this.cancelButton.SetTitle(TranslatorManager.GetInstance().GetString("Cancel"), UIControlState.Normal);

			if (AreaViewModel != null)
			{
				KeepBindingInMemory(this.SetBinding(() => AreaViewModel.IsElectroluxProduct, () => this.headerLabel.Text)
					.ConvertSourceToTarget((bool isElectroluxProduct) =>
					{
						if (isElectroluxProduct)
						{
							return TranslatorManager.GetInstance().GetString("You are inserting an Electrolux brand");
						}
						return TranslatorManager.GetInstance().GetString("You are inserting a Competitors brand");
					}));

				KeepBindingInMemory(this.SetBinding(() => AreaViewModel.QuantityStock, () => this.qtyStockTextField.Text, BindingMode.TwoWay));
				KeepBindingInMemory(this.SetBinding(() => AreaViewModel.QuantityPos, () => this.qtyPOSTextField.Text, BindingMode.TwoWay));
				KeepBindingInMemory(this.SetBinding(() => AreaViewModel.Price, () => this.priceTextField.Text, BindingMode.TwoWay));

				KeepBindingInMemory(this.SetBinding(() => AreaViewModel.Model, () => this.modelContentLabel.Text));
				KeepBindingInMemory(this.SetBinding(() => AreaViewModel.Quantity, () => this.qtyContentLabel.Text));
				KeepBindingInMemory(this.SetBinding(() => AreaViewModel.QuantitySpecial, () => this.qtySpecialContentLabel.Text));
				KeepBindingInMemory(this.SetBinding(() => AreaViewModel.InPromo, () => this.qtyPromoContentLabel.Text));

				KeepBindingInMemory(this.SetBinding(() => AreaViewModel.QuantityPosErrorMessage, () => this.qtyPOSMessageLabelLabel.Text));
				KeepBindingInMemory(this.SetBinding(() => AreaViewModel.PriceErrorMessage, () => this.priceMessageLabel.Text));

				KeepBindingInMemory(this.SetBinding(() => AreaViewModel.IsOverlayVisible).WhenSourceChanges(() =>
				{
					if (!AreaViewModel.IsOverlayVisible)
					{
						Owner.DismissPopover();
					}
				}));

				this.saveButton.SetCommand(AreaViewModel.CommitOverlayCommand);
				this.cancelButton.SetCommand(AreaViewModel.CancelOverlayCommand);
			}
		}
	}
}
