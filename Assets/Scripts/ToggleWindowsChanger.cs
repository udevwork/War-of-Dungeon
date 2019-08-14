using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleWindowsChanger : MonoBehaviour {

    public ToggleGroup togglegroup;
    public IEnumerable<Toggle> toggless;
    public GameObject[] windows;

    private void OnEnable()
    {
        toggless = togglegroup.ActiveToggles();

    }

    public void WindowsChanger()
    {

        foreach (var item in toggless)
        {
            if (item.isOn)
            {
                foreach (var witem in windows)
                {
                    if (item.name == witem.name)
                    {
                        witem.SetActive(true);
                    }
                    else
                    {
                        witem.SetActive(false);
                    }
                }

            }
        }
        SoundFX.play.Clic();
    }
}
