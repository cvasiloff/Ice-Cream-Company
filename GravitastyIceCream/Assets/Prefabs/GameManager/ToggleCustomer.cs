using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCustomer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject customerPrefab;
    public GameObject[] customerSpawns;
    public bool canSpawn = true;
    void Start()
    {
        
    }

    public void ChangeCustomer(GameObject customer)
    {
        customer.GetComponent<Rigidbody>().isKinematic = false;
        //GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(ExitCustomer(customer));
        StartCoroutine(MoveCustomer(customer));  
    }

    public GameObject RandomCustomer()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");

        GameObject tempCust = temp[Random.Range(0, temp.Length)];

        return tempCust;
    }

    public IEnumerator ExitCustomer(GameObject customer)
    {
        yield return new WaitForSeconds(5);
        
        int spawnLoc = customer.GetComponent<PlayerData>().spawnLoc;
        Camera thoughtCam = customer.GetComponent<PlayerData>().thoughtCam;
        Material thoughtBackgroundRender = customer.GetComponent<PlayerData>().thoughtBackgroundRender;
        Material thoughtRender = customer.GetComponent<PlayerData>().thoughtRender;
        Debug.Log(spawnLoc);
        GameObject newCustomer = GameObject.Instantiate(customerPrefab, customerSpawns[spawnLoc].transform.position, customerSpawns[spawnLoc].transform.rotation);
        newCustomer.GetComponent<PlayerData>().spawnLoc = spawnLoc;
        newCustomer.GetComponent<PlayerData>().thoughtCam = thoughtCam;
        newCustomer.GetComponent<PlayerData>().thoughtRender = thoughtRender;
        Destroy(customer);
    }

    public IEnumerator MoveCustomer(GameObject customer)
    {

        customer.GetComponent<Rigidbody>().velocity = this.transform.right.normalized * -5;
        yield return new WaitForSeconds(1f);

     
        customer.GetComponent<Rigidbody>().velocity = this.transform.forward.normalized * -10;

    }


    // Update is called once per frame
    void Update()
    {

    }
}
