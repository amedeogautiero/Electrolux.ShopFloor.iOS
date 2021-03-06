// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using Electrolux.ShopFloor.iOS.ViewControllers;
using Electrolux.ShopFloor.Mvvm.ViewModels;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Electrolux.ShopFloor.Mvvm.ViewModels.Units;
using Electrolux.ShopFloor.Mvvm.ViewModels.EditingAreas;
using Electrolux.ShopFloor.Mvvm.Infrastructure.Types;
using GalaSoft.MvvmLight.Helpers;
using System.Collections.ObjectModel;
using Electrolux.ShopFloor.Middleware.Model;
using Electrolux.ShopFloor.Middleware.Manager;

namespace Electrolux.ShopFloor.iOS
{
	public partial class ReportEditorViewController : BaseViewController<ReportEditorViewModel>, IUITableViewDelegate, IBindableViewController<MenuUnit>
	{
		private int bottomBarMode;
		private bool isSaving;
        private bool abandonChanges;
		private ReportEditorViewSource<MenuUnit> tableViewSource;

        public bool firstAreaSelection;

		public UITableView MenuTableView { get { return menuTableView; } }
		public UIView GeneralInfoView { get { return this.generalInfoView; } }
		public UIView CompetitorsActivitiesView { get { return this.competitorsActivitiesView; } }
		public UIView FloorspaceCompetitorsView { get { return this.floorspaceCompetitorsView; } }
		public UIView FloorspaceElectroluxView { get { return this.floorspaceElectroluxView; } }
		public UIView SelloutView { get { return this.selloutView; } }
		public UIView ShopActivitiesView { get { return this.shopActivitiesView; } }
		public UIView ShopInformationView { get { return this.shopInformationView; } }
		public UIView TrainingView { get { return this.trainingView; } }
		public UIView FloorspaceProductGroupView { get { return this.floorspaceProductGroupView; } }
		public UIView ElectroluxActivitiesView { get { return this.electroluxActivitiesView; } }
		public UIView KitchenView { get { return this.kitchenView; } }
		public UIView PosmActivitiesView { get { return this.posmActivitiesView; } }
		public UIView ProductFeedbackView { get { return this.productFeedbackView; } }
		public UIView SummaryView { get { return this.summaryView; } }
		public UIView KilometersView { get { return this.kilometersView; } }
		public UIView PhotosView { get { return this.photosView; } }
		public UIView CommentView { get { return this.commentView; } }
		public UIView FloorspaceCompetitorsByBrandView { get { return this.floorspaceCompetitorsByBrandView; } }
		
		public int BottomBarMode
		{
			get { return this.bottomBarMode; }
			set
			{
				this.bottomBarMode = value;
				switch ((BottomBarModes)this.bottomBarMode)
				{
					case BottomBarModes.Normal:
						this.createButton.Hidden = true;
						this.saveButton.Hidden = false;
						this.cancelButton.Hidden = true;
                        this.saveOfficialDataButton.Hidden = true;

                        break;
					case BottomBarModes.AddNew:
						this.createButton.Hidden = false;
						this.saveButton.Hidden = true;
						this.cancelButton.Hidden = true;
                        this.saveOfficialDataButton.Hidden = true;

                        break;
					case BottomBarModes.SaveOrAbort:
						this.createButton.Hidden = true;
						this.saveButton.Hidden = false;
						this.cancelButton.Hidden = false;
                        this.saveOfficialDataButton.Hidden = true;

                        break;
					case BottomBarModes.NewOrSave:
						this.createButton.Hidden = false;
						this.saveButton.Hidden = true;
						this.cancelButton.Hidden = true;
                        this.saveOfficialDataButton.Hidden = false;

                        break;
					case BottomBarModes.NoActions:
						this.createButton.Hidden = true;
						this.saveButton.Hidden = true;
						this.cancelButton.Hidden = true;
                        this.saveOfficialDataButton.Hidden = true;

                        break;
					case BottomBarModes.OptionalDataAndSave:
						this.createButton.Hidden = true;
						this.saveButton.Hidden = true;
						this.cancelButton.Hidden = true;
                        this.saveOfficialDataButton.Hidden = false;

                        break;
				}

                this.saveButton.Enabled = ViewModel.SaveCommand.CanExecute(null);
                this.saveOfficialDataButton.Enabled = ViewModel.SaveOfficialDataCommand.CanExecute(null);
				this.createButton.Enabled = ViewModel.AddNewCommand.CanExecute(null);
			}
		}

