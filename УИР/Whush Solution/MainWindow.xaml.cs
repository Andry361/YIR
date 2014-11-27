using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Microsoft.Research.DynamicDataDisplay;
using System.Linq;
using Microsoft.Research.DynamicDataDisplay.DataSources;


namespace Whush.Demo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
          InitializeComponent();
            //var x = Enumerable.Range(0, 1001).Select(i => i / 10.0).ToArray();

            //// Compute y array as sin(x)/x function defined on x grid
            //var y = x.Select(v => Math.Abs(v) < 1e-10 ? 1 : Math.Sin(v) / v).ToArray();
            //var xDataSource = x.AsXDataSource();
            //var yDataSource = y.AsYDataSource();
            //CompositeDataSource compositeDataSource = xDataSource.Join(yDataSource);
            //plotter.AddLineGraph(compositeDataSource, Colors.Goldenrod, 3,"Sine");
            //plotter.FitToView();
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          StBar.Visibility = Visibility.Collapsed;
          ButtonArrow.Visibility = Visibility.Collapsed;
          MainGrid.Visibility = Visibility.Collapsed;
          MenuPagesStacPanel.Width = Double.NaN;
          MenuItemButtonArrow.Visibility = Visibility.Visible;
          DoubleAnimation buttonWidth = new DoubleAnimation();
          buttonWidth.From = Menu.ActualWidth;
          buttonWidth.To = 160;
          buttonWidth.Duration = TimeSpan.FromSeconds(0.1);
          Menu.BeginAnimation(StackPanel.WidthProperty, buttonWidth);          
        }

        private void MenuItemButtonArrow_Click(object sender, RoutedEventArgs e)
        {
          
          
          DoubleAnimation buttonWidth = new DoubleAnimation();
          buttonWidth.From = Menu.ActualWidth;
          buttonWidth.To = 1;
          buttonWidth.Duration = TimeSpan.FromSeconds(0.1);
          Menu.BeginAnimation(StackPanel.WidthProperty, buttonWidth);
          MainGrid.Visibility = Visibility.Visible;
          MenuPagesStacPanel.Width = 0;
          StBar.Visibility = Visibility.Visible;
          ButtonArrow.Visibility = Visibility.Visible;
          MenuItemButtonArrow.Visibility = Visibility.Hidden;
        }

        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
          Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

          dlg.DefaultExt = ".txt";
          dlg.Filter = "wav файлы (*.wav)|*.wav";

          Nullable<bool> result = dlg.ShowDialog();
          if (result == true)
          {
            ReadWav file = new ReadWav();
            byte[] data = file.StartReadWav(dlg.FileName);
            double step = 1.0;


            var x = Enumerable.Range(0, data.Length).Select(i => i * step).ToArray();
            var y = x.Select(v => data[(int)(v/step)]*1.0).ToArray();
            
            var xDataSource = x.AsXDataSource();
            var yDataSource = y.AsYDataSource();
            CompositeDataSource compositeDataSource = xDataSource.Join(yDataSource);
            plotter.AddLineGraph(compositeDataSource, Colors.Goldenrod, 3, "Sine");
            plotter.FitToView();
          }
        }
    }
}
