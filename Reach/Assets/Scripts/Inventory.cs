using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



//public static class Vibration
//{

//#if UNITY_ANDROID && !UNITY_EDITOR
//    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
//    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
//    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
//#else
//    public static AndroidJavaClass unityPlayer;
//    public static AndroidJavaObject currentActivity;
//    public static AndroidJavaObject vibrator;
//#endif

//    public static void Vibrate()
//    {
//        if (isAndroid())
//            vibrator.Call("vibrate");
//        else
//            Handheld.Vibrate();
//    }


//    public static void Vibrate(long milliseconds)
//    {
//        if (isAndroid())
//            vibrator.Call("vibrate", milliseconds);
//        else
//            Handheld.Vibrate();
//    }

//    public static void Vibrate(long[] pattern, int repeat)
//    {
//        if (isAndroid())
//            vibrator.Call("vibrate", pattern, repeat);
//        else
//            Handheld.Vibrate();
//    }

//    public static bool HasVibrator()
//    {
//        return isAndroid();
//    }

//    public static void Cancel()
//    {
//        if (isAndroid())
//            vibrator.Call("cancel");
//    }

//    private static bool isAndroid()
//    {
//#if UNITY_ANDROID && !UNITY_EDITOR
//	return true;
//#else
//        return false;
//#endif
//    }
//}

public class Inventory : MonoBehaviour
{
    [HideInInspector]
    public static List<ItemSlot> ItemSlots;

    private void Awake()
    {
        ItemSlots = GetComponentsInChildren<ItemSlot>().ToList();
    }

    private void Start()
    {
        AddItem(Resources.Load<Item>("ScriptableObjects/TestItem"));

        //if (isAndroid())
        //{
        //    vibrator.Call("vibrate", 3000);
        //}

        StartCoroutine(RythmVibrate());
        //InvokeRepeating("VibratePhone", 0, 2);
        //Vibration.vibrator.Call("vibrate", 3000);
    }
    IEnumerator RythmVibrate()
    {
        Vibration.CreateOneShot(1000);

        yield return new WaitForSeconds(2f);

        Vibration.CreateOneShot(500);

        yield return new WaitForSeconds(1);

        Vibration.CreateOneShot(1000);

        yield return new WaitForSeconds(2);

        Vibration.CreateOneShot(500);

        yield return new WaitForSeconds(1);

        Vibration.CreateOneShot(500);

        yield return new WaitForSeconds(1);

        Vibration.CreateOneShot(1000);

        yield return new WaitForSeconds(2);

        Vibration.CreateOneShot(500);

        yield return new WaitForSeconds(1);

        Vibration.CreateOneShot(500);
        ////2x 1x 2x 1x  1x 2x 1x 1x
        //float mutliplePause = 0.2f;
        //float newPause = 3f;

        //Handheld.Vibrate();
        //yield return new WaitForSeconds(mutliplePause);
        //Handheld.Vibrate();
        //yield return new WaitForSeconds(mutliplePause);
        //Handheld.Vibrate();

        //yield return new WaitForSeconds(newPause);

        //Handheld.Vibrate();

        //yield return new WaitForSeconds(newPause);

        //Handheld.Vibrate();
        //yield return new WaitForSeconds(mutliplePause);
        //Handheld.Vibrate();
        //yield return new WaitForSeconds(mutliplePause);
        //Handheld.Vibrate();

        //yield return new WaitForSeconds(newPause);

        //Handheld.Vibrate();

        //yield return new WaitForSeconds(newPause);

        //Handheld.Vibrate();

        //yield return new WaitForSeconds(newPause);

        //Handheld.Vibrate();
        //yield return new WaitForSeconds(mutliplePause);
        //Handheld.Vibrate();
        //yield return new WaitForSeconds(mutliplePause);
        //Handheld.Vibrate();

        //yield return new WaitForSeconds(newPause);

        //Handheld.Vibrate();

        //yield return new WaitForSeconds(newPause);

        //Handheld.Vibrate();

        //yield return new WaitForSeconds(newPause);

        //Handheld.Vibrate();
        //yield return new WaitForSeconds(mutliplePause);
        //Handheld.Vibrate();
        //yield return new WaitForSeconds(mutliplePause);
        //Handheld.Vibrate();
        //yield return new WaitForSeconds(mutliplePause);
        //Handheld.Vibrate();
        //yield return new WaitForSeconds(mutliplePause);
        //Handheld.Vibrate();
        //yield return new WaitForSeconds(mutliplePause);
        //Handheld.Vibrate();

        yield return new WaitForSeconds(4);
        StartCoroutine(RythmVibrate());
    }

    private void VibratePhone()
    {
        Handheld.Vibrate();
    }

    public static void AddItem(Item item)
    {
        int indexOfEmptyItemSlot = -1;
        for (int i = 0; i < ItemSlots.Count; i++)
        {
            ItemSlot itemSlot = ItemSlots[i];
            if (itemSlot.Item == null)
            {
                indexOfEmptyItemSlot = i;
                break;
            }
        }

        if (indexOfEmptyItemSlot > -1)
        {
            //An empty item slot is found
            ItemSlot emptyItemSlot = ItemSlots[indexOfEmptyItemSlot];
            emptyItemSlot.AddItem(item);
        }
        else
        {
            //All item slots have items
        }
    }
}