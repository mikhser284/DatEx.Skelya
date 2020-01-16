using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DatEx.Skelya.GUI.CustomCtrls.Commands
{
    public static class AppCommands
    {
        private static RoutedUICommand NewCommand(String commandName, String uiText, params KeyGesture[] keyGestures)
            => new RoutedUICommand(uiText, commandName, typeof(AppCommands), new InputGestureCollection(keyGestures));

        // ▬▬▬▬▬

        public static readonly RoutedUICommand ShowEventSnapshotDialog = NewCommand(nameof(ShowEventSnapshotDialog)
                , "Изображение события ..."
                , new KeyGesture(Key.Space, ModifierKeys.Control | ModifierKeys.Shift));

        public static readonly RoutedUICommand ShowAppSettingsDialog = NewCommand(nameof(ShowAppSettingsDialog)
                , "Настройки приложения ..."
                , new KeyGesture(Key.O, ModifierKeys.Control | ModifierKeys.Shift));

        public static readonly RoutedUICommand ShowAppAboutDialog = NewCommand(nameof(ShowAppAboutDialog)
                , "О программе ..."
                , new KeyGesture(Key.I, ModifierKeys.Control | ModifierKeys.Shift));

        public static readonly RoutedUICommand Exit = NewCommand(nameof(Exit)
                , "Выход"
                , new KeyGesture(Key.F4, ModifierKeys.Alt));

        // ▬▬▬▬▬

        public static readonly RoutedUICommand SaveAsReportInJson = NewCommand(nameof(SaveAsReportInJson)
                , "Сохранить как отчет в формате *.json");

        public static readonly RoutedUICommand SaveAsReportInCsv = NewCommand(nameof(SaveAsReportInCsv)
                , "Сохранить как отчет в формате *.csv");

        public static readonly RoutedUICommand SaveAsReportInExcel = NewCommand(nameof(SaveAsReportInExcel)
                , "Сохранить как отчет в формате *.excel"
                , new KeyGesture(Key.S, ModifierKeys.Alt));

        public static readonly RoutedUICommand SaveAsReportInPdf = NewCommand(nameof(SaveAsReportInPdf)
                , "Сохранить как отчет в формате *.pdf"
                , new KeyGesture(Key.S, ModifierKeys.Control));

        public static readonly RoutedUICommand SaveAsReportInHtml = NewCommand(nameof(SaveAsReportInHtml)
                , "Сохранить как отчет в формате *.html"
                , new KeyGesture(Key.S, ModifierKeys.Alt | ModifierKeys.Shift));
    }
}
