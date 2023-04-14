using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAI : MonoBehaviour
{
    /*
    [SerializeField] private GameObject destinationPoint;
    private NavMeshAgent _agent;
    private void Start()
    {
        _agent= GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        _agent.SetDestination(destinationPoint.transform.position);
    }*/

    public NavMeshAgent _agent; // ajan= NPCI
    [SerializeField] private Transform _player; // Oyuncu
    public LayerMask ground, player;// zemin ve oyuncu katmanlarý

    public Vector3 destinationPoint; //hedef
    private bool destinationPointSet;
    public float walkPointRange;// yürüyebileceði alan

    public float timeBetweenAttacks; //Saldýrý aralýklarý ne sürede?
    private bool alreadyAttacked;//Saldýrý yapýp-yapmadýðýný kontrol edecek.
    public GameObject sphere;//bullet - mermi Instantiate edilecek.

    public float sightRange, attackRange; //NPC görüþ ve saldýrý alaný.
    public bool playerInSightRange, playerInAttackRange;//Oyuncu NPC'nin görüþ ve saldýrý alanýnýn içinde mi?

    private void Awake()
    {
        _agent= GetComponent<NavMeshAgent>();// NPC'mizin NavMeshAgent componentini aldýk.
    }
    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, player); //Görüþ mesafesinde olduðunu kontrol eder.
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, player); //Saldýrý mesafesinde olduðunu kontrol eder.
    // Patrol | Chase | Attack
        if(!playerInSightRange && !playerInAttackRange) //Eðer oyuncu görüþ ve saldýrý alanýnda deðilse
        {
            Patroling(); //Ara
        }
        if(playerInSightRange && !playerInAttackRange)//Eðer oyuncu görüþ alanýnda ve saldýrý alanýnda deðilse
        {
            ChasePlayer();//Kovala
        }
        if(playerInSightRange && playerInAttackRange)//Eðer oyuncu görüþ ve saldýrý alanýndaysa
        {
            AttackPlayer();//Saldýr
        }
    }
    
     void Patroling()//Ara
    {
        Vector3 distanceToDestinationPoint = transform.position - destinationPoint; // Bulunduðumuz pozisyon ile hedefimizin mesafe farký .
        if (distanceToDestinationPoint.magnitude < 1.0f) //Bulunduðumuz pozisyon ile hedefimizin mesafe farkýmýzýn büyüklüðü 1 den küçükse
        {
            destinationPointSet = false;
        }
        if (!destinationPointSet)//false
        {
            SearchWalkPoint();    //Bir sonraki gideceðim yer neresi?
        }
        if(destinationPointSet)//true
        {
            _agent.SetDestination(destinationPoint); //DesrinationPoint' e git. 
        }
    }
    void SearchWalkPoint()
    {
        float randomX=Random.Range(-walkPointRange, walkPointRange);//Random 
        float randomZ=Random.Range(-walkPointRange, walkPointRange); 
        destinationPoint=new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z +randomZ); //yeni destination
        if (Physics.Raycast(destinationPoint, -transform.up, 2.0f, ground))
        {
            //girmeye calýstýgým nokta groundta ise 
            destinationPointSet= true;
        }
    }
    void ChasePlayer()
    {
        _agent.SetDestination(_player.position); //Karakteri kovalýyor.
    }
    void AttackPlayer()
    {
        _agent.SetDestination(transform.position); //Duracak ve ates edecek.
        transform.LookAt(_player); //Karaktere bakar.
        if(!alreadyAttacked)//Saldýrý yapmýyorsa
        {
            Rigidbody rb= Instantiate(sphere,transform.position,Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 25f, ForceMode.Impulse); //Dümdüz gider -Ýleri
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);// Yukarý
            StartCoroutine(ChangeTag(rb.gameObject));
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

        }
    }
    IEnumerator ChangeTag(GameObject gameObject)
    {
        yield return new WaitForSeconds(2);
        gameObject.tag= "Respawn";
    }
    void ResetAttack()
    {
        alreadyAttacked= false;
    }
}
