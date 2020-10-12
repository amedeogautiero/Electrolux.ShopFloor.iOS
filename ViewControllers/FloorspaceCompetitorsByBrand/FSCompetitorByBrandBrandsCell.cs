// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using Electrolux.ShopFloor.Mvvm.ViewModels.EditingAreas;
using Foundation;
using GalaSoft.MvvmLight.Helpers;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class FSCompetitorByBrandBrandsCell : UITableViewCell
	{
		public List<Binding> Bindings { get; set; }

		public Action<UIView> checkBrandAction { get; set; }
		public UILabel BrandLabel { get { return brandLabel; } }
		public UIButton CheckBrandButton { get { return checkBrandButton; } }
		public UIImageView IsSelectedImageView { get { return isSelectedImageView; } }

		public FSCompetitorByBrandBrandsCell(IntPtr handle) : base(handle)
		{
		}

		partial void checkBrandButtonAction(NSObject sender)
		{
			this.checkBrandAction(sender as UIView);
		}

		public void BindCell(CompetitorsFloorSpace2020ViewModel viewModel, NSIndexPath indexPath)
		{
			if (this.Bindings != null)
			{
				foreach (Binding binding in this.Bindings)
				{
					binding.Detach();
				}
				this.Bindings.Clear();
			}
			else
			{
				this.Bindings = new List<Binding>();
			}

			this.BrandLabel.Text = viewModel.Brands[indexPath.Row].Text;
			this.checkBrandAction = (UIView sender) =>
			{
				viewModel.Brands[indexPath.Row].IsSelected = !viewModel.Brands[indexPath.Row].IsSelected;
				viewModel.SetFilterBrandLabel();
			};

			this.Bindings.Add(new Binding<bool, UIImage>(viewModel.Brands[indexPath.Row], () => viewModel.Brands[indexPath.Row].IsSelected, this, () => this.IsSelectedImageView.Image, BindingMode.OneWay)
								  .ConvertSourceToTarget((bool arg) =>
								  {
									  return arg ? UIImage.FromBundle("select-icon") : new UIImage();
								  })
								 );

		}
	}
}