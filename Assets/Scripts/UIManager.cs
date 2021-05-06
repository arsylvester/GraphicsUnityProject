// UIManager.cs - this script manages all UI processes in the game. Anytime the UI states needs to change this script handles it. All UI buttons interface with this script
// which in turn interfaces with the other scripts to run the game. 
// Created by Andrew Sylvester

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject attackButtonsPanel;
    [SerializeField] GameObject attackTextPanel;
    [SerializeField] GameObject optionsButtonsPanel;
    [SerializeField] GameObject optionsTextPanel;
    [SerializeField] GameObject descriptionTextPanel;
    [SerializeField] GameObject EndPanel;
    [SerializeField] Text endText;
    [SerializeField] Text ppText;
    [SerializeField] Text typeText;
    [SerializeField] Text descriptionText;
    [SerializeField] Slider IvyHPBar;
    [SerializeField] Image IvyHPFill;
    [SerializeField] Text IvyHpText;
    [SerializeField] Slider ZubatHPBar;
    [SerializeField] Image ZubatHPFill;
    [SerializeField] Text ZubatHpText;
    Color HighHPColor;
    [SerializeField] Color MediumHPColor;
    [SerializeField] Color LowHPColor;
    [SerializeField] float loseHPSpeed = 1;
    [SerializeField] PlayAttackEffects vfxs;
    [SerializeField] GameObject ContinueImage;
    [SerializeField] AudioClip selectClip;
    [SerializeField] int tacklePP = 35;
    [SerializeField] int vinewhipPP = 25;
    [SerializeField] int poisonPowderPP = 35;
    [SerializeField] int razerleafPP = 25;
    float vfxDuration;
    bool canContinue;
    GameManager1 gameManager;
    AudioPlayer audio;

    // Start is called before the first frame update
    void Start()
    {
        HighHPColor = IvyHPFill.color;
        audio = FindObjectOfType<AudioPlayer>();
        gameManager = FindObjectOfType<GameManager1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canContinue)
        {
            canContinue = false;
            gameManager.NextTurn();
            audio.PlaySound(selectClip);
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
                ppText.text = "PP " + tacklePP + "/35";
                typeText.text = "Normal";
                break;
            case "vine whip":
                ppText.text = "PP " + vinewhipPP + "/25";
                typeText.text = "Grass";
                break;
            case "poison powder":
                ppText.text = "PP " + poisonPowderPP + "/35";
                typeText.text = "Poison";
                break;
            case "razer leaf":
                ppText.text = "PP " + razerleafPP + "/25";
                typeText.text = "Grass";
                break;
            default:
                print("No attack");
                break;
        }
    }

    public void UseMove(string move)
    {
        switch (move)
        {
            case "tackle":
                descriptionText.text = "Ivysaur used Tackle!";
                vfxDuration = vfxs.TackleVFX();
                gameManager.tackleturn();
                tacklePP--;
                break;
            case "vine whip":
                descriptionText.text = "Ivysaur used Vine Whip!";
                vfxDuration = vfxs.VineWhipVFX();
                gameManager.vinewhipturn();
                vinewhipPP--;
                break;
            case "poison powder":
                descriptionText.text = "Ivysaur used Poison Powder!";
                vfxDuration = vfxs.PoisonPowderVFX();
                gameManager.poisonpowderturn();
                poisonPowderPP--;
                break;
            case "razer leaf":
                descriptionText.text = "Ivysaur used Razer Leaf!";
                vfxDuration = vfxs.RazerLeafVFX();
                gameManager.razerleafturn();
                razerleafPP--;
                break;
            case "air slash":
                descriptionText.text = "Zubat used Air Slash!";
                vfxDuration = vfxs.AirSlashVFX();
                //gameManager.AirSlashTurn();
                break;
            case "air cutter":
                descriptionText.text = "Zubat used Air Cutter!";
                vfxDuration = vfxs.AirCutterVFX();
                //gameManager.AirCutterTurn();
                break;
            case "venoshock":
                descriptionText.text = "Zubat used Venoshock!";
                vfxDuration = vfxs.VenoshockVFX();
                //gameManager.VenoshockTurn();
                break;
            case "poisonedIvy":
                descriptionText.text = "Ivysuar was hurt by the poison!";
                vfxDuration = 1;
                //gameManager.VenoshockTurn();
                break;
            case "poisonedZubat":
                descriptionText.text = "Zubat was hurt by the poison!";
                vfxDuration = 1;
                //gameManager.VenoshockTurn();
                break;
            default:
                print("No attack");
                break;
        }
        MoveToText();
        StartCoroutine(VFXWait());
    }

    //Wait for VFX to finish before displaying continue
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
        IvyHpText.text = value + "/100";
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
        ZubatHpText.text = value + "/100";
    }

    //Display the hp going down through this coroutine
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

    public void DisplayWin()
    {
        MoveToClear();
        endText.text = "Zubat was defeated. You win!";
        EndPanel.SetActive(true);
        audio.ChangeToWinMusic();
    }

    public void DisplayLoss()
    {
        MoveToClear();
        endText.text = "Your Ivysaur was defeated. You Lose!";
        EndPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
