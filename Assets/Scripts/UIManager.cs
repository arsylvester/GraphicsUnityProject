using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject attackButtonsPanel;
    [SerializeField] GameObject attackTextPanel;
    [SerializeField] GameObject optionsButtonsPanel;
    [SerializeField] GameObject optionsTextPanel;

    // Start is called before the first frame update
    void Start()
    {
        
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
}
