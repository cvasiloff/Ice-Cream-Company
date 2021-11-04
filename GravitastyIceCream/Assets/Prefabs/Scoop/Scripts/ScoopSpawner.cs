using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoopSpawner : MonoBehaviour
{
    [Header("Set in inspector")]
    public float SpawnInterval;
    public GameObject ScoopPrefab;
    public float SpawnRadius;
    public float SpawnHeight;
    public Text TimerText;
    public Text ScoreText;
    public float TimeLimit;
    public GameObject ConePrefab;
    public GameObject ConeCounter;

    public float ConeSpacing;

    [Header("Don't set in inspector")]
    public bool CanSpawnScoop;
    public bool GameStarted;
    public float TimeRemaining;
    public List<GameObject> Cones;
    public int Score;





    // Start is called before the first frame update
    void Start()
    {
        CanSpawnScoop = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStarted)
        {
            // try spawn scoop
            if (CanSpawnScoop)
            {
                StartCoroutine(SpawnScoop());
            }

            // update timer
            TimeRemaining -= Time.deltaTime;
            if (TimeRemaining > 0)
                TimerText.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(TimeRemaining / 60), Mathf.FloorToInt(TimeRemaining % 60));
            else
                TimerText.text = "00:00";

            if (TimeRemaining <= 0)
                EndGame();
            ScoreText.text = Score.ToString();
        }
        
    }

    public void StartGame()
    {
        DestroyCones();
        DestroyScoops();
        SpawnCones();
        TimeRemaining = TimeLimit;
        GameStarted = true;
        CanSpawnScoop = true;
        Score = 0;
    }
    public void DestroyCones()
    {
        Cone[] currCones = FindObjectsOfType<Cone>();
        foreach (Cone c in currCones)
            Destroy(c.gameObject);
    }

    public void DestroyScoops()
    {
        Scoop[] currScoops = FindObjectsOfType<Scoop>();
        foreach (Scoop s in currScoops)
            if(!s.IsStatic)
                Destroy(s.gameObject);
    }

    public void SpawnCones()
    {
        // destroy any cones spawned
        foreach (GameObject item in Cones)
        {
            Destroy(item);
        }

        // spawn new cones on the counter
        /*
        for (float x = 0.2f; x < ConeCounter.transform.localScale.x - 0.2f; x += ConeSpacing)
        {
            for (float z = 0.2f; z < ConeCounter.transform.localScale.z - 0.2f; z += ConeSpacing)
            {
                Vector3 SpawnPoint = new Vector3
                    (ConeCounter.transform.position.x - transform.localScale.x/.5f + x,
                    ConeCounter.transform.position.y + ConeCounter.transform.localScale.y + .2f,
                    ConeCounter.transform.position.z - transform.localScale.z/4f + z);
                GameObject.Instantiate(ConePrefab, SpawnPoint, Quaternion.identity);
            }
        }
        */
        for (float x = ConeCounter.transform.position.x - 0.5f*ConeCounter.transform.localScale.x;
            x < ConeCounter.transform.position.x + 0.5f*ConeCounter.transform.localScale.x; x += ConeSpacing)
        {
            for (float z = ConeCounter.transform.position.z - 0.5f*ConeCounter.transform.localScale.z;
                z < ConeCounter.transform.position.z + 0.5f*ConeCounter.transform.localScale.z; z += ConeSpacing)
            {
                Vector3 SpawnPoint = new Vector3(x, ConeCounter.transform.position.y + ConeCounter.transform.localScale.y, z);
                GameObject.Instantiate(ConePrefab, SpawnPoint, Quaternion.identity);
            }
        }
    }

    public void EndGame()
    {
        TimeRemaining = 0;
        GameStarted = false;
    }

    public IEnumerator SpawnScoop()
    {
        CanSpawnScoop = false;
        Vector3 spawnPoint = transform.position +
            new Vector3(Random.Range(-SpawnRadius, SpawnRadius), SpawnHeight, Random.Range(-SpawnRadius, SpawnRadius));
        GameObject temp = GameObject.Instantiate(ScoopPrefab, spawnPoint, Quaternion.identity);
        temp.GetComponent<Scoop>().SetFlavor((Flavor)(Random.Range(0, 3)));
        yield return new WaitForSeconds(SpawnInterval);
        CanSpawnScoop = true;
    }

    

    
}
