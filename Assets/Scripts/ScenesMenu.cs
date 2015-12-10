using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class ScenesMenu : MonoBehaviour {

	private GameObject player;
	private Player playerClass;
	private Component[] componentesDeTela;
	public Text textScore;
	public Text textHiScore;
	public bool showBannerOneTime = false;

	// Use this for initialization
	void Start () {
	
		player = GameObject.Find("player");

		playerClass = player.GetComponent<Player>();

		componentesDeTela = GetComponentsInChildren(typeof(CanvasRenderer));

		SetPanelVisivel (false);

		Manager.instance.Load();

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (playerClass.exibirMenu) 
		{
			SetPanelVisivel (true);
			if (!showBannerOneTime) {
				RequestBanner ();
			}

			showBannerOneTime = true;
		} 
		else 
		{
			showBannerOneTime = false;
		}
		textScore.text = "SCORE "+ Manager.instance.GetScore();
		textHiScore.text = "HI SCORE "+ Manager.instance.GetHiScore();
	}

	void SetPanelVisivel(bool visible)
	{
		if (visible) {
			trySaveHiScore();
		}

		foreach (Component b in componentesDeTela)
		{
			GameObject c = ((CanvasRenderer)b).gameObject;
			c.SetActive(visible);
		}

	}

	public void trySaveHiScore()
	{
		if (Manager.instance.GetScore() > Manager.instance.GetHiScore ()) 
		{
			Manager.instance.SetHiScore(Manager.instance.GetScore());
			Manager.instance.Save();
		}
	}
 
	private void RequestInterstitial()
	{
		#if UNITY_ANDROID
		string adUnitId = "Ica-app-pub-6695715042527152/8720779823";
		#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-6695715042527152/8720779823";
		#else
		string adUnitId = "unexpected_platform";
		#endif
		
		// Initialize an InterstitialAd.
		InterstitialAd interstitial = new InterstitialAd(adUnitId);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		interstitial.LoadAd(request);
	}

	private void RequestBanner()
	{
		#if UNITY_ANDROID
		string adUnitId = "ca-app-pub-6695715042527152/7647672622";
		#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-6695715042527152/7647672622";
		#else
		string adUnitId = "unexpected_platform";
		#endif
		
		// Create a 320x50 banner at the top of the screen.
		BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the banner with the request.
		bannerView.LoadAd(request);
	}

 
}
