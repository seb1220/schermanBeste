using System.Windows.Input;

namespace Bilderverwaltungsprogramm
{
    public static class CustomCommands
    {
        public static readonly RoutedUICommand NewAlbum = new RoutedUICommand
            (
                "NewAlbum",
                "NewAlbum",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.N, ModifierKeys.Control)
                }
            );

        public static readonly RoutedUICommand NewPicture = new RoutedUICommand
            (
                "NewPicture",
                "NewPicture",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.P, ModifierKeys.Control)
                }
            );

        public static readonly RoutedUICommand MovePicture = new RoutedUICommand
            (
                "MovePicture",
                "MovePicture",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.M, ModifierKeys.Control)
                }
            );

        public static readonly RoutedUICommand RemovePicture = new RoutedUICommand
            (
                "NewPicture",
                "NewPicture",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.R, ModifierKeys.Control)
                }
            );

        public static readonly RoutedUICommand RotateRight = new RoutedUICommand
            (
                "RotateRight",
                "RotateRight",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.E, ModifierKeys.Control)
                }
            );

        public static readonly RoutedUICommand RotateLeft = new RoutedUICommand
            (
                "RotateLeft",
                "RotateLeft",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.Q, ModifierKeys.Control)
                }
            );

        public static readonly RoutedUICommand Rotate180 = new RoutedUICommand
            (
                "Rotate180",
                "Rotate180",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.W, ModifierKeys.Control)
                }
            );
    }
}
