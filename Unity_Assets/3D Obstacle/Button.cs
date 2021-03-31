using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
   public void QuitMyGame ()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
