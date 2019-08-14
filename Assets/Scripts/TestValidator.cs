// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class TestValidator : MonoBehaviour {

    public static bool isValid;

    private string PlayerCode = "";
    private string GitCode = "";
    private string SafeCode = "cherry";
    public  UnityEngine.UI.InputField CodeInput;

    public Animator anim;
    public Text WarningText;

    public GameObject ValidatorGameObject;

    public void Start()
    {
        if(isValid){
            Destroy(ValidatorGameObject);
        }
    }

    IEnumerator GetText() {
        if (PlayerCode == SafeCode)
        {
            isValid = true;
            anim.SetTrigger("ok");
            WarningText.color = Color.cyan;
            WarningText.text = "OK!";
            StopAllCoroutines();
            Destroy(ValidatorGameObject, 2);
        }

        WarningText.text = "Verification...";    

        UnityWebRequest www = UnityWebRequest.Get("https://raw.githubusercontent.com/udevwork/transfer/master/value");
        yield return www.SendWebRequest();
 
        if(www.isNetworkError || www.isHttpError) {
            WarningText.color = Color.red;
            WarningText.text = "no internet connection";
        }
        else {
            GitCode = www.downloadHandler.text; 
            if (PlayerCode == GitCode.Trim())
            {
                isValid = true;
                anim.SetTrigger("ok");
                WarningText.color = Color.cyan;
                WarningText.text = "OK!";
                Destroy(ValidatorGameObject,2);
                StopAllCoroutines();
            } else {
                WarningText.color = Color.red;
                WarningText.text = "invalid code";
                anim.SetTrigger("error");

            }
        }
    }

    public void Validate(){

        PlayerCode = CodeInput.text;
        StartCoroutine(GetText());
    }

    public void LinkGo(){
        Application.OpenURL("https://www.instagram.com/company_of_dudes/");
    }
}
