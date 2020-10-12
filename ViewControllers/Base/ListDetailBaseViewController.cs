using System;
using Electrolux.ShopFloor.iOS.ViewControllers;
using Electrolux.ShopFloor.Mvvm.ViewModels.EditingAreas;

namespace Electrolux.ShopFloor.iOS.ViewControllers
{
	public class ListDetailBaseViewController<T> : DetailBaseViewController<T> where T : AreaViewModelBase
	{
		public ListDetailBaseViewController(IntPtr handle) : base(handle)
		{
		}
	}
}

