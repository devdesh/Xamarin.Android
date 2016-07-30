
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
	[Activity(Label = "Hot Dog Detail")]
	public class HotDogDetailActivity : Activity
	{
		private ImageView imageViewHotDog;
		private TextView textViewHotDogName;
		private TextView textViewShortDescription;
		private TextView textViewPrice;
		private Button btnCancel;
		private EditText txtNumberOfHotDogs;
		private Button btnOrderNow;

		private HotDog selectedHotDog;
		private HotDogDataService hotDogDataService;


		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here
			SetContentView(RaysHotDogs.Resource.Layout.HotDogDetailView);

			hotDogDataService = new HotDogDataService();
			//selectedHotDog = hotDogDataService.GetHotDogByID(1);
			int selectedHotDogID = Intent.GetIntExtra("selectedHotDogID", 1);
			selectedHotDog = hotDogDataService.GetHotDogByID(selectedHotDogID);
			FindViewControls();
			BindData();
			HandleEvents();
		}

		private void FindViewControls()
		{
			imageViewHotDog = FindViewById<ImageView>(Resource.Id.imageView1);
			textViewHotDogName = FindViewById<TextView>(Resource.Id.textViewHotDogName);
			textViewShortDescription = FindViewById<TextView>(Resource.Id.textViewShortDescription);
			txtNumberOfHotDogs = FindViewById<EditText>(Resource.Id.txtNumberOfHotDogs);
			textViewPrice = FindViewById<TextView>(Resource.Id.textViewPrice);
			btnCancel = FindViewById<Button>(Resource.Id.btnCancel);
			btnOrderNow = FindViewById<Button>(Resource.Id.btnOrderNow);
		}

		private void BindData()
		{
			textViewHotDogName.Text = selectedHotDog.Name;
			textViewShortDescription.Text = selectedHotDog.ShortDescription;
			textViewPrice.Text = "Price :" + selectedHotDog.Price + "$";
			var imageBitmap = ImageHelper.GetImageFromURL("http://gillcleerenpluralsight.blob.core.windows.net/files/" + selectedHotDog.ImagePath + ".jpg");
			imageViewHotDog.SetImageBitmap(imageBitmap);                                              
		}

		private void HandleEvents()
		{
			btnOrderNow.Click += OrderNowClickEvent;
			btnCancel.Click += CancelClickEvent;
		}

		private void OrderNowClickEvent(object sender, EventArgs e)
		{
			int numberOfHotDogs = Int32.Parse(txtNumberOfHotDogs.Text);
			//var dialog = new AlertDialog.Builder(this);
			//dialog.SetTitle("Confirmation");
			//dialog.SetMessage("Your order has been added to the cart!");
			//dialog.Show();


			Intent intent = new Intent();
			intent.PutExtra("selectedHotDogID", selectedHotDog.HotDogID);
			intent.PutExtra("noOfHotDogsSelected", numberOfHotDogs);

			SetResult(Result.Ok, intent);
			this.Finish();
		}

		private void CancelClickEvent(object sender, EventArgs e)
		{
		}
	}
}

