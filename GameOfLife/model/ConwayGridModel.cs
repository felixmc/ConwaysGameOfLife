using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;
using System.Threading;
using System.Windows.Threading;

namespace GameOfLife.model
{
	public class ConwayGridModel
	{
		private GridCellModel[,] grid;
		public readonly int Width, Height;

		public GridCellModel[,] Cells { get { return grid; } }

		public ConwayGridModel(int width, int height)
		{
			Width = width;
			Height = height;
			grid = EmptyGrid();
			//grid = RandomGrid();
		}

		public void next ()
		{
			DateTime calcStart = DateTime.Now;
			//EvaluateGeneration();
			AlternateEvaluate();
			Console.WriteLine("update time: " + DateTime.Now.Subtract(calcStart).Milliseconds);
		}

		public void AlternateEvaluate ()
		{
			//bool[,] cellState = new bool[Width,Height];
			//for(int x = 0; x < Width; x++)
			//{
			//    for(int y = 0; y < Height; y++)
			//    {
			//        cellState[x,y] = grid[x,y].IsAlive;
			//    }
			//}

			int[,] neighbors = new int[Width,Height];

			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					if(grid[x, y].IsAlive)
					{
						for (int xD = -1; xD < 2; xD++)
						{
							for (int yD = -1; yD < 2; yD++)
							{
								if (!(xD == 0 && yD == 0))
								{
									int nX = x + xD;
									int nY = y + yD;
									if ((nX >= 0 && nX < Width) && (nY >= 0 && nY < Height))
									{
										neighbors[nX, nY]++;
									}
								}
							}
						}
					}
				}
			}

			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					

					int n = neighbors[x,y];
					if ((n == 3) && !grid[x, y].IsAlive)
					{
						grid[x, y].IsAlive = true;
					}
					else if ((n == 2 || n == 3) && (grid[x, y].IsAlive))
					{
						grid[x, y].IsAlive = true;
					}
					else
					{
						grid[x, y].IsAlive = false;
					}
				}
			}

		}

		public void Randomize (double freq = .5)
		{
			UpdateGrid( RandomGrid(freq) );
		}

		public void Clear ()
		{
			UpdateGrid( EmptyGrid() );
		}

		private void UpdateGrid (GridCellModel[,] newGrid)
		{
			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					grid[x, y].IsAlive = newGrid[x, y].IsAlive;
				}
			}
		}

		private void EvaluateGeneration ()
		{
			DateTime calcStart = DateTime.Now;

			bool[,] newGrid = new bool[Width, Height];

			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					int n = GetNeighbors(x, y);
					if ((n == 3) && !grid[x, y].IsAlive)
					{
						newGrid[x, y] = true;
					}
					else if ((n == 2 || n == 3) && (grid[x, y].IsAlive))
					{
						newGrid[x, y] = true;
					}
					else
					{
						newGrid[x, y] = false;
					}
				}
			}

			DateTime calcEnd = DateTime.Now;

			Console.WriteLine("Calc Time: " + calcEnd.Subtract(calcStart).Milliseconds);

			GridCellModel[,] mygrid = (GridCellModel[,])grid.Clone();

			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					mygrid[x, y].IsAlive = newGrid[x, y];
				}
			}

			Console.WriteLine("GUI Time: " + DateTime.Now.Subtract(calcEnd).Milliseconds);
			Console.WriteLine();
		}

		private int GetNeighbors (int x, int y)
		{
			int count = 0;

			for (int xD = -1; xD < 2; xD++)
			{
				for (int yD = -1; yD < 2; yD++)
				{
					if (!(xD == 0 && yD == 0))
					{
						int nX = x + xD;
						int nY = y + yD;
						if ((nX >= 0 && nX < Width) && (nY >= 0 && nY < Height) && grid[nX,nY].IsAlive)
							count++;
					}
				}
			}
			return count;
		}

		private GridCellModel[,] RandomGrid(double freq = .5)
		{
			Random r = new Random();
			GridCellModel[,] rgrid = new GridCellModel[Width,Height];
			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					rgrid[x,y] = new GridCellModel( r.NextDouble() <= freq );
				}
			}

			return rgrid;
		}

		private GridCellModel[,] EmptyGrid()
		{
			GridCellModel[,] egrid = new GridCellModel[Width, Height];
			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					egrid[x, y] = new GridCellModel();
				}
			}

			return egrid;
		}

	}
}