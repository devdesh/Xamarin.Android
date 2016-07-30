using System;
using System.Collections.Generic;

namespace RaysHotDogs.Core
{
	public class HotDogDataService
	{
		static HotDogRepository hotDogRepo;

		public HotDogDataService()
		{
			hotDogRepo = new HotDogRepository();
		}


		public List<HotDog> GetAllHotDogs()
		{
			return hotDogRepo.GetAllHotDogs();
		}

		public HotDog GetHotDogByID(int id)
		{

			return hotDogRepo.GetHotDogByID(id);
		}

		public List<HotDogGroup> GetHotDogGroups()
		{
			return hotDogRepo.GetHotDogGroups();
		}


		public List<HotDog> GetHotDogsForAGroup(int groupID)
		{
			return hotDogRepo.GetHotDogsForAGroup(groupID);
		}

		public List<HotDog> GetFavoriteHotDogs()
		{
			return hotDogRepo.GetFavoriteHotDogs();
		}
	}
}

