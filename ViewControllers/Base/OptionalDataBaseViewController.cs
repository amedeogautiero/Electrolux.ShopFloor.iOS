using System;
using System.Collections.Generic;
using CoreGraphics;
using Electrolux.ShopFloor.iOS.ViewControllers;
using Electrolux.ShopFloor.Mvvm.ViewModels.EditingAreas;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using GalaSoft.MvvmLight.Helpers;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public class OptionalDataBaseViewController<T, K> : BaseViewController where T : IndexedUnitCore where K : AreaViewModelBase
	{
		private List<Binding> bindingsLocal;

		public T Item;
		public K AreaViewModel;
		public BasePopoverController Owner;

		public OptionalDataBaseViewController(IntPtr handle) : base(handle)
		{
			
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.bindingsLocal = new List<Binding>();
		}

		public Binding KeepBindingInMemory(Binding b)
		{
			this.bindingsLocal.Add(b);
			return b;
		}

		public void ClearAndDetachBindings()
		{
			foreach (Binding binding in this.bindingsLocal)
			{
				binding.Detach();
			}
			this.bindingsLocal.Clear();
		}
	}
}
