using CoreGraphics;
using Electrolux.ShopFloor.Mvvm.ViewModels;
using Foundation;
using System;
using System.Collections.Generic;
using Electrolux.ShopFloor.Middleware.Contract;
using System.Diagnostics;
using System.Text;
using UIKit;
using System.Linq;
//using HockeyApp.iOS;

namespace Electrolux.ShopFloor.iOS.ViewControllers
{
    public partial class BaseViewController : UIViewController, IUITextFieldDelegate, IUITextViewDelegate
    {
        private List<UITextField> textFieldList;
        private UITextField _activeTextField;
        private UITextView _activeTextView;
        private NSObject keyboardAppearedObserver;
        private NSObject keyboardDisappearedObserver;
        private NSObject textViewDidStartEditingObserver;
        private NSObject textViewDidEndEditingObserver;
        private NSObject textFieldDidStartEditingObserver;
        private NSObject textFieldDidEndEditingObserver;
        private UIView activityIndicatorView;

        protected virtual UITextView[] TextViewArray()
        {
            return new UITextView[] { };
        }

        public virtual List<UITextField> TextFieldList
        {
            get
            {
                if (this.textFieldList == null)
                {
                    this.textFieldList = new List<UITextField>(); ;
                }
                return this.textFieldList;
            }
        }

        public BaseViewController(IntPtr handle) : base(handle)
        {

        }

        public BaseViewController()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.activityIndicatorView = new UIView(this.View.Frame);
            this.activityIndicatorView.UserInteractionEnabled = true;
            this.activityIndicatorView.BackgroundColor = UIColor.Black;
            this.activityIndicatorView.Alpha = 0.3f;
            var ai = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
            ai.TintColor = UIColor.FromRGB(29f / 255f, 32f / 255f, 81f / 255f);
            ai.StartAnimating();
            this.activityIndicatorView.AddSubview(ai);

            ai.TranslatesAutoresizingMaskIntoConstraints = false;

            //Trailing    
            NSLayoutConstraint trailing = NSLayoutConstraint.Create(view1: ai,
                                                                    attribute1: NSLayoutAttribute.Trailing,
                                                                    relation: NSLayoutRelation.Equal,
                                                                    view2: this.activityIndicatorView,
                                                                    attribute2: NSLayoutAttribute.Trailing,
                                                                    multiplier: 1f,
                                                                    constant: 0f);
            //Leading
            NSLayoutConstraint leading = NSLayoutConstraint.Create(ai,
                                                                   NSLayoutAttribute.Leading,
                                                                   NSLayoutRelation.Equal,
                                                                    this.activityIndicatorView,
                                                                    NSLayoutAttribute.Leading,
                                                                    1f,
                                                                    0f);
            //Bottom
            NSLayoutConstraint bottom = NSLayoutConstraint.Create(ai,
                                                                   NSLayoutAttribute.Bottom,
                                                                    NSLayoutRelation.Equal,
                                                                    this.activityIndicatorView,
                                                                    NSLayoutAttribute.Bottom,
                                                                    1f,
                                                                    0f);
            //Topm
            NSLayoutConstraint top = NSLayoutConstraint.Create(ai,
                                                               NSLayoutAttribute.Top,
                                                                NSLayoutRelation.Equal,
                                                                this.activityIndicatorView,
                                                                NSLayoutAttribute.Top,
                                                                1f,
                                                                0f);
            this.activityIndicatorView.AddConstraint(trailing);
            this.activityIndicatorView.AddConstraint(leading);
            this.activityIndicatorView.AddConstraint(bottom);
            this.activityIndicatorView.AddConstraint(top);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            RegisterForUIEvents();
        }

        public override void ViewWillDisappear(bool animated)
        {
            ResignForUIEvents();
            base.ViewWillDisappear(animated);
        }

        public void StartAsync()
        {
            this.View.AddSubview(this.activityIndicatorView);
            this.View.BringSubviewToFront(this.activityIndicatorView);
        }

        public void EndAsync()
        {
            this.activityIndicatorView.RemoveFromSuperview();
        }

        public void StartAsync(UIView sender)
        {
            this.activityIndicatorView.Frame = sender.Frame;
            sender.AddSubview(this.activityIndicatorView);
            sender.BringSubviewToFront(this.activityIndicatorView);
        }

