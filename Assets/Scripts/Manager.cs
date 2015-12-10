using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class Manager : MonoBehaviour {

    private int _currentLevel;
    private int _currentScore;
	private int _maxScore;

    public static Manager instance;

    void Awake()
    {
        if (instance == null) {
			//DontDestroyOnLoad(gameObject);
			instance = this;
		} 
		else if (instance != this) 
		{
			Destroy (gameObject);
		}
    }

    public void SetLevel(int num)
    {
        _currentLevel = num;
    }

    public int GetLevel()
    {
        return _currentLevel;
    }

    public void SetScore(int num)
    {
        _currentScore = _currentScore + num;
    }

    public int GetScore()
    {
        return _currentScore;
    }

	public void SetHiScore(int num)
	{
		_maxScore = num;
	}
	
	public int GetHiScore()
	{
		return _maxScore;
	}

	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");
		PlayerData data = new PlayerData ();
		data.maxScore = _maxScore;

		bf.Serialize (file, data);
		file.Close ();

		if (FbHolder.isLogged) {
			FbHolder.UpdateScore (_maxScore);
		}

	}

	public void Load()
	{
		if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();

			_maxScore = data.maxScore;

		}

		if (FbHolder.isLogged) {
			int fb_score = FbHolder.loadHiScore();
			if(fb_score > _maxScore){
				_maxScore = fb_score;
			}
		}
	}
   
}

[Serializable]
class PlayerData
{
	public int maxScore;
}

