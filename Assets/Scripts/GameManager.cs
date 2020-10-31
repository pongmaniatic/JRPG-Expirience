using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int gameState = 0; //0 is menu, 1 is player turn, 2 is turn transition, and 3 is enemy turn.
    private int characterTurn = 0; //0 is capsule, 1 is cube and 2 is sphere.
    private bool capsuleAction = false;
    private bool cubeAction = false;
    private bool sphereAction = false;


    void ChangeGameState(int newGameState)
    {
        gameState = newGameState;
    }
    void ChangeCharacter(int Character)
    {
        characterTurn = Character;
    }
    void ResetCharacterActions()
    {
        capsuleAction = true;
        cubeAction = true;
        sphereAction = true;
    }


}
