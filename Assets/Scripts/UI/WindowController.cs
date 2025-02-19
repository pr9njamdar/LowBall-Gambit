using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCardSelection : MonoBehaviour
{
    public GameObject Window;

    public void CloseWindow(){
        Window.SetActive(false);
    }
    public void OpenWindow(){
        Window.SetActive(true);
    }

}
