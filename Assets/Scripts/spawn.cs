using UnityEngine;
using System.Collections;

public class spawn : MonoBehaviour {

	public GameObject obstaculos_prefabs;
	public GameObject obstaculos2_prefabs;
	int score = 0;

	// Use this for initialization
	void Start () {
		//InvokeRepeating ("GeraObstaculo", 1f, 1.5f);
		
		//gera obstaculos
		InvokeRepeating("SpawnObstacles",1f, 1f);
        InvokeRepeating("SpawnObstaclesBottom", 1.5f, 1.5f);
    }
	
	// Update is called once per frame
	void Update () {
		//Destroy (obstaculos_prefabs, 3f);
	}


	void OnGUI () {
		GUI.color = Color.black;
		GUILayout.Label("Score: " + score.ToString());
	}

	void GeraObstaculo(){

		if (score > 10) {
			//aumenta nivel de dificuldade
			Instantiate (obstaculos2_prefabs);
		}else{
			Instantiate (obstaculos_prefabs);
		}

		score++;
	}

	void SpawnObstacles()
	{
		//Instantiate (obstaculo_prefab);
		GameObject obj = ObjectPool.current.GetPooledObject ();

		if(obj == null) return;
		
		obj.transform.position = transform.position;
		obj.transform.rotation = transform.rotation;
		obj.SetActive (true);
	}

    
    void SpawnObstaclesBottom()
    {
        //Instantiate (obstaculo_prefab);
        GameObject obj = ObjectPoolBottom.current.GetPooledObject();

        if (obj == null) return;

        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);
    }


}
