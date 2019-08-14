// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class DelayMethod : MonoBehaviour {

    public static DelayMethod instance;
    public Action<IEnumClass> OnNewDelayMethodCreated;
    public List<IEnumClass> ActiveActions;

    private void Awake() { instance = this; }

    public void CreateNewDelayMethod(int seconds){

        GameObject FUCK = new GameObject();
        IEnumClass delay = FUCK.AddComponent<IEnumClass>();
        delay.IEnumClassStart(seconds);
        ActiveActions.Add(delay);
        OnNewDelayMethodCreated.Invoke(delay);
        Destroy(FUCK,(seconds+1));
    }

    public class IEnumClass : MonoBehaviour{

        public string DelayName;
        public Action End;
        public Action<int> SecondPass;

        private int TimeLeft;
        public void IEnumClassStart (int seconds){
            CreateDelay(seconds);
        }

        void CreateDelay(int seconds){
            StartCoroutine("StartDelay",seconds);
        }

        public  IEnumerator StartDelay(int time){
            TimeLeft = time;
            for (int i = 0; i < time; i++)
            {
                yield return new WaitForSecondsRealtime(1);
                TimeLeft -= 1;
                SecondPass.Invoke(TimeLeft);
            }
            End.Invoke();
            DelayMethod.instance.ActiveActions.Remove(this);
            End = null;
            SecondPass = null;
        }
    }

}
