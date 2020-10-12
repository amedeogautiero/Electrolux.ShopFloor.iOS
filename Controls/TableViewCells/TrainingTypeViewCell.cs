using System;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class TrainingTypeViewCell : UITableViewCell, ICellBinding<RegTrainingTypeModelUnit>
	{
		public static readonly NSString Key = new NSString("TrainingTypeViewCell");
		public static readonly UINib Nib;

		static TrainingTypeViewCell()
		{
			Nib = UINib.FromName("TrainingTypeViewCell", NSBundle.MainBundle);
		}

		protected TrainingTypeViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public RegTrainingTypeModelUnit Item { get; set; }

		public void BindCell(RegTrainingTypeModelUnit item)
		{
			this.Item = item;
			this.TextLabel.Text = item.Text;
		}
	}
}
