using UnityEngine;
using System.Collections;

public class bat : MonoBehaviour {

	public Vector2 jumpForce = new Vector2(0,300);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp("space")){
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			GetComponent<Rigidbody2D>().AddForce(jumpForce);
			//rigidbody2D.velocity = Vector2.zero;
			//rigidbody2D.addForce(jumpForce); 

			//para toque
			/* 
			   if(Input.touchCount >= 1)
        		{
		            if(Input.GetTouch(0).phase == TouchPhase.Ended)
		            {
		                rigidbody2D.velocity = Vector2.zero;
		                rigidbody2D.AddForce(jumpForce);
		            }
        		}
			 */

		}

		Vector2 screenPosition = Camera.main.WorldToScreenPoint (transform.position);
		if (screenPosition.y > Screen.height || screenPosition.y < 0) {
			GameOver();
		}

	}

	void OnCollisionEnter2D(Collision2D other){
		GameOver();
	}

	void GameOver(){
		Application.LoadLevel (Application.loadedLevel);
	}

}
