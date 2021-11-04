using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Flavor
{
    Vanilla,
    Chocolate,
    Strawberry
}
public class Scoop : MonoBehaviour
{
    [Header("Set in inspector")]
    public float DestroyHeight;
    public bool IsStatic;
    public GameObject splatPrefab;

    [Header("Don't set in inspector")]
    public Rigidbody MyRig;
    public Flavor MyFlavor;
    public Cone MyCone;
    public Renderer MyRenderer;
    public int ScoopIndex; // index on the cone

    // Start is called before the first frame update
    public void Start()
    {
        MyRig = GetComponent<Rigidbody>();
        MyRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsStatic && MyCone)
        {
            if (transform.parent != null)
            {
                if (CheckIfUnstable())
                {
                    Collapse(0);
                }
            }
            else
            {
                /*
                transform.localScale = Vector3.one;
                transform.localScale = new Vector3(1f / transform.lossyScale.x, 1f / transform.lossyScale.y, 1f / transform.lossyScale.z);
                */
            }
            if (transform.position.y < DestroyHeight)
            {
                Destroy(this);
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsStatic && !MyCone) // only care if not already on a cone
        {
            Cone cone = collision.gameObject.GetComponent<Cone>();
            Transform possibleCone = collision.gameObject.transform;

           
            if (!cone)
            {
                // go up the heirarchy until no more parents or a cone is found
                while (!cone && possibleCone.transform.parent != null)
                {
                    possibleCone = possibleCone.parent;
                    cone = possibleCone.GetComponent<Cone>();
                }
            }

            

            // collision with an empty cone OR topmost scoop of a cone
            if (cone)
            {
                /*
                if (cone.ScoopList.Count > 0)
                {
                    Debug.Log("hit with:");
                    Debug.Log(collision.GetContact(0).otherCollider.gameObject.GetComponent<Scoop>().GetInstanceID());
                    Debug.Log("my cone:");
                
                    Debug.Log(cone.ScoopList[cone.ScoopList.Count - 1]);
                    if (cone.ScoopList[cone.ScoopList.Count - 1])
                        Debug.Log(cone.ScoopList[cone.ScoopList.Count - 1].GetInstanceID());
                }
                */
                
                if ((cone.ScoopList.Count == 0 && collision.GetContact(0).otherCollider.gameObject.name == "ScoopDetector") ||
               (cone.ScoopList.Count != 0 && collision.GetContact(0).otherCollider.gameObject.GetComponent<Scoop>() == cone.ScoopList[cone.ScoopList.Count - 1]))
                {
                    // add it to the cone
                    MyCone = cone;
                    cone.AddScoop(this);
                }
            }

            else if(collision.gameObject.tag == "Floor")
            {
                SpawnSplat();
            }
            
        }
        
    }

    // check if the scoop is going to fall off
    public bool CheckIfUnstable()
    {
        Vector3 fromCone = -MyCone.transform.position + transform.position;
        fromCone = new Vector3(fromCone.x, 0, fromCone.z); // just ignore the Y distance for now'
        
        if (fromCone.magnitude > MyCone.OffsetThreshold || ((MyCone.transform.parent != null) && (MyCone.transform.parent.up.y < 0)))
        {
            Debug.Log(transform.parent.up);
            return true;
        }

        return false;
    }

    // detach this scoop, and detach a subsequent scoop, if it exists
    public void Collapse(int recur)
    {
        Debug.Log("COLLAPSE");
        transform.parent = null;
        MyRig.useGravity = true;
        MyRig.isKinematic = false;
        MyCone.ScoopList.RemoveAt(ScoopIndex-recur);
        if (MyCone.ScoopList.Count > (ScoopIndex - recur))
            MyCone.ScoopList[ScoopIndex - recur].Collapse(++recur);
        
    }

    public void SetFlavor(Flavor flavor)
    {
        if (!MyRenderer)
            MyRenderer = this.GetComponent<Renderer>();
        MyFlavor = flavor;
        switch (flavor)
        {
            case Flavor.Vanilla:
                MyRenderer.material.color = Color.white;
                break;
            case Flavor.Chocolate:
                MyRenderer.material.color = new Color(0.7f, 0.35f, 0f);
                break;
            case Flavor.Strawberry:
                MyRenderer.material.color = new Color(255/ 255f, 135 / 255f, 211 / 255f);
                break;
        }
    }

    public Color SetSplatColor(Flavor flavor)
    {
        switch (flavor)
        {
            case Flavor.Vanilla:
                return Color.white;
            case Flavor.Chocolate:
                return new Color(0.7f, 0.35f, 0f);
            case Flavor.Strawberry:
                return new Color(1f, 0.65f, 0.65f);
            default:
                return Color.black;
        }
    }
    public void SpawnSplat()
    {
        GameObject splat = GameObject.Instantiate(splatPrefab, this.transform.position, Quaternion.identity);
        splat.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = SetSplatColor(MyFlavor);
        Destroy(this.gameObject);
    }
}
