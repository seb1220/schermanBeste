using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Geoguesser
{
    public static class Commands
    {
        public static RoutedUICommand OpenImage = new RoutedUICommand("Open Image", "OpenImage", typeof(Commands), new InputGestureCollection { new KeyGesture(Key.O, ModifierKeys.Control) });
        public static RoutedUICommand SelectImage = new RoutedUICommand("Select Image", "SelectImage", typeof(Commands), new InputGestureCollection { new KeyGesture(Key.I, ModifierKeys.Control) });
        public static RoutedUICommand OpenXML = new RoutedUICommand("Open XML", "OpenXML", typeof(Commands), new InputGestureCollection { new KeyGesture(Key.I, ModifierKeys.Control) });
        public static RoutedUICommand SaveXML = new RoutedUICommand("Save XML", "SaveXML", typeof(Commands), new InputGestureCollection { new KeyGesture(Key.I, ModifierKeys.Control) });
    }
}
