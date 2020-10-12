using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Electrolux.ShopFloor.iOS.ViewControllers;
using Electrolux.ShopFloor.Middleware.Manager;
using Electrolux.ShopFloor.Mvvm.ViewModels;
using Electrolux.ShopFloor.Mvvm.ViewModels.EditingAreas;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class PosmActivitiesViewController : ListBaseViewController<PosmActivitiesViewModel, PosmActivityUnit>
	{
		public PosmActivitiesViewController (IntPtr handle) : base (handle)
		{
		}

		protected override string CellName
		{
			get
			{
				return "PosmActivitiesTableViewCell";
			}
		}

		public override void Translations()
		{
			this.modelCategoryLabel.Text = TranslatorManager.GetInstance().GetString("Model/Category");
			this.brandLabel.Text = TranslatorManager.GetInstance().GetString("Brand");
			this.posmMaterialLabel.Text = TranslatorManager.GetInstance().GetString("POSM Material");
			this.posmCampaignLabel.Text = TranslatorManager.GetInstance().GetString("POSM Campaign");
			this.posmActivityLabel.Text = TranslatorManager.GetInstance().GetString("POSM Activity");
		}

		public override void BindTaskCell(UITableViewCell cell, PosmActivityUnit item, NSIndexPath path)
		{
			base.BindTaskCell(cell, item, path);

			PosmActivitiesTableViewCell listCell = cell as PosmActivitiesTableViewCell;

			listCell.ModelCategoryLabel.Text = item.ModelText;
			listCell.BrandLabel.Text = item.Brand.Text;
			listCell.PosmActivityLabel.Text = item.Activity.Text;
			listCell.PosmMaterialLabel.Text = item.Material.Text;
			listCell.PosmCampaignLabel.Text = item.Campaign.Text;
		}
	}
}