		public bool IsSaving
		{
			get { return this.isSaving; }
			private set
			{
				this.isSaving = value;
				if (this.isSaving)
				{
					this.StartAsync();
				}
				else {
					if (tableViewSource != null)
					{
						if (tableViewSource.SelectedController != null)
						{
							if (tableViewSource.SelectedController.GetType().GetProperty("PublicTableView") != null)
							{
								UITableView tableView = tableViewSource.SelectedController.GetType().GetProperty("PublicTableView").GetValue(tableViewSource.SelectedController, null) as UITableView;
								tableView.ReloadData();
							}
							else {
								tableViewSource.SelectedController.View.EndEditing(true);
							}
						}
						menuTableView.ReloadData();
					}
					this.EndAsync();
				}
			}
		}

        public bool AbandonChanges
        {
            get { return this.abandonChanges; }
            set 
            {
                this.abandonChanges = value;
                this.IsDialogVisible = this.abandonChanges;
            }
        }

		public ReportEditorViewSource<MenuUnit> TableViewSource
		{
			get
			{
				//var itemName = TranslatorManager.GetInstance().GetString("Floorspace - Product Group - Lite");
				//var itemToRemove = ViewModel.Menus.FirstOrDefault((arg) => arg.Menu.Name.Equals(itemName));
				//ViewModel.Menus.Remove(itemToRemove);
				this.tableViewSource = new ReportEditorViewSource<MenuUnit>(ViewModel.Menus, menuTableView, "ReportEditorViewCell", this);
				return this.tableViewSource;
			}
		}

		public ReportEditorViewController(IntPtr handle) : base(handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            firstAreaSelection = true;
            ViewModel.SelectArea(eSection.GeneralInfo);
        }

        public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			ViewModel.Menus.CollectionChanged += Menus_CollectionChanged;
			menuTableView.Source = this.TableViewSource;

			menuTableView.ReloadData();
		}

		public override void ViewWillAppear(bool animated)
		{
			this.NavigationController.SetNavigationBarHidden(false, false);
			this.ConfigureArea();
			base.ViewWillAppear(animated);
		}

		public override void ViewWillDisappear(bool animated)
		{
			this.NavigationController.SetNavigationBarHidden(true, true);
			base.ViewWillDisappear(animated);
		}

		public override void ViewDidDisappear(bool animated)
		{
			//this.ClearAllSources();
			base.ViewDidDisappear(animated);
		}

		public override void Translations()
		{
            this.saveButton.SetTitle(TranslatorManager.GetInstance().GetString("Save"), UIControlState.Normal);
            this.saveOfficialDataButton.SetTitle(TranslatorManager.GetInstance().GetString("Save"), UIControlState.Normal);
			this.createButton.SetTitle(TranslatorManager.GetInstance().GetString("New"), UIControlState.Normal);
			this.cancelButton.SetTitle(TranslatorManager.GetInstance().GetString("Cancel"), UIControlState.Normal);
			this.analyticsBarButtonItem.Title = TranslatorManager.GetInstance().GetString("Analytics");
		}

		public override void RegisterBindings()
		{
			this.KeepBindingInMemoryLocal(this.SetBinding(() => ViewModel.Header, () => this.dateOfLastReportLabel.Text));
			this.KeepBindingInMemoryLocal(this.SetBinding(() => ViewModel.BottomBarMode, () => this.BottomBarMode));
			this.KeepBindingInMemoryLocal(this.SetBinding(() => ViewModel.IsSaving, () => this.IsSaving));
            this.KeepBindingInMemoryLocal(this.SetBinding(() => ViewModel.AbandonChanges, () => this.AbandonChanges));
            this.KeepBindingInMemoryLocal(this.SetBinding(() => ViewModel.DialogMessageHeader, () => this.DialogMessageHeader));
            this.KeepBindingInMemoryLocal(this.SetBinding(() => ViewModel.DialogMessageBody, () => this.DialogMessageBody));
		}

