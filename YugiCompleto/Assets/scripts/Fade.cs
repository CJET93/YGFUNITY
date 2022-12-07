using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    SpriteRenderer establecer;
    // Start is called before the first frame update
    void Start()
    {
        establecer = GetComponent<SpriteRenderer>();
        Color c = establecer.material.color;
        c.a = 0f;
        establecer.material.color = c;
        
    }

    IEnumerator FadeTiempo()
    {
        for(float f = 0.05f; f <= 3; f += 0.05f)
        {
            Color c = establecer.material.color;
            c.a = f;
            establecer.material.color = c;
            yield return null;

        }
        //StartCoroutine(AnimacionQuitarFade());
        //establecer.material.color = Color.Lerp(establecer.material.color, Color.clear, 5f * Time.fixedDeltaTime);
       
    }
    public void QuitarFade()
    {
        StartCoroutine(AnimacionQuitarFade());
    }
    IEnumerator AnimacionQuitarFade()
    {
        
        for (float f = 1; f >= -0.05; f -= 0.05f)
        {
            Color c = establecer.material.color;
            c.a = f;
            establecer.material.color = c;
           
            
            yield return null;

        }
        //gameObject.SetActive(false);
    }
    public void InicioFade()
    {
        GetComponent<Transform>().localScale = new Vector3(20, 20, 0);
        StartCoroutine(FadeTiempo());
    }
    
}
