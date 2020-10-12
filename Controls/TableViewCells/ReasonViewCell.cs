using System;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class ReasonViewCell : UITableViewCell, ICellBinding<ReasonModelUnit>
	{
		public ReasonModelUnit Item
		{
			get;
			set;
		}


		public static readonly NSString Key = new NSString("ReasonViewCell");
		public static readonly UINib Nib;

		static ReasonViewCell()
		{
			Nib = UINib.FromName("ReasonViewCell", NSBundle.MainBundle);
		}

		protected ReasonViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public void BindCell(ReasonModelUnit item)
		{
			this.Item = item;
			this.TextLabel.Text = item.Text;
		}
	}
}
