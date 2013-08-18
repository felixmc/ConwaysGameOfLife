using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameOfLife.control
{
	public partial class LabeledSlider : UserControl
	{
		public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(LabeledSlider));
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(LabeledSlider));

		public string Label
		{
			get { return (string) GetValue(LabelProperty); }
			set { SetValue(LabelProperty, value); }
		}

		public double Value
		{
			get { return (double) GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}

		public double Minimum
		{
			get { return slider.Minimum; }
			set { slider.Minimum = value; }
		}

		public double Maximum
		{
			get { return slider.Maximum; }
			set { slider.Maximum = value; }
		}

		public double TickFrequency
		{
			get { return slider.TickFrequency; }
			set { slider.TickFrequency = value; }
		}

		public bool IsSnapToTickEnabled
		{
			get { return slider.IsSnapToTickEnabled; }
			set { slider.IsSnapToTickEnabled = value; }
		}

		public LabeledSlider()
		{
			InitializeComponent();
			slider.Value = this.Value;
			//slider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(slider_ValueChanged);
			
		}

		public void slider_ValueChanged (object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			this.Value = e.NewValue;
		}
	}
}
