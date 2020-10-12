using System;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class TrainingAudienceViewCell : UITableViewCell, ICellBinding<RegTrainingAudienceModelUnit>
	{
		public static readonly NSString Key = new NSString("TrainingAudienceViewCell");
		public static readonly UINib Nib;

		static TrainingAudienceViewCell()
		{
			Nib = UINib.FromName("TrainingAudienceViewCell", NSBundle.MainBundle);
		}

		protected TrainingAudienceViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public RegTrainingAudienceModelUnit Item { get; set; }

		public void BindCell(RegTrainingAudienceModelUnit item)
		{
			this.Item = item;
			this.TextLabel.Text = item.Text;
		}
	}
}
