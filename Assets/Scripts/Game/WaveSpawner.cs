using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{


    public int CurrentWave;

    [SerializeField] private Collider spawnPlane;
    public List<GameObject> EnemyTypes;
    public bool isInRound = false;
    public Transform[] SpawnPoints;

    // Start is called before the first frame update
    // Update is called once per frame
    private void Start()
    {
        StartRound();
        CurrentWave = 1;
    }
    public void EndRound()
    {
        isInRound = false;
        GameObject levelPanel = PanelController.Instance.ShowPanel("Level");
        int baseExpGain = CurrentWave;
        int expGain = Mathf.RoundToInt(baseExpGain + baseExpGain * (GameAssets.I.player.Accuracy / 100));
        levelPanel.transform.GetChild(2).GetComponent<Text>().text = "Exp Gained:\n+" + baseExpGain + " * " + GameAssets.I.player.Accuracy.ToString("0.0") + "% Accuracy = " + expGain;
        GameAssets.I.player.AddExp(expGain);
        GameAssets.I.player.ResetQuiver();
        CurrentWave++;

    }
    public void StartRound()
    {
        
        isInRound = true;
        GameAssets.I.player.ArrowsHit = 0;
        GameAssets.I.player.ArrowsShot = 0;
        GameAssets.I.player.Accuracy = 0;
        PanelController.Instance.CloseAllPanels();
        StartCoroutine(Spawn());
        
    }
    private IEnumerator Spawn() 
    {
        float delay = 1f;
        yield return new WaitForSeconds(delay);
        GameObject enemy = Instantiate(GetRandomEnemyType(), GetRandomPos(), Quaternion.identity);
        enemy.GetComponent<EnemyHealth>().spawner = this;
    }
    private Transform GetRandomSpawn()
    {
        return SpawnPoints[Random.Range(0, SpawnPoints.Length)];
    }
    private Vector3 GetRandomPos()
    {
        int side = Random.Range(0, 3);
        
        float x = 0;
        float y = 0;
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
    public GameObject GetRandomEnemyType()
    {
        return EnemyTypes[Random.Range(0, EnemyTypes.Count)];
    }
   
    
}
