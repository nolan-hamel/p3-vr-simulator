using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Challenge", menuName = "Challenge")]
public class Challenge : ScriptableObject
{
    // Wolf Values
    #region wolf values
    public int wolfHungerStartingLevel;
    public int wolfMaxAge;
    public int wolfBreedingStartingLevel;
    public int wolfBreedingHungerRequirement;
    public int wolfHungerValue;
    #endregion

    // Rabbit Values
    public int rabbitHungerStartingLevel;
    public int rabbitMaxAge;
    public int rabbitBreedingStartingLevel;
    public int rabbitBreedingHungerRequirement;
    public int rabbitHungerValue;

    // Carrot Values
    public int carrotHungerValue;
    public int carrotMaxAge;
    public float carrotSpawnInterval;
}
