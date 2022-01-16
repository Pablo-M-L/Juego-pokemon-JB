using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Pokemon/Nuevo Movimineto")]
public class MoveBase : ScriptableObject
{
    [SerializeField] private string name;
    public string Name => name;
    [TextArea] [SerializeField] private string description;
    public string Description => description;

    [SerializeField] private pokemonType type;
    [SerializeField] private int power;
    [SerializeField] private int accuracy; //precision
    [SerializeField] private int pp; //power points

    public pokemonType Type => type;
    public int Power => power;
    public int Accuracy => accuracy;
    public int PP => pp;


}


