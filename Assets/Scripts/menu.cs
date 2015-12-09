using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour {


	public void play () {
		Application.LoadLevel ("level_caverna_dark");
	}

	public void reloadLevel(){
		Application.LoadLevel(Application.loadedLevel);
	}

	public void menuPrincipal () {
		Application.LoadLevel ("menu");
	}

	public void exit()
	{
		Application.Quit ();
	}


}
