using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class CombatUtility
{

    public static Collider[] RadiusView(Transform center, float radius, int layer, bool BrakbleFilter)
    {
        List<Collider> colls;
        List<Collider> collsTemp = new List<Collider>();
        colls = Physics.OverlapSphere(center.position, radius, layer).ToList();
        if (colls.Count != 0)
        {
            if (BrakbleFilter)
            {
                foreach (Collider item in colls)
                {
                    if (item.tag != "Brakble")
                    {

                            collsTemp.Add(item);

                    }
                }
                colls = collsTemp;
            }
            else
            {
                foreach (Collider item in colls)
                {
                    
                        collsTemp.Add(item);


                }
            }
            return colls.ToArray();
        }
        return null;
    }

    public static Collider[] SmartRadiusView(Transform center, float radius, int layer)
    {
        List<Collider> colls;
        List<Collider> EnemysColls = new List<Collider>();
        List<Collider> BrakbleColls = new List<Collider>();
        colls = Physics.OverlapSphere(center.position, radius, layer).ToList();
        if (colls.Count != 0)
        {
            foreach (Collider item in colls)
            {
                if (item.tag != "Brakble")
                {
                    if (item.GetComponent<NPC>())
                    {
                        if (TargetRay(center, item.GetComponent<NPC>().RealModelCenter.transform))
                        {
                            EnemysColls.Add(item);
                        }
                    }
                    if (item.GetComponent<MainHero>())
                    {
                        if (TargetRay(center, item.GetComponent<MainHero>().RealModelCenter.transform))
                        {
                            EnemysColls.Add(item);
                        }
                    }
                }

                if (item.tag == "Brakble")
                {
                    if (TargetRay(center, item.transform))
                    {
                        BrakbleColls.Add(item);
                    }
                }

            }

            if(EnemysColls.Count > 0){
                return EnemysColls.ToArray();
            }
            if (BrakbleColls.Count > 0)
            {
                return BrakbleColls.ToArray();
            }
        }
        return null;
    }


    public static GameObject LineView(Transform center, float radius, int layer, bool BrakbleFilter)
    {
        Collider[] colls;
        colls = Physics.OverlapSphere(center.position, radius / 5, layer);
        if (colls.Length != 0)
        {

            foreach (Collider item in colls)
            {
                if (item.transform.GetComponent<IDamageble>() != null)
                {
                    if (BrakbleFilter == false)
                    {
                        if (item.transform.GetComponent<IDamageble>().GetHealthPoints() > 0)
                        {
                            return item.transform.gameObject;
                        }
                    }
                    else if (BrakbleFilter == true)
                    {
                        if (item.transform.GetComponent<IDamageble>().GetHealthPoints() > 0 && item.tag != "Brakble")
                        {
                            return item.transform.gameObject;
                        }
                    }
                }
            }


        }
        return null;
    }

    public static GameObject GetClosestObject(Transform center, Collider[] colliders)
    {
        if(colliders == null){
            return null;
        }
        if(colliders.Length == 1){
            return colliders[0].gameObject;
        }
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = center.position;
        if (colliders != null && colliders.Length != 0)
        {
            foreach (Collider t in colliders)
            {
                if (t.GetComponent<IDamageble>() != null)
                {
                    if (t.GetComponent<IDamageble>().GetHealthPoints() > 0)
                    {
                        float dist = Vector3.Distance(t.transform.position, currentPos);
                        if (dist < minDist)
                        {
                            tMin = t.transform;
                            minDist = dist;
                        }
                    }
                }

            }
            return tMin.gameObject;
        }
        return null;
    }

    private static bool TargetRay(Transform center, Transform destination)
    {
        RaycastHit hit;
        Vector3 fromPosition = center.position;
        Vector3 toPosition = destination.transform.position;
        Vector3 direction = toPosition - fromPosition;

        if (Physics.Raycast(center.position, direction, out hit))
        {
            if (hit.transform.tag == "WALL")
            {
                return false;
            }

        }
        return true;
    }


    public static bool probability (float persent)
    {
        if(persent > 100f){
            persent = 100f;
        }
        float randomValue = Random.Range(0f, 100f);
        if(randomValue >= persent){
            return true;
        }
        return false;
    }

}
