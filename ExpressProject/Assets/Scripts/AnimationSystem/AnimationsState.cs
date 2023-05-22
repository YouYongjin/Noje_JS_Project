using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationsState
{
    Idle,
    Fishing
}

public enum AnimatorTriggers
{
    Noactions = 0,
    FishingCast = 20,
    FishingReal = 28,
    FishingFinish = 29,
}
