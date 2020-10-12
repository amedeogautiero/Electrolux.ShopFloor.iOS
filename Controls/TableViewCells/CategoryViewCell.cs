using System;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class CategoryViewCell : UITableViewCell, ICellBinding<ModelCategoryModelUnit>
	{
		public ModelCategoryModelUnit Item
		{
			get;
			set;
		}

		public static readonly NSString Key = new NSString("CategoryViewCell");
		public static readonly UINib Nib;

		static CategoryViewCell()
		{
			Nib = UINib.FromName("CategoryViewCell", NSBundle.MainBundle);
		}

		protected CategoryViewCell(IntPtr handle) : base(handle)
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
