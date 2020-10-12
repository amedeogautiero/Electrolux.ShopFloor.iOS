using System;
using System.IO;
using CoreGraphics;
using Electrolux.ShopFloor.iOS.ViewControllers;
using Electrolux.ShopFloor.Middleware.Manager;
using Electrolux.ShopFloor.Mvvm.ViewModels.EditingAreas;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Foundation;
using AVFoundation;
using GalaSoft.MvvmLight.Helpers;
using UIKit;
using System.Threading.Tasks;
using Photos;
using System.Diagnostics;
using System.Drawing;

namespace Electrolux.ShopFloor.iOS
{
	public partial class PhotoDetailsViewController : ListDetailBaseViewController<PhotosViewModel>, IUIImagePickerControllerDelegate
	{
		private PopoverViewController<BrandUnit> brandPopoverController;
		private PopoverViewController<ReferToUnit> referToPopoverController;
		private PopoverViewController<QualityLevelUnit> qualityLevelPopoverController;
		private enum eCommand
		{
			TakePhoto = 0,
			LoadPhoto = 1
		}

		public PhotoDetailsViewController(IntPtr handle) : base(handle)
		{
		}

		protected override UIKit.UITextView[] TextViewArray()
		{
			return new UITextView[] { this.descriptionTextView };
		}

		private async Task AuthorizeCameraUse()
		{
			var authorizationStatus = AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video);
			if (authorizationStatus == AVAuthorizationStatus.NotDetermined)
			{
				takePictureButton.Enabled = await AVCaptureDevice.RequestAccessForMediaTypeAsync(AVMediaType.Video);
			}
			else
			{
				takePictureButton.Enabled = (authorizationStatus == AVAuthorizationStatus.Authorized);
			}
		}

		private void AuthorizePhotoLibraryUse()
		{
			var authorizationStatus = PHPhotoLibrary.AuthorizationStatus;
			if (authorizationStatus == PHAuthorizationStatus.NotDetermined)
			{
				PHPhotoLibrary.RequestAuthorization((PHAuthorizationStatus status) =>
				{
					InvokeOnMainThread(() =>
					{
						switch (status)
						{
							case PHAuthorizationStatus.Authorized:
								loadPictureButton.Enabled = true;
								break;
							case PHAuthorizationStatus.Restricted:
								loadPictureButton.Enabled = true;
								break;
							case PHAuthorizationStatus.Denied:
								loadPictureButton.Enabled = false;
								break;
							default:
								break;
						}
					});
				});
			} else
			{
				loadPictureButton.Enabled = (authorizationStatus == PHAuthorizationStatus.Authorized);
			}
		}

		public override void Translations()
		{
			this.brandLabel.Text = TranslatorManager.GetInstance().GetString("Competitor/Electrolux Brand");
			this.performanceLabel.Text = TranslatorManager.GetInstance().GetString("Product Performance");
			this.referToLabel.Text = TranslatorManager.GetInstance().GetString("Photo Refer To");
			this.descriptionLabel.Text = TranslatorManager.GetInstance().GetString("Description");

			this.takePictureButton.SetTitle(TranslatorManager.GetInstance().GetString("Take Picture"), UIControlState.Normal);
			this.loadPictureButton.SetTitle(TranslatorManager.GetInstance().GetString("Load Picture"), UIControlState.Normal);

			this.brandTextField.Placeholder = TranslatorManager.GetInstance().GetString("Mandatory");
			this.performanceTextField.Placeholder = TranslatorManager.GetInstance().GetString("Mandatory");
			this.referToTextField.Placeholder = TranslatorManager.GetInstance().GetString("Mandatory");
		}

