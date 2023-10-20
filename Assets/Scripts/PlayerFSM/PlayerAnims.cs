using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnims
{
    public enum AnimationNames
    {
        Idle,
        Run,
        Jump,
        Dive,
        Rollout,
        Wallslide
    }

    public static readonly Dictionary<AnimationNames, string> AnimationTriggers = new Dictionary<AnimationNames, string>
    {
    { AnimationNames.Idle, "Idle" },
    { AnimationNames.Run, "Run" },
    { AnimationNames.Jump, "Jump"},
    { AnimationNames.Dive, "Dived"},
    { AnimationNames.Rollout, "Rollout"},
    { AnimationNames.Wallslide, "Wallslide"},
    };

}
