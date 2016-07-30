
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
	[Activity(Label = "AboutViewActivity")]
	public class AboutViewActivity : Activity
	{
		private TextView txtRayPhoneNumber;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.AboutView);
			FindControls();
			BindEvents();
		}

		private void FindControls()
		{
			txtRayPhoneNumber = FindViewById<TextView>(Resource.Id.txtRayPhoneNumber);
		}

		private void BindEvents()
		{
			txtRayPhoneNumber.Click += TxtRayPhoneNumber_Click;
		}

		void TxtRayPhoneNumber_Click(object sender, EventArgs e)
		{
			Intent intent = new Intent(Intent.ActionCall);
			intent.SetData(Android.Net.Uri.Parse("tel:" + txtRayPhoneNumber.Text));
			StartActivity(intent);
		}


	}
}

