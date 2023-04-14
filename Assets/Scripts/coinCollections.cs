using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinCollections : MonoBehaviour
{
    private AudioSource _click;
    private int count = 0;
    [SerializeField] private TMPro.TextMeshProUGUI _count;
    private void Start()
    {
        _click = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            //Destroy(other.gameObject);
            count++;
            _count.text = count.ToString(); 
            other.gameObject.SetActive(false);
            _click.Play();
            StartCoroutine(Spawn(other.gameObject));
        }
    }
    IEnumerator Spawn(GameObject gameObject)
    {
        //3 ssaniye sonra setactive false olanlar true olacak.
        yield return new WaitForSeconds(3);
        gameObject.SetActive(true);
    }
}
