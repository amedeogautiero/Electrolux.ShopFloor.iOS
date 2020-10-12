// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Electrolux.ShopFloor.iOS
{
    [Register ("ReportEditorViewController")]
    partial class ReportEditorViewController
    {
        [Outlet]
        UIKit.UIBarButtonItem analyticsBarButtonItem { get; set; }


        [Outlet]
        UIKit.UIButton cancelButton { get; set; }


        [Outlet]
        UIKit.UIView commentView { get; set; }


        [Outlet]
        UIKit.UIView competitorsActivitiesView { get; set; }


        [Outlet]
        UIKit.UIButton createButton { get; set; }


        [Outlet]
        UIKit.UILabel dateOfLastReportLabel { get; set; }


        [Outlet]
        UIKit.UIView electroluxActivitiesView { get; set; }

        [Outlet]
        UIKit.UIView floorspaceCompetitorsByBrandView { get; set; }

        [Outlet]
        UIKit.UIView floorspaceCompetitorsView { get; set; }


        [Outlet]
        UIKit.UIView floorspaceElectroluxView { get; set; }


        [Outlet]
        UIKit.UIView floorspaceProductGroupView { get; set; }


        [Outlet]
        UIKit.UIView generalInfoView { get; set; }


        [Outlet]
        UIKit.UIView kilometersView { get; set; }


        [Outlet]
        UIKit.UIView kitchenView { get; set; }


        [Outlet]
        UIKit.UITableView menuTableView { get; set; }


        [Outlet]
        UIKit.UIView photosView { get; set; }


        [Outlet]
        UIKit.UIView posmActivitiesView { get; set; }


        [Outlet]
        UIKit.UIView productFeedbackView { get; set; }


        [Outlet]
        UIKit.UIButton saveButton { get; set; }


        [Outlet]
        UIKit.UIButton saveOfficialDataButton { get; set; }


        [Outlet]
        UIKit.UIView selloutView { get; set; }


        [Outlet]
        UIKit.UIView shopActivitiesView { get; set; }


        [Outlet]
        UIKit.UIView shopInformationView { get; set; }


        [Outlet]
        UIKit.UIView summaryView { get; set; }


        [Outlet]
        UIKit.UIView trainingView { get; set; }


        [Action ("analyticsButtonAction:")]
        partial void analyticsButtonAction (UIKit.UIBarButtonItem sender);


        [Action ("homeBarButtonAction:")]
        partial void homeBarButtonAction (UIKit.UIBarButtonItem sender);

        void ReleaseDesignerOutlets ()
        {
        }
    }
}