using System;
using System.Linq;
using System.Threading.Tasks;
using Electrolux.ShopFloor.iOS.ViewControllers;
using Electrolux.ShopFloor.Mvvm.ViewModels;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using GalaSoft.MvvmLight.Helpers;
using UIKit;

namespace Electrolux.ShopFloor.iOS
{
	public partial class BaseFilterViewController : BaseViewController<ReportsViewModel>
	{
		public BaseFilterViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ConfigureArea()
		{
			base.ConfigureArea();
		}

		public override void RegisterCommands()
		{
			base.RegisterCommands();
		}

		public override void RegisterBindingsLocal()
		{
			base.RegisterBindingsLocal();
		}
	}
}
