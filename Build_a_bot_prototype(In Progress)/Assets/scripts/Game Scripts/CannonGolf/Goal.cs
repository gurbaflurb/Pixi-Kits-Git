using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

  [SerializeField]
  string nextLevelName;

  AudioSource audioSource;
  Manager manager;

  bool canPlayAudio;
  int timer;

  public void Start()
  {
    audioSource = GetComponent<AudioSource>();
    manager = GameObject.Find("Manager").GetComponent<Manager>();
    canPlayAudio = true;
    timer = 0;
  }

  private void OnTriggerStay2D(Collider2D other)
  {
    //Collision with player
    if (other.gameObject.tag == "Player")
    {
      //If not moving OR CLOSE TO IT
      if (other.GetComponent<Rigidbody2D>().velocity.x <= 0.001 && other.GetComponent<Rigidbody2D>().velocity.y <= 0.001)
      {
        if(canPlayAudio)
        {
          audioSource.Play();
          canPlayAudio = false;
        }
        Debug.Log("Completed");
      }
    }
    if(!canPlayAudio)
    {
      if (timer > audioSource.clip.length * 70)
      {
        manager.NextLevel(nextLevelName);
      }
      timer++;
    }
  }
}
