using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace EfficientControls
{
    public class AnimationContainer : Layout<View>
    {
        private double layoutWidth { get; set; }
        private double layoutHeight { get; set; }
        private View currentView { get; set; }

        public AnimationContainer()
        { 
        
        }

        public void SetView(View view)
        {
            StopAnimation();
            currentView = view;
            Children.Add(view);
            DeleteOldChildren();
        }

        public void SlideFromLeft(View view, uint milliseconds = 250, Easing easing = null)
        {
            StopAnimation();

            currentView = view;
            view.TranslationX = layoutWidth * (-1);
            Children.Insert(0, view);
            this.RaiseChild(view);
            Animation animation = new Animation(SetXTranslation, view.TranslationX, 0);
            animation.Commit(this, "move", 12, milliseconds, easing, FinishAnimation);
        }

        public void SlideFromRight(View view, uint milliseconds = 250, Easing easing = null)
        {
            StopAnimation();

            currentView = view;
            view.TranslationX = layoutWidth;
            Children.Insert(0, view);
            this.RaiseChild(view);
            Animation animation = new Animation(SetXTranslation, view.TranslationX, 0);
            animation.Commit(this, "move", 12, milliseconds, easing, FinishAnimation);
        }

        private void SetXTranslation(double xtrs)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                if (Children[i] == currentView)
                {
                    Children[i].TranslationX = xtrs;
                    break;
                }
            }
        }

        private void FinishAnimation(double arg1, bool arg2)
        {
            DeleteOldChildren();
        }

        public void DeleteOldChildren()
        {
            if (Children.Count > 1)
            {
                for (int i = Children.Count - 1; i >= 0; i--)
                {
                    if (Children[i] != currentView)
                    {
                        Children.RemoveAt(i);
                    }
                }
            }

            if (Children.Count > 0)
            {
                Children[0].TranslationX = 0;
            }
        }

        public void StopAnimation()
        {
            this.AbortAnimation("move");
            DeleteOldChildren();
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            if (layoutWidth != width)
            {
                StopAnimation();
            }
            layoutWidth = width;
            layoutHeight = height;
            for (int i = 0; i < Children.Count; i++)
            {
                if (Children[i] == currentView)
                {
                    View firstChild = Children[i];
                    Rectangle first = new Rectangle(x, y, width, height);
                    firstChild.Layout(first);
                    break;
                }
            }
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            //foreach
            View child = Children.FirstOrDefault();
            SizeRequest childSizeRequest = child.Measure(widthConstraint, heightConstraint); // ignor
            Size returnSize = new Size(10000, 10000);
            return new SizeRequest(returnSize, new Size(50, 50));
        }
    }
}
