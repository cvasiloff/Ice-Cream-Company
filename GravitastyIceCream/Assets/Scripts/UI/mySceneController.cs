using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mySceneController : MonoBehaviour
{
    public bool beenUsed = false;
    public int toScene;
    public GameObject playerPrefab;
    // Use this for initialization
    void Start()
    {
        SceneManager.sceneLoaded += SceneChanger;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void SceneChanger(Scene s, LoadSceneMode m)
    {
        if (beenUsed && SceneManager.GetSceneByBuildIndex(0).name == s.name)
        {
            Debug.Log("HELP");
            SceneManager.sceneLoaded -= SceneChanger;
            GameObject.Instantiate(playerPrefab, new Vector3(-3.0f, 0.5f, -3.0f), Quaternion.identity);
            Destroy(this.gameObject);
        }


        if (s.name != SceneManager.GetSceneByBuildIndex(0).name)
        {
            Debug.Log("Setting beenUsed to true!");
            beenUsed = true;
        }
    }

    public void switchScenes(int x)
    {

        if (!beenUsed)
        {
            Debug.Log("Loading game scene");
            SceneManager.LoadScene(1);//Or whatever index you want.

            beenUsed = true;
        }
        else
        {
            SceneManager.LoadScene(x);
        }
    }
}
