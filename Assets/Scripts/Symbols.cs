using UnityEngine;

public class Symbols : MonoBehaviour
{

    public int symbolNumber;
    public Transform symbolContainer1;
    public Transform symbolContainer2;
    public Transform symbolContainer3;
    public Transform symbolContainerInvisible;
    private float symbolMoveSpeed = 10f;

    private string symbolState = "Idle";
    private Transform objective;

    public void ActivateSymbol()
    {
        gameObject.SetActive(true);
        if (GameManager.gameManager.symbolContainersUsed == 0) { objective = symbolContainer1; }
        if (GameManager.gameManager.symbolContainersUsed == 1) { objective = symbolContainer2; }
        if (GameManager.gameManager.symbolContainersUsed == 2) { objective = symbolContainer3; }
        if (GameManager.gameManager.symbolContainersUsed == 3) { objective = symbolContainerInvisible; }
        GameManager.gameManager.symbolContainersUsed += 1;
    }
    public void DeactivateSymbol()
    {
        gameObject.SetActive(false);
        objective = symbolContainerInvisible; 
    }
    private void Update()
    {
        MoveTowardsObject(objective);
    }
    private void MoveTowardsObject(Transform objective)
    {
        if (transform.position != objective.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, objective.position, Time.deltaTime * symbolMoveSpeed);
        }
    }
}
