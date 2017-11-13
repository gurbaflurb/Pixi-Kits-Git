using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSM_Triggers : MonoBehaviour {

  [SerializeField] public bool SlowTrigger;
  [SerializeField] public bool DestoryTrigger;
  [SerializeField] public bool CorrectTrigger;
  [SerializeField] public bool WrongTrigger;
  [SerializeField] public bool Hazard;
  [SerializeField] public bool HealthFood;
  [SerializeField] public bool JunkFood;

  private float moveSpeed = 1f;

  const float HealthFoodSpeedIncrease = 0.05f;
  const int HAZARD_DAMAGE = 10;

  private void Update()
  {

  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if(DestoryTrigger)
    {
      Destroy(other.gameObject);
    }

    else if(SlowTrigger)
    {
      if(other.GetComponent<HungrySpaceMonkey>() != null)
      {
        other.GetComponent<HungrySpaceMonkey>().Slowed = true;
      }
    }

    else if(CorrectTrigger)
    {
      //change values to make more dificult
    }

    else if(WrongTrigger)
    {
      //change values to make MORE dificult
    }

    else if(Hazard)
    {
      if (other.GetComponent<HungrySpaceMonkey>() != null)
      {
        other.GetComponent<HungrySpaceMonkey>().TakeDamage(HAZARD_DAMAGE);
        Destroy(this.gameObject);
      }
    }

    else if(HealthFood)
    {
      if (other.GetComponent<HungrySpaceMonkey>() != null)
      {
        other.GetComponent<HungrySpaceMonkey>().Boosted = false;
        other.GetComponent<HungrySpaceMonkey>().Speed += HealthFoodSpeedIncrease;
        Destroy(this.gameObject);
      }
    }

    else if(JunkFood)
    {
      if (other.GetComponent<HungrySpaceMonkey>() != null)
      {
        other.GetComponent<HungrySpaceMonkey>().Boosted = true;
        other.GetComponent<HungrySpaceMonkey>().Speed -= HealthFoodSpeedIncrease;
        Destroy(this.gameObject);
      }
    }
  }
}
