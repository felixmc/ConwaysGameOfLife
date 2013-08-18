using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;

namespace GameOfLife.model
{
	public class GridCellModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private int lifeTime = 0;
		public int LifeTime { get { return lifeTime; } }

		private bool isAlive;
		public bool IsAlive
		{
			get
			{
				return isAlive;
			}
			set
			{
				if (value)
				{
					lifeTime = Math.Min(lifeTime + 1, 5);
				}
				else
				{
					lifeTime = 0;
				}

				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("LifeTime"));
				}

				if (isAlive != value)
				{
					isAlive = value;
					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("IsAlive"));
					}
				}

			}
		}

		public GridCellModel (bool alive = false)
		{
			IsAlive = alive;
		}

		public static implicit operator bool (GridCellModel m)
		{
			return m.IsAlive;
		}

	}
}