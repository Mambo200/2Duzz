using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace _2Duzz.Tools
{
    public class HoverOverDifferentImageGrid : Grid
    {
        /// <summary>Invalid Point Value</summary>
        public Point InvalidPoint { get => new Point(-1, -1); }

        public bool ValidPosition { get => LatestImagePosition.X >= 0 && LatestImagePosition.Y >= 0; }
        /// <summary>Get the latest image position chosen</summary>
        public Point LatestImagePosition { get; protected set; }

        #region Functions overwritten
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // Get Position of Mouse
            PointHitTestResult result = ItemAtCursor(e, out Point hit);

            if(result == null)
            {
                return;
            }

            // check if Hit was valid
            Image img = result.VisualHit as Image;
            if (img == null)
                return;

            // Try get image size
            Point size = GetImageSize(img);
            if (size == InvalidPoint)
                return;

            Point temp = CalculateImagePosition(hit, size);

            if(SwitchImage != null && temp != LatestImagePosition)
                SwitchImage(this, e, LatestImagePosition, temp);

            LatestImagePosition = temp;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Get Position of Mouse
            PointHitTestResult result = ItemAtCursor(e, out Point hit);

            // check if Hit was valid
            Image img = result.VisualHit as Image;
            if (img == null)
                return;

            // Try get image size
            Point size = GetImageSize(img);
            if (size == InvalidPoint)
                return;

            Point temp = CalculateImagePosition(hit, size);

            if (OnClickImage != null)
                OnClickImage(this, e, temp);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            LatestImagePosition = InvalidPoint;
        }
        #endregion

        private Point GetImageSize(Image _img)
        {
            DrawingImage dImage = _img.Source as DrawingImage;
            if (dImage == null)
                return InvalidPoint;

            DrawingGroup dGroup = dImage.Drawing as DrawingGroup;
            if (dGroup == null)
                return InvalidPoint;

            // We are here, that means we pointed to an Image with a DrawingGroup as DrawimgImage.Drawing and a DrawingImage as Image.Source
            // because we hovered over something, DrawingGroup.Children must at least contain one children.
            ImageDrawing iDrawing = dGroup.Children[0] as ImageDrawing;
            if (iDrawing == null)
                return InvalidPoint;


            return new Point(iDrawing.Rect.Width, iDrawing.Rect.Height);
        }

        private PointHitTestResult ItemAtCursor(MouseEventArgs _mouseEvent, out Point _hit)
        {
            _hit = _mouseEvent.GetPosition(this);
            return VisualTreeHelper.HitTest(this, _hit) as PointHitTestResult;
        }

        private Point CalculateImagePosition(Point _hit, Point _size)
        {
            int x, y = 0;

            x = (int)(_hit.X / _size.X);
            y = (int)(_hit.Y / _size.Y);

            return new Point(x, y);
        }

        public event Delegates.SwitchImagesEventHandler SwitchImage;
        public event Delegates.OnClickImageEventHandler OnClickImage;
    }
}
