using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public static class Utils
{
    // Returns the float number with X decimals.
    public static float RoundFloat(float number, int decimals)
    {
        float mult = Mathf.Pow(10.0f, decimals);
        float rounded = Mathf.Round(number * mult) / mult;
        return rounded;
    }


    // Define la musica actual que está sonando
    public enum PlayingNow
    {
        NONE,
        MAINMENU,
        INGAME
    };

    // Define el tipo de audio, para controlar volumenes
    public enum AudioType
    {
        NONE,
        EFFECT,
        MUSIC,
        VOICE
    };

    // Define el tipo de audio, para controlar su spawn aleatorio
    public enum SpecialEffect
    {
        NONE,
        TYPE1,
        TYPE2,
    };

    public enum MusicZone
    {
        NONE,
        INGAME,
        MENU,
    };

}