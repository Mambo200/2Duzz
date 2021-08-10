using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace _2Duzz.Tools
{
    public class WheelControlScollViewer : ScrollViewer
    {
        /// <summary>Main Window. Delete later?</summary>
        public IStatusBar MainW { get; set; }

        /// <summary>Tells if wheel button is pressed</summary>
        public bool MiddleButtonPressed { get; protected set; }
        /// <summary>Position of curson when button was pressed</summary>
        private Point OnClickedPosition { get; set; }
        public double MoveMultiplicator { get; set; }
        public double DeadZone { get; set; }

        public WheelControlScollViewer()
            : base()
        {
            MoveMultiplicator = 1;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.MiddleButton == MouseButtonState.Pressed
                && !MiddleButtonPressed)
            {
                OnMiddleMouseDown(e);
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.MiddleButton == MouseButtonState.Released
                && MiddleButtonPressed)
            {
                OnMiddleMouseUp(e);
            }
        }

        protected virtual void OnMiddleMouseDown(MouseButtonEventArgs e)
        {
            MiddleButtonPressed = true;
            OnClickedPosition = e.GetPosition(this);
            Mouse.OverrideCursor = Cursors.ScrollAll;
        }

        protected virtual void OnMiddleMouseUp(MouseButtonEventArgs e)
        {
            MiddleButtonPressed = false;
            Mouse.OverrideCursor = null;
        }

        public void CallOnMouseMove(MouseEventArgs e) => OnMouseMove(e);
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!MiddleButtonPressed) return;

            // Check Position
            Point currentPosition = e.GetPosition(this);

            // Get difference.
            double offsetX = (OnClickedPosition.X - currentPosition.X);
            double offsetY = (OnClickedPosition.Y - currentPosition.Y);

            // Apply Deadzone
            // Check if deadzone is between positive and negative offset value. If so do not move --> offset = 0
            if (IsBetween(offsetX, DeadZone))
            {
                // deadzone is not between positive and negative offset value. Check sign of offset and ether add or substract deadzone from offset.
                if (offsetX > 0)
                    offsetX -= DeadZone;
                else
                    offsetX += DeadZone;
            }
            else
                // Deadzone is between positive and negative offset value. Set offset to 0 so ScrollViewer do not move
                offsetX = 0;

            // Check if deadzone is between positive and negative offset value. If so do not move --> offset = 0
            if (IsBetween(offsetY, DeadZone))
            {
                // deadzone is not between positive and negative offset value. Check sign of offset and ether add or substract deadzone from offset.
                if (offsetY > 0)
                    offsetY -= DeadZone;
                else
                    offsetY += DeadZone;
            }
            else
                // deadzone is not between positive and negative offset value. Check sign of offset and ether add or substract deadzone from offset.
                offsetY = 0;

            // Move Scrollbar
            ScrollToVerticalOffset(VerticalOffset - (offsetY * MoveMultiplicator));
            ScrollToHorizontalOffset(HorizontalOffset - (offsetX * MoveMultiplicator));

            SetCursorFromOffset(offsetX, offsetY);
        }

        /// <summary>
        /// Set Scroll cursor from Offset
        /// </summary>
        /// <param name="_x">Offset x</param>
        /// <param name="_y">Offset y</param>
        private void SetCursorFromOffset(double _x, double _y)
        {
            // center
            if (_x == 0 && _y == 0)
                Mouse.OverrideCursor = Cursors.ScrollAll;

            // DIAGONAL
            // top left (x+,y+)
            else if (_y > 0 && _x > 0
                && IsBetween(_y, GetCursorChangeValue(_x)) && IsBetween(_x, GetCursorChangeValue(_y)))
            {
                Mouse.OverrideCursor = Cursors.ScrollNW;
            }
            // top right (x-,y+)
            else if (_y > 0 && _x < 0
               && IsBetween(_y, GetCursorChangeValue(_x)) && IsBetween(_x, GetCursorChangeValue(_y)))
            {
                Mouse.OverrideCursor = Cursors.ScrollNE;
            }
            // down left (x+,y-)
            else if (_y < 0 && _x > 0
               && IsBetween(_y, GetCursorChangeValue(_x)) && IsBetween(_x, GetCursorChangeValue(_y)))
            {
                Mouse.OverrideCursor = Cursors.ScrollSW;
            }
            // down right (x-,y-)
            else if (_y < 0 && _x < 0
               && IsBetween(_y, GetCursorChangeValue(_x)) && IsBetween(_x, GetCursorChangeValue(_y)))
            {
                Mouse.OverrideCursor = Cursors.ScrollSE;
            }

            // NOT DIAGONAL
            // left (x+)
            else if (_x > 0 && IsBetween(GetCursorChangeValue(_x), _y))
            {
                Mouse.OverrideCursor = Cursors.ScrollW;
            }
            // right (x-)
            else if (_x < 0 && IsBetween(GetCursorChangeValue(_x), _y))
            {
                Mouse.OverrideCursor = Cursors.ScrollE;
            }
            // top (y+)
            else if (_y > 0 && IsBetween(GetCursorChangeValue(_y), _x))
            {
                Mouse.OverrideCursor = Cursors.ScrollN;
            }
            // down (y-)
            else if (_y < 0 && IsBetween(GetCursorChangeValue(_y), _x))
            {
                Mouse.OverrideCursor = Cursors.ScrollS;
            }
        }

        private double GetCursorChangeValue(double _other)
        {
            return _other/4;
            //return 0.1;
        }

        /// <summary>
        /// Check if number is between two numbers
        /// </summary>
        /// <param name="_x">Range 1</param>
        /// <param name="_y">Range 2</param>
        /// <param name="_value">Value to check in Range</param>
        /// <returns></returns>
        private bool IsBetween(double _x, double _y, double _value)
        {
            if(_x > _y)
            {
                return _value > _y && _value < _x;
            }
            else
            {
                return _value > _x && _value < _y;
            }
        }

        /// <summary>
        /// check if number is between two numbers which only differ by sign
        /// </summary>
        /// <param name="_x">positive/negative rane</param>
        /// <param name="_value">Value to check in Range</param>
        /// <returns></returns>
        private bool IsBetween(double _x, double _value)
        {
            // we make an absolute of value _x because we do not know if value _x is positive or negative.
            double positiveX = Math.Abs(_x);
            double negativeX = positiveX * -1;

            return _value > negativeX && _value < positiveX;
        }
    }
}
