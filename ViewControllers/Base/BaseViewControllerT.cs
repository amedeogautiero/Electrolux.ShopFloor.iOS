using Electrolux.ShopFloor.Mvvm.ViewModels;
using Foundation;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight.Helpers;
using UIKit;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Electrolux.ShopFloor.Mvvm.ViewModels.EditingAreas;

namespace Electrolux.ShopFloor.iOS.ViewControllers
{
	[Register("AppBaseViewController")]
	public class BaseViewController<T> : BaseViewController where T : AppViewModelBase
	{
		private List<Binding> bindingsLocal;
		private List<Binding> bindingsLocalArea;
		private bool isDialogVisible;
        private string dialogMessageHeader;
        private string dialogMessageBody;
 
        public BaseViewController(IntPtr handle) : base(handle)
		{

		}

		public BaseViewController()
		{
		}

		public T ViewModel
		{
			get 
			{
				if (AppDelegate.Locator != null)
				{
					return AppDelegate.Locator.GetInstance<T>();
				}
				else {
					return null;
				}
			}
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
            RegisterCommands();
            this.bindingsLocal = new List<Binding>();
			this.bindingsLocalArea = new List<Binding>();
		}

        public override void ViewWillAppear(bool animated)
        {
            RegisterBindings();
			Translations();
            base.ViewWillAppear(animated);
        }

        public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			if (!ViewModel.Activated)
			{
				ViewModel.OnActivated();
			}
		}

		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);

			if (ViewModel.Activated)
			{
				ViewModel.OnDeActivated();
			}
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
            DetachBindings();
        }

		public virtual void ConfigureArea()
		{
			this.DetachBindingsLocal();
			this.RegisterBindingsLocal();
		}

		#region Translations in viewwillappear

		public virtual void Translations()
		{
		}

		#endregion

		#region view appear/disappear bindings management - no area bindings here

		public virtual void RegisterBindings()
        {
			if (DialogViewModel != null)
			{
				this.KeepBindingInMemoryLocal(this.SetBinding(() => DialogViewModel.IsDialogVisible, () => this.IsDialogVisible));
                this.KeepBindingInMemoryLocal(this.SetBinding(() => DialogViewModel.DialogMessageBody, () => this.DialogMessageBody));
                this.KeepBindingInMemoryLocal(this.SetBinding(() => DialogViewModel.DialogMessageHeader, () => this.DialogMessageHeader));
			}
        }

        public virtual void DetachBindings()
        {
            this.ClearAndDetachBindingsLocal();
        }

        public virtual void RegisterCommands()
        {

        }

        public Binding KeepBindingInMemoryLocal(Binding b)
		{
			this.bindingsLocal.Add(b);
			return b;
		}

		public void ClearAndDetachBindingsLocal()
		{
			foreach (Binding binding in this.bindingsLocal)
			{
				binding.Detach();
			}
			this.bindingsLocal.Clear();
		}

		#endregion

		#region area related bindings management - area bindings go here

		public Binding KeepBindingInMemoryLocalArea(Binding b)
		{
			this.bindingsLocalArea.Add(b);
			return b;
		}

		public void ClearAndDetachBindingsLocalArea()
		{
 			foreach (Binding binding in this.bindingsLocalArea)
			{
				binding.Detach();
			}
			this.bindingsLocalArea.Clear();
		}

		public virtual void RegisterBindingsLocal()
		{
		}

		public virtual void DetachBindingsLocal()
		{
			this.ClearAndDetachBindingsLocalArea();
		}

		#endregion

        #region Dialog Related Code

        public bool IsDialogVisible
        {
            get { return this.isDialogVisible; }
            set
            {
                this.isDialogVisible = value;
                UIAlertController alertController = UIAlertController.Create(this.DialogMessageHeader, this.DialogMessageBody, UIAlertControllerStyle.Alert);
                alertController.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Default, (UIAlertAction obj) =>
                {
                    DialogViewModel.CancelCommand.Execute(null);
                }));
                alertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, (UIAlertAction obj) =>
                {
                    DialogViewModel.ConfirmCommand.Execute(null);
                }));

                if (this.isDialogVisible)
                {
                    this.PresentViewController(alertController, true, null);
                }
            }
        }

        public string DialogMessageBody { get { return this.dialogMessageBody; } set { this.dialogMessageBody = value; } }

        public string DialogMessageHeader { get { return this.dialogMessageHeader; } set { this.dialogMessageHeader = value; } }

        public DialogViewModelBase DialogViewModel
        {
            get
            {
                return this.ViewModel as DialogViewModelBase;
            }
        }

        #endregion
    }

	public class BaseViewController<T, K> : BaseViewController<T>, IBindableViewController<K> where T : AppViewModelBase where K : IndexedUnitCore
	{
		public BaseViewController(IntPtr handle) : base(handle)
		{
		}

		public BaseViewController()
		{
		}

		public virtual void BindTaskCell(UITableViewCell cell, K item, NSIndexPath path)
		{
		}
	}
}
