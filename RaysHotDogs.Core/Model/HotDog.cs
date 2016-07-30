using System;
using System.Collections.Generic;

namespace RaysHotDogs.Core
{
	public class HotDog
	{
		public int HotDogID
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public string ShortDescription
		{
			get;
			set;
		}

		public int Price
		{
			get;
			set;
		}	

		public int PrepTime
		{
			get;
			set;
		}

		public string ImagePath
		{
			get;
			set;
		}	

		public bool IsAvailable
		{
			get;
			set;
		}

		public bool IsFavorite
		{
			get;
			set;
		}

		public List<string> Ingredients
		{
			get;
			set;
		}

		public string GroupName
		{
			get;
			set;
		}


		public HotDog()
		{
			
		}
	}
}

