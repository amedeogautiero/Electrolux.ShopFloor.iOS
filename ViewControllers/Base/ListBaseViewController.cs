using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Electrolux.ShopFloor.Mvvm.ViewModels;
using Electrolux.ShopFloor.Mvvm.ViewModels.EditingAreas;
using Foundation;
using UIKit;
using System.Linq;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using GalaSoft.MvvmLight.Helpers;
using Electrolux.ShopFloor.iOS;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Electrolux.ShopFloor.iOS.ViewControllers
{
	public partial class ListBaseViewController<T, K> : BaseViewController<ReportEditorViewModel, K> where T : AreaListViewModelBase<K> where K : IndexedUnitCore
	{
		private bool isEditMode;
		private int selectedArea;
		private Binding isEditModeBinding;
		private Binding selectedAreaBinding;

		public virtual UITableView PublicTableView
		{
			get { return this.TableView; }
		}

		public virtual UIView PublicDetailsView
		{
			get { return this.DetailsView; }
		}

		public T AreaViewModel
		{
			get { return ViewModel.ActiveArea as T; }
		}

		protected virtual string CellName
		{
			get { return ""; }
		}

		public virtual bool ListRowEditingEnabled
		{
			get { return true; }
		}

		public bool IsEditMode
		{
			get { return this.isEditMode; }
			set
			{
				this.isEditMode = value;
				if (this.isEditMode)
				{
					((ListDetailBaseViewController<T>)this.ChildViewControllers[0]).ConfigureArea();
					if (this.DetailsView != null)
					{
						this.View.BringSubviewToFront(this.DetailsView);
					}
				}
				else {
					if (this.DetailsView != null)
					{
						this.View.SendSubviewToBack(this.DetailsView);
					}
					this.TableView.ReloadData();
				}
				this.EndAsync();
			}
		}

		public int SelectedArea
		{
			get { return this.selectedArea; }
			set
			{
				this.selectedArea = value;
				this.DetachAreaBindings();
				this.RegisterAreaBindings();
			}
		}

		public ListBaseViewController(IntPtr handle) : base(handle)
		{
		}

		public override void RegisterBindings()
		{
			//base.RegisterBindings();
			this.selectedAreaBinding = new Binding<int, int>(ViewModel, () => ViewModel.SelectedArea, this, () => this.SelectedArea);
		}

		public override void DetachBindings()
		{
			base.DetachBindings();
			this.selectedAreaBinding.Detach();
		}

		public void RegisterAreaBindings()
		{
			if (AreaViewModel != null)
			{
				this.isEditModeBinding = new Binding<bool, bool>(AreaViewModel, () => AreaViewModel.IsEditMode, this, () => this.IsEditMode);
			}
		}

		public void DetachAreaBindings()
		{
			if (this.isEditModeBinding != null)
				this.isEditModeBinding.Detach();
		}

		public override void ConfigureArea()
		{
			base.ConfigureArea();

			//if (this.TableView.Source == null)
			//{
				this.TableView.Source = new ListBaseTableViewSource<K, T>(AreaViewModel.Items, this.TableView, CellName, this);
			//}
			this.TableView.ReloadData();
		}
	}
}
