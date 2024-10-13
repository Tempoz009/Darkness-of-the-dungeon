using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenResolution : MonoBehaviour
{
    public Dropdown drop;

    public void Drop()
    {
        if (drop.value == 0)
        {
            Screen.SetResolution(2560, 1600, true);
            
        }
        if (drop.value == 1)
        {
            Screen.SetResolution(1920, 1080, true);
        }

        if (drop.value == 2)
        {
            Screen.SetResolution(1880, 1620, true);
        }

        if(drop.value == 3)
        {
            Screen.SetResolution(1024, 768, true);
        }

        if(drop.value == 4)
        {
            Screen.SetResolution(800, 600, true);
        }


    }
}