		public override void RegisterCommands()
		{
            this.saveButton.SetCommand("TouchUpInside", this.ViewModel.SaveCommand);
            this.saveOfficialDataButton.SetCommand("TouchUpInside", this.ViewModel.SaveOfficialDataCommand);
			this.createButton.SetCommand("TouchUpInside", this.ViewModel.AddNewCommand);
			this.cancelButton.SetCommand("TouchUpInside", this.ViewModel.AbortCommand);
		}

		void Menus_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			foreach (var item in ViewModel.Menus)
			{
				item.PropertyChanged += Item_PropertyChanged;
			}
		}

		void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName != "IsSelected")
			{
				menuTableView.ReloadData();
			}
		}

		partial void analyticsButtonAction(UIKit.UIBarButtonItem sender)
		{
			if (this.ViewModel.AnalyticsCommand.CanExecute(null))
			{
				this.ViewModel.AnalyticsCommand.Execute(null);
			}
		}

        partial void homeBarButtonAction(UIKit.UIBarButtonItem sender)
        {
            if (this.ViewModel.NavigateHomeCommand.CanExecute(null))
            {
                this.ViewModel.NavigateHomeCommand.Execute(null);
            }
        }

		private void ClearAllSources()
		{
			foreach (UIViewController controller in this.ChildViewControllers)
			{
				if (controller.GetType().GetProperty("PublicTableView") != null)
				{
					UITableView tableView = controller.GetType().GetProperty("PublicTableView").GetValue(controller, null) as UITableView;
					tableView.Source = null;
				}
			}
		}
		public void BindTaskCell(UITableViewCell cell, MenuUnit item, NSIndexPath path)
		{

		}
	}

	#region UITableViewSource

	public class ReportEditorViewSource2<T> : BindableBaseTableViewSource<T>
	{
		private ReportEditorViewController ownerController { get { return this.OwnerController as ReportEditorViewController; } }
		private ReportEditorViewModel viewModel;
		private int selectedArea;
		//private int currentSelectedArea = -1;
		//private bool firstAreaSelection = true;

		private UIView newView;
		private BaseViewController<ReportEditorViewModel> selectedController;

		public BaseViewController<ReportEditorViewModel> SelectedController { get { return selectedController; } }

		public ReportEditorViewSource2(ObservableCollection<T> dataSource, UITableView tableView, string cellName, IBindableViewController<T> ownerController)
			: base (dataSource, tableView, cellName, ownerController)
		{
			this.viewModel = this.ownerController.ViewModel;
            this.ownerController.KeepBindingInMemoryLocal(new Binding<int, int>(viewModel, () => viewModel.SelectedArea, this, () => this.SelectedArea));
		}

        // remove when section will be implemented in iOS
		/*
        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
			var itemName = TranslatorManager.GetInstance().GetString("Floorspace - Product Group - Lite");
			if (viewModel.Menus[indexPath.Row].Name.Equals(itemName))
			{
				return 0;
			}
			return 50;
		}
		*/
        //

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = base.GetCell(tableView, indexPath);
			var cellBackgroundView = new UIView();
			cellBackgroundView.BackgroundColor = UIColor.FromRGBA(79f / 255f, 98f / 255f, 133f / 255f, 1f);
			cell.SelectedBackgroundView = cellBackgroundView;
			// remove when section will be implemented in iOS
			var itemName = TranslatorManager.GetInstance().GetString("Floorspace - Product Group - Lite");
			if (viewModel.Menus[indexPath.Row].Name.Equals(itemName))
            {
				cell.Hidden = true;
            }
            //
			return cell;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var selectedRow = indexPath.Row;
			if (viewModel.SelectedMenu.Menu.Section != viewModel.Menus[selectedRow].Menu.Section)
			{
                ownerController.StartAsync(ownerController.View.Superview);
                viewModel.SelectedMenu = viewModel.Menus[selectedRow];
			}
		}

		public int SelectedArea
		{
			get { return this.selectedArea; }
			set
			{
				ownerController.EndAsync(ownerController.View.Superview);

				this.selectedArea = value;
                if (!ownerController.firstAreaSelection)
				{
                    ownerController.MenuTableView.SelectRow(NSIndexPath.FromRowSection(ownerController.ViewModel.SelectedMenu.Menu.Order-1, 0), false, UITableViewScrollPosition.None);
					UIViewController[] filtered = this.FilterByID(ownerController.ChildViewControllers, ((eSection)this.selectedArea).ToString());
					if (filtered.Length > 0)
					{
						hideAllViews();

						selectedController = (BaseViewController<ReportEditorViewModel>)filtered[0];

						selectedController.ConfigureArea();
                         
						switch ((eSection)this.selectedArea)
						{
							case Middleware.Model.eSection.GeneralInfo:
								{
									newView = ownerController.GeneralInfoView;
									break;
								}
							case Middleware.Model.eSection.SellOutModels:
								{
									newView = ownerController.SelloutView;
									break;
								}
							case Middleware.Model.eSection.ShopInformation:
								{
									newView = ownerController.ShopInformationView;
									break;
								}
							case Middleware.Model.eSection.ShopActivities:
								{
									newView = ownerController.ShopActivitiesView;
									break;
								}
							case Middleware.Model.eSection.CompetitorsActivities:
								{
									newView = ownerController.CompetitorsActivitiesView;
									break;
								}
							case Middleware.Model.eSection.Training:
								{
									newView = ownerController.TrainingView;
									break;
								}
							case Middleware.Model.eSection.Kitchen:
								{
									newView = ownerController.KitchenView;
									break;
								}
							case Middleware.Model.eSection.FloorSpace_ElectroluxProducts:
								{
									newView = ownerController.FloorspaceElectroluxView;
                                    break;
								}
							case Middleware.Model.eSection.FloorSpace_CompetitorsProducts:
								{
									newView = ownerController.FloorspaceCompetitorsView;
									break;
								}
							case Middleware.Model.eSection.Floorspace_ProductGroup:
								{
									newView = ownerController.FloorspaceProductGroupView;
									break;
								}
							case Middleware.Model.eSection.ElectroluxActivities:
								{
									newView = ownerController.ElectroluxActivitiesView;
									break;
								}
							case Middleware.Model.eSection.POSMActivities:
								{
									newView = ownerController.PosmActivitiesView;
									break;
								}
							case Middleware.Model.eSection.ProductFeedback:
								{
									newView = ownerController.ProductFeedbackView;
									break;
								}
							case Middleware.Model.eSection.FullOverview:
								{
									newView = ownerController.SummaryView;
									break;
								}
							case Middleware.Model.eSection.Kilometres:
								{
									newView = ownerController.KilometersView;
									break;
								}
							case Middleware.Model.eSection.Photos:
								{
									newView = ownerController.PhotosView;
									break;
								}
							case Middleware.Model.eSection.Comments:
								{
									newView = ownerController.CommentView;
									break;
								}
							default:
								{
									newView = ownerController.GeneralInfoView;
									break;
								}
						}
						UIView.Transition(newView, 0.3, UIViewAnimationOptions.TransitionCrossDissolve, () =>
						{
							newView.Hidden = false;
						}, null);

						//if (selectedController.GetType().GetProperty("PublicTableView") != null)
						//{
						//	UITableView tableView = selectedController.GetType().GetProperty("PublicTableView").GetValue(selectedController, null) as UITableView;
						//	tableView.ReloadData();
						//}
						//controller.MenuTableView.ReloadData();
					}
				}
				else {
					// this is to force the binding to trigger again even if the new value is not different from the first time, i.e. = 0
					//this.selectedArea = -1;
                    if (ownerController.firstAreaSelection)
					{
                        this.selectedArea = -1;
						ownerController.StartAsync(ownerController.View.Superview);
					}
				}
                ownerController.firstAreaSelection = false;
			}
		}

		private UIViewController[] FilterByID(UIViewController[] input, string restorationID)
		{
			return input.Where(c => c.RestorationIdentifier == restorationID).ToArray();
		}

		private void hideAllViews()
		{
			ownerController.GeneralInfoView.Hidden =
							ownerController.SelloutView.Hidden =
								ownerController.ShopInformationView.Hidden =
									ownerController.ShopActivitiesView.Hidden =
										ownerController.ElectroluxActivitiesView.Hidden =
											ownerController.CompetitorsActivitiesView.Hidden =
												ownerController.PosmActivitiesView.Hidden =
													ownerController.ProductFeedbackView.Hidden =
														ownerController.TrainingView.Hidden =
															ownerController.PhotosView.Hidden =
																ownerController.FloorspaceElectroluxView.Hidden =
																	ownerController.FloorspaceCompetitorsView.Hidden =
																		ownerController.FloorspaceProductGroupView.Hidden =
																			ownerController.KitchenView.Hidden =
																				ownerController.CommentView.Hidden =
																					ownerController.KilometersView.Hidden =
																						ownerController.SummaryView.Hidden = true;
		}

	}

	public class ReportEditorViewSource<T> : BindableBaseTableViewSource<T>
	{
		private ReportEditorViewController ownerController { get { return this.OwnerController as ReportEditorViewController; } }
		private ReportEditorViewModel viewModel;
		private int selectedArea;
		//private int currentSelectedArea = -1;
		//private bool firstAreaSelection = true;

		private UIView newView;
		private BaseViewController<ReportEditorViewModel> selectedController;

		public BaseViewController<ReportEditorViewModel> SelectedController { get { return selectedController; } }

		public ReportEditorViewSource(ObservableCollection<T> dataSource, UITableView tableView, string cellName, IBindableViewController<T> ownerController)
			: base(dataSource, tableView, cellName, ownerController)
		{
			this.viewModel = this.ownerController.ViewModel;
			this.ownerController.KeepBindingInMemoryLocal(new Binding<int, int>(viewModel, () => viewModel.SelectedArea, this, () => this.SelectedArea));
		}

		// remove when section will be implemented in iOS
		/*
        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
			var itemName = TranslatorManager.GetInstance().GetString("Floorspace - Product Group - Lite");
			if (viewModel.Menus[indexPath.Row].Name.Equals(itemName))
			{
				return 0;
			}
			return 50;
		}
        */
		//

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = base.GetCell(tableView, indexPath);
			var cellBackgroundView = new UIView();
			cellBackgroundView.BackgroundColor = UIColor.FromRGBA(79f / 255f, 98f / 255f, 133f / 255f, 1f);
			cell.SelectedBackgroundView = cellBackgroundView;
			// remove when section will be implemented in iOS
			/*
			var itemName = TranslatorManager.GetInstance().GetString("Floorspace - Product Group - Lite");
			if (viewModel.Menus[indexPath.Row].Name.Equals(itemName))
            {
				cell.Hidden = true;
            }
            */
			//
			return cell;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var selectedRow = indexPath.Row;
			if (viewModel.SelectedMenu.Menu.Section != viewModel.Menus[selectedRow].Menu.Section)
			{
				ownerController.StartAsync(ownerController.View.Superview);
				viewModel.SelectedMenu = viewModel.Menus[selectedRow];
			}
		}

		public int SelectedArea
		{
			get { return this.selectedArea; }
			set
			{
				ownerController.EndAsync(ownerController.View.Superview);

				this.selectedArea = value;
				if (!ownerController.firstAreaSelection)
				{
					ownerController.MenuTableView.SelectRow(NSIndexPath.FromRowSection(ownerController.ViewModel.SelectedMenu.Menu.Order - 1, 0), false, UITableViewScrollPosition.None);
					UIViewController[] filtered = this.FilterByID(ownerController.ChildViewControllers, ((eSection)this.selectedArea).ToString());
					if (filtered.Length > 0)
					{
						hideAllViews();

						selectedController = (BaseViewController<ReportEditorViewModel>)filtered[0];

						selectedController.ConfigureArea();

						switch ((eSection)this.selectedArea)
						{
							case Middleware.Model.eSection.GeneralInfo:
								{
									newView = ownerController.GeneralInfoView;
									break;
								}
							case Middleware.Model.eSection.SellOutModels:
								{
									newView = ownerController.SelloutView;
									break;
								}
							case Middleware.Model.eSection.ShopInformation:
								{
									newView = ownerController.ShopInformationView;
									break;
								}
							case Middleware.Model.eSection.ShopActivities:
								{
									newView = ownerController.ShopActivitiesView;
									break;
								}
							case Middleware.Model.eSection.CompetitorsActivities:
								{
									newView = ownerController.CompetitorsActivitiesView;
									break;
								}
							case Middleware.Model.eSection.Training:
								{
									newView = ownerController.TrainingView;
									break;
								}
							case Middleware.Model.eSection.Kitchen:
								{
									newView = ownerController.KitchenView;
									break;
								}
							case Middleware.Model.eSection.FloorSpace_ElectroluxProducts:
								{
									newView = ownerController.FloorspaceElectroluxView;
									break;
								}
							case Middleware.Model.eSection.FloorSpace_CompetitorsProducts:
								{
									newView = ownerController.FloorspaceCompetitorsView;
									break;
								}
							case Middleware.Model.eSection.Floorspace_ProductGroup:
								{
									newView = ownerController.FloorspaceProductGroupView;
									break;
								}
							case Middleware.Model.eSection.ElectroluxActivities:
								{
									newView = ownerController.ElectroluxActivitiesView;
									break;
								}
							case Middleware.Model.eSection.POSMActivities:
								{
									newView = ownerController.PosmActivitiesView;
									break;
								}
							case Middleware.Model.eSection.ProductFeedback:
								{
									newView = ownerController.ProductFeedbackView;
									break;
								}
							case Middleware.Model.eSection.FullOverview:
								{
									newView = ownerController.SummaryView;
									break;
								}
							case Middleware.Model.eSection.Kilometres:
								{
									newView = ownerController.KilometersView;
									break;
								}
							case Middleware.Model.eSection.Photos:
								{
									newView = ownerController.PhotosView;
									break;
								}
							case Middleware.Model.eSection.Comments:
								{
									newView = ownerController.CommentView;
									break;
								}
							case Middleware.Model.eSection.FloorSpace_CompetitorsProducts_2020:
								{
									newView = ownerController.FloorspaceCompetitorsByBrandView;
									break;
								}
							default:
								{
									newView = ownerController.GeneralInfoView;
									break;
								}
						}
						UIView.Transition(newView, 0.3, UIViewAnimationOptions.TransitionCrossDissolve, () =>
						{
							newView.Hidden = false;
						}, null);

						//if (selectedController.GetType().GetProperty("PublicTableView") != null)
						//{
						//	UITableView tableView = selectedController.GetType().GetProperty("PublicTableView").GetValue(selectedController, null) as UITableView;
						//	tableView.ReloadData();
						//}
						//controller.MenuTableView.ReloadData();
					}
				}
				else
				{
					// this is to force the binding to trigger again even if the new value is not different from the first time, i.e. = 0
					//this.selectedArea = -1;
					if (ownerController.firstAreaSelection)
					{
						this.selectedArea = -1;
						ownerController.StartAsync(ownerController.View.Superview);
					}
				}
				ownerController.firstAreaSelection = false;
			}
		}

		private UIViewController[] FilterByID(UIViewController[] input, string restorationID)
		{
			return input.Where(c => c.RestorationIdentifier == restorationID).ToArray();
		}

		private void hideAllViews()
		{
			ownerController.GeneralInfoView.Hidden =
							ownerController.SelloutView.Hidden =
								ownerController.ShopInformationView.Hidden =
									ownerController.ShopActivitiesView.Hidden =
										ownerController.ElectroluxActivitiesView.Hidden =
											ownerController.CompetitorsActivitiesView.Hidden =
												ownerController.PosmActivitiesView.Hidden =
													ownerController.ProductFeedbackView.Hidden =
														ownerController.TrainingView.Hidden =
															ownerController.PhotosView.Hidden =
																ownerController.FloorspaceElectroluxView.Hidden =
																	ownerController.FloorspaceCompetitorsView.Hidden =
																		ownerController.FloorspaceProductGroupView.Hidden =
																			ownerController.KitchenView.Hidden =
																				ownerController.CommentView.Hidden =
																					ownerController.KilometersView.Hidden =
																						ownerController.SummaryView.Hidden =
																							ownerController.FloorspaceCompetitorsByBrandView.Hidden = true;
		}

	}

	#endregion
}
