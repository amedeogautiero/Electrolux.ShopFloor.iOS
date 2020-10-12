using System;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class ModelProductSetTableViewCell : UITableViewCell, ICellBinding<ModelProductSetUnit>
	{
		public static readonly NSString Key = new NSString("ModelProductSetTableViewCell");
		public static readonly UINib Nib;

		static ModelProductSetTableViewCell()
		{
			Nib = UINib.FromName("ModelProductSetTableViewCell", NSBundle.MainBundle);
		}

		protected ModelProductSetTableViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public ModelProductSetUnit Item { get; set; }

		public void BindCell(ModelProductSetUnit item)
		{
			this.Item = item;
			if (item == null)
				return;

			this.TextLabel.Text = item.Text;
			this.SubtitleLabel.Text = item.Text2;

		}


	}
}
