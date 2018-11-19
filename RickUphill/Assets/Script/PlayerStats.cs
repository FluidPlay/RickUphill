using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : ScriptableObject {
    public float JumpForce = 2f;
    public float JumpAddTime = 0.5f; //Max time you can hold the button and add jump force
    public float RockCheckLength = 100f;
    public float RespawnTime = 2f;
    public SoundFXs SoundFX;

    [System.Serializable]
    public struct SoundFXs
    {
        public AudioClip Jump, Death;
    }
}
