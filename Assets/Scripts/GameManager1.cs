// GameManger.cs - This keeps track of all of the numbers and the overall flow of the game.
// Created by Yash Bhalavat and edited by Andrew Sylvester

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum battleturn {PLAYERTURN, ENEMYTURN};

public class GameManager1 : MonoBehaviour
{

    public int enemyhealth = 100;
    public int currHealth = 100;
    public int damage = 0;
    public bool playerturn = true;
    public int poisondamage = 5;
    public int playerPoisonCounter = 0;
    public int enemyPoisonCounter = 0;
    //Attack Damages
    [SerializeField] int airSlashDamage = 20;
    [SerializeField] int airCutterDamage = 25;
    [SerializeField] int venoshockDamage = 15;
    [SerializeField] int tackleDamage = 20;
    [SerializeField] int vineWhipDamage = 25;
    [SerializeField] int razerLeafDamage = 25;
    [SerializeField] int poisonPowderDamage = 10;
    UIManager ui;
    PlayAttackEffects effects;
    bool poisonThisTurn = false;
    
    // Start is called before the first frame update
    void Start()
    {
        ui = FindObjectOfType<UIManager>();
        effects = FindObjectOfType<PlayAttackEffects>();
    }

    public void NextTurn()
    {
        //Check if won or loss
        if(currHealth < 1)
        {
            ui.DisplayLoss();
            effects.IvyDrop();
        }
        else if(enemyhealth < 1)
        {
            ui.DisplayWin();
            effects.ZubatDrop();
        }
        else
        {
            if (playerturn)
            {
                //If zubat is poisoned deal damage at the start of player turn
                if (!poisonThisTurn && enemyPoisonCounter > 0)
                {
                    enemyhealth = dealDamage(enemyhealth, poisondamage);
                    ui.SetHPBarZubat(enemyhealth);
                    ui.UseMove("poisonedZubat");
                    poisonThisTurn = true;
                    if (--enemyPoisonCounter <= 0)
                    {
                        effects.DisablePoisonZubat();
                    }
                }
                else
                {
                    poisonThisTurn = false;
                    ui.MoveToOptions();
                }
            }
            else
            {
                //If Ivysaur is poisoned deal damage at the start of enemy turn
                if (!poisonThisTurn && playerPoisonCounter > 0)
                {
                    currHealth = dealDamage(currHealth, poisondamage);
                    ui.SetHPBarIvy(currHealth);
                    ui.UseMove("poisonedIvy");
                    poisonThisTurn = true;
                    if(--playerPoisonCounter <= 0)
                    {
                        effects.DisablePoisonIvy();
                    }
                }
                else
                {
                    poisonThisTurn = false;
                    ZubatTurn();
                }
            }
        }
    }

    public void ZubatTurn()
    {
        if (!playerturn)
        {
            int attack = Random.Range(0, 3);
            print(attack);
            if(attack == 0)
            {
                AirSlashTurn();
            }
            else if(attack == 1)
            {
                AirCutterTurn();
            }
            else
            {
                VenoshockTurn();
            }
        }
    }

    //Zubat
    public void AirSlashTurn()
    {
        currHealth = dealDamage(currHealth, airSlashDamage);
        ui.SetHPBarIvy(currHealth);
        ui.UseMove("air slash");
        Debug.Log("this is current health after tackle" + currHealth);
        playerturn = true;
    }
    public void AirCutterTurn()
    {
        currHealth = dealDamage(currHealth, airCutterDamage);
        ui.SetHPBarIvy(currHealth);
        ui.UseMove("air cutter");
        Debug.Log("this is current health after tackle" + currHealth);
        playerturn = true;
    }
    public void VenoshockTurn()
    {
        currHealth = dealDamage(currHealth, venoshockDamage);
        ui.SetHPBarIvy(currHealth);
        ui.UseMove("venoshock");
        Debug.Log("this is current health after tackle" + currHealth);
        playerturn = true;
        poisonThisTurn = false;
        playerPoisonCounter = 3;
    }

    //Ivysaur
    public void tackleturn()
    {
        if(playerturn == true)
        {
            enemyhealth = tackelattack(enemyhealth);
            ui.SetHPBarZubat(enemyhealth);
            Debug.Log("this is enemy health after tackle" + enemyhealth);
            playerturn = false;

        }
    }

    public void vinewhipturn()
    {
        if(playerturn == true)
        {
            enemyhealth = vinewhip(enemyhealth);
            ui.SetHPBarZubat(enemyhealth);
            Debug.Log("this is enemy health after vine whip" + enemyhealth);
            playerturn = false;
        }
    }

    public void poisonpowderturn()
    {
        if(playerturn == true)
        {
            enemyPoisonCounter = 3;
            enemyhealth = poisonpowder(enemyhealth);
            ui.SetHPBarZubat(enemyhealth);
            Debug.Log("this is enemy health after poision powder" + enemyhealth);
            playerturn = false;
            poisonThisTurn = false;
        }
    }


    public void razerleafturn()
    {
        if(playerturn == true)
        {
            enemyhealth = razerleaf(enemyhealth);
            ui.SetHPBarZubat(enemyhealth);
            Debug.Log("this is enemy health after razer leaf" + enemyhealth);
            playerturn = false;
        }
    }


    // Attacks of the  user      
    public int tackelattack(int hp)
    {
        damage = tackleDamage;  
        if(hp - damage  <= 0)
        {
            Debug.Log("You Died in tackle attack");
            return 0;
        }
        hp = hp - damage;  

        return hp;
    }

      public int vinewhip(int hp){
        damage = vineWhipDamage;  
        if(hp - damage  <= 0)
        {
            Debug.Log("You Died in vine whip");
            return 0;
        }
    
        hp = hp - damage;
    
        return hp;
    }


    public int poisonpowder(int hp){
        damage = poisonPowderDamage;
        if(hp - damage  <= 0)
        {
            Debug.Log("You Died in poision powder");
            return 0;
        }
        hp = hp - damage;
         return hp;
    }


    public int razerleaf(int hp){
        damage = 25;
        if(hp - damage  <= 0)
        {
            Debug.Log("You Died in razer leaf");
            return 0;
        }
        hp = hp - damage;
    
        return hp;
    }

    private int dealDamage(int hp, int damageDealt)
    {
        if (hp - damageDealt <= 0)
        {
            return 0;
        }
        hp = hp - damageDealt;

        return hp;
    }


}
