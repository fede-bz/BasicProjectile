using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevaDiana", menuName = "BasicProjectile/Diana")]
public class DianaData : ScriptableObject
{
    [Header("Identidad")]
    public string nombreDiana;
    [TextArea] public string descripcion;

    [Header("Gameplay")]
    public int puntaje;
    public float velocidad;
    public int vidas;

    [Header("Visual")]
    public Color colorDiana;
}