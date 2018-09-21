using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class G_Variable {


    private static string lvl_selected, deaths, assists, points;

    
    public static string Lvl_selected
    {
        get
        {
            return lvl_selected;
        }
        set
        {
            lvl_selected = value;
        }
    }

    public static string Deaths
    {
        get
        {
            return deaths;
        }
        set
        {
            deaths = value;
        }
    }

    public static string Assists
    {
        get
        {
            return assists;
        }
        set
        {
            assists = value;
        }
    }

    public static string Points
    {
        get
        {
            return points;
        }
        set
        {
            points = value;
        }
    }
}
