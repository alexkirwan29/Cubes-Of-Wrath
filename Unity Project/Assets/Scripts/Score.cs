using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Score : MonoBehaviour {

    #region Instance Stuff
    private static Score sceneInstance;
    public static Score instance
    {
        get
        {
            if (sceneInstance == null)
            {
                sceneInstance = FindObjectOfType<Score>();
                if (sceneInstance == null)
                {
                    Debug.LogWarning("Whoa!, you need at least one Score script in your scene. did you add a camera prefab?");
                }
            }
            return sceneInstance;
        }
    }
    #endregion

    Text score;
    private int currentscore = 0;

    public void AddScore(int value)
    {
        currentscore += value;
        score.text = string.Format("Score: {0}", currentscore);

    }


    // Use this for initialization
    void Start () {
        score = GetComponent<Text>();
        score.text = "Score : " + currentscore;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Space))
            AddScore(564);
	
	}

}
