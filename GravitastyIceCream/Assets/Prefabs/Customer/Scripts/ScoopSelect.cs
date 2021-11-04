using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoopSelect : MonoBehaviour
{
    [Header("Set in inspector")]
    public GameObject StaticCone;
    public GameObject StaticScoop;
    public GameObject ThoughtBubble;
    public Material HappyMaterial;
    public Material ConfusedMaterial;

    [Header("Don't set in inspector")]
    public List<Flavor> order = new List<Flavor>();
    public int maxScoops = 5;
    public GameObject ThoughtObject;
    public bool IsSatisfied;
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        int scoopNum = Random.Range(1, maxScoops + 1);
        ThoughtBubble.GetComponent<Renderer>().material = GetComponent<PlayerData>().thoughtRender;
        //Debug.Log(displayOrder.name);
        ScoopOrder(scoopNum);
        UpdateOrderThoughtObject();
    }

    public void ScoopOrder(int scoopAmount)
    {
        for(int i = 0; i < scoopAmount; i++)
        {
            Flavor choice = (Flavor)(Random.Range(0, System.Enum.GetNames(typeof(Flavor)).Length));
            order.Add(choice);
        }
        
    }

    // update thought objects to show the order
    public void UpdateOrderThoughtObject()
    {
        PlayerData pData = this.GetComponent<PlayerData>();
        Vector3 spawnPoint = pData.thoughtCam.transform.position + pData.thoughtCam.transform.forward * 1.6f;
        if (ThoughtObject) // destroy it if it exists
            Destroy(ThoughtObject);

        ThoughtObject = GameObject.Instantiate(StaticCone, spawnPoint + new Vector3(0, -0.3f, 0), Quaternion.identity);
        for (int i = 0; i < order.Count; i++)
        {
            GameObject temp = GameObject.Instantiate(StaticScoop, spawnPoint + new Vector3(0, 0.8f*StaticScoop.transform.localScale.y*i-0.2f, 0), Quaternion.identity);
            temp.transform.parent = ThoughtObject.transform;
            Scoop scoop = temp.GetComponent<Scoop>();
            scoop.Start();
            scoop.SetFlavor(order[i]);
        }
    }

    public void SatisfyOrder()
    {
        ToggleCustomer tc = FindObjectOfType<ToggleCustomer>();
        Destroy(ThoughtObject.gameObject);
        ScoopSpawner spawner = FindObjectOfType<ScoopSpawner>();
        spawner.Score += 10 * order.Count;
        tc.ChangeCustomer(this.gameObject);
        ThoughtBubble.GetComponent<Renderer>().material = HappyMaterial;
        IsSatisfied = true;
    }

    public IEnumerator FailOrder()
    {
        // inform the user
        ThoughtBubble.GetComponent<Renderer>().material = ConfusedMaterial;
        yield return new WaitForSeconds(1f);
        ThoughtBubble.GetComponent<Renderer>().material = GetComponent<PlayerData>().thoughtRender;
    }
}
