﻿using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Platform;
using Avalonia.Styling;

namespace nxDumpFuse
{
    public class FluentWindow : Window, IStyleable
    {
        Type IStyleable.StyleKey => typeof(Window);

        public FluentWindow()
        {
            ExtendClientAreaToDecorationsHint = true;
            ExtendClientAreaTitleBarHeightHint = -1;

            TransparencyLevelHint = WindowTransparencyLevel.AcrylicBlur;

            this.GetObservable(WindowStateProperty)
                .Subscribe(x =>
                {
                    PseudoClasses.Set(":maximized", x == WindowState.Maximized);
                    PseudoClasses.Set(":fullscreen", x == WindowState.FullScreen);
                });

            this.GetObservable(IsExtendedIntoWindowDecorationsProperty)
                .Subscribe(x =>
                {
                    if (x) return;
                    SystemDecorations = SystemDecorations.Full;
                    TransparencyLevelHint = WindowTransparencyLevel.Blur;
                });
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            ExtendClientAreaChromeHints =
                ExtendClientAreaChromeHints.PreferSystemChrome |
                ExtendClientAreaChromeHints.OSXThickTitleBar;
        }
    }
}