using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    public abstract void Execute();
}

public class Ability1 : Command
{
    Character theCharacter;
    public Ability1(Character character)
    {
        theCharacter = character;
    }

    public override void Execute()
    {
        theCharacter.readyToChangeState = true;
        GameManager.gameManager.charactersAttacking = true;
    }
}
