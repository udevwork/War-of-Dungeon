using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIPanelManager : MonoBehaviour {

    [SerializeField] private Animator anima;
    [SerializeField] private GameObject container;


    public virtual void Start()
    {
        if (container)
        {
            container.SetActive(false);
        }
    }

    public virtual void Open(){
        container.SetActive(true);
        anima.Play("PanelOpen");
        SoundFX.play.Clic();
    }
    public virtual void Close(){
        anima.Play("PanelClose");
        SoundFX.play.Clic();
    }

    public virtual void DiactivePanel(){
        container.SetActive(false);
    }

}
