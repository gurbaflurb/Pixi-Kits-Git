using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungrySpaceMonkey : MonoBehaviour {

  private float speed;
  private bool slowed;
  private int health;

  private bool boosted;
  private float boostedTimer;
  private float boostedSpeed;

  public int Health
  {
    get { return health; }
  }
  public bool Slowed
  {
    get { return slowed; }
    set { slowed = value; }
  }
  public float Speed
  {
    get { return speed; }
    set { speed = value; }
  }
  public bool Boosted
  {
    get { return boosted; }
    set { boosted = value; }
  }

	// Use this for initialization
	void Start () {
    health = 100;
    speed = 0.1f;
    boostedSpeed = 1.5f;
    boostedTimer = 3f;
    boosted = false;
    slowed = false;
	}
	
	// Update is called once per frame
	void Update () {
    var x = Input.GetAxis("Horizontal");
    var y = Input.GetAxis("Vertical");

    if(boosted)
    {
      boostedSpeed = 3f;
      StartCoroutine(SugarRush());
    }
    else
    {
      boostedSpeed = 1f;
    }

    if(x > 0)
    {
      transform.position += Vector3.right * speed * boostedSpeed;
    }
    else if(x < 0)
    {
      transform.position -= Vector3.right * speed * boostedSpeed;
    }

    if (y > 0)
    {
      transform.position += Vector3.up * speed;
    }
    else if(y < 0)
    {
      transform.position -= Vector3.up * speed;
    }
  }

  public void TakeDamage(int damage)
  {
    health -= damage;
    if(health < 0)
    {
      //end game somehow (return to beginning or minigame select?)
    }
  }

  IEnumerator SugarRush()
  {
    yield return new WaitForSeconds(boostedTimer);
    boosted = false;
  }
}
