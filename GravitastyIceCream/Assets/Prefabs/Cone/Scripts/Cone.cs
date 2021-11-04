using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cone : MonoBehaviour
{
    [Header("Set in inspector")]
    public float OffsetThreshold;
    
    [Header("Don't set in inspector")]
    public List<Scoop> ScoopList; // sequence of scoops collected
    
   

    // Start is called before the first frame update
    void Start()
    {
        ScoopList = new List<Scoop>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScoop(Scoop scoop)
    {
        Vector3 worldScale = scoop.transform.localScale;
        scoop.MyRig.isKinematic = true;
        scoop.MyRig.useGravity = false;
        scoop.ScoopIndex = ScoopList.Count;
        Debug.Log("Add Scoop To this line debug");
        if (ScoopList.Count > 0)
        {
            scoop.transform.parent = ScoopList[ScoopList.Count - 1].transform;        
        }            
        else
        {
            scoop.transform.parent = transform;
            //scoop.transform.position = this.transform.position + new Vector3(0,0.5f,0);
            //scoop.transform.rotation = transform.rotation;
        }
            
        // set global scale to 1
        /*
        scoop.transform.localScale = Vector3.one;
        scoop.transform.localScale = new Vector3(1f / scoop.transform.lossyScale.x, 1f / scoop.transform.lossyScale.y, 1f / scoop.transform.lossyScale.z);
        */
        // add to the list
        ScoopList.Add(scoop);
        Debug.Log("ScoopList:");
        foreach (Scoop item in ScoopList)
        {
            Debug.Log(item);
        }

    }

}
