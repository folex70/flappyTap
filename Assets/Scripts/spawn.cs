using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class spawn : MonoBehaviour {


	//int score = 0;
    int localLevel = 0;
    public Text textLevel;
    

    // Use this for initialization
    void Start () {
        //InvokeRepeating ("GeraObstaculo", 1f, 1.5f);
        
		//gera obstaculos
		InvokeRepeating ("SpawnBugs",5f, 5f);
		InvokeRepeating ("SpawnObstacles",5f, 5f);
        InvokeRepeating ("SpawnObstaclesBottom", 5.5f, 5.5f);

        Manager.instance.SetLevel(localLevel);
    }
	
	// Update is called once per frame
	void Update () {
        //Destroy (obstaculos_prefabs, 3f);
        localLevel = Manager.instance.GetLevel();
        textLevel.text = "LEVEL "+ localLevel;
        
        LevelUp();
    }

    /*
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
	}*/

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

	void SpawnBugs()
	{
		GameObject obj = ObjectPoolColectbles.current.GetPooledObject ();

		if (obj != null) {
			obj.transform.position = transform.position;
			obj.transform.rotation = transform.rotation;
			obj.SetActive (true);
		}
	}


    void LevelUp()
    {
        int score = Manager.instance.GetScore();
        if(localLevel == 0 && score > 100)
        {
            localLevel = 1;
        }
        else if ((localLevel < 10) && score > (100 * localLevel))
        {
            localLevel++;
            CancelInvoke();
			InvokeRepeating ("SpawnBugs",5f, 5f);
            InvokeRepeating("SpawnObstacles", 2f, 2f);
            InvokeRepeating("SpawnObstaclesBottom", 2.5f, 2.5f);
        }
        else if ((localLevel >= 10) && score > (150 * localLevel))
        {
            localLevel++;
            CancelInvoke();
			InvokeRepeating ("SpawnBugs",2.5f, 2.5f);
            InvokeRepeating("SpawnObstacles", 1f, 1f);
            InvokeRepeating("SpawnObstaclesBottom", 1.5f, 1.5f);
        }
        Manager.instance.SetLevel(localLevel);
    }

}
