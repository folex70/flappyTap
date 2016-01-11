using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class GoogleMobileAdsDemoHandler : IInAppPurchaseHandler
{
	private readonly string[] validSkus = { "android.test.purchased" };
	
	//Will only be sent on a success.
	public void OnInAppPurchaseFinished(IInAppPurchaseResult result)
	{
		result.FinishPurchase();
		ScenesMenu.OutputMessage = "Purchase Succeeded! Credit user here.";
	}
	
	//Check SKU against valid SKUs.
	public bool IsValidPurchase(string sku)
	{
		foreach(string validSku in validSkus) {
			if (sku == validSku) {
				return true;
			}
		}
		return false;
	}
	
	//Return the app's public key.
	public string AndroidPublicKey
	{
		//In a real app, return public key instead of null.
		get { return null; }
	}
}

public class ScenesMenu : MonoBehaviour {
	
	private GameObject player;
	private Player playerClass;
	private Component[] componentesDeTela;
	public Text textScore;
	public Text textHiScore;
	public bool showBannerOneTime = false;
	public bool destroyBannerOneTime = false;
	public bool criouBanner = false;
	private BannerView bannerView;
	private InterstitialAd interstitial;
	private static string outputMessage = "";
	
	// Use this for initialization
	void Start () {
		
		player = GameObject.Find("player");
		
		playerClass = player.GetComponent<Player>();
		
		componentesDeTela = GetComponentsInChildren(typeof(CanvasRenderer));
		
		SetPanelVisivel (false);
		
		Manager.instance.Load();

		RequestBanner ();
		bannerView.Hide();
	}

	public static string OutputMessage
	{
		set { outputMessage = value; }
	}

	void OnDestroy()
	{
		bannerView.Hide();
		bannerView.Destroy();
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
			destroyBannerOneTime = false;
		} 
		else 
		{
		//	if (!destroyBannerOneTime)
		//	{
				//RequestBanner("hide");
				//if(criouBanner == true){
					//bannerView.Hide();
					//bannerView.Destroy();
				//}

		//	}
			showBannerOneTime = false;
			destroyBannerOneTime = true;
			
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
		
		// Create an interstitial.
		interstitial = new InterstitialAd(adUnitId);
		// Register for ad events.
		interstitial.AdLoaded += HandleInterstitialLoaded;
		interstitial.AdFailedToLoad += HandleInterstitialFailedToLoad;
		interstitial.AdOpened += HandleInterstitialOpened;
		interstitial.AdClosing += HandleInterstitialClosing;
		interstitial.AdClosed += HandleInterstitialClosed;
		interstitial.AdLeftApplication += HandleInterstitialLeftApplication;
		GoogleMobileAdsDemoHandler handler = new GoogleMobileAdsDemoHandler();
		interstitial.SetInAppPurchaseHandler(handler);
		// Load an interstitial ad.
		interstitial.LoadAd(createAdRequest());
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
		bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
		// Register for ad events.
		bannerView.AdLoaded += HandleAdLoaded;
		bannerView.AdFailedToLoad += HandleAdFailedToLoad;
		bannerView.AdOpened += HandleAdOpened;
		bannerView.AdClosing += HandleAdClosing;
		bannerView.AdClosed += HandleAdClosed;
		bannerView.AdLeftApplication += HandleAdLeftApplication;
		// Load a banner ad.
		bannerView.LoadAd(createAdRequest());
		
		criouBanner = true;
	}

	// Returns an ad request with custom ad targeting.
	private AdRequest createAdRequest()
	{
		return new AdRequest.Builder()
			.AddTestDevice(AdRequest.TestDeviceSimulator)
				.AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
				.AddKeyword("game")
				.SetGender(Gender.Male)
				.SetBirthday(new DateTime(1985, 1, 1))
				.TagForChildDirectedTreatment(false)
				.AddExtra("color_bg", "9B30FF")
				.Build();
		
	}
	
	private void ShowInterstitial()
	{
		if (interstitial.IsLoaded())
		{
			interstitial.Show();
		}
		else
		{
			print("Interstitial is not ready yet.");
		}
	}

	#region Banner callback handlers
	
	public void HandleAdLoaded(object sender, EventArgs args)
	{
		print("HandleAdLoaded event received.");
	}
	
	public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		print("HandleFailedToReceiveAd event received with message: " + args.Message);
	}
	
	public void HandleAdOpened(object sender, EventArgs args)
	{
		print("HandleAdOpened event received");
	}
	
	void HandleAdClosing(object sender, EventArgs args)
	{
		print("HandleAdClosing event received");
	}
	
	public void HandleAdClosed(object sender, EventArgs args)
	{
		print("HandleAdClosed event received");
	}
	
	public void HandleAdLeftApplication(object sender, EventArgs args)
	{
		print("HandleAdLeftApplication event received");
	}
	
	#endregion
	
	#region Interstitial callback handlers
	
	public void HandleInterstitialLoaded(object sender, EventArgs args)
	{
		print("HandleInterstitialLoaded event received.");
	}
	
	public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		print("HandleInterstitialFailedToLoad event received with message: " + args.Message);
	}
	
	public void HandleInterstitialOpened(object sender, EventArgs args)
	{
		print("HandleInterstitialOpened event received");
	}
	
	void HandleInterstitialClosing(object sender, EventArgs args)
	{
		print("HandleInterstitialClosing event received");
	}
	
	public void HandleInterstitialClosed(object sender, EventArgs args)
	{
		print("HandleInterstitialClosed event received");
	}
	
	public void HandleInterstitialLeftApplication(object sender, EventArgs args)
	{
		print("HandleInterstitialLeftApplication event received");
	}
	
	#endregion
	
}