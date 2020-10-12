// This file has been autogenerated from a class added in the UI designer.

using System;
using CoreGraphics;
using Electrolux.ShopFloor.iOS.ViewControllers;
using Electrolux.ShopFloor.Middleware.Manager;
using Electrolux.ShopFloor.Mvvm.ViewModels.EditingAreas;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using GalaSoft.MvvmLight.Helpers;
using UIKit;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Electrolux.ShopFloor.iOS
{
	public partial class FloorspaceProductGroupViewController : ListBaseViewController<ProductGroupFloorSpaceViewModel, FloorSpaceProductGroupUnit>
	{
		public FloorspaceProductGroupViewController (IntPtr handle) : base (handle)
		{
		}

		protected override string CellName
		{
			get
			{
				return "FloorspaceProductGroupTableViewCell";
			}
		}

		public override bool ListRowEditingEnabled
		{
			get
			{
				return false;
			}
		}

		public override void Translations()
		{
			this.productGroupLabel.Text = TranslatorManager.GetInstance().GetString("Product Group");
			this.itemsCountLabel.Text = TranslatorManager.GetInstance().GetString("Items count");
		}

		public override void BindTaskCell(UITableViewCell cell, FloorSpaceProductGroupUnit item, NSIndexPath path)
		{
 
			base.BindTaskCell(cell, item, path);

			FloorspaceProductGroupTableViewCell listCell = cell as FloorspaceProductGroupTableViewCell;

			listCell.ProductGroupLabel.Text = item.Text;
			listCell.ItemsCountLabel.Text = item.Unit.ItemsCount.ToString();
		}
	}
}
