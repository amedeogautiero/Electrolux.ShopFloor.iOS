using System;

using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class BaseTableViewCell : UITableViewCell, ICellBinding<string>
	{
		public string Item { get; set; }

		public static readonly NSString Key = new NSString("BaseTableViewCell");
		public static readonly UINib Nib;

		static BaseTableViewCell()
		{
			Nib = UINib.FromName("BaseTableViewCell", NSBundle.MainBundle);
		}

		protected BaseTableViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public void BindCell(string item)
		{
			this.Item = item;
			this.TextLabel.Text = item;
		}
	}
}
