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
        public MainWindow main { get; set; }

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
            //TODO: Change Cursor
        }

        protected virtual void OnMiddleMouseUp(MouseButtonEventArgs e)
        {
            MiddleButtonPressed = false;

            //TODO: Change Cursor
        }

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
            if (Math.Abs(offsetX) >= DeadZone
                && Math.Abs(offsetX) * -1 <= DeadZone)
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
            if (Math.Abs(offsetY) >= DeadZone
                && Math.Abs(offsetY) * -1 <= DeadZone)
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
            ScrollToVerticalOffset(VerticalOffset + (offsetY * MoveMultiplicator));
            ScrollToHorizontalOffset(HorizontalOffset + (offsetX * MoveMultiplicator));

            main.ChangeStatusBar($"{OnClickedPosition.X - currentPosition.X},{OnClickedPosition.Y - currentPosition.Y}");
        }
    }
}
