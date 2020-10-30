﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SourceApp.Mobile.Controls
{
    public class CustomEditor : Editor
    {
        public CustomEditor()
           : base()
        {

        }



        #region Properties

        public static BindableProperty BorderColorProperty = BindableProperty.Create<CustomEditor, Color>(o => o.BorderColor, Color.Transparent);

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }


        public static BindableProperty BorderWidthProperty = BindableProperty.Create<CustomEditor, float>(o => o.BorderWidth, 0);

        public float BorderWidth
        {
            get { return (float)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }

        public static BindableProperty BorderRadiusProperty = BindableProperty.Create<CustomEditor, float>(o => o.BorderRadius, 0);

        public float BorderRadius
        {
            get { return (float)GetValue(BorderRadiusProperty); }
            set { SetValue(BorderRadiusProperty, value); }
        }

        public static BindableProperty LeftPaddingProperty = BindableProperty.Create<CustomEditor, int>(o => o.LeftPadding, 5);

        public int LeftPadding
        {
            get { return (int)GetValue(LeftPaddingProperty); }
            set { SetValue(LeftPaddingProperty, value); }
        }

        public static BindableProperty RightPaddingProperty = BindableProperty.Create<CustomEditor, int>(o => o.RightPadding, 5);

        public int RightPadding
        {
            get { return (int)GetValue(RightPaddingProperty); }
            set { SetValue(RightPaddingProperty, value); }
        }

        #endregion
    }
}
