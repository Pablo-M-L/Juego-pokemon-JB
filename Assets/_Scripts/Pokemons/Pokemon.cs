using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon
{
    private PokemonBase _base;
    private int _level;

    private List<Move> _moves;
    public List<Move> Moves
    {
        get => _moves;
        set => _moves = value;
    }


    //vida actual del pokemon (healt points)
    private int _hp;
    public int Hp
    {
        get => _hp;
        set => _hp = value;

    }

    //constructor
    public Pokemon(PokemonBase pokBase, int pokemonLevel)
    {
        _base = pokBase;
        _level = pokemonLevel;
        _hp = _base.MaxHP;

        _moves = new List<Move>();

        //añade los movimientos aprendidos segun el nivel del pokemon
        foreach(var lMove in _base.LearnableMoves)
        {
            if (lMove.Level <= _level){
                _moves.Add(new Move(lMove.Move));
            }

            if(_moves.Count >= 4)
            {
                break;
            }
        }


    }

    //para calcular el ataque, se multiplica el valor del ataque base por el nivel del pokemon, y se divide por 100.
    //se utiliza un truncamiento para que el dato sea un valor entero.
    //se le suma 1 por si el resultado de la division es menor de 1, ya que al hacer el truncamiento el resultado seria 0.
    public int MaxHP => Mathf.FloorToInt((_base.MaxHP * _level) / 100) + 1;
    public int Defense => Mathf.FloorToInt((_base.Defense * _level) / 100) + 1;
    public int Attack => Mathf.FloorToInt((_base.Attack * _level) / 100) + 1;
    public int SpAttack => Mathf.FloorToInt((_base.SpAttack * _level) / 100) + 1;
    public int SpDefense => Mathf.FloorToInt((_base.SpDefense * _level) / 100) + 1;
    public int Speed => Mathf.FloorToInt((_base.Speed * _level) / 100) + 1;


}
