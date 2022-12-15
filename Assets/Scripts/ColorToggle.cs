using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorToggle : MonoBehaviour
{
    public enum OurColorsEnum
    {
        red,
        green,
        blue,
        orange,
        yellow,
        lightblue,
        purple,
        pink,
        white,
        black
    }

    public OurColorsEnum color;
    public SearchResults searchResults;
    
    public void ChangeColorFilter()
    {
        searchResults.ChangeColorFilter(GetOurColor());
    }

    public OurColors GetOurColor()
    {
        switch (color)
        {
            case OurColorsEnum.red:
                return OurColors.red;
            case OurColorsEnum.green:
                return OurColors.green;
            case OurColorsEnum.purple:
                return OurColors.purple;
            case OurColorsEnum.blue:
                return OurColors.blue;
            case OurColorsEnum.orange:
                return OurColors.orange;
            case OurColorsEnum.yellow:
                return OurColors.yellow;
            case OurColorsEnum.lightblue:
                return OurColors.lightblue;
            case OurColorsEnum.pink:
                return OurColors.pink ;
            case OurColorsEnum.white:
                return OurColors.white;
            case OurColorsEnum.black:
                return OurColors.black;
            default:
                return OurColors.blue;
        }
    }
}
