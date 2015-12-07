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

		Manager.instance.Load();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (playerClass.exibirMenu) {
			SetPanelVisivel (true);
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
}
