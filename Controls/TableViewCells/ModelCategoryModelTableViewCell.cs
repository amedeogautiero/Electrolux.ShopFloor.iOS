using System;
using Electrolux.ShopFloor.Middleware.Manager;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class ModelCategoryModelTableViewCell : UITableViewCell, ICellBinding<ModelCategoryModelUnit>
	{
		public ModelCategoryModelUnit Item { get; set; }

		public static readonly NSString Key = new NSString("ModelCategoryModelTableViewCell");
		public static readonly UINib Nib;

		static ModelCategoryModelTableViewCell()
		{
			Nib = UINib.FromName("ModelCategoryModelTableViewCell", NSBundle.MainBundle);
		}

		protected ModelCategoryModelTableViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public void BindCell(ModelCategoryModelUnit item)
		{
			this.Item = item;
			this.TextLabel.Text = item.Text;
		}

	}
}
