using System;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class QualityLevelViewCell : UITableViewCell, ICellBinding<QualityLevelUnit>
	{
		public static readonly NSString Key = new NSString("QualityLevelViewCell");
		public static readonly UINib Nib;

		static QualityLevelViewCell()
		{
			Nib = UINib.FromName("QualityLevelViewCell", NSBundle.MainBundle);
		}

		protected QualityLevelViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public QualityLevelUnit Item { get; set; }

		public void BindCell(QualityLevelUnit item)
		{
			this.Item = item;
			this.TextLabel.Text = item.Text;
		}
	}
}
