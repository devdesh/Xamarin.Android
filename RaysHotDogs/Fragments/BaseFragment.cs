
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using RaysHotDogs.Core;

namespace RaysHotDogs
{
	public class BaseFragment : Fragment
	{
		protected List<HotDog> hotDogs;
		protected HotDogDataService dataService;
		protected ListView listView;

		public BaseFragment()
		{
			dataService = new HotDogDataService();

		}

		protected void AttachEvent()
		{
			listView.ItemClick += ListView_ItemClick;
		}

		protected void FindControls()
		{
			listView = this.View.FindViewById<ListView>(Resource.Id.hotDogListView);
		}


		void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			Intent intent = new Intent(this.Activity, typeof(HotDogDetailActivity));
			intent.PutExtra("selectedHotDogID", hotDogs[e.Position].HotDogID);
			StartActivityForResult(intent, 100);
		}


		public override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			if (resultCode == Result.Ok && requestCode == 100)
			{
				var selectedHotDogid = data.GetIntExtra("selectedHotDogID", 1);
				var selectedHotDog = dataService.GetHotDogByID(selectedHotDogid);
				var noOfHotDogsSelected = data.GetIntExtra("noOfHotDogsSelected", 1);

				var dialog = new AlertDialog.Builder(this.Activity);
				dialog.SetTitle("Confirmation");
				dialog.SetMessage(string.Format("You have selected {0} {1}.",
												noOfHotDogsSelected, selectedHotDog.Name));
				dialog.Show();
			}
		}
		
	}
}

