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
	public partial class ProductFeedbackViewController : ListBaseViewController<FeedbackViewModel, ProductFeedbackUnit>
	{
		public ProductFeedbackViewController (IntPtr handle) : base (handle)
		{
		}

		protected override string CellName
		{
			get
			{
				return "ProductFeedbackTableViewCell";
			}
		}

		public override void Translations()
		{
			this.modelCategoryLabel.Text = TranslatorManager.GetInstance().GetString("Model/Category");
			this.brandLabel.Text = TranslatorManager.GetInstance().GetString("Brand");
			this.descriptionLabel.Text = TranslatorManager.GetInstance().GetString("Description");
		}

		public override void BindTaskCell(UITableViewCell cell, ProductFeedbackUnit item, NSIndexPath path)
		{
			base.BindTaskCell(cell, item, path);

			ProductFeedbackTableViewCell listCell = cell as ProductFeedbackTableViewCell;

			listCell.ModelCategoryLabel.Text = item.ModelText;
			listCell.BrandLabel.Text = item.Brand.Text;
			listCell.DescriptionLabel.Text = item.Description;
		}
	
	}
}
