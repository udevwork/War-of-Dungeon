using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class SceneLoadingManager : MonoBehaviour {

    public static SceneLoadingManager load = null;

    [SerializeField] public Animator anima;
    public Text LoadingText;
    private string SceneName;

    public enum Scenes
    {
        LOADING, DUNGEON, HEROES, MAINMENU, VILLAGE
    }

    public Action OnAnimationComplete;

    void Start()
    {

        if (load == null)
        { 
            load = this; 
        }
        else if (load != null)
        { 
            Destroy(gameObject); 
        }
        DontDestroyOnLoad(gameObject);
    }


    public void Scene(Scenes scene, string titletext){
        LoadingText.text = titletext;
        SceneName = scene.ToString();
        anima.SetTrigger("start");
    }



    public void FadeOn(){
        OnAnimationComplete = null;
        StartCoroutine(LoadYourAsyncScene());
    }

    public void animComplete(){
        if(OnAnimationComplete != null){
            OnAnimationComplete.Invoke();
        }
    }

    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
        async.allowSceneActivation = false;
        while (async.progress < 0.8f)
        {
            yield return null;
        }
        async.allowSceneActivation = true;
        yield return new WaitForSeconds(3);
        anima.SetTrigger("ok");
    }
}
