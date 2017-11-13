using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Stage { Q1, Q2, Q3, Q4 }

public class HSM_Manager : MonoBehaviour
{

  private GameObject player;

  //Prefab Support
  [SerializeField] GameObject meteorPrefab;
  [SerializeField] GameObject healthyFoodPrefab;
  [SerializeField] GameObject junkFoodPrefab;

  //Spawning Support
  private float meteorTimer;
  private float foodTimer;
  private bool canSpawnMeteor;
  private bool canSpawnFood;
  private bool coolingDownMeteor;
  private bool coolingDownFood;

  //random spawn support;
  const float spawnY = 7f;
  const float maxX = 7.5f;
  float spawnX;
  int whatSide;
  Random random;

  private bool questioning;

  //Level Increases
  private float Q1Time;
  private float Q2Time;
  private float Q3Time;
  private float Q4Time;

  // Use this for initialization
  void Start()
  {
    player = GameObject.Find("Player");
    meteorTimer = 5f;
    foodTimer = 5f;
    canSpawnMeteor = true;
    canSpawnFood = true;
    coolingDownMeteor = false;
    coolingDownFood = false;
    questioning = false;

    Q1Time = 10f;
    Q2Time = Q1Time * 2;
    Q3Time = Q2Time * 2;
    Q4Time = Q3Time * 2;
  }

  // Update is called once per frame
  void Update()
  {
    if (!questioning)
    {
      if (canSpawnMeteor)
      {
        SpawnMeteor();
      }
      else
      {
        if(!coolingDownMeteor)
        {
          StartCoroutine(MeteorCooldown(meteorTimer));
        }
      }
      if (canSpawnFood)
      {
        SpawnFood();
      }
      else
      {
        if (!coolingDownFood)
        {
          StartCoroutine(FoodCooldown(foodTimer));
        }
      }
      /*
       * If can spawn meteor spawn meteor
       *  set can spawn to false
       *  
       * If can spawn food spawn food
       *  set can spawn to false
       *  
       *  if cant spawn increment timer until cooldown is over;
       *    set can spawn to true
       *   
       * if time >= Q1Time spawn slowed collider
       * 
       * if player.Slowed = true
       *  spawn question
       *  spawn answer correct/wrong triggers
       *  
      */
    }
  }
  void SpawnMeteor()
  {
    whatSide = Random.Range(0, 2) * 2 - 1;
    spawnX = Random.Range(0, maxX);
    spawnX *= whatSide;

    var meteor = Instantiate(meteorPrefab,new Vector3(spawnX,spawnY,0), Quaternion.identity);
  }
  void SpawnFood()
  {
    var spawnX1 = maxX / 2;
    var spawnX2 = maxX / 2 * -1;

    var healthy = Instantiate(healthyFoodPrefab, new Vector3(spawnX2, spawnY, 0), Quaternion.identity);
    var junk = Instantiate(junkFoodPrefab, new Vector3(spawnX1, spawnY, 0), Quaternion.identity);
  }
  IEnumerator MeteorCooldown(float time)
  {
    canSpawnMeteor = false;
    coolingDownMeteor = true;
    yield return new WaitForSeconds(time);
    canSpawnMeteor = true;
    coolingDownMeteor = false;
  }
  IEnumerator FoodCooldown(float time)
  {
    canSpawnFood = false;
    coolingDownFood = true;
    yield return new WaitForSeconds(time);
    canSpawnFood = true;
    coolingDownFood = false;
  }
}
