// This file has been autogenerated from a class added in the UI designer.

using System;
using Electrolux.ShopFloor.iOS.ViewControllers;
using Electrolux.ShopFloor.Middleware.Manager;
using Electrolux.ShopFloor.Mvvm.ViewModels.EditingAreas;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class ShopActivitiesViewController : ListBaseViewController<ShopActivitiesViewModel, ShopActivityUnit>
	{
		protected override string CellName
		{
			get { return "ShopActivitiesTableViewCell"; }
		}

		public override void Translations()
		{
			this.ModelHeaderLabel.Text = TranslatorManager.GetInstance().GetString("Model/Category");
			this.BrandHeaderLabel.Text = TranslatorManager.GetInstance().GetString("Brand");
			this.TimeSpentHeaderLabel.Text = TranslatorManager.GetInstance().GetString("Time Spent");
			this.ShopActivityHeaderLabel.Text = TranslatorManager.GetInstance().GetString("Shop Activities");
			this.ReasonHeaderLabel.Text = TranslatorManager.GetInstance().GetString("Store Reason");
			this.DescriptionHeaderLabel.Text = TranslatorManager.GetInstance().GetString("Description");
		}

		public override void BindTaskCell(UITableViewCell cell, ShopActivityUnit item, NSIndexPath path)
		{
			base.BindTaskCell(cell, item, path);
			                  
			ShopActivitiesTableViewCell listCell = cell as ShopActivitiesTableViewCell;

			listCell.ModelLabel.Text = item.ModelText;
			listCell.BrandLabel.Text = item.Brand.Text;
			listCell.ShopActivityLabel.Text = item.Activity.Text;
			listCell.ReasonLabel.Text = item.Reason.Text;
			listCell.TimeSpentLabel.Text = item.TimeSpent.Text;
			listCell.DescriptionLabel.Text = item.Description;
		}

		#region .ctor
		public ShopActivitiesViewController(IntPtr handle) : base(handle)
		{
		}
		#endregion

	}
}
