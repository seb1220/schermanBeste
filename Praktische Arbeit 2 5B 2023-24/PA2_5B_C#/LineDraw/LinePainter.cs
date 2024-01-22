using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LineDraw
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:LineDraw"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:LineDraw;assembly=LineDraw"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class LinePainter : Control
    {
        static LinePainter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinePainter), new FrameworkPropertyMetadata(typeof(LinePainter)));
        }

        private double _angle = 0;
        private double _x = 0;
        private double _y = 0;
        private double x = 0;
        private double y = 0;
        private List<Line> _lines = new List<Line>();

        public void Rotate(int angle)
        {
            _angle += angle;
        }

        public void Clear()
        {
            _lines.Clear();
            canvas?.Children.Clear();
            _angle = 0;
            _x = 0;
            _y = 0;
            x = 0;
            y = 0;
        }

        public void Draw(int distance)
        {
            _x = x;
            _y = y;
            x = _x + Math.Cos(_angle / 180 * Math.PI) * distance;
            y = _y + Math.Sin(_angle / 180 * Math.PI) * distance;
            if (canvas == null)
            {
                _lines.Add(new Line() { X1 = _x, Y1 = _y, X2 = x, Y2 = y, Stroke = Brushes.Red, StrokeThickness = 1 });
            }
            else
            {
                canvas.Children.Add(new Line() { X1 = _x, Y1 = _y, X2 = x, Y2 = y, Stroke = Brushes.Red, StrokeThickness = 1 });
            }
        }

        private Canvas? canvas = null;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            canvas = (Canvas)this.Template.FindName("PaintArea", this);
            foreach (Line line in _lines)
            {
                canvas.Children.Add(line);
            }
        }
    }
}
