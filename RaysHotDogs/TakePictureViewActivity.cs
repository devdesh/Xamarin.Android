
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;

namespace RaysHotDogs
{
	[Activity(Label = "TakePictureViewActivity")]
	public class TakePictureViewActivity : Activity
	{
		private ImageView imagePictureWithRay;
		private Button btnLaunchCamera;
		private File imageDirectory;
		private File imageFile;
		private Bitmap bitmapImage;

		public TakePictureViewActivity()
		{
			imageDirectory = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "RaysHotDogs");
			if (!imageDirectory.Exists())
			{
				imageDirectory.Mkdirs();
			}
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.TakePictureView);
			FindControls();
			AttachEvent();
		}

		private void FindControls()
		{
			imagePictureWithRay = FindViewById<ImageView>(Resource.Id.imageViewPictureWithRay);
			btnLaunchCamera = FindViewById<Button>(Resource.Id.btnLaunchCamera);
		}

		private void AttachEvent()
		{
			btnLaunchCamera.Click += BtnLaunchCamera_Click;
		}

		void BtnLaunchCamera_Click(object sender, EventArgs e)
		{
			imageFile = new File(imageDirectory, string.Format("PictureWithRay_{0}", Guid.NewGuid()));
			Intent intent = new Intent(MediaStore.ActionImageCapture);
			intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(imageFile));
			StartActivityForResult(intent, 0);
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			int height = imagePictureWithRay.Height;
			int width = imagePictureWithRay.Width;
			bitmapImage = ImageHelper.GetImageBitmapFromFile(imageFile.Path, width, height);
			if (bitmapImage != null)
			{
				imagePictureWithRay.SetImageBitmap(bitmapImage);
				bitmapImage = null;
			}
			//to avoid memory leaks
			GC.Collect();
		}
	}
}

