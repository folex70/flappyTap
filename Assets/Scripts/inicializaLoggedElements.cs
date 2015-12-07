using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class inicializaLoggedElements : MonoBehaviour {

	public Text welcomeMessege;
	public Image fotoPerfil;
	// Use this for initialization
	void Awake () {
	
		if (FbHolder.isLogged) {
			welcomeMessege.text = FbHolder.welcomeMessageText;
			fotoPerfil.sprite = FbHolder.fotoPerfilSprite;
		} else {
			gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
