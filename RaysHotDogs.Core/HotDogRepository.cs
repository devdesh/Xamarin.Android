using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RaysHotDogs.Core
{
	public class HotDogRepository
	{
		private static List<HotDogGroup> HotDogGroups = new List<HotDogGroup>();
		string url = "http://gillcleerenpluralsight.blob.core.windows.net/files/hotdogs.json";


		public HotDogRepository()
		{
			Task.Run(() => LoadHotDogDataAsync(url)).Wait();
		}


		private async Task LoadHotDogDataAsync(string uri)
		{
			if (HotDogGroups != null)
			{
				string jsonResponse = null;
				using (var httpClient = new HttpClient())
				{
					try
					{
						Task<HttpResponseMessage> httpResponse = httpClient.GetAsync(uri);
						HttpResponseMessage response = await httpResponse;
						jsonResponse = await response.Content.ReadAsStringAsync();
						HotDogGroups = JsonConvert.DeserializeObject<List<HotDogGroup>>(jsonResponse);
					}
					catch (Exception ex)
					{
						throw;
					}
				}
			}
		}

		//private static List<HotDogGroup> HotDogGroups = new List<HotDogGroup>()
		//{
		//	new HotDogGroup() {HotDogGroupID = 1,ImagePath = "",Title = "Meat Lovers",HotDogs = new List<HotDog>()
		//		{

		//			new HotDog
		//			{
		//				HotDogID = 1,
		//				ImagePath = "hotdog1",
		//				Name = "Regular Meat Hot Dog",
		//				Ingredients = new List<string> {"meat","cheese","pepper"},
		//				IsAvailable = true,
		//				Price = 5,
		//				ShortDescription = "the best meat hot dog",
		//				PrepTime = 10
		//			},
		//			new HotDog
		//			{
		//				HotDogID = 2,
		//				ImagePath = "hotdog2",
		//				Name = "Haute Dog",
		//				Ingredients = new List<string> {"tomato sauce","meat","cheese","pepper","onions"},
		//				IsAvailable = true,
		//				IsFavorite = true,
		//				Price = 10,
		//				ShortDescription = "The classy one",
		//				PrepTime = 15
		//			},
		//			new HotDog
		//			{
		//				HotDogID = 3,
		//				ImagePath = "hotdog3",
		//				Name = "Extra Long",
		//				Ingredients = new List<string> {"long sub","tomato sauce","meat","cheese","pepper","onions"},
		//				IsAvailable = true,
		//				Price = 15,
		//				ShortDescription = "when regular isn't enough",
		//				PrepTime = 15
		//			}
		//		}

		//	},
		//	new HotDogGroup() {HotDogGroupID = 2,ImagePath = "",Title = "Veggie Lovers",HotDogs = new List<HotDog>()
		//		{

		//			new HotDog
		//			{
		//				HotDogID = 4,
		//				ImagePath = "hotdog4",
		//				Name = "Regular Veggie Hot Dog",
		//				Ingredients = new List<string> {"tomatoes","cheese","pepper"},
		//				IsAvailable = true,
		//				IsFavorite = true,
		//				Price = 5,
		//				ShortDescription = "the best meat hot dog",
		//				PrepTime = 10
		//			},
		//			new HotDog
		//			{
		//				HotDogID = 5,
		//				ImagePath = "hotdog5",
		//				Name = "Haute Dog",
		//				Ingredients = new List<string> {"tomato sauce","mozerella","blue cheese","pepper","onions"},
		//				IsAvailable = true,
		//				Price = 10,
		//				ShortDescription = "The classy veggie",
		//				PrepTime = 15
		//			},
		//			new HotDog
		//			{
		//				HotDogID = 6,
		//				ImagePath = "hotdog6",
		//				Name = "Extra Long",
		//				Ingredients = new List<string> {"long sub","tomato sauce","cheese","pepper","onions"},
		//				IsAvailable = true,
		//				IsFavorite = true,
		//				Price = 15,
		//				ShortDescription = "when regular isn't enough",
		//				PrepTime = 15
		//			}
		//		}

		//	}
		//};


		public List<HotDog> GetAllHotDogs()
		{
			List<HotDog> hotDogs = new List<HotDog>();
			foreach (var h in HotDogGroups)
			{
				foreach (var hd in h.HotDogs)
				{
					hotDogs.Add(hd as HotDog);
				}
			}
			return hotDogs;
		}

		public HotDog GetHotDogByID(int id)
		{

			//HotDogGroups.GroupBy(x => x.HotDogGroupID).ToList().ForEach(x => x.HotDogs.Where(h => h.HotDogID == id)).ToList().FirstOrDefault();
			IEnumerable<HotDog> hotDogs =
				from hdg in HotDogGroups
				from hd in hdg.HotDogs
				where hd.HotDogID == id
				select hd;
			return hotDogs.FirstOrDefault();
		}

		public List<HotDogGroup> GetHotDogGroups()
		{
			return HotDogGroups;
		}


		public List<HotDog> GetHotDogsForAGroup(int groupID)
		{
			return HotDogGroups.Where(x => x.HotDogGroupID == groupID).FirstOrDefault().HotDogs;
		}


		public List<HotDog> GetFavoriteHotDogs()
		{
			List<HotDog> hotDogs = GetAllHotDogs();
			return hotDogs.Where(x => x.IsFavorite).ToList();
		}
	}
}

