﻿using System;
using System.Windows;
using System.Windows.Media;

namespace BotRetreat.ScriptEditor.Wpf
{
    public class DrawingControl : FrameworkElement
    {
        private VisualCollection visuals;
        private DrawingVisual visual;

        public DrawingControl()
        {
            visual = new DrawingVisual();
            visuals = new VisualCollection(this);
            visuals.Add(visual);
        }

        public DrawingContext GetContext()
        {
            return visual.RenderOpen();
        }

        protected override int VisualChildrenCount
        {
            get { return visuals.Count; }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= visuals.Count)
                throw new ArgumentOutOfRangeException();
            return visuals[index];
        }
    }
}