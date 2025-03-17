using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyManager : MonoBehaviour
{
    public static EnemyManager main;
    
    public Transform[] checkpoints;
    public Transform spawnPoint;

    [SerializeField] private GameObject alien1;
    [SerializeField] private GameObject alien2;
    [SerializeField] private GameObject alien3;

    [SerializeField] private int wave = 1;
    [SerializeField] private int enemyCount = 6;
    [SerializeField] private float enemyCountRate = 0.2f;
 
    [SerializeField] private float alien1SpawnRate = 0.5f;
    [SerializeField] private float alien2SpawnRate = 0.4f;
    [SerializeField] private float alien3SpawnRate = 0.1f;

    private bool waveDone = false;
    private List<GameObject> waveset = new List<GameObject>();
    private int enemyLeft;

    private int alien1Count;
    private int alien2Count;
    private int alien3Count;

    void Awake(){
        main = this;
    }

    // Removed SetWave() call from Start().
    // Instead, we'll call it from GameControl.

    void Update(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0 && waveDone){
            wave++;
            enemyCount += Mathf.RoundToInt(enemyCount * enemyCountRate);
            waveDone = false;
            SetWave();
        }

        // TEMP DELETE LATER
        if (Input.GetKeyDown(KeyCode.D)){
            for (int i = 0; i < enemies.Length; i++){
                Destroy(enemies[i]);
            }
        }
    }

    public void StartGame()
    {
        // Called when the game starts (by pressing play).
        SetWave();
    }

    private void SetWave(){
        alien1Count = Mathf.RoundToInt(enemyCount * alien1SpawnRate);
        alien2Count = Mathf.RoundToInt(enemyCount * alien2SpawnRate);
        alien3Count = 0;

        if (wave % 5 == 0){
            alien1Count = Mathf.RoundToInt(enemyCount * alien1SpawnRate);
            alien3Count = Mathf.RoundToInt(enemyCount * alien3SpawnRate);
        }

        enemyLeft = alien1Count + alien2Count + alien3Count;
        enemyCount = enemyLeft;

        waveset = new List<GameObject>();

        for (int i = 0; i < alien1Count; i++){
            waveset.Add(alien1);
        }
        for (int i = 0; i < alien2Count; i++){
            waveset.Add(alien2);
        }
        for (int i = 0; i < alien3Count; i++){
            waveset.Add(alien3);
        }
        waveset = Shuffle(waveset);

        StartCoroutine(spawn());
    }

    public List<GameObject> Shuffle(List<GameObject> waveSet){
        List<GameObject> temp = new List<GameObject>(waveSet);
        List<GameObject> result = new List<GameObject>();
        for (int i = 0; i < waveSet.Count; i++){
            int index = Random.Range(0, temp.Count);
            result.Add(temp[index]);
            temp.RemoveAt(index);
        }
        return result;
    }

    IEnumerator spawn(){
        for (int i = 0; i < waveset.Count; i++){
            Instantiate(waveset[i], spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
        waveDone = true;
    }
}
