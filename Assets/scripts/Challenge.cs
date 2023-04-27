using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Challenge", menuName = "Challenge")]
public class Challenge : ScriptableObject
{
    // Wolf Values
    [Header("Wolf Values")]
    public int wolfHungerStartingLevel;
    public int wolfMaxAge;
    public int wolfBreedingStartingLevel;
    public int wolfBreedingHungerRequirement;
    public int wolfHungerValue;
    public int wolfStartingCount;

    // Rabbit Values
    [Header("Rabbit Values")]
    public int rabbitHungerStartingLevel;
    public int rabbitMaxAge;
    public int rabbitBreedingStartingLevel;
    public int rabbitBreedingHungerRequirement;
    public int rabbitHungerValue;
    public int rabbitStartingCount;

    // Carrot Values
    [Header("Carrot Values")]
    public int carrotHungerValue;
    public int carrotMaxAge;
    public float carrotSpawnInterval;

    // Timing Values
    [Header("Misc. Values")]
    public float timeLimit;
    
}
