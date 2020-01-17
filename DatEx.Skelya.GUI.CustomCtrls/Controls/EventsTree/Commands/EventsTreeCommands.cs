using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DatEx.Skelya.GUI.CustomCtrls.Controls.EventsTree.Commands
{
    class EventsTreeCommands
    {    
        private static RoutedUICommand NewCommand(String commandName, String uiText, params KeyGesture[] keyGestures)
            => new RoutedUICommand(uiText, commandName, typeof(EventsTreeCommands), new InputGestureCollection(keyGestures));

        // ▬▬▬▬▬

        public static readonly RoutedUICommand ExpandLevel = NewCommand(nameof(ExpandLevel)
                , "Развернуть уровень"
                , new KeyGesture(Key.Add, ModifierKeys.Control | ModifierKeys.Shift));

        public static readonly RoutedUICommand CollapseLevel = NewCommand(nameof(CollapseLevel)
                , "Свернуть уровень"
                , new KeyGesture(Key.Subtract, ModifierKeys.Control | ModifierKeys.Shift));
    }
}
