using System;
using System.Windows;
using GameOfLife.model;
using System.Threading;
using System.ComponentModel;
using System.Windows.Threading;

namespace GameOfLife
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private ConwayGridModel gridModel = new ConwayGridModel(100, 100);
		private DispatcherTimer timer;
		private bool isPlaying = false;
		private int iterations = 0;
		private bool isReady = true;

		public MainWindow()
		{
			InitializeComponent();
			gameGrid.GridData = gridModel;

			timer = new DispatcherTimer(DispatcherPriority.Send);
			timer.Tick += new EventHandler(timer_tick);
			timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
		}

		private void timer_tick (Object sender, EventArgs e)
		{
			//if (iterations < 5)
			//{
			//    iterations++;
			//}
			//else if (iterations == 5)
			//{
			//    timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
			//}
			//if (isReady)
			//{
				//isReady = false;
				gridModel.next();
				//isReady = true;			
			//}

		}

		private void clearButton_Click (object sender, RoutedEventArgs e)
		{
			gridModel.Clear();
		}

		private void randButton_Click (object sender, RoutedEventArgs e)
		{
			gridModel.Randomize(lifeFreqSlider.Value);
		}

		private void nextButton_Click (object sender, RoutedEventArgs e)
		{
			gridModel.next();
		}

		private void updateButton_Click (object sender, RoutedEventArgs e)
		{
			gridModel = new ConwayGridModel((int)widthSlider.Value, (int)heightSlider.Value);
			gameGrid.GridData = gridModel;
		}

		private void playButton_Click (object sender, RoutedEventArgs e)
		{
			isPlaying = !isPlaying;

			if (isPlaying)
			{
				timer.Start();
				nextButton.IsEnabled = false;
				randButton.IsEnabled = false;
				clearButton.IsEnabled = false;

				updateButton.IsEnabled = false;
				heightSlider.IsEnabled = false;
				widthSlider.IsEnabled = false;

				playButton.Content = "Stop";
			}
			else
			{
				timer.Stop();
				nextButton.IsEnabled = true;
				randButton.IsEnabled = true;
				clearButton.IsEnabled = true;

				updateButton.IsEnabled = true;
				heightSlider.IsEnabled = true;
				widthSlider.IsEnabled = true;

				playButton.Content = "Play";
			}
		}

	}
}
