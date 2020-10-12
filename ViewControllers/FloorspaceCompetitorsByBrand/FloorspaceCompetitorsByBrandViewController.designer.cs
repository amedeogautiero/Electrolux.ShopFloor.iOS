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
    [Register ("FloorspaceCompetitorsByBrandViewController")]
    partial class FloorspaceCompetitorsByBrandViewController
    {
        [Outlet]
        UIKit.UIButton backToFiltersButton { get; set; }


        [Outlet]
        UIKit.UILabel dataSavedLabel { get; set; }


        [Outlet]
        UIKit.UIView DetailsView { get; set; }


        [Outlet]
        UIKit.UILabel genericErrorMessageLabel { get; set; }


        [Outlet]
        UIKit.UICollectionView mainContentCollectionView { get; set; }


        [Action ("backToFiltersAction:")]
        partial void backToFiltersAction (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
        }
    }
}