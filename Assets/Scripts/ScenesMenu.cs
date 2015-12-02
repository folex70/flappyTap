using UnityEngine;
using System.Collections;

public class ScenesMenu : MonoBehaviour {

	private GameObject player;
	private Player playerClass;
	private Component[] componentesDeTela;

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
