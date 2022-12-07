using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usuario : MonoBehaviour
{
   public List<int>deck = new List<int>();
    public int pVida = 8000;
    // Start is called before the first frame update
    private void Awake()
    {
       
    }
    void Start()
    {
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    public void setPuntosVida(int vida)
    {
        pVida = pVida - vida;
    }
}
