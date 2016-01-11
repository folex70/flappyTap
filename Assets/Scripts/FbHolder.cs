using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class FbHolder : MonoBehaviour {

	private static string nomeKey = "first_name";
	private static string sobreNomeKey = "last_name";
	private static string fotoKey = "picture";
	public static bool isLogged;
	public static bool isPublishPermissionTrue = true;
	public GameObject loggedElements;
	public GameObject notLoggedElements;
	public Text welcomeMessege;
	public Image fotoPerfil;
	public static string welcomeMessageText;
	public static Sprite fotoPerfilSprite;
	private IEnumerable<string> permissoes = new string[]{"public_profile", "email","publish_actions"};
	static int maxScore;
	Dictionary<string, string> profile;

	void Awake() 
	{
		if (!FB.IsInitialized) {
			FB.Init (SetInit, onUnityHide);
		} else {
			ReconfiguraElementos ();
		}


		DontDestroyOnLoad(loggedElements);

	}

	void SetInit()
	{
		isLogged = FB.IsLoggedIn;
		ReconfiguraElementos ();
		Debug.Log ("Logado: " + isLogged);
	}

	void onUnityHide(bool isGameShowing)
	{
		if(!isGameShowing)
		{
			Time.timeScale = 0; // pausa o jogo
		}
		else
		{
			Time.timeScale = 1; // despausa o jogo
		}
	}

	public void FbLoggin()
	{
		FB.Login ("email,publish_actions", LogginCallBack);
	}

	public void FbLogOut()
	{
		FB.Logout ();
		isLogged = FB.IsLoggedIn;
		ReconfiguraElementos ();
	}

	void LogginCallBack(FBResult result)
	{
		isLogged = FB.IsLoggedIn;
		ReconfiguraElementos ();

		if (isLogged) {
			configuraPerfil ();
		}
	}

	void ReconfiguraElementos ()
	{
		if (loggedElements != null) {
			loggedElements.SetActive (isLogged);
		}
		if (notLoggedElements != null) {
			notLoggedElements.SetActive (!isLogged);
		}
	}

	void configuraPerfil ()
	{
		// get profile picture code
		FB.API (Util.GetPictureURL("me", 128, 128), Facebook.HttpMethod.GET, configuraFoto);
		FB.API ("/me?fields=id,first_name", Facebook.HttpMethod.GET, configuraNome);
		// get username code

	}

	/*void configuraFoto ()
	{
		FB.API("/me/picture?redirect=false", HttpMethod.GET, ProfilePhotoCallback);
	}*/

	void configuraNome (FBResult result)
	{

		if(result.Error != null)
		{
			Debug.Log ("problem with getting profile picture");
			
			FB.API ("/me?fields=id,first_name", Facebook.HttpMethod.GET, configuraNome);
			return;
		}
		
		profile = Util.DeserializeJSONProfile(result.Text);

		welcomeMessageText = "Hello, " + profile["first_name"];
		welcomeMessege.text = welcomeMessageText;
	}

	void configuraFoto (FBResult result)
	{
		if(result.Error != null)
		{
			Debug.Log ("problem with getting profile picture");
			
			FB.API (Util.GetPictureURL("me", 128, 128), Facebook.HttpMethod.GET, configuraFoto);
			return;
		}
 
		// create sprite from received texture
		fotoPerfilSprite = Sprite.Create (result.Texture, new Rect(0,0,128,128), new Vector2(0,0));

		fotoPerfil.sprite = fotoPerfilSprite;
	}

	public static void UpdateScore (int _maxScore)
	{
		Dictionary<string,string> scoreData = new Dictionary<string,string> ();
		scoreData ["score"] = _maxScore.ToString();
		FB.API ("/me/scores", Facebook.HttpMethod.POST, delegate(FBResult result) {
			Debug.Log ("Score submit result: " + result.Text);
		}, scoreData);
	}

	public static int loadHiScore ()
	{
		//throw new NotImplementedException ();
		return 0;
	}
}
