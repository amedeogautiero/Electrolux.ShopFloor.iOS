using System;
using Electrolux.ShopFloor.Mvvm.ViewModels;
using UIKit;
using GalaSoft.MvvmLight.Helpers;
using Electrolux.ShopFloor.Mvvm.ViewModels.EditingAreas;
using Foundation;

namespace Electrolux.ShopFloor.iOS.ViewControllers
{
	[Register("DialogAppBaseViewController")]
	public class DialogAppBaseViewController<T> : BaseViewController<T> where T : AppViewModelBase
	{
		private Binding isDialogVisibleBinding;
		private Binding dialogMessageHeaderBinding;
        private Binding dialogMessageBodyBinding;

		public DialogAppBaseViewController(IntPtr handle) : base(handle)
		{

		}

		public override void RegisterBindings()
		{
			base.RegisterBindings();
			this.isDialogVisibleBinding = this.SetBinding(() => DialogViewModel.IsDialogVisible, () => this.IsDialogVisible);
            this.dialogMessageHeaderBinding = this.SetBinding(() => DialogViewModel.DialogMessageHeader, () => this.DialogMessageHeader);
            this.dialogMessageBodyBinding = this.SetBinding(() => DialogViewModel.DialogMessageBody, () => this.DialogMessageBody);
		}

		public override void DetachBindings()
		{
			this.isDialogVisibleBinding.Detach();
			this.dialogMessageHeaderBinding.Detach();
            this.dialogMessageBodyBinding.Detach();
			base.DetachBindings();
		}
	}
}
