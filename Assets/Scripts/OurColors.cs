using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct OurColors
{
    public float r;
    public float g;
    public float b;
    public float a;
    public static OurColors blue = new OurColors(0, 0, 1, 1);
    public static OurColors green = new OurColors(0, 1, 0, 1);
    public static OurColors red = new OurColors(1, 0, 0, 1);
    public static OurColors lightblue = new OurColors(0, 1, 1, 1);
    public static OurColors orange = new OurColors(1, 0.5f, 0, 1);
    public static OurColors yellow = new OurColors(1, 1, 0, 1);
    public static OurColors purple = new OurColors(1, 0, 1, 1);
    public static OurColors pink = new OurColors(1, 0.5f, 0.68f, 1);
    public static OurColors white = new OurColors(1, 1, 1, 1);
    public static OurColors black = new OurColors(0, 0, 0, 1);

    public OurColors(float r, float g, float b, float a)
    {
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = a;
    }
    public static Color GetColor(OurColors color)
    {
        Color newColor = new Color(color.r, color.g, color.b, color.a);
        return newColor;
    }
}
