using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerStart : MonoBehaviour
{
    private PlayableDirector _playableDirector;
    private void Start()
    {
        _playableDirector = GetComponent<PlayableDirector>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playableDirector.Play();
        }
    }
}
