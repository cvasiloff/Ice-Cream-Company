using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // Start is called before the first frame update
    public Material myMat;
    public int spawnLoc = 0;
    public bool isDone = false;
    public Camera thoughtCam;
    public Material thoughtRender;
    public Material thoughtBackgroundRender;

    void Start()
    {
        //myMat = this.GetComponent<Material>();
        //myMat.color = Random.ColorHSV();
        this.GetComponent<Renderer>().material.color = Random.ColorHSV();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
