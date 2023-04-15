using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{ 
    public enum Scene
    {
        Title,
        Game
    }

    public enum UIEvent
    {
        Click
    }

    public enum State
    {
        Idle,
        Move,
        Attack,
        Hit,
        Die,
        Stun
    }

    public enum AudioSources
    {
        Unknown,
        BGM,
        SFX
    }

    public enum Creature
    {
        Player,
        Mob
    }

    public enum MobType
    {
        Wolf,
        InfernoDragon,
        Crab
    }

    public enum BombType
    {
        Damage,
        Slow,
        Stun
    }

    public enum Ailment
    {
        Stun,
        Slow
    }

    public enum BGM
    {
        Title,
        Game,
        Boss,
        Clear,
        Over, 
        Heal,
    }

    public enum SoundEffect
    {
        Click,
        Fire,
        Stun,
        Slow,
        Damage,
        ExplosionTower,
        Fireball,
        Buy,
        Upgrade,
        MaxCount
    }

    public enum EndGame
    {
        Unknow,
        Clear,
        Over, 
        MaxCount
    }

    public enum Hit
    {
        Bullet,
        Bomb
    }
}
