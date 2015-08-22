using UnityEngine;
using System.Collections;
using UI;
using UnityEngine.UI;
using System;

namespace UI
{
	public class TestListItem : MonoBehaviour
	{
		public Text title;
		public Text description;
		public RawImage image;
		public Text rating;
		public Text playCount;
		public Text author;
		public Texture2D defaultImage;
		public void FillContents(JsonTests.ListElement data)
		{
			title.text = data.title;
			description.text = data.description;
			rating.text = string.Format("Rating {0}/5", data.rating);
			playCount.text = string.Format("Plays {0}", data.playCount);
			author.text = string.Format("Author: {0}", data.author);
			StopAllCoroutines();
			StartCoroutine(SetImage(data.imageURL));
		}
		
		private IEnumerator SetImage(string imageURL)
		{
			image.texture = defaultImage;
			WWW www = new WWW(imageURL);
			yield return www;
			image.texture = www.texture;
		}
	}
}
