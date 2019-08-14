using UnityEngine;
using UnityEngine.UI;

public class SimpleTextNotificationCotroller : MonoBehaviour {

    public Text text;

    public void DeleteMassage(){
        Destroy(gameObject);
    }
}
