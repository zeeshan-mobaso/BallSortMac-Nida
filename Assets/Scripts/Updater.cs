using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading;
using UnityEngine.Networking;
//https://www.dropbox.com/s/gyi526u5izumji4/Antistress%20Version%20Android.txt?dl=0
//https://www.dropbox.com/s/gyi526u5izumji4/Antistress%20Version%20Android.txt?dl=1
public class Updater : MonoBehaviour
{
	public static Updater instance;
	public string Dropbox_File_URL = "https://ca-apps-n.s3.us-east-2.amazonaws.com/Games/BallSortData.txt";
	private void Awake()
	{
		instance = this;
	}

	void Start()
	{
		if (Application.internetReachability != NetworkReachability.NotReachable)
		{
			StartCoroutine(Getversion());
		}
	}


	IEnumerator Getversion()
	{

		UnityWebRequest uwr = UnityWebRequest.Get(Dropbox_File_URL);
		yield return uwr.SendWebRequest();
		string[] file = uwr.downloadHandler.text.Split(',');
		PlayerPrefs.SetString("AdsCanBeDisplayed", "YES");
		//ShowAd.Instance.ShowBanner();
        foreach (string s in file) {
			if (s == "ShouldShowAdsYES")
            {
				PlayerPrefs.SetString("AdsCanBeDisplayed", "YES");
				//Debug.LogError("asdfdaddgfadd");
				//ShowAd.Instance.ShowBanner();
				break;
            }
           
		}
	}
}
