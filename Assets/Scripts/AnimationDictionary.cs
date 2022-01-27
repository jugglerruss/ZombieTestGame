using System;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationMain
{
    idle, walk, attack, dead
}
public enum AnimationDirection
{
    N, E, W, S
}
public static class AnimationDicrionary
{
    public static readonly Dictionary<AnimationMain, string>  MainDict = new Dictionary<AnimationMain, string>()
    {
        {AnimationMain.idle, "Idle"},
        {AnimationMain.walk, "walk"},
        {AnimationMain.attack, "attack"},
        {AnimationMain.dead, "dead"}
    };
    public static readonly Dictionary<AnimationDirection, string>  DirectionDict = new Dictionary<AnimationDirection, string>()
    {
        {AnimationDirection.N, "N"},
        {AnimationDirection.E, "E"},
        {AnimationDirection.W, "W"},
        {AnimationDirection.S, "S"}
    };

    public static AnimationDirection GetDirection(Vector3 position)
    {
        AnimationDirection direction;
        if (Math.Abs(position.y) > Math.Abs(position.x))
        {
            if (position.y > 0)
                direction = AnimationDirection.N;
            else
                direction = AnimationDirection.S;
        }
        else
        {
            if (position.x > 0)
                direction = AnimationDirection.E;
            else
                direction = AnimationDirection.W;
        }
        return direction;
    }
}
