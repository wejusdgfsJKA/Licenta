using UnityEngine;

public struct GlobalConfig
{
    public static readonly float VerticalBoundary = 5, HorizontalBoundary = 10;
    public static readonly float TorqueCoefficient = 1, ImpulseCoefficient = 1;
    public static readonly float BulletSpeed = 5;
    public static readonly float StarGravity = .3f;
    public static bool Player = true;
    public static Transform Winner;
}
