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
	public partial class ElectroluxActivitiesViewController : ListBaseViewController<ElectroluxActivitiesViewModel, ElectroluxActivityUnit>
	{
		public ElectroluxActivitiesViewController (IntPtr handle) : base (handle)
		{
		}

		protected override string CellName
		{
			get
			{
				return "ElectroluxActivitiesTableViewCell";
			}	
		}

		public override void Translations()
		{
			this.activityLabel.Text = TranslatorManager.GetInstance().GetString("Electrolux Activities");
			this.activityReasonLabel.Text = TranslatorManager.GetInstance().GetString("Reason");
			this.descriptionLabel.Text = TranslatorManager.GetInstance().GetString("Description");
			this.modelCategoryLabel.Text = TranslatorManager.GetInstance().GetString("Model/Category");
			this.brandLabel.Text = TranslatorManager.GetInstance().GetString("Brand");
			this.timeSpentLabel.Text = TranslatorManager.GetInstance().GetString("Time Spent");
		}

		public override void BindTaskCell(UITableViewCell cell, ElectroluxActivityUnit item, NSIndexPath path)
		{
			base.BindTaskCell(cell, item, path);

			ElectroluxActivitiesTableViewCell listCell = cell as ElectroluxActivitiesTableViewCell;

			listCell.ActivityLabel.Text = item.Activity.Text;
			listCell.ActivityReasonLabel.Text = item.Reason.Text;
			listCell.DescriptionLabel.Text = item.Description;
			listCell.ModelCategoryLabel.Text = item.ModelText;
			listCell.BrandLabel.Text = item.Brand.Text;
			listCell.TimeSpentlabel.Text = item.TimeSpent.Text;
		}
	
	}
}
