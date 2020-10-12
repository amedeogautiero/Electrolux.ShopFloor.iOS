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
using System.Diagnostics;
using Electrolux.ShopFloor.iOS;
using System.Collections.ObjectModel;

namespace Electrolux.ShopFloor.iOS
{
	public partial class KitchenDetailsViewController : ListDetailBaseViewController<KitchenViewModel>
	{
		private PopoverViewController<ModelCategoryUnit> modelPopoverController;
		private PopoverViewController<BrandUnit> brandPopoverController;
		private PopoverViewController<ModelCategoryModelUnit> categoryPopoverController;
		private OptionalDataPopoverController<KitchenOptionalDataViewController, KitchenCompleteUnit, KitchenViewModel> optionalDataPopoverController;

		public KitchenDetailsViewController(IntPtr handle) : base(handle)
		{
		}

		public override void RegisterBindingsLocal()
		{
			if (this.AreaViewModel != null)
			{
				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.KitchenName, this, () => this.kitchenNameTextField.Text, BindingMode.TwoWay));
				KeepBindingInMemoryLocal(new Binding<int, string>(this.AreaViewModel, () => this.AreaViewModel.TotalAppliances, this, () => this.totalAppliancesTextField.Text)
										 .ConvertSourceToTarget((int arg) => arg.ToString()));
				KeepBindingInMemoryLocal(new Binding<int, string>(this.AreaViewModel, () => this.AreaViewModel.TotalAppliances).WhenSourceChanges(() =>
				{
					this.kitchenItemsTableView.ReloadData();
				}));
				KeepBindingInMemoryLocal(new Binding<int, string>(this.AreaViewModel, () => this.AreaViewModel.TotalElectroluxAppliances, this, () => this.electroluxAppliancesTextField.Text)
										 .ConvertSourceToTarget((int arg) => arg.ToString()));
				KeepBindingInMemoryLocal(new Binding<int, string>(this.AreaViewModel, () => this.AreaViewModel.TotalElectroluxAppliances).WhenSourceChanges(() =>
				{
					this.kitchenItemsTableView.ReloadData();
				}));
				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.Description, this, () => this.kitchenDescriptionTextView.Text, BindingMode.TwoWay));

				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.KitchenNameErrorMessage, this, () => this.kitchenNameMessageLabel.Text));
				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.DescriptionErrorMessage, this, () => this.descriptionMessageLabel.Text));
				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.ProductErrorMessage, this, () => this.errorMessagLabel.Text));

				KeepBindingInMemoryLocal(new Binding<bool, bool>(this.AreaViewModel, () => this.AreaViewModel.CanEditModel, this, () => this.modelTextField.Enabled));
				KeepBindingInMemoryLocal(new Binding<bool, bool>(this.AreaViewModel, () => this.AreaViewModel.CanEditCategory, this, () => this.categoryTextField.Enabled));
				KeepBindingInMemoryLocal(new Binding<bool, bool>(this.AreaViewModel, () => this.AreaViewModel.CanEditBrandModel, this, () => this.brandTextField.Enabled));

				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.SelectedCategory.Text, this, () => this.categoryTextField.Text, BindingMode.TwoWay));

				// model field
				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.Model, this, () => this.modelTextField.Text, BindingMode.TwoWay));

				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.BrandName, this, () => this.brandTextField.Text, BindingMode.TwoWay));
				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.Price, this, () => this.priceTextField.Text, BindingMode.TwoWay));
				KeepBindingInMemoryLocal(new Binding<string, string>(this.AreaViewModel, () => this.AreaViewModel.Quantity, this, () => this.qtyTextField.Text, BindingMode.TwoWay));

				KeepBindingInMemoryLocal(new Binding<bool, bool>(this.AreaViewModel, () => this.AreaViewModel.IsOverlayVisible).WhenSourceChanges(() =>
				{
					if (!this.AreaViewModel.IsOverlayVisible && optionalDataPopoverController != null)
					{
						optionalDataPopoverController.DismissPopover();
					}
					kitchenItemsTableView.ReloadData();
				}));

				KeepBindingInMemoryLocal(new Binding<string, bool>(this.AreaViewModel, () => this.AreaViewModel.QuantitySpecial, this, () => this.specialPlacementSwitch.On, BindingMode.TwoWay)
                      .ConvertTargetToSource((bool arg) =>
                      {
                          return arg ? "1" : "0";
                      })
                      .ConvertSourceToTarget((string arg) =>
                      {
                          return ((string.IsNullOrWhiteSpace(arg)) || (arg == "0")) ? false : true;
                      })
                     );

				KeepBindingInMemoryLocal(new Binding<string, bool>(this.AreaViewModel, () => this.AreaViewModel.InPromo, this, () => this.promoSwitch.On, BindingMode.TwoWay)
                      .ConvertTargetToSource((bool arg) =>
                      {
                          return arg ? "1" : "0";
                      })
                      .ConvertSourceToTarget((string arg) =>
                      {
                          return ((string.IsNullOrWhiteSpace(arg)) || (arg == "0")) ? false : true;
                      })
                     );
			}
		}

		partial void addItemButtonAction(NSObject sender)
		{
			if (AreaViewModel.AddCommand.CanExecute(null))
			{
				AreaViewModel.AddCommand.Execute(null);
				this.modelTextField.Text = "";
			}
		}

		partial void optionalDataButtonAction(NSObject sender)
		{
			if (AreaViewModel.OpenOverlayCommand.CanExecute(null))
			{
				AreaViewModel.OpenOverlayCommand.Execute(null);
			}
			optionalDataPopoverController.ShowPopover(sender as UIView, new CGSize(540f, 372f), null);
		}

		public override void Translations()
		{
			this.kitchenNameLabel.Text = TranslatorManager.GetInstance().GetString("Kitchen name");
			this.totalAppliancesLabel.Text = TranslatorManager.GetInstance().GetString("Total appliances");
			this.electroluxAppliancesLabel.Text = TranslatorManager.GetInstance().GetString("Total Electrolux appliances");
			this.kitchenDescriptionLabel.Text = TranslatorManager.GetInstance().GetString("Kitchen description");
			this.modelLabel.Text = TranslatorManager.GetInstance().GetString("Model");
			this.categoryLabel.Text = TranslatorManager.GetInstance().GetString("Category");
			this.brandLabel.Text = TranslatorManager.GetInstance().GetString("Brand");
			this.qtyLabel.Text = TranslatorManager.GetInstance().GetString("Qty");
			this.qtySpecialLabel.Text = TranslatorManager.GetInstance().GetString("Qty FK");
			this.qtyPromoLabel.Text = TranslatorManager.GetInstance().GetString("Qty P");

			this.modelHeaderLabel.Text = TranslatorManager.GetInstance().GetString("Model");
			this.categoryHeaderLabel.Text = TranslatorManager.GetInstance().GetString("Category");
			this.brandHeaderLabel.Text = TranslatorManager.GetInstance().GetString("Brand");
			this.qtyHeaderLabel.Text = TranslatorManager.GetInstance().GetString("Qty");
			this.qtySpecialHeaderLabel.Text = TranslatorManager.GetInstance().GetString("Qty FK");
			this.qtyPromoHeaderLabel.Text = TranslatorManager.GetInstance().GetString("Qty P");
		}

		public override void ConfigureArea()
		{
			base.ConfigureArea();

			//this.kitchenDescriptionTextView.Delegate = this;

			#region Model

			this.modelTextField.Ended += (sender, e) =>
			{
				this.EditingEnded(this.modelTextField);
			};

			this.modelTextField.Started += (sender, e) =>
			{
				this.EditingStarted(this.modelTextField);
			};

			this.modelTextField.ShouldChangeCharacters += (textField, range, replacementString) =>
			{
				var newContent = new NSString(textField.Text).Replace(range, new NSString(replacementString)).ToString();
				if (newContent.Length > this.AreaViewModel.ApplicationController.SearchThreshold)
				{
					//AreaViewModel.Model = newContent;
					modelPopoverController.ShowPopover(this.modelTextField, forceWidth: true);
				}
				else
				{
					modelPopoverController.DismissPopover();
				}
				return true;
			};

			this.modelTextField.ShouldEndEditing += (UITextField textField) =>
			{
				AreaViewModel.ModelConfirmed();
				return true;
			};

			this.modelTextField.ShouldClear += (UITextField textField) =>
			{
				this.AreaViewModel.SelectedModel = null;
				return true;
			};

			modelPopoverController = new PopoverViewController<ModelCategoryUnit>(this.AreaViewModel.Models, new CGSize(480f, 320f), "ModelCategoryTableViewCell", UIPopoverArrowDirection.Down, (UITableViewCell cell) =>
			{
				if (cell is ModelCategoryTableViewCell)
				{
					ModelCategoryUnit item = ((ModelCategoryTableViewCell)cell).Item;
					this.AreaViewModel.SelectedModel = item;
					AreaViewModel.Model = this.modelTextField.Text = item.Text;
					this.modelTextField.ResignFirstResponder();
				}
				modelPopoverController.DismissPopover();
			});

			#endregion

			#region Brand

			this.brandTextField.Ended += (sender, e) =>
			{
				this.EditingEnded(this.brandTextField);
			};

			this.brandTextField.Started += (sender, e) =>
			{
				this.EditingStarted(this.brandTextField);
			};

			this.brandTextField.ShouldChangeCharacters += (textField, range, replacementString) =>
			{
				var newContent = new NSString(textField.Text).Replace(range, new NSString(replacementString)).ToString();
				if (newContent.Length > this.AreaViewModel.ApplicationController.SearchThreshold)
				{
					brandPopoverController.ShowPopover(this.brandTextField, forceWidth: true);
				}
				else
				{
					brandPopoverController.DismissPopover();
				}
				return true;
			};

			brandPopoverController = new PopoverViewController<BrandUnit>(this.AreaViewModel.Brands, new CGSize(480f, 320f), "BrandTableViewCell", UIPopoverArrowDirection.Down, (UITableViewCell cell) =>
			{
				if (cell is BrandTableViewCell)
				{
					this.AreaViewModel.SelectedBrandModel = ((BrandTableViewCell)cell).Item;
					this.brandTextField.ResignFirstResponder();
				}
				brandPopoverController.DismissPopover();
			});

			#endregion

			#region Category

			this.categoryTextField.ShouldBeginEditing += (UITextField textField) =>
			{
				categoryPopoverController.ShowPopover(textField, forceWidth: true);
				return false;
			};

			this.categoryTextField.ShouldClear += (UITextField textField) =>
			{
				this.AreaViewModel.SelectedCategory = null;
				return true;
			};

			categoryPopoverController = new PopoverViewController<ModelCategoryModelUnit>(this.AreaViewModel.Categories, new CGSize(480f, 320f), "ModelCategoryModelTableViewCell", UIPopoverArrowDirection.Down, (UITableViewCell cell) =>
			{
				if (cell is ModelCategoryModelTableViewCell)
				{
					this.AreaViewModel.SelectedCategory = ((ModelCategoryModelTableViewCell)cell).Item;
					this.categoryTextField.ResignFirstResponder();
				}
				categoryPopoverController.DismissPopover();
			});

			#endregion

			#region Kitchen products list tableview

			//if (this.kitchenItemsTableView.Source == null)
			{
				this.kitchenItemsTableView.Source = new KitchenItemsTableSource(AreaViewModel, AreaViewModel.KitchenProducts, this.kitchenItemsTableView, "KitchenItemsTableViewCell");
				this.kitchenItemsTableView.ReloadData();
			}

			#endregion

			#region Optional Data

			this.optionalDataPopoverController = new OptionalDataPopoverController<KitchenOptionalDataViewController, KitchenCompleteUnit, KitchenViewModel>(UIPopoverArrowDirection.Any, AreaViewModel);

			#endregion
		}
	}

	public class KitchenItemsTableSource : BaseTableViewSource<KitchenProductUnit>
	{
		private KitchenViewModel areaViewModel;

		public KitchenItemsTableSource(KitchenViewModel areaViewModel) : base()
		{
			this.areaViewModel = areaViewModel;
		}

		public KitchenItemsTableSource(KitchenViewModel areaViewModel, ObservableCollection<KitchenProductUnit> dataSource, UITableView tableView, string cellName)
			: base(dataSource, tableView, cellName)
		{
			this.areaViewModel = areaViewModel;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(this.cellName, indexPath);
			cell.BackgroundColor = (indexPath.Row % 2 == 0) ? UIColor.FromRGB(243f / 255f, 243f / 255f, 243f / 255f) : UIColor.White;
			this.BindTaskCell(cell, this.dataSource.ToList()[indexPath.Row], indexPath);
			return cell;
		}

		public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
		{
			return true;
		}

		public override void CommitEditingStyle(UITableView tableView,
												UITableViewCellEditingStyle editingStyle,
												NSIndexPath indexPath)
		{
			switch (editingStyle)
			{
				case UITableViewCellEditingStyle.Delete:
					this.areaViewModel.DeleteProductCommand.Execute(this.dataSource.ToList()[indexPath.Row]);
					//List<NSIndexPath> Rows = new List<NSIndexPath> { indexPath };
					//tableView.DeleteRows(Rows.ToArray(), UITableViewRowAnimation.Fade);
					break;
				default:
					break;
			}
		}

		public override UITableViewCellEditingStyle EditingStyleForRow(UITableView tableView,
																		NSIndexPath indexPath)
		{
			return UITableViewCellEditingStyle.Delete;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			KitchenProductUnit selectedItem = this.dataSource.ToList()[indexPath.Row];
			this.areaViewModel.SelectedKitchenProduct = selectedItem;
		}

		void BindTaskCell(UITableViewCell cell, KitchenProductUnit item, NSIndexPath indexPath)
		{
			KitchenItemsTableViewCell listCell = cell as KitchenItemsTableViewCell;

			if (listCell.Bindings != null)
			{
				foreach (Binding binding in listCell.Bindings)
				{
					binding.Detach();
				}
				listCell.Bindings.Clear();
			}
			else
			{
				listCell.Bindings = new List<Binding>();
			}

			listCell.Bindings.Add(new Binding<string, string>(item, () => item.Text, listCell, () => listCell.ModelLabel.Text));
			listCell.Bindings.Add(new Binding<string, string>(item, () => item.Brand.Text, listCell, () => listCell.BrandLabel.Text));
			listCell.Bindings.Add(new Binding<string, string>(item, () => item.Category.Text, listCell, () => listCell.CategoryLabel.Text));
			listCell.Bindings.Add(new Binding<int, string>(item, () => item.Quantity, listCell, () => listCell.QtyLabel.Text)
									  	.ConvertSourceToTarget((arg) => arg.ToString()));
			listCell.Bindings.Add(new Binding<bool, bool>(item, () => item.IsLinkedToKitchen, listCell, () => listCell.PreferredModelIndicatorLabel.Hidden)
					  					.ConvertSourceToTarget((arg) => !arg));
			listCell.Bindings.Add(new Binding<int, string>(item, () => item.QuantitySpecial, listCell, () => listCell.QtySpecialLabel.Text)
                                          .ConvertSourceToTarget((arg) => arg.ToString()));
			listCell.Bindings.Add(new Binding<int, string>(item, () => item.QuantityPromo, listCell, () => listCell.QtyPromoLabel.Text)
                                          .ConvertSourceToTarget((arg) => arg.ToString()));
		}

	}

}
