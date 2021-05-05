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

    public float PoisonPowderVFX()
    {
        PoisonPowderPS.Play();
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
        return RazerLeafPS.main.duration;
    }
    public float AirCutterVFX()
    {
        AirCutterPS.Play();
        audio.PlaySound(AirCutterClip);
        return AirCutterPS.main.duration;
    }

    public float VineWhipVFX()
    {
        VineWhipPS.Play();
        audio.PlaySound(VineWhipClip);
        return VineWhipPS.main.duration;
    }

    public float VenoshockVFX()
    {
        VenoshockPS.Play();
        audio.PlaySound(VenoshockClip);
        Renderer[] rends = mainPokemon.GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in rends)
        {
            rend.material = playerPoisonMaterial;
        }
        PoisonBubblesPS.Play();
        return VenoshockPS.main.duration;
    }

    public float TackleVFX()
    {
        StartCoroutine(TackleCourtine());
        return TackleHitVFX.main.duration;
    }

    public float AirSlashVFX()
    {
        StartCoroutine(AirSlashCourtine());
        return AirSlash.main.duration;
    }

    public void DisablePoisonIvy()
    {
        Renderer[] rends = mainPokemon.GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in rends)
        {
            rend.material = playerMaterial;
        }
        PoisonBubblesPS.Stop();
    }

    public void DisablePoisonZubat()
    {
        Renderer[] rends = enemyPokemon.GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in rends)
        {
            rend.material = enemyMaterial;
        }
        PoisonBubblesPS.Stop();
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
