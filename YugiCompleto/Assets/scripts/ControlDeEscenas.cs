using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlDeEscenas : MonoBehaviour
{
   public void GetEscena(string escena)
    {
        SceneManager.LoadScene(escena);
    }
}
