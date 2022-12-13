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
        switch(color)
        {
            case OurColorsEnum.red:
            searchResults.ChangeColorFilter(OurColors.red);
                break;
            case OurColorsEnum.green:
                searchResults.ChangeColorFilter(OurColors.green);
                break;
            case OurColorsEnum.purple:
                searchResults.ChangeColorFilter(OurColors.purple);
                break;
            case OurColorsEnum.blue:
                searchResults.ChangeColorFilter(OurColors.blue);
                break;
            case OurColorsEnum.orange:
                searchResults.ChangeColorFilter(OurColors.orange);
                break;
            case OurColorsEnum.yellow:
                searchResults.ChangeColorFilter(OurColors.yellow);
                break;
            case OurColorsEnum.lightblue:
                searchResults.ChangeColorFilter(OurColors.lightblue);
                break;
            case OurColorsEnum.pink:
                searchResults.ChangeColorFilter(OurColors.pink);
                break;
            case OurColorsEnum.white:
                searchResults.ChangeColorFilter(OurColors.white);
                break;
            case OurColorsEnum.black:
                searchResults.ChangeColorFilter(OurColors.black);
                break;
        }
    }
}
