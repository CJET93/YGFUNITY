using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proba : MonoBehaviour
{
    public SpriteRenderer[] sp=new SpriteRenderer[8];
    // Start is called before the first frame update
    void Start()
    {
        sp[0].GetComponentInChildren<SpriteRenderer>().color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
