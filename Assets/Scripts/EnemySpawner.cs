using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using JetBrains.Annotations;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private Window diceWindow;
    [SerializeField] private ArenaTile[] arenaTiles;
    [Space]
    
    public int enemyAmount;
    private Transform[] locations;
    public int wave;
    private float waveAmount;
    private bool wasDicewindow;
    private bool isCountingDown = false;
    private bool isSpawningEnemies = false;
    [SerializeField]private float spawnDelay = 0.6f;
    
    [Space]
    [SerializeField] private Enemy dice1Prefab;
    [SerializeField] private Enemy dice2Prefab;
    [SerializeField] private Enemy dice6Prefab;
    // Start is called before the first frame update
    void Start()
    {
        locations =  GetComponentsInChildren<Transform>();
        UIManager.Instance.OnCountDownComplete += GetWave;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAmount == 0 && !isSpawningEnemies)
        {            
            if (!wasDicewindow && !isCountingDown)
            {
                diceWindow.Open();
                wasDicewindow = true;               
            }

            if (diceWindow.IsOpen)
            {
                StartCoroutine(Wait());
            }
            else if (!isCountingDown)
            {
                UIManager.Instance.StartCountdown();
                wave++;
                wasDicewindow = false;
                isCountingDown = true;
                ChangeTiles();
            }

        }
    }

    private void ChangeTiles()
    {
        
        foreach (var tile in arenaTiles)
        {
            ArenaTileType type = (ArenaTileType)Random.Range(0, 3);
            tile.SwitchToType(type);
        }
        
    }

    private IEnumerator Wait()
    {
        yield return Utils.WaitNonAlloc(spawnDelay);
    } 

    private void GetWave()
    {
        isCountingDown = false;   
        waveAmount = 100 * wave * Mathf.Pow(1.01f, wave);
        int spawn1 = Random.Range(0, locations.Length);
        int spawn2 = Random.Range(0, locations.Length);;
        while (spawn1 == spawn2)
        {
            spawn2 = Random.Range(0, locations.Length);
        }

        Transform[] spawns = { locations[spawn1], locations[spawn2] };
        StartCoroutine(GetEnemies(waveAmount, spawns));
    }

    private IEnumerator GetEnemies( float amount, Transform[] spawns)
    {
        isSpawningEnemies = true;
        int loc = 0;
        while (amount > 0)
        {
            if (loc == 2)
            {
                loc = 0;
                yield return Utils.WaitNonAlloc(spawnDelay);
            }
            
            if (amount >= 150)
            {
                int number = Random.Range(0, 3);
                if (number == 0)
                {
                    var enemy = Instantiate(dice1Prefab, spawns[loc].position, new Quaternion(0, 0, 0, 0));
                    enemyAmount++;
                    enemy.Spawner = this;
                    amount -= enemy.MaxHealth;
                }
                else if (number == 1)
                {
                    var enemy = Instantiate(dice2Prefab, spawns[loc].position, new Quaternion(0, 0, 0, 0));
                    enemyAmount++;
                    enemy.Spawner = this;
                    amount -= enemy.MaxHealth;
                }
                else
                {
                    var enemy = Instantiate(dice6Prefab, spawns[loc].position, new Quaternion(0, 0, 0, 0));
                    enemyAmount++;
                    enemy.Spawner = this;
                    amount -= enemy.MaxHealth;
                }

            }
            else
            {
                int number = Random.Range(0, 2);
                if (number == 0)
                {
                    var enemy = Instantiate(dice1Prefab, spawns[loc].position, new Quaternion(0, 0, 0, 0));
                    enemyAmount++;
                    enemy.Spawner = this;
                    amount -= enemy.MaxHealth;
                }
                else if (number == 1)
                {
                    var enemy = Instantiate(dice2Prefab, spawns[loc].position, new Quaternion(0, 0, 0, 0));
                    enemyAmount++;
                    enemy.Spawner = this;
                    amount -= enemy.MaxHealth;
                }
            }
            loc++;
        }
        isSpawningEnemies = false;
    }
}
