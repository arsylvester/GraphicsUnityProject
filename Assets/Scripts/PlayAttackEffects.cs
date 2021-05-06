// PlayAttackEffects.cs - This script manages all of the VFXs in the game. When an attack occurs this scripted is called on to play the right effect at the right time.
// Created by Andrew Sylvester

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAttackEffects : MonoBehaviour
{
    [SerializeField] GameObject mainPokemon;
    [SerializeField] GameObject enemyPokemon;
    [SerializeField] ParticleSystem TackleHitVFX;
    [SerializeField] ParticleSystem AirSlash;
    [SerializeField] ParticleSystem AirSlashBurst;
    [SerializeField] ParticleSystem PoisonPowderPS;
    [SerializeField] ParticleSystem PoisonBubblesPS;
    [SerializeField] ParticleSystem PoisonBubblesIvyPS;
    [SerializeField] ParticleSystem RazerLeafPS;
    [SerializeField] ParticleSystem VineWhipPS;
    [SerializeField] ParticleSystem AirCutterPS;
    [SerializeField] ParticleSystem VenoshockPS;
    [SerializeField] float tackleDistance = 1;
    [SerializeField] float tackleSpeed = 1;
    [SerializeField] float AirSlashDistance = 1;
    [SerializeField] float AirSlashSpeed = 1;
    [SerializeField] float DropDistance = 1;
    [SerializeField] float DropSpeed = 1;
    [SerializeField] Material enemyPoisonMaterial;
    [SerializeField] Material playerPoisonMaterial;
    [SerializeField] AudioClip PoisonPowderClip;
    [SerializeField] AudioClip TackleClip;
    [SerializeField] AudioClip AirSlashClip;
    [SerializeField] AudioClip RazerLeafClip;
    [SerializeField] AudioClip VineWhipClip;
    [SerializeField] AudioClip AirCutterClip;
    [SerializeField] AudioClip VenoshockClip;
    AudioPlayer audio;
    Material enemyMaterial;
    Material playerMaterial;

    // Start is called before the first frame update
    void Start()
    {
        tackleDistance += mainPokemon.transform.localPosition.z;
        AirSlashDistance += enemyPokemon.transform.localPosition.x;
        audio = FindObjectOfType<AudioPlayer>();
        enemyMaterial = enemyPokemon.GetComponentInChildren<Renderer>().material;
        playerMaterial = mainPokemon.GetComponentInChildren<Renderer>().material;
    }

    public float PoisonPowderVFX()
    {
        PoisonPowderPS.Play();
        //Swap to poison material
        Renderer[] rends = enemyPokemon.GetComponentsInChildren<Renderer>();
        foreach( Renderer rend in rends)
        {
            rend.material = enemyPoisonMaterial;
        }
        PoisonBubblesPS.Play();
        audio.PlaySound(PoisonPowderClip);
        return PoisonPowderPS.main.duration;
    }

    public float RazerLeafVFX()
    {
        RazerLeafPS.Play();
        audio.PlaySound(RazerLeafClip);
        StartCoroutine(FlickerPokemon(enemyPokemon, 8));
        return RazerLeafPS.main.duration;
    }
    public float AirCutterVFX()
    {
        AirCutterPS.Play();
        audio.PlaySound(AirCutterClip);
        StartCoroutine(FlickerPokemon(mainPokemon, 6));
        return AirCutterPS.main.duration;
    }

    public float VineWhipVFX()
    {
        VineWhipPS.Play();
        audio.PlaySound(VineWhipClip);
        StartCoroutine(FlickerPokemon(enemyPokemon, 5));
        return VineWhipPS.main.duration;
    }

    public float VenoshockVFX()
    {
        VenoshockPS.Play();
        audio.PlaySound(VenoshockClip);
        Renderer[] rends = mainPokemon.GetComponentsInChildren<Renderer>();
        //Swap to poison material
        foreach (Renderer rend in rends)
        {
            rend.material = playerPoisonMaterial;
        }
        StartCoroutine(FlickerPokemon(mainPokemon, 8));
        PoisonBubblesIvyPS.Play();
        return VenoshockPS.main.duration;
    }

    public float TackleVFX()
    {
        StartCoroutine(TackleCourtine());
        StartCoroutine(FlickerPokemon(enemyPokemon, 5));
        return TackleHitVFX.main.duration;
    }

    public float AirSlashVFX()
    {
        StartCoroutine(AirSlashCourtine());
        StartCoroutine(FlickerPokemon(mainPokemon, 5));
        return AirSlash.main.duration;
    }

    //Swap back to regular material
    public void DisablePoisonIvy()
    {
        Renderer[] rends = mainPokemon.GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in rends)
        {
            rend.material = playerMaterial;
        }
        PoisonBubblesPS.Stop();
    }

    //Swap back to regular material
    public void DisablePoisonZubat()
    {
        Renderer[] rends = enemyPokemon.GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in rends)
        {
            rend.material = enemyMaterial;
        }
        PoisonBubblesPS.Stop();
    }

    //When dead drop through ground
    public void ZubatDrop()
    {
        StartCoroutine(DropPokemon(enemyPokemon.transform));
    }

    public void IvyDrop()
    {
        StartCoroutine(DropPokemon(mainPokemon.transform));
    }

    //Courtine to show Zubat moving forward then back quickly
    IEnumerator AirSlashCourtine()
    {
        float startPosition = enemyPokemon.transform.localPosition.x;
        float scaledSpeed = 0;

        while (AirSlashDistance != enemyPokemon.transform.localPosition.x)
        {
            enemyPokemon.transform.localPosition = new Vector3(Mathf.LerpAngle(startPosition, AirSlashDistance, scaledSpeed), enemyPokemon.transform.localPosition.y, enemyPokemon.transform.localPosition.z);
            scaledSpeed += AirSlashSpeed * Time.deltaTime;
            //print(scaledSpeed);
            yield return new WaitForEndOfFrame();
        }
        AirSlash.Play();
        AirSlashBurst.Play();
        audio.PlaySound(AirSlashClip);
        while (startPosition != enemyPokemon.transform.localPosition.x)
        {
            enemyPokemon.transform.localPosition = new Vector3(Mathf.LerpAngle(startPosition, AirSlashDistance, scaledSpeed), enemyPokemon.transform.localPosition.y, enemyPokemon.transform.localPosition.z);
            scaledSpeed -= AirSlashSpeed * Time.deltaTime;
            //print(scaledSpeed);
            yield return new WaitForEndOfFrame();
        }
    }

    //Courtine to show Ivysaur moving forward then back quickly
    IEnumerator TackleCourtine()
    {
        float startPosition = mainPokemon.transform.localPosition.z;
        float scaledSpeed = 0;

        while (tackleDistance != mainPokemon.transform.localPosition.z)
        {
            mainPokemon.transform.localPosition = new Vector3(mainPokemon.transform.localPosition.x, mainPokemon.transform.localPosition.y, Mathf.LerpAngle(startPosition, tackleDistance, scaledSpeed));
            scaledSpeed += tackleSpeed * Time.deltaTime;
            //print(scaledSpeed);
            yield return new WaitForEndOfFrame();
        }
        TackleHitVFX.Play();
        audio.PlaySound(TackleClip);
        while (startPosition != mainPokemon.transform.localPosition.z)
        {
            mainPokemon.transform.localPosition = new Vector3(mainPokemon.transform.localPosition.x, mainPokemon.transform.localPosition.y, Mathf.LerpAngle(startPosition, tackleDistance, scaledSpeed));
            scaledSpeed -= tackleSpeed * Time.deltaTime;
            //print(scaledSpeed);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator DropPokemon(Transform pokemon)
    {
        float startPosition = pokemon.localPosition.y;
        float scaledSpeed = 0;

        while (DropDistance != pokemon.localPosition.y)
        {
            pokemon.localPosition = new Vector3(pokemon.localPosition.x, Mathf.LerpAngle(startPosition, DropDistance, scaledSpeed), pokemon.localPosition.z);
            scaledSpeed += DropSpeed * Time.deltaTime;
            //print(scaledSpeed);
            yield return new WaitForEndOfFrame();
        }
        PoisonBubblesPS.Stop();
        PoisonBubblesIvyPS.Stop();
    }

    //Flickers the pokemon to show that the pokemon was damaged
    IEnumerator FlickerPokemon(GameObject pokemon, int duration)
    {
        Renderer[] rends = pokemon.GetComponentsInChildren<Renderer>();
        for (int x = 0; x < duration; x++)
        {
            foreach (Renderer rend in rends)
            {
                rend.enabled = false;
            }
            yield return new WaitForSeconds(.1f);
            foreach (Renderer rend in rends)
            {
                rend.enabled = true;
            }
            yield return new WaitForSeconds(.1f);
        }
    }

}
