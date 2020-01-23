using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DatEx.Skelya.GUI.CustomCtrls.Commands
{
    class AppSettingsCommands
    {
        private static RoutedUICommand NewCommand(String commandName, String uiText, params KeyGesture[] keyGestures)
            => new RoutedUICommand(uiText, commandName, typeof(AppSettingsCommands), new InputGestureCollection(keyGestures));

        // ▬▬▬▬▬

        public static readonly RoutedUICommand SaveAppSettings = NewCommand(nameof(SaveAppSettings)
                , "Сохранить настройки приложения");
    }
}