		public override async void ConfigureArea()
		{
			base.ConfigureArea();

			#region Behaviors

			//this.descriptionTextView.Delegate = this;

			#region Brand 

			this.brandTextField.ShouldChangeCharacters += (textField, range, replacementString) =>
			{
				var newContent = new NSString(textField.Text).Replace(range, new NSString(replacementString)).ToString();
				if (newContent.Length > this.AreaViewModel.ApplicationController.SearchThreshold)
				{
					brandPopoverController.ShowPopover(textField);
				}
				else
				{
					brandPopoverController.DismissPopover();
				}
				return true;
			};

			brandPopoverController = new PopoverViewController<BrandUnit>(
				this.AreaViewModel.Brands,
				new CGSize(this.brandTextField.Frame.Size.Width, 320f),
				"BrandTableViewCell",
				UIPopoverArrowDirection.Left,
				(UITableViewCell cell) =>
				{
					if (cell is BrandTableViewCell)
					{
						BrandUnit unit = ((BrandTableViewCell)cell).Item;
						this.AreaViewModel.BrandName = unit.Text;

						this.AreaViewModel.SelectedBrand = unit;
						this.brandTextField.ResignFirstResponder();
					}
					brandPopoverController.DismissPopover();
				}
			);

			#endregion

			#region Refer To

			this.referToTextField.ShouldBeginEditing += (UITextField textField) =>
			{
				referToPopoverController.ShowPopover(textField);
				return false;
			};

			referToPopoverController = new PopoverViewController<ReferToUnit>(
				this.AreaViewModel.Referrals,
				new CGSize(this.referToTextField.Frame.Size.Width, 320f),
				"ReferToViewCell",
				UIPopoverArrowDirection.Any,
				(UITableViewCell cell) =>
				{
					if (cell is ReferToViewCell)
					{
						this.AreaViewModel.SelectedReferral = ((ReferToViewCell)cell).Item;

						referToPopoverController.DismissPopover();
					}
				}
			);

			#endregion

			#region Quality Level

			this.performanceTextField.ShouldBeginEditing += (UITextField textField) =>
			{
				qualityLevelPopoverController.ShowPopover(textField);
				return false;
			};

			qualityLevelPopoverController = new PopoverViewController<QualityLevelUnit>(
				this.AreaViewModel.QualityLevels,
				new CGSize(this.performanceTextField.Frame.Size.Width, 320f),
				"QualityLevelViewCell",
				UIPopoverArrowDirection.Any,
				(UITableViewCell cell) =>
				{
					if (cell is QualityLevelViewCell)
					{
						this.AreaViewModel.SelectedQualityLevel = ((QualityLevelViewCell)cell).Item;

						qualityLevelPopoverController.DismissPopover();
					}
				}
			);

			#endregion

			#endregion

			#region Camera/Photo Library authorizations

			await AuthorizeCameraUse();
			AuthorizePhotoLibraryUse();

			#endregion
		}

		public override void RegisterBindingsLocal()
		{
			if (AreaViewModel == null)
			{
				return;
			}

			this.AreaViewModel.HandleCameraCallback += AreaViewModel_HandleCameraCallback;

			this.takePictureButton.SetCommand("TouchUpInside", this.AreaViewModel.TakePhotoCommand);
			this.loadPictureButton.SetCommand("TouchUpInside", this.AreaViewModel.LoadPhotoCommand);

			KeepBindingInMemoryLocal(this.SetBinding(() => AreaViewModel.BrandName, () => brandTextField.Text, BindingMode.TwoWay));
			KeepBindingInMemoryLocal(this.SetBinding(() => AreaViewModel.SelectedReferral.Text, () => referToTextField.Text, BindingMode.OneWay));
			KeepBindingInMemoryLocal(this.SetBinding(() => AreaViewModel.SelectedQualityLevel.Text, () => performanceTextField.Text, BindingMode.OneWay));
			KeepBindingInMemoryLocal(this.SetBinding(() => AreaViewModel.Description, () => descriptionTextView.Text, BindingMode.TwoWay));

			KeepBindingInMemoryLocal(this.SetBinding(() => AreaViewModel.BrandErrorMessage, () => brandMessageLabel.Text, BindingMode.OneWay));
			KeepBindingInMemoryLocal(this.SetBinding(() => AreaViewModel.ReferToErrorMessage, () => referToMessageLabel.Text, BindingMode.OneWay));
			KeepBindingInMemoryLocal(this.SetBinding(() => AreaViewModel.QualityLevelErrorMessage, () => performanceMessageLabel.Text, BindingMode.OneWay));
			KeepBindingInMemoryLocal(this.SetBinding(() => AreaViewModel.DescriptionErrorMessage, () => descriptionMessageLabel.Text, BindingMode.OneWay));

			KeepBindingInMemoryLocal(this.SetBinding(() => AreaViewModel.PhotoFilename).WhenSourceChanges(() =>
			{
				if (String.IsNullOrWhiteSpace(AreaViewModel.PhotoFilename))
				{
					ShowPicturePreview(null);
					return;
				}

				UIImage image = UIImage.FromFile(ImagePathiOS(AreaViewModel.PhotoFilename));
				ShowPicturePreview(image);
			}));

		}

