using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DataBase;

public class CheckPoint : MonoBehaviour
{
   
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position,Vector3.one);
    }
}
