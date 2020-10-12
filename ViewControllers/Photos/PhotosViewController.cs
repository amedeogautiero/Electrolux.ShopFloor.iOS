using System;
using Electrolux.ShopFloor.iOS.ViewControllers;
using Electrolux.ShopFloor.Middleware.Manager;
using Electrolux.ShopFloor.Mvvm.ViewModels.EditingAreas;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class PhotosViewController : ListBaseViewController<PhotosViewModel, PhotoUnit>
	{
		public PhotosViewController(IntPtr handle) : base(handle)
		{
		}

		protected override string CellName
		{
			get
			{
				return "PhotosTableViewCell";
			}
		}

		public override void Translations()
		{
			this.brandLabel.Text = TranslatorManager.GetInstance().GetString("Competitor/Electrolux Brand");
			this.qualityLabel.Text = TranslatorManager.GetInstance().GetString("Product Performance");
			this.subjectLabel.Text = TranslatorManager.GetInstance().GetString("Photo Refer To");
			this.descriptionLabel.Text = TranslatorManager.GetInstance().GetString("Description");
		}

		public override void BindTaskCell(UITableViewCell cell, PhotoUnit item, NSIndexPath path)
		{
			base.BindTaskCell(cell, item, path);

			var listCell = cell as PhotosTableViewCell;

			listCell.BrandLabel.Text = item.Brand.Text;
			listCell.SubjectLabel.Text = item.ReferTo.Text;
			listCell.QualityLabel.Text = item.QualityLevel.Text;
			listCell.DescriptionLabel.Text = item.Description;
		}
	}
}
