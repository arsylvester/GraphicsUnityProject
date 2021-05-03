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
    [SerializeField] GameObject descriptionTextPanel;
    [SerializeField] Text ppText;
    [SerializeField] Text typeText;
    [SerializeField] Text descriptionText;
    [SerializeField] Slider IvyHPBar;
    [SerializeField] Image IvyHPFill;
    [SerializeField] Slider ZubatHPBar;
    [SerializeField] Image ZubatHPFill;
    Color HighHPColor;
    [SerializeField] Color MediumHPColor;
    [SerializeField] Color LowHPColor;
    [SerializeField] float loseHPSpeed = 1;
    [SerializeField] PlayAttackEffects vfxs;
    [SerializeField] GameObject ContinueImage;
    float vfxDuration;
    bool canContinue;

    // Start is called before the first frame update
    void Start()
    {
        HighHPColor = IvyHPFill.color;
    }

    // Update is called once per frame
    void Update()
    {
        //TESTING, REMOVE
        if(Input.GetKeyDown(KeyCode.L))
        {
            SetHPBarIvy(20);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            SetHPBarZubat(20);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && canContinue)
        {
            MoveToOptions();
            canContinue = false;
        }
    }

    public void MoveToAttack()
    {
        attackButtonsPanel.SetActive(true);
        attackTextPanel.SetActive(true);
        optionsButtonsPanel.SetActive(false);
        optionsTextPanel.SetActive(false);
        descriptionTextPanel.SetActive(false);
    }

    public void MoveToOptions()
    {
        attackButtonsPanel.SetActive(false);
        attackTextPanel.SetActive(false);
        optionsButtonsPanel.SetActive(true);
        optionsTextPanel.SetActive(true);
        descriptionTextPanel.SetActive(false);
    }

    public void MoveToText()
    {
        attackButtonsPanel.SetActive(false);
        attackTextPanel.SetActive(false);
        optionsButtonsPanel.SetActive(false);
        optionsTextPanel.SetActive(false);
        descriptionTextPanel.SetActive(true);
        ContinueImage.SetActive(false);
    }
    public void MoveToClear()
    {
        attackButtonsPanel.SetActive(false);
        attackTextPanel.SetActive(false);
        optionsButtonsPanel.SetActive(false);
        optionsTextPanel.SetActive(false);
        descriptionTextPanel.SetActive(false);
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

    public void ButtonPress(string move)
    {
        switch (move)
        {
            case "tackle":
                descriptionText.text = "Ivysaur used Tackle!";
                vfxDuration = vfxs.TackleVFX();
                break;
            case "vine whip":
                descriptionText.text = "Ivysaur used Vine Whip!";
                vfxDuration = vfxs.VineWhipVFX();
                break;
            case "poison powder":
                descriptionText.text = "Ivysaur used Poison Powder!";
                vfxDuration = vfxs.PoisonPowderVFX();
                break;
            case "razer leaf":
                descriptionText.text = "Ivysaur used Razer Leaf!";
                vfxDuration = vfxs.RazerLeafVFX();
                break;
            default:
                print("No attack");
                break;
        }
        MoveToText();
        StartCoroutine(VFXWait());
    }

    IEnumerator VFXWait()
    {
        yield return new WaitForSeconds(vfxDuration);
        ContinueImage.SetActive(true);
        canContinue = true;
    }

    public void SetHPBarIvy(float value)
    {
        StartCoroutine(ReduceHPBar(IvyHPBar, value));
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
        StartCoroutine(ReduceHPBar(ZubatHPBar, value));
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

    IEnumerator ReduceHPBar(Slider bar, float newValue)
    {
        float currentValue = bar.value;
        float scaledSpeed = 0;
        while (bar.value != newValue)
        {
            bar.value = Mathf.Lerp(currentValue, newValue, scaledSpeed);
            scaledSpeed += loseHPSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
