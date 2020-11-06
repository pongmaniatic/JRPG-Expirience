using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public TextMeshProUGUI turnText;
    public TextMeshProUGUI characterText;
    private bool gameState = false; //false if player turn, true if boss turn.
    public GameObject characterSelected; //what game object is selected currently 
    public GameObject shadowRealm; //the shadow realm is where every token and effect goes when its unused. 
    Camera cam; // the camera
    public Character capsule;
    public Character cube;
    public Character sphere;
    public Button endTurnButton;
    public Button useAbility1Button;
    public Button useAbility2Button;
    // 
    public int characterNumber = 0;/// <summary>
                                   /// characterNumber is a number that goes from 0 to 2, when you click on an hability of a character, they get the current number and then the number goes +1
                                   /// this will mark each character telling them the order in witch they used their abilities, this is resets.
                                   /// </summary>
    public bool charactersAttacking = false;
    public GameObject SelectToken;
    public int symbolContainersUsed = 0;

    public static Action onSelectedCharacter = delegate { };
    public static Action onSelectedAbility1 = delegate { };
    public static Action onSelectedAbility2 = delegate { };

    void Awake() 
    {
        gameManager = this; 
        ChangeGameState(); 
        cam = Camera.main;
        characterText.text = "Character: " + characterSelected.name;
    }
    public void ChangeGameState()
    {
        gameState = !gameState;
        if (gameState == true) { turnText.text = "Turn: " + "Player"; }
        if (gameState == false) { turnText.text = "Turn: " + "Boss"; }
    }

    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit, 100) && hit.collider.gameObject.tag == "Character")
        {
            onSelectedCharacter();
            characterSelected = hit.collider.gameObject;
            characterText.text = "Character: " + characterSelected.name;
        }
        if (capsule.abilityEnergy == 0 && cube.abilityEnergy == 0 && sphere.abilityEnergy == 0)
        {
             
            if (charactersAttacking == true) { endTurnButton.interactable = false; SelectToken.SetActive(false); }
            else { endTurnButton.interactable = true;}
        }
        else
        {
            
            endTurnButton.interactable = false;
        }
    }

    public void UseAbility1()
    {
        onSelectedAbility1();
    }

    public void UseAbility2()
    {
        onSelectedAbility2();
    }
    public void RefillEnergy()
    {
        capsule.abilityEnergy += 1;
        cube.abilityEnergy += 1;
        sphere.abilityEnergy += 1;
    }
    public void StartAttack(){ characterNumber = 0; charactersAttacking = true; }
    public void NewTurn()
    {
        ChangeGameState();
        characterNumber = 0;
        RefillEnergy();
        charactersAttacking = false;
        SelectToken.SetActive(true);
    }
}

