using UnityEngine;
using DataBase;

public class BulletForce : MonoBehaviour
{
    // The target marker.
    public Transform target;

    // Speed in units per sec.
    public float speed;

    public float damage;

    public GameObject hitEffect;


    void Update()
    {
        float step = speed * Time.deltaTime;
        if (target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
        else {
            transform.Translate(Vector3.forward);
        }
    }

    public void applyBulletSpell(IDamageble H)
    {
        H.GetDamage(damage,null);
        if (gameObject.layer.Equals(LayerMask.NameToLayer("EBullet")) == false)
        {
            if (H.GetTag() != "Brakble")
            {
                LevelModel.Player.MainHeroScript.currentEnemyToAttack = (H as NPC).gameObject;
                LevelModel.Dungeon.OnEnemyDamaged.Invoke((NPC)H,damage);
            }
            if (H.GetHealthPoints() <= 0)
            {
                LevelModel.Player.MainHeroScript.currentEnemyToAttack = null;
            }
        }
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER: " + other.name);

        if (other.GetComponent<IDamageble>() != null)
        {
            applyBulletSpell(other.GetComponent<IDamageble>());
        } else {
            Destroy(gameObject);
        }
    }

}