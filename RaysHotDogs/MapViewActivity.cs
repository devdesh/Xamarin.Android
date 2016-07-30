
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace RaysHotDogs
{
	[Activity(Label = "MapViewActivity")]
	public class MapViewActivity : Activity
	{
		private Button btnOpenMapsForRaysStore;
		private FrameLayout mapFrameLayout;
		private MapFragment mapFragmant;
		private GoogleMap googleMap;
		private LatLng raysStoreLocation;


		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			raysStoreLocation = new LatLng(37.3382082, -121.8863286);

			// Create your application here
			SetContentView(Resource.Layout.MapView);
			btnOpenMapsForRaysStore = FindViewById<Button>(Resource.Id.btnLaunchRaysStoreLocation);
			mapFrameLayout = FindViewById<FrameLayout>(Resource.Id.mapFrameLayout);
			btnOpenMapsForRaysStore.Click += BtnOpenMapsForRaysStore_Click;
		}


		//this method is for opening external google maps thru an intent.
		//void BtnOpenMapsForRaysStore_Click(object sender, EventArgs e)
		//{
		//	Android.Net.Uri locationURI = Android.Net.Uri.Parse("geo:37.3382082 -121.8863286");
		//	Intent intent = new Intent(Intent.ActionView, locationURI);
		//	StartActivity(intent);
		//}

		void BtnOpenMapsForRaysStore_Click(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void CreateMapFragment()
		{
			mapFragmant = FragmentManager.FindFragmentByTag("maps") as MapFragment;
			if (mapFragmant != null)
			{
				var googleMapOptions = new GoogleMapOptions().InvokeMapType(GoogleMap.MapTypeSatellite)
															 .InvokeCompassEnabled(true)
															 .InvokeZoomControlsEnabled(true);

				FragmentTransaction transaction = FragmentManager.BeginTransaction();
				mapFragmant = MapFragment.NewInstance(googleMapOptions);
				transaction.Add(Resource.Id.mapFrameLayout, mapFragmant, "maps");
				transaction.Commit();
			}
		}

		private void LoadMaps()
		{
			var localMapReady = new LocalMapReady();
			localMapReady.MapReady += async (object sender, EventArgs e) =>
			{
				googleMap  = (sender as LocalMapReady).Map;
				if (googleMap != null)
				{
					MarkerOptions markeroptions = new MarkerOptions();
					markeroptions.SetPosition(raysStoreLocation);
					markeroptions.SetTitle("Ray's Hot Dogs");
					googleMap.AddMarker(markeroptions);
					CameraUpdate cameraUpdate = CameraUpdateFactory.NewLatLngZoom(raysStoreLocation, 15);
					googleMap.MoveCamera(cameraUpdate);
				}
				
			};

			mapFragmant.GetMapAsync(localMapReady);
		}
	}


	public class LocalMapReady : Java.Lang.Object, IOnMapReadyCallback
	{
		public GoogleMap Map
		{
			get;
			private set;
		}

		public event EventHandler MapReady;

		public void OnMapReady(GoogleMap googleMap)
		{
			Map = googleMap;
			var handler = MapReady;
			if (handler != null)
			{
				handler(this, EventArgs.Empty);
			}
		}
	}
}