        public void EndAsync(UIView sender)
        {
            this.activityIndicatorView.RemoveFromSuperview();
        }

        public virtual void RegisterForUIEvents()
        {
            if (this.scrollView == null)
                return;

            // textView events
            this.textViewDidStartEditingObserver = NSNotificationCenter.DefaultCenter.AddObserver(UITextView.TextDidBeginEditingNotification, textViewDidBeginEditing);
            this.textViewDidEndEditingObserver = NSNotificationCenter.DefaultCenter.AddObserver(UITextView.TextDidEndEditingNotification, textViewDidEndEditing);
            // textField events
            this.textFieldDidStartEditingObserver = NSNotificationCenter.DefaultCenter.AddObserver(UITextField.TextDidBeginEditingNotification, textFieldDidBeginEditing);
            this.textFieldDidEndEditingObserver = NSNotificationCenter.DefaultCenter.AddObserver(UITextField.TextDidEndEditingNotification, textFieldDidEndEditing);
            // Keyboard events
            this.keyboardAppearedObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.DidShowNotification, keyboardWasShown);
            this.keyboardDisappearedObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, keyboardWillHide);
        }

        public virtual void ResignForUIEvents()
        {
            if (keyboardAppearedObserver != null)
                NSNotificationCenter.DefaultCenter.RemoveObserver(keyboardAppearedObserver);

            if (keyboardDisappearedObserver != null)
                NSNotificationCenter.DefaultCenter.RemoveObserver(keyboardDisappearedObserver);

            if (textViewDidStartEditingObserver != null)
                NSNotificationCenter.DefaultCenter.RemoveObserver(textViewDidStartEditingObserver);

            if (textViewDidEndEditingObserver != null)
                NSNotificationCenter.DefaultCenter.RemoveObserver(textViewDidEndEditingObserver);

            if (textFieldDidStartEditingObserver != null)
                NSNotificationCenter.DefaultCenter.RemoveObserver(textFieldDidStartEditingObserver);

            if (textFieldDidEndEditingObserver != null)
                NSNotificationCenter.DefaultCenter.RemoveObserver(textFieldDidEndEditingObserver);
        }

        private void textViewDidBeginEditing(NSNotification notification)
        {
            UITextView textView = notification.Object as UITextView;
            if (this.TextViewArray().Contains(textView))
            {
                this.EditingStarted(textView);
            }
        }

        private void textViewDidEndEditing(NSNotification notification)
        {
            UITextView textView = notification.Object as UITextView;
            if (this.TextViewArray().Contains(textView))
            {
                this.EditingEnded(textView);
            }
        }

        private void textFieldDidBeginEditing(NSNotification notification)
        {
            UITextField textField = notification.Object as UITextField;
            if (this.TextFieldList.Contains(textField))
            {
                this.EditingStarted(textField);
            }
        }

        private void textFieldDidEndEditing(NSNotification notification)
        {
            UITextField textField = notification.Object as UITextField;
            if (this.TextFieldList.Contains(textField))
            {
                this.EditingEnded(textField);
            }
        }

        protected virtual void keyboardWillHide(NSNotification obj)
        {
            //if (_activeTextView == null && _activeTextField == null)
            //{
            //	return;
            //}
            var insetsZero = UIEdgeInsets.Zero;

            UIView.AnimateAsync(1.0, () =>
            {
                this.scrollView.ContentInset = insetsZero;
                this.scrollView.ScrollIndicatorInsets = insetsZero;
            });
            _activeTextView = null;
            _activeTextField = null;
        }

        protected virtual void keyboardWasShown(NSNotification notification)
        {
            if (_activeTextView == null && _activeTextField == null)
            {
                return;
            }
            // Step 1: Get the size of the keyboard.
            CGRect keyboardRect = UIKeyboard.FrameEndFromNotification(notification);

#if DEBUG
            Debugger.Log(0, "BaseViewController.keyboardWasShown", String.Format("Keyboard Rect before conversion x:{0} y:{0} w:{0} h:{0}", keyboardRect.Location.X.ToString(), keyboardRect.Location.Y.ToString(), keyboardRect.Size.Width.ToString(), keyboardRect.Size.Height.ToString()));
#endif
            // convert the coordinates because we are always rotated landscape
            keyboardRect = this.View.ConvertRectToView(keyboardRect, null);
#if DEBUG
            Debugger.Log(0, "BaseViewController.keyboardWasShown", String.Format("Keyboard Rect after conversion x:{0} y:{0} w:{0} h:{0}", keyboardRect.Location.X.ToString(), keyboardRect.Location.Y.ToString(), keyboardRect.Size.Width.ToString(), keyboardRect.Size.Height.ToString()));
#endif
            this.scrollOpenMainView(keyboardRect);
        }

        private void scrollOpenMainView(CGRect keyboardRect)
        {
            //todo ottenere l'altezza della nav bar dal nav controller 
            nfloat navBarHeight = (nfloat)64.0;
            if (this.NavigationController.NavigationBarHidden)
            {
                navBarHeight = (nfloat)0.0;
            }

            // Step 2: Adjust the bottom content inset of your scroll view by the keyboard height.
            UIEdgeInsets contentInsets = new UIEdgeInsets((nfloat)0.0, (nfloat)0.0, keyboardRect.Size.Height, (nfloat)0.0);
            this.scrollView.ContentInset = contentInsets;
            this.scrollView.ScrollIndicatorInsets = contentInsets;
            // Step 3: Scroll the target text field into view.
            CGRect aRect = this.View.Window.Frame;
            aRect.Height -= (keyboardRect.Size.Height + navBarHeight);
            // the following is to convert the frame of the activeTextField in the self.view reference system if that control belongs to a different view (e.g. one cell of the tableview)
            CGRect activeTextFieldRect;
            if (_activeTextView != null)
            {
                activeTextFieldRect = _activeTextView.Superview.ConvertRectToView(_activeTextView.Frame, null);
            }
            else
            {
                activeTextFieldRect = _activeTextField.Superview.ConvertRectToView(_activeTextField.Frame, null);
            }
            // this is the control's lower-right vertex
            CGPoint lowerRightVertex = new CGPoint(activeTextFieldRect.X + activeTextFieldRect.Size.Width, activeTextFieldRect.Y + activeTextFieldRect.Size.Height);
            //#if DEBUG
            //            NSLog(@"aRect x: %f y:%f w:%f h:%f", aRect.origin.x, aRect.origin.y, aRect.size.width, aRect.size.height);
            //            NSLog(@"activeTextFieldRect x: %f y:%f w:%f h:%f", activeTextFieldRect.origin.x, activeTextFieldRect.origin.y, activeTextFieldRect.size.width, activeTextFieldRect.size.height);
            //#endif
            if (!aRect.Contains(lowerRightVertex))
            {
                //        CGPoint scrollPoint = CGPointMake(0.0, activeTextFieldRect.origin.y - (keyboardRect.size.height-activeTextFieldRect.size.height-15));
                CGPoint scrollPoint = new CGPoint(0.0, activeTextFieldRect.Y + navBarHeight - keyboardRect.Y + activeTextFieldRect.Height + 15);
                //# ifdef kDebugMode
                //                NSLog(@"setcontentoffset x:%f y:%f", scrollPoint.x, scrollPoint.y);
                //#endif
                this.scrollView.SetContentOffset(scrollPoint, true);
            }
        }

        #region UITextFieldDelegate

        [Export("textFieldDidBeginEditing:")]
        public void EditingStarted(UITextField textField)
        {
            _activeTextField = textField;
        }

        [Export("textFieldDidEndEditing:")]
        public void EditingEnded(UITextField textField)
        {
            //_activeTextField = null;
        }

        #endregion

        #region IUITextViewDelegate
        [Export("textViewDidBeginEditing:")]
        public void EditingStarted(UITextView textView)
        {
            _activeTextView = textView;
        }

        [Export("textViewDidEndEditing:")]
        public void EditingEnded(UITextView textView)
        {
            //_activeTextView = null;
        }
        #endregion

        #region Shake Gesture

        public override bool CanBecomeFirstResponder
        {
            get
            {
                return true;
            }
        }

        public override void MotionEnded(UIEventSubtype motion, UIEvent evt)
        {
            IAppConfig config = RegistryService.Instance.Resolve<IAppConfig>();
            if (motion == UIEventSubtype.MotionShake)
            {
#if !PROD
                //if (config.IsHockeyAppActive)
                //{
                //    BITHockeyManager.SharedHockeyManager.FeedbackManager.ShowFeedbackComposeViewWithGeneratedScreenshot();
                //}
#endif
            }
		}

#endregion
	}
}
