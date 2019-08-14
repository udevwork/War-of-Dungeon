using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Runtime.Remoting;

public class Human : MonoBehaviour
{
    public CharacterSoundFX SFX;

    public float FildOfView;

    [Header("-----ATTACK STAFF----")]
    public GameObject hitPos;
    public GameObject RealModelCenter;
    public float hitDistance;
    public float hitRange;
    public float hitDelay;
    public GameObject currentEnemyToAttack;
    public bool isCanHit = true;
    public bool InAtackRange = false;

    public bool isDead;
    public float attackSpeed;
    [Header("---------------------")]

    [SerializeField] protected int LayerToScan;

    public int GetLayerToScan{ get { return LayerToScan; }}

    event Action<GameObject> OnSomeoneInViewFild;
    public Action<float, IDamageble> OnDamaged;

    [SerializeField] public Animator anim;
    [SerializeField] public NavMeshAgent navAgent;
    [SerializeField] public GameObject AgentTarget;


    public List<Collider> ObjectsInView;

    void OnEnable()
    {
        OnSomeoneInViewFild += (GameObject obj) => SetNavAgentTarget(obj);
    }

    public virtual void Start()
    {

    }


    public virtual void LateUpdate()
    {

    }

    public void autoPilot()
    {
        if (!isDead)
        {
            if (navAgent.enabled == false)
            {
                return;
            }

            if (!AgentTarget)
            {
                return;
            }


                if (Vector3.Distance(transform.position, AgentTarget.transform.position) <= navAgent.stoppingDistance)
                {
                    navAgent.velocity = Vector3.zero;
                     var lookPos = AgentTarget.transform.position - transform.position;
                     lookPos.y = 0;
                     Quaternion targettolook = Quaternion.LookRotation(lookPos);
                    transform.rotation = Quaternion.Slerp(transform.rotation,targettolook, 0.1f);
                    

                    if (AgentTarget.gameObject.tag == "Player")
                    {
                        if (navAgent.remainingDistance != 0)
                        {
                            InAtackRange = true;
                        }
                    }

                }
                else if (Vector3.Distance(transform.position, AgentTarget.transform.position) > navAgent.stoppingDistance)
                {
                    NavAgentMove();
                    InAtackRange = false;
                }
            
            if (InAtackRange)
            {
                if (isCanHit)
                {
                    Hit();
                }
            }

            anim.SetFloat("Run", navAgent.velocity.magnitude);
        }
    }

    public void NavAgentMove()
    {
        navAgent.SetDestination(AgentTarget.transform.position);
    }

    public virtual void Hit()
    {
            Quaternion targettolook = Quaternion.LookRotation(AgentTarget.transform.position - transform.position);
            transform.rotation = targettolook;
            StartCoroutine("AttackDelay");
    }

    private IEnumerator AttackDelay()
    {
        anim.SetTrigger("Atack");
        isCanHit = false;
        yield return new WaitForSeconds(hitDelay);
        isCanHit = true;

    }

    public virtual void SetDamage()
    {
 
    }

    public virtual void Fire()
    {

    }


    public virtual void GetDamage(float damage, IDamageble atacker)
    {
        if(OnDamaged != null){
            OnDamaged.Invoke(damage,atacker);
        }
    }

   

    public virtual void Death()
    {
       // anim.SetFloat("Death", UnityEngine.Random.Range(1, 4));
        SFX.PlayRandomDeathSound();
        gameObject.layer = LayerMask.NameToLayer("Dead");
    }

    public void SetNavAgentTarget(GameObject target)
    {
        AgentTarget = target;
    }

    public virtual GameObject serchForEnemy()
    {
        GameObject go = CombatUtility.LineView(hitPos.transform,FildOfView,LayerToScan,false);
        if (go == null)
        {
            go = CombatUtility.GetClosestObject(transform, CombatUtility.RadiusView(transform,FildOfView,LayerToScan,false));
        }
        if (go)
        {
            return go;
        }
        return null;
    }

    public virtual void OnParticleCollision(GameObject other)
    {
        
    }




    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, FildOfView);
    }

}

