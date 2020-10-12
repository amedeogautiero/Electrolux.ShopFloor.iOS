using System;
using Electrolux.ShopFloor.Mvvm.ViewModels;

namespace Electrolux.ShopFloor.iOS
{
	public interface ICellBindingViewModel<T, K> : ICellBinding<T> where K : AppViewModelBase
	{
		void BindEvents(T item, K viewModel);
	}
}

