using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControleInput : MonoBehaviour {

	public Collider2D colliderEsquerdo;
	public Collider2D colliderDireto;
	public float velocidade;
	private GameObject player;
	private enum direcao{DIREITA, ESQUERDA};
	private direcao direcaoPlayer = direcao.DIREITA;


	// Use this for initialization
	void Start () {
		//camera = GetComponent.<Camera>;
		player = GameObject.Find("player");
	}

	
	// Update is called once per frame
	void Update () {
		#if UNITY_EDITOR_WIN
		// if unity editor
		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log("click");

			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 touchPos = new Vector2(wp.x, wp.y);

			player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,300));

			if(colliderEsquerdo.OverlapPoint(touchPos)) {
				Debug.Log("apertou pra tras");

				if(direcaoPlayer == direcao.DIREITA)
				{
					direcaoPlayer = direcao.ESQUERDA;
					player.transform.eulerAngles = new Vector2(0,180);
				}
			}
			if(colliderDireto.OverlapPoint(touchPos)) {
				Debug.Log("apertou pra frente");

				if(direcaoPlayer == direcao.ESQUERDA)
				{
					direcaoPlayer = direcao.DIREITA;
					player.transform.eulerAngles = new Vector2(0,0);
				}
			}
		}

		if(direcaoPlayer == direcao.DIREITA) {
			transform.Translate (Vector2.right * velocidade * Time.deltaTime);
		}
		else
		{
			transform.Translate (-Vector2.right * velocidade * Time.deltaTime);
		}

		// endif unity editor
		
		#endif
	}
}
