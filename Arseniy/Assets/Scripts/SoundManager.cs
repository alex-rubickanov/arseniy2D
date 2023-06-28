using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClipsRefsSO audioClipsRefsSO;
    

    [SerializeField] private FireGun firegun;
    private AudioSource firegunAudioSource;

    [SerializeField] private float ballistaShotVolume;
    [SerializeField] private float arrowHitVolume;
    [SerializeField] private float mortarShotVolume;
    [SerializeField] private float bombExplosionVolume;
    [SerializeField] private float firegunParticleVolume;
    [SerializeField] private float superBombMagnetEffectVolume;

    private void Start()
    {
        Crossbow.OnBallistaDefaultShot += Crossbow_OnBallistaDefaultShot;
        Crossbow.OnBallistaSuperShot += Crossbow_OnBallistaSuperShot;
        Mortar.OnMortarShot += Mortar_OnMortarShot;
        FireGun.OnFireGunStartShooting += FireGun_OnFireGunStartShooting;
        FireGun.OnFireGunStopShooting += FireGun_OnFireGunStopShooting;

        firegunAudioSource = firegun.GetComponent<AudioSource>();

        BombProjectile.OnBombExplosion += BombProjectile_OnBombExplostion;
        SuperBombProjectile.OnSuperBombActivation += SuperBombProjectile_OnSuperBombActivation;
        ArrowProjectile.OnArrowHit += ArrowProjectile_OnArrowHit;
    }

    private void ArrowProjectile_OnArrowHit(object sender, System.EventArgs e)
    {
        ArrowProjectile arrowProjectile = sender as ArrowProjectile;
        PlaySound(audioClipsRefsSO.arrowHitSound, arrowProjectile.transform.position, arrowHitVolume);
    }

    private void BombProjectile_OnBombExplostion(object sender, System.EventArgs e)
    {
        BombProjectile bombProjectile = sender as BombProjectile;
        PlaySound(audioClipsRefsSO.bombExplosionSound, bombProjectile.transform.position, bombExplosionVolume);
    }

    private void SuperBombProjectile_OnSuperBombActivation(object sender, System.EventArgs e)
    {
        SuperBombProjectile superBombProjectile = sender as SuperBombProjectile;
        PlaySound(audioClipsRefsSO.bombExplosionSound, superBombProjectile.transform.position, bombExplosionVolume);
        PlaySound(audioClipsRefsSO.superBombExplosionSound, superBombProjectile.transform.position, superBombMagnetEffectVolume);
    }

    private void FireGun_OnFireGunStopShooting(object sender, System.EventArgs e)
    {
        firegunAudioSource.Pause();
    }

    private void FireGun_OnFireGunStartShooting(object sender, System.EventArgs e)
    {
        firegunAudioSource.clip = audioClipsRefsSO.firegunParticleSound;
        firegunAudioSource.volume = firegunParticleVolume;
        firegunAudioSource.Play();
    }

    private void Mortar_OnMortarShot(object sender, System.EventArgs e)
    {
        Mortar mortar = sender as Mortar;
        PlaySound(audioClipsRefsSO.mortarShotSound, mortar.transform.position, mortarShotVolume);
    }

    private void Crossbow_OnBallistaSuperShot(object sender, System.EventArgs e)
    {
        Crossbow ballista = sender as Crossbow;
        PlaySound(audioClipsRefsSO.ballistaAbilityShotSound, ballista.transform.position, ballistaShotVolume);
    }

    private void Crossbow_OnBallistaDefaultShot(object sender, System.EventArgs e)
    {
        Crossbow ballista = sender as Crossbow;
        PlaySound(audioClipsRefsSO.ballistaShotSound, ballista.transform.position, ballistaShotVolume);
    }



    private void PlaySound(AudioClip audioClip, Vector3 position, float volume)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }
}
