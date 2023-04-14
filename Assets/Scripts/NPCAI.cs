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
    public LayerMask ground, player;// zemin ve oyuncu katmanlar�

    public Vector3 destinationPoint; //hedef
    private bool destinationPointSet;
    public float walkPointRange;// y�r�yebilece�i alan

    public float timeBetweenAttacks; //Sald�r� aral�klar� ne s�rede?
    private bool alreadyAttacked;//Sald�r� yap�p-yapmad���n� kontrol edecek.
    public GameObject sphere;//bullet - mermi Instantiate edilecek.

    public float sightRange, attackRange; //NPC g�r�� ve sald�r� alan�.
    public bool playerInSightRange, playerInAttackRange;//Oyuncu NPC'nin g�r�� ve sald�r� alan�n�n i�inde mi?

    private void Awake()
    {
        _agent= GetComponent<NavMeshAgent>();// NPC'mizin NavMeshAgent componentini ald�k.
    }
    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, player); //G�r�� mesafesinde oldu�unu kontrol eder.
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, player); //Sald�r� mesafesinde oldu�unu kontrol eder.
    // Patrol | Chase | Attack
        if(!playerInSightRange && !playerInAttackRange) //E�er oyuncu g�r�� ve sald�r� alan�nda de�ilse
        {
            Patroling(); //Ara
        }
        if(playerInSightRange && !playerInAttackRange)//E�er oyuncu g�r�� alan�nda ve sald�r� alan�nda de�ilse
        {
            ChasePlayer();//Kovala
        }
        if(playerInSightRange && playerInAttackRange)//E�er oyuncu g�r�� ve sald�r� alan�ndaysa
        {
            AttackPlayer();//Sald�r
        }
    }
    
     void Patroling()//Ara
    {
        Vector3 distanceToDestinationPoint = transform.position - destinationPoint; // Bulundu�umuz pozisyon ile hedefimizin mesafe fark� .
        if (distanceToDestinationPoint.magnitude < 1.0f) //Bulundu�umuz pozisyon ile hedefimizin mesafe fark�m�z�n b�y�kl��� 1 den k���kse
        {
            destinationPointSet = false;
        }
        if (!destinationPointSet)//false
        {
            SearchWalkPoint();    //Bir sonraki gidece�im yer neresi?
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
            //girmeye cal�st�g�m nokta groundta ise 
            destinationPointSet= true;
        }
    }
    void ChasePlayer()
    {
        _agent.SetDestination(_player.position); //Karakteri koval�yor.
    }
    void AttackPlayer()
    {
        _agent.SetDestination(transform.position); //Duracak ve ates edecek.
        transform.LookAt(_player); //Karaktere bakar.
        if(!alreadyAttacked)//Sald�r� yapm�yorsa
        {
            Rigidbody rb= Instantiate(sphere,transform.position,Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 25f, ForceMode.Impulse); //D�md�z gider -�leri
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);// Yukar�
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
