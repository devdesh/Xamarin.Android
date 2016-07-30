
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

namespace RaysHotDogs
{
	[Activity(Label = "Ray's Hot Dogs", MainLauncher = true, Icon = "@drawable/Favorites")]
	public class MainViewActivity : Activity
	{
		

		private Button btnOrderHotDog;
		private Button btnViewCart;
		private Button btnTakePicture;
		private Button btnGoToMap;
		private Button btnAboutView;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.MainMenuView);
			FindControls();
			BindEvents();
		}

		private void FindControls()
		{
			btnOrderHotDog = FindViewById<Button>(Resource.Id.btnOrderHotDogs);
			btnViewCart = FindViewById<Button>(Resource.Id.btnViewCart);
			btnTakePicture = FindViewById<Button>(Resource.Id.btnTakePicture);
			btnGoToMap = FindViewById<Button>(Resource.Id.btnGoToMap);
			btnAboutView = FindViewById<Button>(Resource.Id.btnAboutView);
		}

		private void BindEvents()
		{
			btnOrderHotDog.Click += BtnOrderHotDog_Click;
			btnAboutView.Click += BtnAboutView_Click;
			btnTakePicture.Click += BtnTakePicture_Click;
			btnGoToMap.Click += BtnGoToMap_Click;
		}

		void BtnOrderHotDog_Click(object sender, EventArgs e)
		{
			Intent intent = new Intent(this, typeof(HotDogMenuActivity));
			StartActivity(intent);
		}

		void BtnAboutView_Click(object sender, EventArgs e)
		{
			Intent intent = new Intent(this, typeof(AboutViewActivity));
			StartActivity(intent);
		}

		void BtnTakePicture_Click(object sender, EventArgs e)
		{
			Intent intent = new Intent(this, typeof(TakePictureViewActivity));
			StartActivity(intent);
		}

		void BtnGoToMap_Click(object sender, EventArgs e)
		{
			Intent intent = new Intent(this, typeof(MapViewActivity));
			StartActivity(intent);
		}
	}
}

