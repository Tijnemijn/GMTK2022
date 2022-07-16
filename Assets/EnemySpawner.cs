using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using JetBrains.Annotations;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

public class EnemySpawner : MonoBehaviour
{

    public int enemyAmount;
    private Transform[] locations;
    private int wave;
    private float waveAmount;
    
    [SerializeField] private Enemy dice1Prefab;
    [SerializeField] private Enemy dice2Prefab;
    [SerializeField] private Enemy dice6Prefab;
    // Start is called before the first frame update
    void Start()
    {
        locations =  this.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAmount == 0)
        {
            wave++;
            GetWave();
        }
    }

    private void GetWave()
    {
        waveAmount = waveAmount * 1.15f + 50;
        GetEnemies(waveAmount);
    }

    private void GetEnemies(float amount)
    {
        while (amount > 0)
        {
            int loc = 0;
            if (amount >= 60)
            {
                loc = loc % locations.Length;
                int number = Random.Range(0, 3);
                if (number == 0)
                {
                    var enemy = Instantiate(dice1Prefab, locations[loc].position, new Quaternion(0, 0, 0, 0));
                    enemyAmount++;
                    enemy.Spawner = this;
                    amount -= enemy.MaxHealth;
                }
                else if (number == 1)
                {
                    var enemy = Instantiate(dice2Prefab, locations[loc].position, new Quaternion(0, 0, 0, 0));
                    enemyAmount++;
                    enemy.Spawner = this;
                    amount -= enemy.MaxHealth;
                }
                else
                {
                    var enemy = Instantiate(dice6Prefab, locations[loc].position, new Quaternion(0, 0, 0, 0));
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
                    var enemy = Instantiate(dice1Prefab, locations[loc].position, new Quaternion(0, 0, 0, 0));
                    enemyAmount++;
                    enemy.Spawner = this;
                    amount -= enemy.MaxHealth;
                }
                else if (number == 1)
                {
                    var enemy = Instantiate(dice2Prefab, locations[loc].position, new Quaternion(0, 0, 0, 0));
                    enemyAmount++;
                    enemy.Spawner = this;
                    amount -= enemy.MaxHealth;
                }
            }
            loc++;
        }
    }
}
