using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opciones : MonoBehaviour
{
    public void SetCalidad(int indiceCalidad)
    {
        QualitySettings.SetQualityLevel(indiceCalidad);
    }
}
