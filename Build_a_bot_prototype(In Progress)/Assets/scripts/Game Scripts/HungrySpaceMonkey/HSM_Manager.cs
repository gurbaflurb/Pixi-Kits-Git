using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum Stage { Q1, Q2, Q3, Q4 }

public class HSM_Manager : MonoBehaviour
{

  private GameObject player;

  //Prefab Support
  [SerializeField] GameObject meteorPrefab;
  [SerializeField] GameObject healthyFoodPrefab;
  [SerializeField] GameObject junkFoodPrefab;

  //Slider Support
  private Slider slider;

  //Spawning Support
  private float meteorTimer;
  private float foodTimer;
  private bool canSpawnMeteor;
  private bool canSpawnFood;
  private bool coolingDownMeteor;
  private bool coolingDownFood;
  Vector2 fallingSpeed = Vector2.down * 100f;

  //random spawn support;
  private const float spawnY = 7f;
  private const float maxX = 7.5f;
  private float spawnX;
  private int whatSide;
  private Random random;

  //Question Support
  private GameObject canvas;
  private bool questioning;
  private bool questioningCooldown;

  //Level Increases
  private float Q1Time;
  private float Q2Time;
  private float Q3Time;
  private float Q4Time;

  public bool Questioning
  {
    set { questioning = value; }
  }

  // Use this for initialization
  void Start()
  {
    player = GameObject.Find("Player");
    canvas = GameObject.Find("Canvas");
    slider = GameObject.FindGameObjectWithTag("DistanceSlider").GetComponent<Slider>();
    meteorTimer = 1.5f;
    foodTimer = 3f;
    canSpawnMeteor = true;
    canSpawnFood = true;
    coolingDownMeteor = false;
    coolingDownFood = false;
    questioning = false;
    questioningCooldown = false;

    Q1Time = 10f;
    Q2Time = Q1Time * 2;
    Q3Time = Q2Time * 2;
    Q4Time = Q3Time * 2;

    slider.maxValue = Q1Time + Q2Time + Q3Time + Q4Time;
  }

  // Update is called once per frame
  void Update()
  {
    //check player health and exit if 0
    if(player.GetComponent<HungrySpaceMonkey>().Health >= 0)
    {
      //go back to minigame select?
    }

    if (!questioning)
    {
      //increment deltatime to move slider;
      slider.value += Time.deltaTime;

      if (!questioningCooldown)
      {

      }
      if (canSpawnMeteor)
      {
        SpawnMeteor();
        if (!coolingDownMeteor)
        {
          StartCoroutine(MeteorCooldown(meteorTimer));
        }
      }
      if (canSpawnFood)
      {
        SpawnFood();
        if (!coolingDownFood)
        {
          StartCoroutine(FoodCooldown(foodTimer));
        }
      }
    }
    else
    {
      //do nothing
    }
  }
  //Spawn 3 meteors
  void SpawnMeteor()
  {
    whatSide = Random.Range(0, 2) * 2 - 1;
    spawnX = Random.Range(0, maxX);
    spawnX *= whatSide;

    var meteor = Instantiate(meteorPrefab,new Vector3(spawnX,spawnY,0), Quaternion.identity);

    whatSide = Random.Range(0, 2) * 2 - 1;
    spawnX = Random.Range(0, maxX);
    spawnX *= whatSide;

    var meteor2 = Instantiate(meteorPrefab, new Vector3(spawnX, spawnY, 0), Quaternion.identity);

    whatSide = Random.Range(0, 2) * 2 - 1;
    spawnX = Random.Range(0, maxX);
    spawnX *= whatSide;

    var meteor3 = Instantiate(meteorPrefab, new Vector3(spawnX, spawnY, 0), Quaternion.identity);

    meteor.GetComponent<Rigidbody2D>().AddForce(fallingSpeed);
    meteor2.GetComponent<Rigidbody2D>().AddForce(fallingSpeed);
    meteor3.GetComponent<Rigidbody2D>().AddForce(fallingSpeed);
  }

  //Spawn Junk and Healthy Food
  void SpawnFood()
  {
    var spawnX1 = maxX / 2;
    var spawnX2 = maxX / 2 * -1;

    var healthy = Instantiate(healthyFoodPrefab, new Vector3(spawnX2, spawnY, 0), Quaternion.identity);
    var junk = Instantiate(junkFoodPrefab, new Vector3(spawnX1, spawnY, 0), Quaternion.identity);

    healthy.GetComponent<Rigidbody2D>().AddForce(fallingSpeed);
    junk.GetComponent<Rigidbody2D>().AddForce(fallingSpeed);
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
  IEnumerator QuestionCooldown(float time)
  {
    questioningCooldown = true;
    yield return new WaitForSeconds(time);
    questioning = true;

    //spawn question first

    //spawn correct and incorrect answers on random sides

  }
}
