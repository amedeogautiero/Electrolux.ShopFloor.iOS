using System;
using UIKit;
using CoreGraphics;
using Foundation;
using System.Linq;

namespace Electrolux.ShopFloor.iOS
{
    public class MultpleLineLayout : UICollectionViewFlowLayout
    {
        private nint itemWidth;
        private nint itemHeight;

        public MultpleLineLayout()
        {
            itemWidth = 80;
            itemHeight = 80;
        }

        public override CGSize CollectionViewContentSize
        {
            get
            {
                nint xSize = this.CollectionView.NumberOfItemsInSection(0) * (itemWidth + 2); // the 2 is for spacing between cells.
                nint ySize = this.CollectionView.NumberOfSections() * (itemHeight + 2);
                return new CGSize(xSize, ySize);
            }
        }

        public override UICollectionViewLayoutAttributes LayoutAttributesForItem(NSIndexPath path)
        {
            UICollectionViewLayoutAttributes attributes = UICollectionViewLayoutAttributes.CreateForCell(path);
            nint xValue;
            attributes.Size = new CGSize(itemWidth, itemHeight);
            xValue = itemWidth / 2 + path.Row * (itemWidth + 2);
            nint yValue = itemHeight + path.Section * (itemHeight + 2);
            attributes.Center = new CGPoint(xValue, yValue);
            return attributes;
        }

        public override UICollectionViewLayoutAttributes[] LayoutAttributesForElementsInRect(CGRect rect)
        {
            nfloat minRow = (rect.X > 0) ? rect.X / (itemWidth + 2) : 0; // need to check because bounce gives negative values  for x.
            nfloat maxRow = rect.Width / (itemWidth + 2) + minRow;
            NSMutableArray<UICollectionViewLayoutAttributes> attributes = new NSMutableArray<UICollectionViewLayoutAttributes>();
            for (nint i = 0; i < this.CollectionView.NumberOfSections(); i++)
            {
                for (nint j = (nint)minRow; j < maxRow; j++)
                {
                    NSIndexPath indexPath = NSIndexPath.FromItemSection(j, i);
                    attributes.Add(this.LayoutAttributesForItem(indexPath));
                }
            }
            return attributes.Cast<UICollectionViewLayoutAttributes>().ToArray();
        }
    }
}