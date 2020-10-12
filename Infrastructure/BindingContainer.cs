using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Helpers;

namespace Electrolux.ShopFloor.iOS
{
	public class ViewCellItemBinding<T> : Dictionary<T, List<Binding>>
	{
	}

	public class BindingContainer<T> : BindingContainer
	{
		private Dictionary<T, List<Binding>> _items;

		public void Add(T item, List<Binding> binding)
		{
			if (!_items.ContainsKey(item))
			{
				_items.Add(item, binding);
			}
		}

		public override void DetachAll()
		{
			foreach (KeyValuePair<T, List<Binding>> bindingList in _items)
			{
				foreach (Binding binding in bindingList.Value)
				{
					binding.Detach();
				}
			}
			_items.Clear();
		}

		public void DetachAll(T item)
		{
			if (_items.ContainsKey(item))
			{
				List<Binding> bindingList = _items[item];
				foreach (Binding binding in bindingList)
				{
					binding.Detach();
				}
				_items.Remove(item);
			}
		}

		public BindingContainer()
		{
			_items = new Dictionary<T, List<Binding>>();
		}

		public bool ContainsItem(T item)
		{
			return (_items.ContainsKey(item));
		}
	}

	public class BindingContainer
	{
		private List<Binding> _items;

		public virtual void Add(Binding binding)
		{
			if (!Exist(binding))
			{
				_items.Add(binding);
			}
		}

		public virtual void DetachAll()
		{
			foreach (var item in _items)
			{
				item.Detach();
			}
		}

		public BindingContainer()
		{
			_items = new List<Binding>();
		}

		#region Private Methods

		private bool Exist(Binding binding)
		{
			return _items.IndexOf(binding) != -1;
		}

		#endregion
	}
}

