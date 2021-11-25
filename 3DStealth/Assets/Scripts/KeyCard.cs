using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCard : MonoBehaviour
{
    public AudioClip music_pickup;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.player)
        {
            Player player = other.GetComponent<Player>();
            player.hasKey = true;
            AudioSource.PlayClipAtPoint(music_pickup, transform.position);
            Destroy(this.gameObject);
        }
    }
}
