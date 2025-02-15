﻿using System;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;
using ToastNotifications.Core;
using NitroxModel.Logger;

namespace NitroxLauncher
{
    internal static class LauncherNotifier
    {
        private static Notifier notifier;

        public static void Setup()
        {
            if (notifier == null)
            {
                notifier = new(cfg =>
                {
                    cfg.PositionProvider = new WindowPositionProvider(
                        parentWindow: Application.Current.MainWindow,
                        corner: Corner.BottomRight,
                        offsetX: 10,
                        offsetY: 10
                    );

                    // Should we display toaster over other applications
                    cfg.DisplayOptions.TopMost = false;

                    cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                        notificationLifetime: TimeSpan.FromSeconds(20),
                        maximumNotificationCount: MaximumNotificationCount.FromCount(5)
                    );

                    cfg.Dispatcher = Application.Current.Dispatcher;
                });
            }
            else
            {
                Log.Error("Notifier is already set up");
            }
        }

        public static void Shutdown()
        {
            notifier?.Dispose();
            notifier = null;
        }

        public static void Info(string message) => notifier.ShowInformation(message);
        public static void Info(string message, MessageOptions options) => notifier.ShowInformation(message, options);

        public static void Error(string message) => notifier.ShowError(message);
        public static void Error(string message, MessageOptions options) => notifier.ShowError(message, options);

        public static void Success(string message) => notifier.ShowSuccess(message);
        public static void Success(string message, MessageOptions options) => notifier.ShowSuccess(message, options);

        public static void Warning(string message) => notifier.ShowWarning(message);
        public static void Warning(string message, MessageOptions options) => notifier.ShowWarning(message, options);
    }
}
