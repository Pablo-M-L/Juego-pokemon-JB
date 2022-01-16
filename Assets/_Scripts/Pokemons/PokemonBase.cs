using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//extender el editor de unity a nuestras necesidades.
[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Nuevo Pokemon")]

// los scriptableobjects se instacian a traves del motor (editor)
public class PokemonBase : ScriptableObject
{

    //serializable para poder ser modificada desde el editor
    [SerializeField] private int ID;

    [SerializeField] private string name;
    public string Name => name; // crea un getter de la variable name.

    [TextArea] [SerializeField] private string description;
    public string Description => description;

    [SerializeField] private Sprite frontSprite;
    [SerializeField] private Sprite backSprite;

    [SerializeField] private pokemonType type1;
    public pokemonType Type1 => type1;

    [SerializeField] private pokemonType type2;
    public pokemonType Type2 => type2;


    //Stats
    [SerializeField] private int maxHP; //max Point
    [SerializeField] private int attack; 
    [SerializeField] private int defense;
    [SerializeField] private int spAttack; //special attack
    [SerializeField] private int spDefense; //special defense
    [SerializeField] private int speed;


    //getters de los stats
    public int MaxHP => maxHP; //max Point
    public int Attack => attack;
    public int Defense => defense;
    public int SpAttack => spAttack; //special attack
    public int SpDefense => spDefense; //special defense
    public int Speed => speed;


    //lista de ataques que el pokemon va a poder aprender
    [SerializeField] private List<LearnableMove> learnableMoves;
    public List<LearnableMove> LearnableMoves => learnableMoves;
}


public enum pokemonType
{
    Normal,
    Fire,
    Water,
    Electric,
    Grass,
    Fight,
    Ice,
    Poison,
    Ground,
    Fly,
    Psychic,
    Rock,
    Bug,
    Ghost,
    Dragon,
    Dark,
    Fairy,
    Steel
}

[Serializable()]
public class LearnableMove
{
    [SerializeField] private MoveBase move;
    [SerializeField] private int level;

    //getters
    public MoveBase Move => move;
    public int Level => level;
}