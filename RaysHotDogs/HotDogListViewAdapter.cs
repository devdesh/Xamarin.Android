using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using RaysHotDogs.Core;
namespace RaysHotDogs
{
	public class HotDogListViewAdapter : BaseAdapter<HotDog>
	{
		List<HotDog> items;
		Activity context;

		public override int Count
		{
			get
			{
				return items.Count;
			}
		}

		public override HotDog this[int position]
		{
			get
			{
				return items[position];
			}
		}

		public HotDogListViewAdapter(List<HotDog> hotDogList, Activity listViewActivity)
		{
			items = hotDogList;
			context = listViewActivity;
		}

		public override long GetItemId(int position)
		{
			return position;
		}


		//Creating CustomRowStyle for each item
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var item = items[position];
			var imageBitmap = ImageHelper.GetImageFromURL("http://gillcleerenpluralsight.blob.core.windows.net/files/" + item.ImagePath + ".jpg");
			if (convertView == null)
			{
				convertView = context.LayoutInflater.Inflate(Resource.Layout.HotDogRowView, null);
			}
			convertView.FindViewById<ImageView>(Resource.Id.hotDogImageView).SetImageBitmap(imageBitmap);
			convertView.FindViewById<TextView>(Resource.Id.textViewName).Text = item.Name;
			convertView.FindViewById<TextView>(Resource.Id.textViewShortDescription).Text = item.ShortDescription;
			convertView.FindViewById<TextView>(Resource.Id.textViewPrice).Text = "$" + item.Price;
			return convertView;
		}


		//Creating ListViewItem with an image by using Android built in template
		//public override View GetView(int position, View convertView, ViewGroup parent)
		//{
		//	var item = items[position];
		//	var imageBitmap = ImageHelper.GetImageFromURL("http://gillcleerenpluralsight.blob.core.windows.net/files/" + item.ImagePath + ".jpg");
		//	if (convertView == null)
		//	{
		//		convertView = context.LayoutInflater.Inflate(Android.Resource.Layout.ActivityListItem, null);
		//	}
		//	convertView.FindViewById<ImageView>(Android.Resource.Id.Icon).SetImageBitmap(imageBitmap);
		//	convertView.FindViewById<TextView>(Android.Resource.Id.Text1).Text = item.Name;
		//	return convertView;
		//}


		//Creating basic listitemview
		//public override View GetView(int position, View convertView, ViewGroup parent)
		//{
		//	var item = items[position];
		//	if (convertView == null)
		//	{

		//		convertView = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
		//	}
		//	convertView.FindViewById<TextView>(Android.Resource.Id.Text1).Text = item.Name;
		//	return convertView;
		//}


	}
}

