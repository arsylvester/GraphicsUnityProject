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
    [SerializeField] ParticleSystem RazerLeafPS;
    [SerializeField] ParticleSystem VineWhipPS;
    [SerializeField] ParticleSystem AirCutterPS;
    [SerializeField] ParticleSystem VenoshockPS;
    [SerializeField] float tackleDistance = 1;
    [SerializeField] float tackleSpeed = 1;
    [SerializeField] float AirSlashDistance = 1;
    [SerializeField] float AirSlashSpeed = 1;
    [SerializeField] Material enemyPoisonMaterial;
    [SerializeField] AudioClip PoisonPowderClip;
    [SerializeField] AudioClip TackleClip;
    [SerializeField] AudioClip AirSlashClip;
    [SerializeField] AudioClip RazerLeafClip;
    [SerializeField] AudioClip VineWhipClip;
    [SerializeField] AudioClip AirCutterClip;
    [SerializeField] AudioClip VenoshockClip;
    AudioPlayer audio;

    // Start is called before the first frame update
    void Start()
    {
        tackleDistance += mainPokemon.transform.localPosition.z;
        AirSlashDistance += enemyPokemon.transform.localPosition.x;
        audio = FindObjectOfType<AudioPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            TackleVFX();
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            AirSlashVFX();
        }
        else if(Input.GetKeyDown(KeyCode.P))
        {
            PoisonPowderVFX();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            AirCutterVFX();
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            VenoshockVFX();
        }
    }

    public void PoisonPowderVFX()
    {
        PoisonPowderPS.Play();
        Renderer[] rends = enemyPokemon.GetComponentsInChildren<Renderer>();
        foreach( Renderer rend in rends)
        {
            rend.material = enemyPoisonMaterial;
        }
        PoisonBubblesPS.Play();
        audio.PlaySound(PoisonPowderClip);
    }

    public void RazerLeafVFX()
    {
        RazerLeafPS.Play();
        audio.PlaySound(RazerLeafClip);
    }
    public void AirCutterVFX()
    {
        AirCutterPS.Play();
        audio.PlaySound(AirCutterClip);
    }

    public void VineWhipVFX()
    {
        VineWhipPS.Play();
        audio.PlaySound(VineWhipClip);
    }

    public void VenoshockVFX()
    {
        VenoshockPS.Play();
        audio.PlaySound(VenoshockClip);
    }

    public void TackleVFX()
    {
        StartCoroutine(TackleCourtine());
    }

    public void AirSlashVFX()
    {
        StartCoroutine(AirSlashCourtine());
    }

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
}
