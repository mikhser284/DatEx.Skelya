using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DatEx.Skelya.GUI.CustomCtrls.Commands
{
    class EventFilterCommands
    {
        private static RoutedUICommand NewCommand(String commandName, String uiText, params KeyGesture[] keyGestures)
            => new RoutedUICommand(uiText, commandName, typeof(EventFilterCommands), new InputGestureCollection(keyGestures));

        // ▬▬▬▬▬

        public static readonly RoutedUICommand ApplyFilter = NewCommand(nameof(ApplyFilter)
                , "Применить фильтр"
                , new KeyGesture(Key.F, ModifierKeys.Control | ModifierKeys.Shift));

        public static readonly RoutedUICommand SaveFilter = NewCommand(nameof(SaveFilter)
                , "Сохранить фильтр");

        public static readonly RoutedUICommand SaveFilterAs = NewCommand(nameof(SaveFilterAs)
                , "Сохранить фильтр как...");

        public static readonly RoutedUICommand DeleteFilter = NewCommand(nameof(DeleteFilter)
                , "Удалиь фильтр");
    }
}
