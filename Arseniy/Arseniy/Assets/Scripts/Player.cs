using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Weapon { Mortar = 1, Crossbow = 0, FireGun = -1 };
    
    public Weapon activeGun { get; private set; } = Weapon.Crossbow;
    [SerializeField] private float _time = 0.8f;

    static Player instance;

    Coroutine toMortar;
    Coroutine toCrossbow;
    Coroutine toThirdWeapon;

    private void Awake() //singleton
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        ResetAllCoroutines();
        SwipeDetection.OnSwipeEvent += ChangeWeapon;
        
        

        Debug.Log(toMortar);
        Debug.Log(toCrossbow);
        Debug.Log(toThirdWeapon);
        DontDestroyOnLoad(gameObject);
    }

    private void ChangeWeapon(int i)
    {
        switch (activeGun + i)
        {
            case Weapon.Mortar:
                CheckAndStopCoroutine(toCrossbow);
                CheckAndStopCoroutine(toThirdWeapon);
                toMortar = StartCoroutine(WeaponTransition(new Vector3(-7.5f, 3, 0), Weapon.Mortar));
                Debug.Log(activeGun);
                break;

            case Weapon.Crossbow:
                CheckAndStopCoroutine(toThirdWeapon);
                CheckAndStopCoroutine(toMortar);    
                toCrossbow = StartCoroutine(WeaponTransition(new Vector3(-7.5f, 0, 0), Weapon.Crossbow));
                Debug.Log(activeGun);
                break;

            case Weapon.FireGun:
                CheckAndStopCoroutine(toCrossbow);
                CheckAndStopCoroutine(toMortar);
                toThirdWeapon = StartCoroutine(WeaponTransition(new Vector3(-7.5f, -3, 0), Weapon.FireGun));
                Debug.Log(activeGun);
                break; 
        }
    }

    private IEnumerator WeaponTransition(Vector3 destination, Weapon weapon)
    {
        Vector3 startingPos = transform.position;
        Vector3 finalPos = destination;
        float elapsedTime = 0;

        while (elapsedTime < _time)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / _time));
            elapsedTime += Time.deltaTime;
            activeGun = weapon;
            yield return null;
        }
        
    }

    public void CheckAndStopCoroutine(Coroutine coroutine)
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    private void ResetAllCoroutines()
    {
        toMortar = null;
        toThirdWeapon = null;
        toCrossbow= null;
    }
}
