using System;
using System.Net;
using Android.Graphics;

namespace RaysHotDogs
{
	public class ImageHelper
	{
		public ImageHelper()
		{
		}

		public static Bitmap GetImageFromURL(string url)
		{
			Bitmap imageBitMap = null;
			using (WebClient client = new WebClient())
			{
				var imageBytes = client.DownloadData(url);
				if (imageBytes != null && imageBytes.Length > 0)
				{
					imageBitMap = BitmapFactory.DecodeByteArray(imageBytes,0,imageBytes.Length);

				}
			}
			return imageBitMap;
		}


		public static Bitmap GetImageBitmapFromFile(string fileName, int width, int height)
		{
			//first we get the dimensions of the file on disk
			BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = true };
			BitmapFactory.DecodeFile(fileName, options);

			//next we calculate the ratio with which we need to resize the image inorder to match the requested dimensions
			int outHeight = options.OutHeight;
			int outWidth = options.OutWidth;
			int inSampleSize = 1;

			if (outHeight > height || outWidth > width)
			{
				inSampleSize = outWidth > outHeight ? outHeight / height : outWidth / width;
			}

			//now we will load the image and have the BitmapFactory resize it for us
			options.InSampleSize = inSampleSize;
			options.InJustDecodeBounds = false;
			Bitmap resizedBitmap = BitmapFactory.DecodeFile(fileName, options);
			return resizedBitmap;
		}
	}
}

