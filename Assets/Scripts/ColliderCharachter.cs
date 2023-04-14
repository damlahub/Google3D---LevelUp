using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColliderCharachter : MonoBehaviour
{
    private int health = 3;
    [SerializeField] private GameObject[] _healthUI;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject gameOverPanel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            health--;
            _healthUI[health].gameObject.SetActive(false);
            if (health == 0)
            {
                gameOver.SetActive(true);
                gameOverPanel.SetActive(true);
                StartCoroutine(Fade());
            }
        }
        if (other.gameObject.CompareTag("UI")) 
        {
            other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("UI"))
        {
            other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    IEnumerator Fade()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale= 0;
    }
}
