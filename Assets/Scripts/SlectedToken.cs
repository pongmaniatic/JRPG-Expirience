using UnityEngine;

public class SlectedToken : MonoBehaviour
{
    private float speed = 300f;
    public TrailRenderer trail;
    private bool clearingMode = false;
    private float setTime = 1f;
    private float currentTime = 0;


    private void Start()
    {
        ClearTrail();
        OnEnable();
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * speed);
        transform.position = new Vector3(GameManager.gameManager.characterSelected.transform.position.x, 1, GameManager.gameManager.characterSelected.transform.position.z);
        if (clearingMode == true)
        {
            currentTime += 2f;
            ClearTrail();

            if (currentTime > setTime)
            {
                clearingMode = false;
            }
        }
    }

    void ClearTrail()
    {
        trail.Clear();
    }
    void StartClear()
    {
        clearingMode = true;
        currentTime = setTime;
    }

    private void OnEnable()
    {
        GameManager.onSelectedCharacter += StartClear;
    }
    private void OnDisable()
    {
        GameManager.onSelectedCharacter -= StartClear;
    }
}

