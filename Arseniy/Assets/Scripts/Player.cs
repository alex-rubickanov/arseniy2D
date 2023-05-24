using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Weapon { Mortar = 1, Crossbow = 0, FireGun = -1 };

    [SerializeField] public Weapon activeGun;

    static Player instance;



    static Weapon topGun;
    static Weapon midGun;
    static Weapon botGun;

     public static GameObject topGunObj;
     public static GameObject midGunObj;
     public static GameObject botGunObj;

    private void Awake() //singleton
    {
        topGunObj = GameObject.Find("Mortar");
        midGunObj = GameObject.Find("Crossbow");
        botGunObj = GameObject.Find("FireGun");

        topGun = Weapon.Mortar;
        midGun = Weapon.Crossbow;
        botGun = Weapon.FireGun;

        activeGun = midGun;

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
        

        SwipeDetection.OnSwipeEvent += SwapWeapon;
        

        //Debug.Log(toMortar);
        //Debug.Log(toCrossbow);
        //Debug.Log(toThirdWeapon);

        DontDestroyOnLoad(gameObject);

        


    }

    private void Update()
    {
        activeGun = midGun;
        
    }






    //private void ChangeWeapon(int i)
    //{
    //    switch (activeGun + i)
    //    {
    //        case Weapon.Mortar:
    //            CheckAndStopCoroutine(toCrossbow);
    //            CheckAndStopCoroutine(toThirdWeapon);
    //            Debug.Log(activeGun);
    //            break;

    //        case Weapon.Crossbow:
    //            CheckAndStopCoroutine(toThirdWeapon);
    //            CheckAndStopCoroutine(toMortar);    
    //            Debug.Log(activeGun);
    //            break;

    //        case Weapon.FireGun:
    //            CheckAndStopCoroutine(toCrossbow);
    //            CheckAndStopCoroutine(toMortar);
    //            Debug.Log(activeGun);
    //            break; 
    //    }
    //}

    public void CheckAndStopCoroutine(Coroutine coroutine)
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    //private void ResetAllCoroutines()
    //{
    //    toMortar = null;
    //    toThirdWeapon = null;
    //    toCrossbow= null;
    //}
    private void SwapWeapon(int i)
    {
        if(i == 1) //SwipeUP
        {
            //Changing WEAPON
            Weapon bridge;
            bridge = topGun;
            topGun = midGun;
            midGun = botGun;
            botGun = bridge;

            //Changing OBJECTS' POSITION
            Vector3 bridgeObjPos;
            bridgeObjPos = topGunObj.transform.position;
            topGunObj.transform.position = botGunObj.transform.position;
            botGunObj.transform.position = midGunObj.transform.position;
            midGunObj.transform.position = bridgeObjPos;
            

            //Changing OBJECT
            GameObject bridgeObj;
            bridgeObj = topGunObj;
            topGunObj = midGunObj;
            midGunObj = botGunObj;
            botGunObj = bridgeObj;

        } else //SwipeDOWN
        {
            //Changing WEAPON
            Weapon bridge;
            bridge = botGun;
            botGun = midGun;
            midGun = topGun;
            topGun = bridge;

            //Changing OBJECTS' POSITION
            Vector3 bridgeObjPos;
            bridgeObjPos = botGunObj.transform.position;
            botGunObj.transform.position = topGunObj.transform.position;
            topGunObj.transform.position = midGunObj.transform.position;
            midGunObj.transform.position = bridgeObjPos;

            //Changing OBJECT
            GameObject bridgeObj;
            bridgeObj = botGunObj;
            botGunObj = midGunObj;
            midGunObj = topGunObj;
            topGunObj = bridgeObj;

        }

        
    }

    

}
