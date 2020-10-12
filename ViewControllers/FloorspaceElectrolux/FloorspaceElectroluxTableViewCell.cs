using Foundation;
using System;
using UIKit;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using GalaSoft.MvvmLight.Helpers;
using Electrolux.ShopFloor.Mvvm.ViewModels.EditingAreas;
using Electrolux.ShopFloor.Mvvm.ViewModels;
using System.Diagnostics;
using System.Collections.Generic;
using Electrolux.ShopFloor.Middleware.Manager;

namespace Electrolux.ShopFloor.iOS
{
	public partial class FloorspaceElectroluxTableViewCell : UITableViewCell
	{
		public Action<UIView> optionalDataAction { get; set; }
		public Action<UIView> checkboxAction { get; set; }
		public List<Binding> Bindings { get; set; }

		public UILabel BrandLabel { get { return brandLabel; } }
		public UILabel CategoryLabel { get { return categoryLabel; } }
		public UILabel IsInPromoLabel { get { return isInPromoLabel; } }
		public UISwitch IsInPromoSwitch { get { return isInPromoSwitch; } }
		public UILabel ModelLabel { get { return modelLabel; } }
		public UIButton OptionalDataButton { get { return optionalDataButton; } }
		public UILabel PriceLabel { get { return priceLabel; } }
		public UITextField PriceTextField { get { return priceTextField; } }
		public UILabel QuantityOnDisplayLabel { get { return quantityOnDisplayLabel; } }
		public UITextField QuantityOnDisplayTextField { get { return quantityOnDisplayTextField; } }
		public UILabel QuantitySpecialPlacementLabel { get { return quantitySpecialPlacementLabel; } }
		public UILabel QuantityWithPOSMLabel { get { return quantityWithPOSMLabel; } }
		public UISwitch SpecialPlacementSwitch { get { return specialPlacementSwitch; } }

		public UISwitch WithPOSMSwitch { get { return withPOSMSwitch; } }
		public UIButton CheckboxButton { get { return checkboxButton; } }

		public UILabel IsPreferredLabel { get { return isPreferredLabel; } }
		public UILabel ErrorMessageLabel { get { return errorMessageLabel; } }

		public FloorspaceElectroluxTableViewCell(IntPtr handle) : base(handle)
		{
		}

		partial void optionalDataButtonAction(NSObject sender)
		{
			this.optionalDataAction(sender as UIView);
		}

		partial void checkboxButtonAction(Foundation.NSObject sender)
		{
			this.checkboxAction(sender as UIView);
		}

		public void Translations()
		{
			this.QuantityOnDisplayLabel.Text = TranslatorManager.GetInstance().GetString("# on display");
			this.PriceLabel.Text = TranslatorManager.GetInstance().GetString("Price");
			this.QuantitySpecialPlacementLabel.Text = TranslatorManager.GetInstance().GetString("Sp. Placem");
			this.QuantityWithPOSMLabel.Text = TranslatorManager.GetInstance().GetString("With POSM");
			this.IsInPromoLabel.Text = TranslatorManager.GetInstance().GetString("Is in Promo");
		}

	}
}