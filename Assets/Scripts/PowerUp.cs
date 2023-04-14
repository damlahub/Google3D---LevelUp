using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PowerUp : MonoBehaviour
{
    private GameObject _music;
    private MusicFiles _musicFiles;
    private ThirdPersonController _thirdPerson;
    [SerializeField] private int musicNumber = 1;
    [SerializeField] private GameObject _powerUpUI;
    private void Start()
    {
        _music = GameObject.Find("AudioManager");
        _musicFiles=_music.GetComponent(typeof(MusicFiles)) as MusicFiles;
        _thirdPerson= GetComponent<ThirdPersonController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            AudioSource.PlayClipAtPoint(_musicFiles.music[musicNumber], gameObject.transform.position);
            _thirdPerson.SprintSpeed = 10.0f;
            _powerUpUI.SetActive(true);
            Invoke("BackToNormalSpeed", 3.0f);
        }
    }
    private void BackToNormalSpeed()
    {
        _thirdPerson.SprintSpeed = 5.33f;
        _powerUpUI.SetActive(false);
    }
}
