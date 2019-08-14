using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Controller : MonoBehaviour
{
    public ExList<int> Bitch = new ExList<int>();


    public void add()
    {
        Bitch.Add(UnityEngine.Random.Range(1, 100));
    }

    public void remove()
    {
        Bitch.Remove(Bitch[UnityEngine.Random.Range(0,Bitch.Count-1)]);
    }

    public void AddMyValue(int i)
    {
        Debug.Log("элементов в коллекции: " + Bitch.Count);
        Debug.Log("Мы добавили : " + i);
    }


    public void RemoveMyValue(int i)
    {

        Debug.Log("элементов в коллекции: " + Bitch.Count);
        Debug.Log("Мы убрали : " + i);
		Bitch[1] = 1;
    }

    private void OnEnable()
    {
        Bitch.OnAdd += AddMyValue;
        Bitch.OnRemove += RemoveMyValue;
    }

    private void OnDisable()
    {
        Bitch.OnAdd -= AddMyValue;
        Bitch.OnRemove += RemoveMyValue;
    }

}


public class ExList<T>
{
    private List<T> mainList = new List<T>();
	public T this[int index] {get {return mainList[index];} set {mainList[index] = value;} }
    public int Count { get { return mainList.Count; } }

    public Action<T> OnAdd;
    public Action<T> OnRemove;

    public void Add(T item)
    {
        mainList.Add(item);
        OnAdd(item);
    }

    public void Remove(T item)
    {
        mainList.Remove(item);
        OnRemove(item);
    }

}