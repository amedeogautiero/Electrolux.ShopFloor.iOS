using CoreGraphics;
using Electrolux.ShopFloor.Core.UI;
using Foundation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UIKit;

namespace Electrolux.ShopFloor.iOS.Services.Providers
{
	public class UIServiceProvider : NSObject, IUIServiceProvider
	{
		private readonly UINavigationController navigationFrame;

		/// <summary>
		/// Initializes a new instance of the <see cref="UiServiceProvider"/> class.
		/// </summary>
		/// <param name="navigationFrame">The navigation frame.</param>
		public UIServiceProvider(UINavigationController navigationFrame)
		{
			this.navigationFrame = navigationFrame;
		}

		public bool CanGoBack
		{
			get
			{
				return (this.navigationFrame.ViewControllers.Length > 1);
			}
		}

		public void ClearBackStack()
		{
			Debug.Write("Clear Back Stack");
		}

		public void Defer(Action callback)
		{
			InvokeOnMainThread(() =>
			{
				callback();
			});
		}

		public void Defer(int delay, Action callback)
		{
			NSTimer.CreateScheduledTimer(new TimeSpan(0, 0, 0, 0, delay), delegate
			{
				callback();
			});
		}
	}
}
