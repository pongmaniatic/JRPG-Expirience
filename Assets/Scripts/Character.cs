using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum AbilityType { CapsuleAttack, CubeAttack, SphereAttack }
public class Character : MonoBehaviour
{
    #region ability related variables
    public AbilityType abilityType;
    private IAbility iAbility; 
    public int abilityEnergy = 1;
    public TextMeshProUGUI abilityEnergyText;
    public Button useAbility1Button;
    public Button useAbility2Button;
    #endregion
    #region other variables
    public bool selected = false;
    public Command command;
    private int SelectedSymbol;
    public Symbols symbol1;
    public Symbols symbol2;
    public Symbols symbol3;
    public Symbols symbol4;
    public Symbols symbol5;
    public Symbols symbol6; 
    public int maxhealth = 50;
    public int currentHealth;
    public Image characterHealthBar;
    #endregion
    #region state Machine related variables

    private int characterNumber;
    public bool readyToChangeState = false;// this is only true
    private string characterState = "idle"; // the states are: idle, moving, usingAbility
    public Transform startingPosition;
    public Transform attackingPosition;
    private float moveSpeed = 10f;
    private bool abilitySelected; // true is if ability 1 and false if ability 2
    private float setTimer = 1f;
    private float currentTime = 0;
    
    #endregion

    void Start()
    {
        HandleWeaponType();
        OnEnableUnselected();
    }

    private void HandleWeaponType()
    {
        switch (abilityType)
        {
            case AbilityType.CapsuleAttack:
                iAbility = gameObject.AddComponent<CapsuleAttack>();
                break;
            case AbilityType.CubeAttack:
                iAbility = gameObject.AddComponent<CubeAttack>();
                break;
            case AbilityType.SphereAttack:
                iAbility = gameObject.AddComponent<SphereAttack>();
                break;
            default:
                iAbility = gameObject.AddComponent<CapsuleAttack>();
                break;
        }
    }

    private void Update()
    {
        StateMachine();

        if (GameManager.gameManager.characterSelected == gameObject)
        {
            abilityEnergyText.text = "Ability Energy: " + abilityEnergy;
            if (selected == false)
            {
                OnEnableAttack1();
                OnEnableAttack2();
                selected = true;
            }
            if (abilityEnergy < 0) { abilityEnergy = 0; }
            if (abilityEnergy == 0)
            {
                useAbility1Button.interactable = false;
                useAbility2Button.interactable = false;
                
            }
            else
            {
                useAbility1Button.interactable = true;
                useAbility2Button.interactable = true;
            }
        }
    }

    void StateMachine()
    {
        if (readyToChangeState == true && GameManager.gameManager.characterNumber == characterNumber && characterState == "idle")
        {
            characterState = "moving"; 
            readyToChangeState = false;
        }
        if (characterState == "idle" && transform.position != startingPosition.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, startingPosition.position, Time.deltaTime * moveSpeed);
        }
        if (characterState == "moving" && transform.position != attackingPosition.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, attackingPosition.position, Time.deltaTime * moveSpeed);
        }
        if (characterState == "moving" && transform.position == attackingPosition.position)
        {
            characterState = "usingAbility";
            if (abilitySelected == true) { Attack1(); }
            else { Attack2(); }
            currentTime = setTimer;
        }
        if (characterState == "usingAbility")
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 0)
            {
                characterState = "idle";
                GameManager.gameManager.characterNumber += 1;
            }
        }
        if (GameManager.gameManager.characterNumber == 3 )
        {
            ResetSymbols();
            BossManager.bossManager.bossTurn = true;
        }
    }
    void Unselected()
    {
        OnDisableAttack1();
        OnDisableAttack2();
        selected = false;
    }
    void AddCommand1()
    {
        characterNumber = GameManager.gameManager.characterNumber;
        GameManager.gameManager.characterNumber += 1;
        abilityEnergy -= 1;
        abilitySelected = true;
        CommandInvoker.AddCommand(new Ability1(this));
        if (GameManager.gameManager.characterNumber > 2) { GameManager.gameManager.characterNumber = 2; }
    }
    void AddCommand2()
    {
        characterNumber = GameManager.gameManager.characterNumber;
        GameManager.gameManager.characterNumber += 1;
        abilityEnergy -= 1;
        abilitySelected = false;
        CommandInvoker.AddCommand(new Ability1(this));
        if (GameManager.gameManager.characterNumber > 2) { GameManager.gameManager.characterNumber = 2; }
    }

    public void Attack1()
    {
        SelectedSymbol = iAbility.UseAbility1();
        ActivateSymbol();
    }
    public void Attack2()
    {
        SelectedSymbol = iAbility.UseAbility2();
        ActivateSymbol();
    }

    void ActivateSymbol()
    {
        if (SelectedSymbol == 1) { symbol1.ActivateSymbol(); }
        if (SelectedSymbol == 2) { symbol2.ActivateSymbol(); }
        if (SelectedSymbol == 3) { symbol3.ActivateSymbol(); }
        if (SelectedSymbol == 4) { symbol4.ActivateSymbol(); }
        if (SelectedSymbol == 5) { symbol5.ActivateSymbol(); }
        if (SelectedSymbol == 6) { symbol6.ActivateSymbol(); }
    }
    void ResetSymbols()
    {
        symbol1.DeactivateSymbol(); 
        symbol2.DeactivateSymbol(); 
        symbol3.DeactivateSymbol(); 
        symbol4.DeactivateSymbol(); 
        symbol5.DeactivateSymbol();
        symbol6.DeactivateSymbol();
        GameManager.gameManager.symbolContainersUsed = 0;
    }
    public void ExecuteCommands(){CommandInvoker.ExecuteCommands();}

    public void LoseHealth()
    {
        currentHealth -= 40;
        float currentHelathPorcentage = (float)currentHealth / (float)maxhealth;
        characterHealthBar.fillAmount = currentHelathPorcentage;
    }

    #region OnEnableAndDisable
    //this detects the signal of selecting a new character
    private void OnEnableUnselected() { GameManager.onSelectedCharacter += Unselected; }
    private void OnDisableUnselected() { GameManager.onSelectedCharacter -= Unselected; }
    //this detects the signal of pressing the action button 1
    private void OnEnableAttack1() { GameManager.onSelectedAbility1 += AddCommand1; }
    private void OnDisableAttack1() { GameManager.onSelectedAbility1 -= AddCommand1; }
    //this detects the signal of pressing the action button 1
    private void OnEnableAttack2() { GameManager.onSelectedAbility2 += AddCommand2; }
    private void OnDisableAttack2() { GameManager.onSelectedAbility2 -= AddCommand2; }
    #endregion
}
