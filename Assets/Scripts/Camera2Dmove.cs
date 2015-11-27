using UnityEngine;
using System.Collections;

public class Camera2Dmove : MonoBehaviour {


	public float velocidade = 3;
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D> ().velocity = Vector2.right * velocidade;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
