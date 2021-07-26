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
            Point size = GetImageSpriteSize(img);
            if (size == InvalidPoint)
                return;

            Point temp = CalculateImagePosition(hit, size);

            // We have to check if image would be out of bounce because if left mouse hold down and go right out of Image, it would insert the image at an invalid position.
            bool isValid = CheckIfPositionIsValid(temp, size);
            if (!isValid)
                return;

            if (SwitchImage != null && temp != LatestImagePosition)
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

            // Try get image sprite size
            Point size = GetImageSpriteSize(img);
            if (size == InvalidPoint)
                return;

            Point temp = CalculateImagePosition(hit, size);
            bool isValid = CheckIfPositionIsValid(temp, size);
            if (!isValid)
                return;

            if (OnClickImage != null)
                OnClickImage(this, e, temp);

            LatestImagePosition = temp;
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            LatestImagePosition = InvalidPoint;
        }
        #endregion

        /// <summary>
        /// This checks if the position is a valid position between 0 and the maximum image count of this image
        /// </summary>
        /// <param name="_indexPosition">index position</param>
        /// <param name="_spriteSize">size of sprites</param>
        /// <returns>true if the given indexposition is valid; otherwise false</returns>
        private bool CheckIfPositionIsValid(Point _indexPosition, Point _spriteSize)
        {
            Point maxImageCount = GetMaxImageCount(_spriteSize);
            return _indexPosition.X >= 0
                && _indexPosition.Y >= 0
                && _indexPosition.X < maxImageCount.X
                && _indexPosition.Y < maxImageCount.Y;
        }

        /// <summary>
        /// Get the maximum image count of this Grid
        /// </summary>
        /// <param name="_spriteSize">sprite size of images int Grid</param>
        /// <returns>returns the sprite size</returns>
        private Point GetMaxImageCount(Point _spriteSize)
        {
            int x, y = 0;

            x = (int)(Width / _spriteSize.X);
            y = (int)(Height / _spriteSize.Y);

            return new Point(x, y);
        }

        /// <summary>
        /// Get Sprite size of Images
        /// </summary>
        /// <param name="_img">Image hit</param>
        /// <returns></returns>
        private Point GetImageSpriteSize(Image _img)
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

        /// <summary>
        /// Check if mouse hits an <see cref="Image"/>.
        /// </summary>
        /// <param name="_mouseEvent">Mouseevent for mouse position</param>
        /// <param name="_hit">Posiion hit relative to this <see cref="HoverOverDifferentImageGrid"/></param>.
        /// <returns>returns the hit item</returns>
        private PointHitTestResult ItemAtCursor(MouseEventArgs _mouseEvent, out Point _hit)
        {
            _hit = _mouseEvent.GetPosition(this);
            return VisualTreeHelper.HitTest(this, _hit) as PointHitTestResult;
        }

        /// <summary>
        /// Calculates the image index where the mouse is currently hovering.
        /// </summary>
        /// <param name="_hit">hit position</param>
        /// <param name="_size">sprite size of images</param>
        /// <returns>image position as index</returns>
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
