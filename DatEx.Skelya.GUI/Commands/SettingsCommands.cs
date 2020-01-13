using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DatEx.Skelya.GUI.Commands
{
    class SettingsCommands
    {
        private static RoutedUICommand NewCommand(String commandName, String uiText, params KeyGesture[] keyGestures)
            => new RoutedUICommand(uiText, commandName, typeof(SettingsCommands), new InputGestureCollection(keyGestures));

        // ▬▬▬▬▬

        public static readonly RoutedUICommand SaveAppSettings = NewCommand(nameof(SaveAppSettings)
                , "Сохранить настройки приложения");
    }
}
