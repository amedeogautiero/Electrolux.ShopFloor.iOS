using System;
using CoreGraphics;
using Electrolux.ShopFloor.iOS.ViewControllers;
using Electrolux.ShopFloor.Middleware.Manager;
using Electrolux.ShopFloor.Mvvm.ViewModels.EditingAreas;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using GalaSoft.MvvmLight.Helpers;
using UIKit;
using System.Linq;
using System.Collections.Generic;
using System.Timers;
using System.Collections.ObjectModel;

namespace Electrolux.ShopFloor.iOS
{
	public partial class FloorspaceProductGroupDetailsViewController : ListDetailBaseViewController<ProductGroupFloorSpaceViewModel>
	{
		private PopoverViewController<BrandProductGroupUnit> brandPopoverController;
		private OptionalDataPopoverController<FSProductGroupOptionalDataViewController, FloorSpaceProductGroupUnit, ProductGroupFloorSpaceViewModel> optionalDataPopoverController;

		public FloorspaceProductGroupDetailsViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);
            BrandTimer.Stop();
		}

		public override void RegisterBindingsLocal()
		{
			if (this.AreaViewModel != null)
			{
				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.SelectedProductGroup.Text, this, () => this.productGroupTextField.Text));
				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.SelectedBrandModel.Text, this, () => this.brandTextField.Text));
				this.KeepBindingInMemoryLocal(this.SetBinding(() => this.brandTextField.Text).WhenSourceChanges(() =>
				{
                    BrandTimer.Start();
				}));

				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.Quantity, this, () => this.qtyTextField.Text, BindingMode.TwoWay));
				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.QuantityPos, this, () => this.qtyPOSTextField.Text, BindingMode.TwoWay));
				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.QuantitySpecial, this, () => this.qtySpecialTextField.Text, BindingMode.TwoWay));
				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.InPromo, this, () => this.qtyPromoTextField.Text, BindingMode.TwoWay));

				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.ErrorMessage, this, () => this.brandMessageLabel.Text));
			}
		}

		partial void addBrandAction(NSObject sender)
		{
			if (AreaViewModel.UpdateCommand.CanExecute(null))
			{
				AreaViewModel.UpdateCommand.Execute(null);
                //this.brandTextField.Text = "";
            }
		}

		partial void optionalDataAction(NSObject sender)
		{
			if (AreaViewModel.OpenOverlayCommand.CanExecute(null))
			{
				AreaViewModel.OpenOverlayCommand.Execute(null);
			}
			optionalDataPopoverController.ShowPopover(sender as UIView, new CGSize(540f, 372f), null);
		}

		public override void Translations()
		{
			this.productGroupLabel.Text = TranslatorManager.GetInstance().GetString("Product Group");
			this.brandLabel.Text = TranslatorManager.GetInstance().GetString("Brand");
			this.qtyLabel.Text = TranslatorManager.GetInstance().GetString("Qty");
			this.qtyPOSLabel.Text = TranslatorManager.GetInstance().GetString("Qty PoS");
			this.qtySpecialLabel.Text = TranslatorManager.GetInstance().GetString("Qty. Sp. Pl.");
			this.qtyPromoLabel.Text = TranslatorManager.GetInstance().GetString("Is in Promo");

			this.brandHeaderLabel.Text = TranslatorManager.GetInstance().GetString("Brand");
			this.qtyHeaderLabel.Text = TranslatorManager.GetInstance().GetString("Qty");
			this.qtyPOSHeaderLabel.Text = TranslatorManager.GetInstance().GetString("Qty PoS");
			this.qtySpecialHeaderLabel.Text = TranslatorManager.GetInstance().GetString("Qty. Sp. Pl.");
			this.qtyPromoHeaderLabel.Text = TranslatorManager.GetInstance().GetString("Is in Promo");
		}

		public override void ConfigureArea()
		{
			base.ConfigureArea();

			#region Brand

			this.brandTextField.Ended += (sender, e) =>
			{
				this.EditingEnded(this.brandTextField);
			};

			//this.brandTextField.ShouldChangeCharacters += (textField, range, replacementString) =>
			//{
			//	var newContent = new NSString(textField.Text).Replace(range, new NSString(replacementString)).ToString();
			//	if (newContent.Length > this.AreaViewModel.ApplicationController.SearchThreshold)
			//	{
   //                 AreaViewModel.BrandName = newContent;
			//		brandPopoverController.ShowPopover(this.brandTextField);
			//	}
			//	else
			//	{
			//		brandPopoverController.DismissPopover();
			//	}
			//	return true;
			//};

			brandPopoverController = new PopoverViewController<BrandProductGroupUnit>(this.AreaViewModel.Brands, new CGSize(this.brandTextField.Frame.Size.Width, 320f), "ProductGroupBrandTableViewCell", UIPopoverArrowDirection.Up, (UITableViewCell cell) =>
			{
				if (cell is ProductGroupBrandTableViewCell)
				{
					var item = ((ProductGroupBrandTableViewCell)cell).Item;
                    AreaViewModel.BrandName = this.brandTextField.Text = item.Text;
					this.AreaViewModel.SelectedBrandModel = item;
                    this.brandTextField.ResignFirstResponder();
                }
                brandPopoverController.DismissPopover();
			});

			#endregion

			#region Brand list tableview

			//if (this.tableView.Source == null)
			{
				this.tableView.Source = new ProductGroupBrandsTableSource(AreaViewModel, AreaViewModel.CategoryBrands, this.tableView, "FloorspaceProductGroupBrandsTableViewCell");
				this.tableView.ReloadData();
			}

			#endregion

			#region Optional Data

			this.optionalDataPopoverController = new OptionalDataPopoverController<FSProductGroupOptionalDataViewController, FloorSpaceProductGroupUnit, ProductGroupFloorSpaceViewModel>(UIPopoverArrowDirection.Any, AreaViewModel);

			#endregion
		}

        private Timer brandTimer;
        public Timer BrandTimer
        {
            get 
            {
                if (brandTimer == null)
                {
                    brandTimer = new Timer(1000);
                    brandTimer.AutoReset = false;
                    brandTimer.Elapsed += (object sender, ElapsedEventArgs e) =>
                    {
                        InvokeOnMainThread(() =>
                        {
                            if (this.brandTextField.Text.Length > this.AreaViewModel.ApplicationController.SearchThreshold)
                            {
                                AreaViewModel.BrandName = this.brandTextField.Text;
                                brandPopoverController.ShowPopover(this.brandTextField);
                            }
                            else
                            {
                                brandPopoverController.DismissPopover();
                            }
                            this.AreaViewModel.BrandName = this.brandTextField.Text;
                        });
                    };
                }
                return brandTimer; 
            }
        }
	}

	public class ProductGroupBrandsTableSource : BaseTableViewSource<InteractiveCategoryBrand>
	{
		private ProductGroupFloorSpaceViewModel areaViewModel;

		public ProductGroupBrandsTableSource(ProductGroupFloorSpaceViewModel areaViewModel) : base()
		{
			this.areaViewModel = areaViewModel;
		}

		public ProductGroupBrandsTableSource(ProductGroupFloorSpaceViewModel areaViewModel, ObservableCollection<InteractiveCategoryBrand> dataSource, UITableView tableView, string cellName)
			: base(dataSource, tableView, cellName)
		{
			this.areaViewModel = areaViewModel;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(this.cellName, indexPath);
			//cell.BackgroundColor = (indexPath.Row % 2 == 0) ? UIColor.FromRGB(243f / 255f, 243f / 255f, 243f / 255f) : UIColor.White;
			this.BindTaskCell(cell, this.dataSource.ToList()[indexPath.Row], indexPath);
			return cell;
		}

		public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
		{
			return false;
		}

		public override UITableViewCellEditingStyle EditingStyleForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return UITableViewCellEditingStyle.Delete;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			InteractiveCategoryBrand selectedItem = this.dataSource.ToList()[indexPath.Row];
			this.areaViewModel.SelectedCategoryBrand = selectedItem;
		}

		public virtual void BindTaskCell(UITableViewCell cell, InteractiveCategoryBrand item, NSIndexPath path)
		{
			FloorspaceProductGroupBrandsTableViewCell listCell = cell as FloorspaceProductGroupBrandsTableViewCell;

			if (listCell.bindings.Count() > 0)
			{
				foreach (Binding binding in listCell.bindings)
				{
					binding.Detach();
				}
				listCell.bindings.Clear();
				listCell.bindings = new List<Binding>();
			}

			listCell.bindings.Add(new Binding<string, string>(item, () => item.Text, listCell, () => listCell.BrandLabel.Text));
			listCell.bindings.Add(new Binding<int, string>(item, () => item.Quantity, listCell, () => listCell.QtyLabel.Text)
									  .ConvertSourceToTarget((arg) => arg.ToString()));
			listCell.bindings.Add(new Binding<int, string>(item, () => item.QuantityPos, listCell, () => listCell.QtyPOSLabel.Text)
								  	.ConvertSourceToTarget((arg) => arg.ToString()));
			listCell.bindings.Add(new Binding<int, string>(item, () => item.QuantitySpecial, listCell, () => listCell.QtySpecialLabel.Text)
					  				.ConvertSourceToTarget((arg) => arg.ToString()));
			listCell.bindings.Add(new Binding<int, string>(item, () => item.InPromotion, listCell, () => listCell.QtyPromoLabel.Text)
					  				.ConvertSourceToTarget((arg) => arg.ToString()));

			listCell.QtyMinusAction = (UIView sender) =>
			{
				if (item.QuantityCommand.CanExecute("0"))
				{
					item.QuantityCommand.Execute("0");
				}
			};

			listCell.QtyPlusAction = (UIView sender) =>
			{
				if (item.QuantityCommand.CanExecute("1"))
				{
					item.QuantityCommand.Execute("1");
				}
			};

			listCell.QtyPOSMinusAction = (UIView sender) =>
			{
				if (item.QuantityPosCommand.CanExecute("0"))
				{
					item.QuantityPosCommand.Execute("0");
				}
			};

			listCell.QtyPOSPlusAction = (UIView sender) =>
			{
				if (item.QuantityPosCommand.CanExecute("1"))
				{
					item.QuantityPosCommand.Execute("1");
				}
			};

			listCell.QtySpecialMinusAction = (UIView sender) =>
			{
				if (item.QuantitySpecialCommand.CanExecute("0"))
				{
					item.QuantitySpecialCommand.Execute("0");
				}
			};

			listCell.QtySpecialPlusAction = (UIView sender) =>
			{
				if (item.QuantitySpecialCommand.CanExecute("1"))
				{
					item.QuantitySpecialCommand.Execute("1");
				}
			};

			listCell.QtyPromoMinusAction = (UIView sender) =>
			{
				if (item.InPromoCommand.CanExecute("0"))
				{
					item.InPromoCommand.Execute("0");
				}
			};

			listCell.QtyPromoPlusAction = (UIView sender) =>
			{
				if (item.InPromoCommand.CanExecute("1"))
				{
					item.InPromoCommand.Execute("1");
				}
			};
		}
	}

}
