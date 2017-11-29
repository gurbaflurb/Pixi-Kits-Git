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

    //var x = Input.GetAxis("Horizontal");
    //var y = Input.GetAxis("Vertical");
    bool up = Input.GetKey(KeyCode.W);
    bool left = Input.GetKey(KeyCode.A);
    bool down = Input.GetKey(KeyCode.S);
    bool right = Input.GetKey(KeyCode.D);

    if (boosted)
    {
      boostedSpeed = 1.5f;
      StartCoroutine(SugarRush());
    }
    else
    {
      boostedSpeed = 1f;
    }

    if(right)//if(x > 0)
    {
      transform.position += Vector3.right * speed * boostedSpeed;
    }
    else if(left)//if(x < 0)
    {
      transform.position -= Vector3.right * speed * boostedSpeed;
    }
    if(up)//if (y > 0)
    {
      transform.position += Vector3.up * speed * boostedSpeed;
    }
    else if(down)//if(y < 0)
    {
      transform.position -= Vector3.up * speed * boostedSpeed;
    }
  }

  public void TakeDamage(int damage)
  {
    health -= damage;
  }

  IEnumerator SugarRush()
  {
    yield return new WaitForSeconds(boostedTimer);
    boosted = false;
  }
}
