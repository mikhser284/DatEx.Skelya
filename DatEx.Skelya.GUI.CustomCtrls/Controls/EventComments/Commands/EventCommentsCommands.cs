using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DatEx.Skelya.GUI.CustomCtrls.Commands
{
    class EventCommentsCommands
    {
        private static RoutedUICommand NewCommand(String commandName, String uiText, params KeyGesture[] keyGestures)
            => new RoutedUICommand(uiText, commandName, typeof(EventCommentsCommands), new InputGestureCollection(keyGestures));

        // ▬▬▬▬▬

        public static readonly RoutedUICommand ApplyFilter = NewCommand(nameof(ApplyFilter)
                , "Применить комментарий"
                , new KeyGesture(Key.C, ModifierKeys.Control | ModifierKeys.Shift));
    }
}
