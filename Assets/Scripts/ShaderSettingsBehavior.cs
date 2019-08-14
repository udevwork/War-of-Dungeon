// Denis super code 
using UnityEngine;
using UnityEngine.PostProcessing;
using DataBase;

public class ShaderSettingsBehavior : MonoBehaviour {

    public PostProcessingBehaviour CameraShader;
	void Awake () {
        if (CameraShader == null)
        {
            if (GetComponent<PostProcessingBehaviour>())
            {
                CameraShader = GetComponent<PostProcessingBehaviour>();
            }
        }
        Settings.OnUseShadersChange +=  (bool obj) => CameraShader.enabled = obj;
        if(Settings.UseShaders == true){
            CameraShader.enabled = true;
        } else if (Settings.UseShaders == false)
        {
            CameraShader.enabled = false;
        }
	}


   

}
