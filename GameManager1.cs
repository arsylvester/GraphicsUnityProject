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
    public int poisiondamage;
    public int counter = 0;
    
    // Start is called before the first frame update
    void Start()
    {
            poisonpowderturn();
            Debug.Log("coubnter is : " + counter);
            tackleturn();
            vinewhipturn();
            razerleafturn();

            Debug.Log("hello this is current" + currHealth);
         Debug.Log("hello this is enenmy" + enemyhealth);



    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void buttonchanger(string moves)
    {
        // if(moves == "tackle")
        // {
        //                 tackleturn();
        // }

        // if(moves == "vine whip")
        // {
                    
        //         vinewhipturn();
        // }
    }
    public void tackleturn()
    {
        if(playerturn == true)
        {
            enemyhealth = tackelattack(enemyhealth);
            Debug.Log("this is enemy health after tackle" + enemyhealth);
            playerturn = false;

            if(counter > 0)
            {
                currHealth = currHealth - 5;
                counter--;
            }
        }

        else if (playerturn == false) {

            currHealth = tackelattack(currHealth);
            Debug.Log("this is current health after tackle" + currHealth);
            playerturn = true;

            if(counter > 0)
            {
                enemyhealth = enemyhealth - 5;
                counter--;
            }
        }
    }

    public void vinewhipturn()
    {
        if(playerturn == true)
        {
            enemyhealth = vinewhip(enemyhealth);
            Debug.Log("this is enemy health after vine whip" + enemyhealth);
            playerturn = false;

            if(counter > 0)
            {
                currHealth = currHealth - 5;
                counter--;
            }
        }

        else if (playerturn == false) {

            currHealth = vinewhip(currHealth);
            Debug.Log("this is current health after vine whip" + currHealth);
            playerturn = true;

             if(counter > 0)
            {
                enemyhealth = enemyhealth - 5;
                counter--;
            }
        }
    }

        public void poisonpowderturn()
    {
        if(playerturn == true)
        {
            counter = counter+3;
            enemyhealth = poisonpowder(enemyhealth);
            Debug.Log("this is enemy health after poision powder" + enemyhealth);
            playerturn = false;

            if(counter > 0)
            {
                currHealth = currHealth - 5;
                counter--;
            }
        }

        else if (playerturn == false) {
            counter = counter+3;
            currHealth = poisonpowder(currHealth);
            Debug.Log("this is current health after poision powder" + currHealth);
            playerturn = true;

            if(counter > 0)
            {
                enemyhealth = enemyhealth - 5;
                counter--;
            }
        }
    }


        public void razerleafturn()
    {
        if(playerturn == true)
        {
            enemyhealth = razerleaf(enemyhealth);
            Debug.Log("this is enemy health after razer leaf" + enemyhealth);
            playerturn = false;

            if(counter > 0)
            {
                currHealth = currHealth - 5;
                counter--;
            }
        }

        else if (playerturn == false) {

            currHealth = razerleaf(currHealth);
            Debug.Log("this is current health after razer leaf" + currHealth);
            playerturn = true;

            if(counter > 0)
            {
                enemyhealth = enemyhealth - 5;
                counter--;
            }
        }
    }



// Attacks of the  user      
    public int tackelattack(int hp)
    {
        damage = 35;  
        if(hp - damage  <= 0)
        {
            Debug.Log("You Died in tackle attack");
            return -1;
        }
        hp = hp - damage;  

        return hp;
    }

      public int vinewhip(int hp){
        damage = 25;  
        if(hp - damage  <= 0)
        {
            Debug.Log("You Died in vine whip");
            return -1;
        }
    
        hp = hp - damage;
    
        return hp;
    }


    public int poisonpowder(int hp){
        damage = 35;
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


}
