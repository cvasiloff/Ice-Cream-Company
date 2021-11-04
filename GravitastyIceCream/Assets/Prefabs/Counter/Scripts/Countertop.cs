using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countertop : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        Cone cone = other.GetComponent<Cone>();
        Debug.Log("countertopTrigger");
        if (cone && cone.ScoopList.Count > 0)
        {
            ScoopSelect foundOrder = TryDeliverOrder(cone);
            if (foundOrder)
            {
                Debug.Log("Order found");
                // deliver it, reward the player
                foundOrder.SatisfyOrder();
            }
            else
            {
                Debug.Log("No matching order");
                // no matching order, penalize the player
                ScoopSelect[] allOrders = FindObjectsOfType<ScoopSelect>();

                Debug.Log(allOrders.Length);
                for (int i = 0; i < allOrders.Length; i++)
                {
                    Debug.Log(allOrders[i]);
                    StartCoroutine(allOrders[i].FailOrder());
                }
                    
            }
            Debug.Log("DestroyCone");
            Destroy(cone.transform.gameObject);
        }
    }
    // returns reference to a matching outstanding order, or null if none match
    public ScoopSelect TryDeliverOrder(Cone cone)
    {
        ScoopSelect[] orders = GameObject.FindObjectsOfType<ScoopSelect>();
        // check each outstanding order
        for (int i = 0; i < orders.Length; i++)
        {
            bool found = false;
            // check each scoop in the sequence
            if (orders[i].order.Count == cone.ScoopList.Count)
            {
                if (!(orders[i].IsSatisfied))
                {
                    found = true;
                    for (int j = 0; j < orders[i].order.Count; j++)
                    {
                        found = (orders[i].order[j] == cone.ScoopList[j].MyFlavor);
                        if (!found)
                            break;
                    }
                }
            }
            
            if (found)
            {
                return orders[i];
            }
        }
        return null;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
