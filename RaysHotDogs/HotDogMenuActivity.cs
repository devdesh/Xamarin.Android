
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RaysHotDogs.Core;

namespace RaysHotDogs
{
	[Activity(Label = "Hot Dog Menu" )]
	public class HotDogMenuActivity : Activity
	{
		private ListView hotDogListView;
		private List<HotDog> hotDogs;
		private HotDogDataService dataService;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

			SetContentView(Resource.Layout.HotDogMenuView);

			AddTab("Favorites", Resource.Mipmap.Fav, new FavouriteHotDogFragment());
			AddTab("Meat Lovers", Resource.Mipmap.Icon, new MeatLoverFragment());
			AddTab("Veggie Lovers", Resource.Mipmap.Icon, new VeggieLoverFragment());

			// commenting below code as it is related to ListView approach.Now TabView is being used.
			//Initialize();
			//GetallHotDogs();
			//AttachListViewAdapter();
		}

		private void Initialize()
		{
			hotDogListView = FindViewById<ListView>(Resource.Id.hotDogListView);
			dataService = new HotDogDataService();
			hotDogListView.ItemClick += HotDogListView_ItemClick;
		}


		private void AddTab(string tabText, int tabIconImageResourceID, Fragment view)
		{

			var tab = ActionBar.NewTab();
			tab.SetText(tabText);

			tab.SetIcon(tabIconImageResourceID);

			tab.TabSelected +=
				delegate (object sender, ActionBar.TabEventArgs e) 
			{
				var fragment = this.FragmentManager.FindFragmentById(Resource.Id.fragmentContainer);
				if (fragment != null)
				{
					e.FragmentTransaction.Remove(fragment);
				}
				e.FragmentTransaction.Add(Resource.Id.fragmentContainer, view);

			};

			tab.TabUnselected += delegate (object sender, ActionBar.TabEventArgs e)
			{
				e.FragmentTransaction.Remove(view);
			};

			this.ActionBar.AddTab(tab);
		}


		//this code is no longer used as it is related to ListView approach. Now TabView is being used.
		void HotDogListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			Intent intent = new Intent(this, typeof(HotDogDetailActivity));
			intent.PutExtra("selectedHotDogID", hotDogs[e.Position].HotDogID);
			StartActivityForResult(intent, 100);
		}

		//no longer being used as it is related to ListView approach. Moved this code to BaseFragment
		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			if (resultCode == Result.Ok && requestCode == 100)
			{
				var selectedHotDogid = data.GetIntExtra("selectedHotDogID",1);
				var selectedHotDog = dataService.GetHotDogByID(selectedHotDogid);
				var noOfHotDogsSelected = data.GetIntExtra("noOfHotDogsSelected", 1);

				var dialog = new AlertDialog.Builder(this);
				dialog.SetTitle("Confirmation");
				dialog.SetMessage(string.Format("You have selected {0} {1}.",
				                                noOfHotDogsSelected,selectedHotDog.Name));
				dialog.Show();
			}
		}


		//this code is no longer used as it is related to ListView approach. Now TabView is being used.
		private void GetallHotDogs()
		{
			hotDogs = dataService.GetAllHotDogs();
		}

		//this code is no longer used as it is related to ListView approach. Now TabView is being used.
		private void AttachListViewAdapter()
		{
			hotDogListView.Adapter = new HotDogListViewAdapter(hotDogs, this);
		}
	}
}

