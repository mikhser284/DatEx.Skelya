using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DatEx.Skelya.GUI.CustomCtrls.Commands
{
    class CtrlTemplateCommands
    {
        private static RoutedUICommand NewCommand(String commandName, String uiText, params KeyGesture[] keyGestures)
            => new RoutedUICommand(uiText, commandName, typeof(CtrlTemplateCommands), new InputGestureCollection(keyGestures));

        // ▬▬▬▬▬

        public static readonly RoutedUICommand CommandName = NewCommand(nameof(CommandName)
            , "Command name"
            , new KeyGesture(Key.Space, ModifierKeys.Control | ModifierKeys.Shift | ModifierKeys.Alt));
         
    }
}
