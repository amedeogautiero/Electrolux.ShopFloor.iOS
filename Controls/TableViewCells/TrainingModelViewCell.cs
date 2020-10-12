using System;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class TrainingModelViewCell : UITableViewCell, ICellBinding<RegTrainingModelUnit>
	{
		public static readonly NSString Key = new NSString("TrainingModelViewCell");
		public static readonly UINib Nib;

		static TrainingModelViewCell()
		{
			Nib = UINib.FromName("TrainingModelViewCell", NSBundle.MainBundle);
		}

		protected TrainingModelViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}
		public RegTrainingModelUnit Item { get; set; }

		public void BindCell(RegTrainingModelUnit item)
		{
			this.Item = item;
			this.TextLabel.Text = item.Text;
		}
	}
}
