using UnityEngine;
using TMPro;

public class FactoryText : MonoBehaviour
{
    public static FactoryText factoryText;
    public float upSpeed = 0.01f;
    public TextMeshProUGUI healthtext;
    private bool startDisappearing = false;
    private float transparencyLevel = 0;
    public Transform Panel;
    private void Start()
    {
        factoryText = this;
        healthtext.alpha = transparencyLevel;
        //ActivateText(15,0);
    }


    void ChangePosition(int target)
    {
        if (target == 0) { transform.position = new Vector3 (Panel.position.x + 135, Panel.position.y + 180, 0) ; }//boss
        if (target == 1) { transform.position = new Vector3(Panel.position.x - 125, Panel.position.y + 145, 0); }//cube
        if (target == 2) { transform.position = new Vector3(Panel.position.x - 70, Panel.position.y + 195, 0); }//capsule
        if (target == 3) { transform.position = new Vector3(Panel.position.x - 85, Panel.position.y + 65, 0); }//sphere

    }
    public void ActivateText(int Damage, int position)
    {
        healthtext.text = Damage.ToString();
        transparencyLevel = 1;
        startDisappearing = true; 
        ChangePosition(position);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (startDisappearing == true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + upSpeed, transform.position.z);
            healthtext.alpha = transparencyLevel; // makes the color zm transparent
            transparencyLevel -= 0.0045f;
            Debug.Log(transparencyLevel);
        }
        if (transparencyLevel <= 0)
        {
            transparencyLevel = 1;
            startDisappearing = false;
            transform.position = new Vector3(Panel.position.x, Panel.position.y, Panel.position.y); ;
        }
    }
}
