using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Electrolux.ShopFloor.iOS
{
    public interface ICellBinding<T>
    {
		T Item { get; set; }
		void BindCell(T item);
    }
}