using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Image timeUntilWaveBar;
    public Text waveText;

    public Wave CurrentWave;
    private int EnemyCounter = 0;

    [SerializeField] private Collider spawnPlane;
    public List<BaseEnemy> EnemyTypes;
    public GameObject TestEnemy;
    // Start is called before the first frame update
    void Start()
    {
        CurrentWave = new Wave(1, 1, false);
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            StartSpawning();
        }
    }
    private void UpdateText()
    {
        // waveText.text = "Wave " + CurrentWave.Number + "\n" + CurrentWave.numOfEnemies + "/" + CurrentWave.maxEnemies; 
    }
    
    public void RemoveEnemy()
    {
        CurrentWave.numOfEnemies--;
        if (CurrentWave.numOfEnemies <= 0)
        {
            NextWave();
        }
        UpdateText();
    }
    public void StartSpawning()
    {
        Debug.Log("SPAWNING");
        if(CurrentWave.hasStarted )
        {
            return;
        }
        else
        {
            CurrentWave.hasStarted = true;
            StartCoroutine(Spawn());
        }
        
        
    }
    private IEnumerator Spawn()
    {
        float delay = 1f;
        for (int i = 0; i < CurrentWave.numOfEnemies; i++)
        {
            GameObject enemy = Instantiate(TestEnemy, GetRandomPos(), Quaternion.identity);
            yield return new WaitForSeconds(delay);
        }
    }
    private Vector3 GetRandomPos()
    {
        int side = Random.Range(0, 3);
        
        float x = 0;
        float y = 1;
        float z = 0;
        switch (side)
        {
            case 0: //Left
                x = spawnPlane.bounds.min.x;
                z = Random.Range(spawnPlane.bounds.min.z, spawnPlane.bounds.max.z);
               
                break;
            case 1: //Right
                x = spawnPlane.bounds.max.x;
                z = Random.Range(spawnPlane.bounds.min.z, spawnPlane.bounds.max.z);
                break;
            case 2: //Top
                x = Random.Range(spawnPlane.bounds.min.x, spawnPlane.bounds.max.x);
                z = spawnPlane.bounds.min.z;
                break;
            case 3: //Bottom
                x = Random.Range(spawnPlane.bounds.min.x, spawnPlane.bounds.max.x);
                z = spawnPlane.bounds.max.z;
                break;
        }
        return new Vector3(x, y, z);
        
    }
    public BaseEnemy GetRandomEnemyType()
    {
        return EnemyTypes[Random.Range(0, EnemyTypes.Count)];
    }
    private void NextWave ()
    {
        int num = CurrentWave.Number + 1;
        int noE = 1 + Mathf.RoundToInt(num / 3) + EnemyCounter;
        EnemyCounter++;
        if (num % 5 == 0) EnemyCounter = 0;
        CurrentWave = new Wave(num, noE, (num % 10 == 0));
        UpdateText();
    }
         
    public class Wave
    {
        public int Number;
        public bool hasStarted;
        public int numOfEnemies;
        public bool bossWave;
        public readonly int maxEnemies;
        public Wave(int num, int noE, bool boss)
        {
            Number = num;
            hasStarted = false;
            numOfEnemies = noE;
            maxEnemies = numOfEnemies;
            bossWave = boss;
            
        }
    }
}
