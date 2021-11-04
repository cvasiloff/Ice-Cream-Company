using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatRemover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RemoveSplat());
    }

    public IEnumerator RemoveSplat()
    {
        Debug.Log("REMOVING SPLAT");
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
