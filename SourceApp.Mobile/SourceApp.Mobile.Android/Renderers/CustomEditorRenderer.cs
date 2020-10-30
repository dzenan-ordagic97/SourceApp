using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SourceApp.Mobile.Controls;
using SourceApp.Mobile.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]
namespace SourceApp.Mobile.Droid.Renderers
{
#pragma warning disable CS0618 // Type or member is obsolete
    public class CustomEditorRenderer : EditorRenderer
    {
        #region Private fields and properties

        private BorderRenderer _renderer;
        private const GravityFlags DefaultGravity = GravityFlags.Start;

        #endregion

        #region Parent override

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || this.Element == null)
                return;
            Control.Gravity = DefaultGravity;
            var customEditor = Element as CustomEditor;
            UpdateBackground(customEditor);
            UpdatePadding(customEditor);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Element == null)
                return;
            var customEditor = Element as CustomEditor;
            if (e.PropertyName == CustomEditor.BorderWidthProperty.PropertyName ||
                e.PropertyName == CustomEditor.BorderColorProperty.PropertyName ||
                e.PropertyName == CustomEditor.BorderRadiusProperty.PropertyName ||
                e.PropertyName == CustomEditor.BackgroundColorProperty.PropertyName)
            {
                UpdateBackground(customEditor);
            }
            else if (e.PropertyName == CustomEditor.LeftPaddingProperty.PropertyName ||
                e.PropertyName == CustomEditor.RightPaddingProperty.PropertyName)
            {
                UpdatePadding(customEditor);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (_renderer != null)
                {
                    _renderer.Dispose();
                    _renderer = null;
                }
            }
        }

        #endregion

        #region Utility methods

        private void UpdateBackground(CustomEditor customEditor)
        {
            if (_renderer != null)
            {
                _renderer.Dispose();
                _renderer = null;
            }
            var oldBg = customEditor.BackgroundColor;
            customEditor.BackgroundColor = Xamarin.Forms.Color.Transparent;
            _renderer = new BorderRenderer();
            Control.SetBackground(_renderer.GetBorderBackground(customEditor.BorderColor, oldBg, customEditor.BorderWidth, customEditor.BorderRadius));

        }

        private void UpdatePadding(CustomEditor customEditor)
        {
            Control.SetPadding((int)Forms.Context.ToPixels(customEditor.LeftPadding), 0,
                (int)Forms.Context.ToPixels(customEditor.RightPadding), 0);
        }

        #endregion
    }
#pragma warning restore CS0618 // Type or member is obsolete
}