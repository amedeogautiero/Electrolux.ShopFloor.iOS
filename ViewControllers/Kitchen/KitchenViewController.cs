using System;
using CoreGraphics;
using Electrolux.ShopFloor.iOS.ViewControllers;
using Electrolux.ShopFloor.Middleware.Manager;
using Electrolux.ShopFloor.Mvvm.ViewModels.EditingAreas;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using GalaSoft.MvvmLight.Helpers;
using UIKit;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Electrolux.ShopFloor.iOS
{
	public partial class KitchenViewController : ListBaseViewController<KitchenViewModel, KitchenCompleteUnit>
	{
		public KitchenViewController (IntPtr handle) : base (handle)
		{
		}

		protected override string CellName
		{
			get
			{
				return "KitchenListTableViewCell";
			}
		}

		public override void Translations()
		{
			this.kitchenNameHeaderLabel.Text = TranslatorManager.GetInstance().GetString("Kitchen name");
			this.totalAppliancesHeaderLabel.Text = TranslatorManager.GetInstance().GetString("Total appliances");
			this.electroluxAppliancesHeaderLabel.Text = TranslatorManager.GetInstance().GetString("Total Electrolux appliances");
			this.kitchenDescriptionHeaderLabel.Text = TranslatorManager.GetInstance().GetString("Kitchen description");
		}

		public override void BindTaskCell(UITableViewCell cell, KitchenCompleteUnit item, NSIndexPath path)
		{
			base.BindTaskCell(cell, item, path);

			KitchenListTableViewCell listCell = cell as KitchenListTableViewCell;

			listCell.KitchenNameLabel.Text = item.Unit.KitchenName;
			listCell.TotalAppliancesLabel.Text = item.Unit.AppliancesTotal.ToString();
			listCell.ElectroluxAppliancesLabel.Text = item.Unit.AppliancesElectrolux.ToString();
			listCell.DescriptionLabel.Text = item.Unit.KitchenDescription.ToString();
		}
	}
}
