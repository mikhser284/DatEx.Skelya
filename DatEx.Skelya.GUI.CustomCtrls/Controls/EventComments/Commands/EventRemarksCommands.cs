using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DatEx.Skelya.GUI.CustomCtrls.Commands
{
    public static class EventRemarksCommands
    {
        private static RoutedUICommand NewCommand(String commandName, String uiText, params KeyGesture[] keyGestures)
            => new RoutedUICommand(uiText, commandName, typeof(EventRemarksCommands), new InputGestureCollection(keyGestures));

        // ▬▬▬▬▬

        public static readonly RoutedUICommand AddRemark = NewCommand(nameof(AddRemark)
                , "Добавить примечание"
                , new KeyGesture(Key.C, ModifierKeys.Control | ModifierKeys.Shift));
    }
}
