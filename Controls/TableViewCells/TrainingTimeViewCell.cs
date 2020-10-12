using System;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class TrainingTimeViewCell : UITableViewCell, ICellBinding<RegTrainingTimeModelUnit>
	{
		public static readonly NSString Key = new NSString("TrainingTimeViewCell");
		public static readonly UINib Nib;

		static TrainingTimeViewCell()
		{
			Nib = UINib.FromName("TrainingTimeViewCell", NSBundle.MainBundle);
		}

		protected TrainingTimeViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public RegTrainingTimeModelUnit Item { get; set; }

		public void BindCell(RegTrainingTimeModelUnit item)
		{
			this.Item = item;
			this.TextLabel.Text = item.Text;
		}
	}
}
