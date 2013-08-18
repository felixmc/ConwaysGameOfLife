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
using GameOfLife.model;
using System.ComponentModel;
using System.Globalization;

namespace GameOfLife.control
{
	/// <summary>
	/// Interaction logic for ConwayGrid.xaml
	/// </summary>
	public partial class ConwayGrid : UserControl
	{
		private const int cellDimension = 8;
		
		public static readonly DependencyProperty GridDataProperty =
		DependencyProperty.Register(
			"GridData",
			typeof(ConwayGridModel),
			typeof(ConwayGrid),
			new PropertyMetadata(null, GridDataSet)
		);

		public ConwayGridModel GridData
		{
			get { return (ConwayGridModel) GetValue(GridDataProperty); }
			set { SetValue(GridDataProperty, value); }
		}

		public ConwayGrid()
		{
			InitializeComponent();
		}

		public static void GridDataSet (DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.NewValue == null)
				return;
			
			ConwayGridModel data = (ConwayGridModel)e.NewValue;
			ConwayGrid self = ((ConwayGrid)d);

			self.SetupGrid(data);
		}

		private void SetupGrid (ConwayGridModel model)
		{
			gameCanvas.Children.Clear();
			gameWrapper.Width = model.Width * cellDimension;
			gameWrapper.Height = model.Height * cellDimension;

			gameCanvas.Width = model.Width * cellDimension;
			gameCanvas.Height = model.Height * cellDimension;

			this.DataContext = model;

			for (int i = 0; i < model.Width; i++)
			{
				for (int j = 0; j < model.Height; j++)
				{
					Shape shape = new Rectangle();
					shape.Width = cellDimension;
					shape.Height = cellDimension;

					Canvas.SetTop(shape, j * cellDimension);
					Canvas.SetLeft(shape, i * cellDimension);

					shape.StrokeThickness = 1;
					//shape.SetResourceReference(Shape.StrokeProperty, "BorderColor");

					shape.DataContext = model.Cells[i, j];

					gameCanvas.Children.Add(shape);
				}
			}
		}

		private int CalculateIndex (int x, int y)
		{
			return x + (y * GridData.Width);
		}

		private void gameCanvas_MouseDown (object sender, MouseButtonEventArgs e)
		{
			Point p = e.GetPosition(gameCanvas);

			int x = (int) Math.Floor(p.X / cellDimension);
			int y = (int) Math.Floor(p.Y / cellDimension);

			Shape s = (Shape)gameCanvas.Children[CalculateIndex(x, y)];

			ConwayGridModel model = (ConwayGridModel) this.GetValue(GridDataProperty);
			model.Cells[x, y].IsAlive = !model.Cells[x, y].IsAlive;
		}

	}

}