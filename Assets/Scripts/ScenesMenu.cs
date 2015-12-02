using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScenesMenu : MonoBehaviour {

	private GameObject player;
	private Player playerClass;
	private Component[] componentesDeTela;
	public Text textScore;
	public Text textHiScore;

	// Use this for initialization
	void Start () {
	
		player = GameObject.Find("player");

		playerClass = player.GetComponent<Player>();

		componentesDeTela = GetComponentsInChildren(typeof(CanvasRenderer));

		SetPanelVisivel (false);
	}
	
	// Update is called once per frame
	void Update () {
	
		if (playerClass.exibirMenu) {
			SetPanelVisivel (true);
		}
		textScore.text = "SCORE "+ Manager.instance.GetScore();
		textHiScore.text = "HI SCORE "+ Manager.instance.GetHiScore();
	}

	void SetPanelVisivel(bool invisible)
	{
		foreach (Component b in componentesDeTela)
		{
			Debug.Log(b.name);
			GameObject c = ((CanvasRenderer)b).gameObject;
			c.SetActive(invisible);
		}

	}
}
