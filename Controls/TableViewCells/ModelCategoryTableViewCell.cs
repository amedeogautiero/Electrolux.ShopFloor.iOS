using System;
using Electrolux.ShopFloor.Middleware.Manager;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class ModelCategoryTableViewCell : UITableViewCell, ICellBinding<ModelCategoryUnit>
	{
		public ModelCategoryUnit Item { get; set; }

		public static readonly NSString Key = new NSString("ModelCategoryTableViewCell");
		public static readonly UINib Nib;

		static ModelCategoryTableViewCell()
		{
			Nib = UINib.FromName("ModelCategoryTableViewCell", NSBundle.MainBundle);
		}

		protected ModelCategoryTableViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public void BindCell(ModelCategoryUnit item)
		{
			this.Item = item;
			this.TextLabel.Text = item.Text;
			this.DetailTextLabel.Text = TranslatorManager.GetInstance().GetString(item.Text2);
		}
	}
}
