using System;
using System.Collections;
using System.Collections.Generic;
using Random = System.Random;
using UnityEngine;
using System.Linq;

public class CoinInst : MonoBehaviour
{   //Rastgele Spawn Points sayýsýnýn yarýsý kadar spawn point seçtik ve spawnladýk.
    public List<Transform> spawnPoints= new List<Transform>();
    [SerializeField] private GameObject coin;
    HashSet<int> randomValues = new HashSet<int>();

    private void Start()
    {
        var random= new Random();
        int piece=(int)Math.Ceiling(spawnPoints.Count/2.0f); //Toplam spawn pointi 2ye böler. 11 2 ye bölünüðünde int çýkmaz bu yüzden yuvarladýk.
        //HashSet<int> randomValues= new HashSet<int>();
        while(randomValues.Count< piece)
        {
            randomValues.Add(random.Next(0, spawnPoints.Count()-1));
        }
        //var randomValues=Enumerable.Range(0, piece)
        // .Select(e => spawnPoints[random.Next(spawnPoints.Count)]); 
        foreach(var x in randomValues)
        {
            Instantiate(coin, spawnPoints[x]);
        }
    }
}
