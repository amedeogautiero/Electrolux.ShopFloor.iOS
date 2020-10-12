using System;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class PhotosTableViewCell : UITableViewCell
	{
		public PhotoUnit Item { get; set; }

		public UILabel BrandLabel { get { return brandLabel; } }
		public UILabel SubjectLabel { get { return subjectLabel; } }
		public UILabel QualityLabel { get { return qualityLabel; } }
		public UILabel DescriptionLabel { get { return descriptionLabel; } }

		public PhotosTableViewCell (IntPtr handle) : base (handle)
		{
		}
	}
}