		private string ImagePathiOS(string fullPath)
		{
			string filename = Path.GetFileName(fullPath);
			return Path.Combine(AreaViewModel.MediaPath, filename);
		}

		private void AreaViewModel_HandleCameraCallback(int command)
		{
			switch (command)
			{
				case (int)eCommand.TakePhoto:
					TakePhoto();
					break;
				case (int)eCommand.LoadPhoto:
					LoadPhoto();
					break;
			}
		}

		private void TakePhoto()
		{
			bool hasCamera = UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.Camera);

			UIImagePickerController picker = new UIImagePickerController();
			picker.WeakDelegate = this;
			picker.SourceType = (hasCamera) ? UIImagePickerControllerSourceType.Camera : UIImagePickerControllerSourceType.PhotoLibrary;
			picker.ModalPresentationStyle = UIModalPresentationStyle.Popover;

			if (this.PopoverPresentationController != null)
			{
				this.DismissViewController(true, null);
			}

			this.PresentViewController(picker, true, () =>
			{
				picker.PreferredContentSize = new CGSize(460f, 460f);
			});

			UIPopoverPresentationController popController = picker.PopoverPresentationController;
			popController.PermittedArrowDirections = UIPopoverArrowDirection.Any;
			popController.SourceView = this.View;
			popController.SourceRect = this.takePictureButton.Frame;
		}

		private void LoadPhoto()
		{
			UIImagePickerController picker = new UIImagePickerController();
			picker.WeakDelegate = this;
			picker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
			picker.ModalPresentationStyle = UIModalPresentationStyle.Popover;

			if (this.PopoverPresentationController != null)
			{
				this.DismissViewController(true, null);
			}

			this.PresentViewController(picker, true, () =>
			{
				picker.PreferredContentSize = new CGSize(460f, 460f);
			});

			UIPopoverPresentationController popController = picker.PopoverPresentationController;
			popController.PermittedArrowDirections = UIPopoverArrowDirection.Any;
			popController.SourceView = this.View;
			popController.SourceRect = this.loadPictureButton.Frame;

		}

		#region UIImagePickerControllerDelegate

		[Export("imagePickerController:didFinishPickingMediaWithInfo:")]
		public void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
		{
			this.DismissViewController(true, null);

			UIImage image = info.ObjectForKey(UIImagePickerController.OriginalImage) as UIImage;

			SavePicture(image, (picker.SourceType == UIImagePickerControllerSourceType.Camera));
		}

		[Export("imagePickerControllerDidCancel:")]
		public void Canceled(UIImagePickerController picker)
		{
			this.DismissViewController(true, null);
		}

		#endregion


		private async void SavePicture(UIImage picture, bool saveToLibrary)
		{
			var FileStorage = Services.Providers.FileStorageProvider.Instance;

			var mediaPath = AreaViewModel.MediaPath;

			var photoFilename = FileStorage.GetUniqueFilenameInFolder(mediaPath, "jpg");

			using (NSData imageData = picture.AsPNG())
			{
				Byte[] myByteArray = new Byte[imageData.Length];

				System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, myByteArray, 0, Convert.ToInt32(imageData.Length));

				try
				{
					await FileStorage.SaveBinaryAsync(photoFilename, myByteArray);
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.Message);
				}
			}

			UIImage normalizedPicture = ScaleAndRotateImage(picture, picture.Orientation);

			normalizedPicture.SaveToPhotosAlbum((UIImage image, NSError error) =>
			{
				if (error != null)
				{
					Debug.WriteLine(error.LocalizedDescription);
				}
			});

			ShowPicturePreview(normalizedPicture);

			AreaViewModel.PhotoFilename = photoFilename;
		}

		private UIImage ShowPicturePreview(UIImage picture)
		{
			if (picture == null)
			{
				this.pictureImageView.Image = new UIImage();
			}
			else {
				this.pictureImageView.Image = picture;
			}
			return picture;
		}

		private UIImage ScaleAndRotateImage(UIImage imageIn, UIImageOrientation orIn)
		{
			int kMaxResolution = 2048;

			CGImage imgRef = imageIn.CGImage;
			float width = imgRef.Width;
			float height = imgRef.Height;
			CGAffineTransform transform = CGAffineTransform.MakeIdentity();
			RectangleF bounds = new RectangleF(0, 0, width, height);

			if (width > kMaxResolution || height > kMaxResolution)
			{
				float ratio = width / height;

				if (ratio > 1)
				{
					bounds.Width = kMaxResolution;
					bounds.Height = bounds.Width / ratio;
				}
				else
				{
					bounds.Height = kMaxResolution;
					bounds.Width = bounds.Height * ratio;
				}
			}

			float scaleRatio = bounds.Width / width;
			SizeF imageSize = new SizeF(width, height);
			UIImageOrientation orient = orIn;
			float boundHeight;

			switch (orient)
			{
				case UIImageOrientation.Up:                                        //EXIF = 1
					transform = CGAffineTransform.MakeIdentity();
					break;

				case UIImageOrientation.UpMirrored:                                //EXIF = 2
					transform = CGAffineTransform.MakeTranslation(imageSize.Width, 0f);
					transform = CGAffineTransform.MakeScale(-1.0f, 1.0f);
					break;

				case UIImageOrientation.Down:                                      //EXIF = 3
					transform = CGAffineTransform.MakeTranslation(imageSize.Width, imageSize.Height);
					transform = CGAffineTransform.Rotate(transform, (float)Math.PI);
					break;

				case UIImageOrientation.DownMirrored:                              //EXIF = 4
					transform = CGAffineTransform.MakeTranslation(0f, imageSize.Height);
					transform = CGAffineTransform.MakeScale(1.0f, -1.0f);
					break;

				case UIImageOrientation.LeftMirrored:                              //EXIF = 5
					boundHeight = bounds.Height;
					bounds.Height = bounds.Width;
					bounds.Width = boundHeight;
					transform = CGAffineTransform.MakeTranslation(imageSize.Height, imageSize.Width);
					transform = CGAffineTransform.MakeScale(-1.0f, 1.0f);
					transform = CGAffineTransform.Rotate(transform, 3.0f * (float)Math.PI / 2.0f);
					break;

				case UIImageOrientation.Left:                                      //EXIF = 6
					boundHeight = bounds.Height;
					bounds.Height = bounds.Width;
					bounds.Width = boundHeight;
					transform = CGAffineTransform.MakeTranslation(0.0f, imageSize.Width);
					transform = CGAffineTransform.Rotate(transform, 3.0f * (float)Math.PI / 2.0f);
					break;

				case UIImageOrientation.RightMirrored:                             //EXIF = 7
					boundHeight = bounds.Height;
					bounds.Height = bounds.Width;
					bounds.Width = boundHeight;
					transform = CGAffineTransform.MakeScale(-1.0f, 1.0f);
					transform = CGAffineTransform.Rotate(transform, (float)Math.PI / 2.0f);
					break;

				case UIImageOrientation.Right:                                     //EXIF = 8
					boundHeight = bounds.Height;
					bounds.Height = bounds.Width;
					bounds.Width = boundHeight;
					transform = CGAffineTransform.MakeTranslation(imageSize.Height, 0.0f);
					transform = CGAffineTransform.Rotate(transform, (float)Math.PI / 2.0f);
					break;

				default:
					throw new Exception("Invalid image orientation");
			}

			UIGraphics.BeginImageContext(bounds.Size);

			CGContext context = UIGraphics.GetCurrentContext();

			if (orient == UIImageOrientation.Right || orient == UIImageOrientation.Left)
			{
				context.ScaleCTM(-scaleRatio, scaleRatio);
				context.TranslateCTM(-height, 0);
			}
			else
			{
				context.ScaleCTM(scaleRatio, -scaleRatio);
				context.TranslateCTM(0, -height);
			}

			context.ConcatCTM(transform);
			context.DrawImage(new RectangleF(0, 0, width, height), imgRef);

			UIImage imageCopy = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();

			return imageCopy;
		}
	}

}
