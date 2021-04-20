using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject attackButtonsPanel;
    [SerializeField] GameObject attackTextPanel;
    [SerializeField] GameObject optionsButtonsPanel;
    [SerializeField] GameObject optionsTextPanel;
    [SerializeField] Text ppText;
    [SerializeField] Text typeText;
    [SerializeField] Slider IvyHPBar;
    [SerializeField] Image IvyHPFill;
    [SerializeField] Slider ZubatHPBar;
    [SerializeField] Image ZubatHPFill;
    Color HighHPColor;
    [SerializeField] Color MediumHPColor;
    [SerializeField] Color LowHPColor;

    // Start is called before the first frame update
    void Start()
    {
        HighHPColor = IvyHPFill.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToAttack()
    {
        attackButtonsPanel.SetActive(true);
        attackTextPanel.SetActive(true);
        optionsButtonsPanel.SetActive(false);
        optionsTextPanel.SetActive(false);
    }

    public void MoveToOptions()
    {
        attackButtonsPanel.SetActive(false);
        attackTextPanel.SetActive(false);
        optionsButtonsPanel.SetActive(true);
        optionsTextPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ButtonHover(string move)
    {
        switch(move)
        {
            case "tackle":
                ppText.text = "PP 35/35";
                typeText.text = "Normal";
                break;
            case "vine whip":
                ppText.text = "PP 25/25";
                typeText.text = "Grass";
                break;
            case "poison powder":
                ppText.text = "PP 35/35";
                typeText.text = "Poison";
                break;
            case "razer leaf":
                ppText.text = "PP 25/25";
                typeText.text = "Grass";
                break;
            default:
                print("No attack");
                break;
        }
    }

    public void SetHPBarIvy(float value)
    {
        IvyHPBar.value = value;
        if (value < 20)
        {
            IvyHPFill.color = LowHPColor;
        }
        else if(value < 40)
        {
            IvyHPFill.color = MediumHPColor;
        }
        else
        {
            IvyHPFill.color = HighHPColor;
        }
    }

    public void SetHPBarZubat(float value)
    {
        ZubatHPBar.value = value;
        if (value < 20)
        {
            ZubatHPFill.color = LowHPColor;
        }
        else if (value < 40)
        {
            ZubatHPFill.color = MediumHPColor;
        }
        else
        {
            ZubatHPFill.color = HighHPColor;
        }
    }
}
