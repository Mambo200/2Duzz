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
            if(e.MiddleButton == MouseButtonState.Pressed
                && !MiddleButtonPressed)
            {
                OnMiddleMouseDown(e);
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            if(e.MiddleButton == MouseButtonState.Released
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

            // Get difference. Substract Deadzone
            // TODO: This now only works in positive direction. Shoulkd also work with negative
            double offsetX = (OnClickedPosition.X - currentPosition.X) - DeadZone;
            double offsetY = (OnClickedPosition.Y - currentPosition.Y) - DeadZone;

            // Cap at 0
            offsetX = Math.Max(0, offsetX);
            offsetY = Math.Max(0, offsetY);

            // Move Scrollbar
            ScrollToVerticalOffset(VerticalOffset + (offsetY * MoveMultiplicator));
            ScrollToHorizontalOffset(HorizontalOffset + (offsetX * MoveMultiplicator));

            main.ChangeStatusBar(offsetX);
        }
    }
}
