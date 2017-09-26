using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEPlayer
{
    static SE se;
    public static void Play(SE.Name name, float volume, float pitch = 1f)
    {
        if (se == null) se = GameObject.Find("SE").GetComponent<SE>();
        se.Play(name, volume, pitch);
    }
}

public class SE : MonoBehaviour {

    public enum Name
    {
        START,
        BOOK,
        DROP_BREAK1,
        DROP_BREAK2,
        DROP_BREAK3,
        BUTTON,
        DAMAGED,
        ATTACK,
        ENEMY_ATTACK
    }

    public AudioClip TitleStart;
    public AudioClip Book;
    public AudioClip DropBreak1;
    public AudioClip DropBreak2;
    public AudioClip DropBreak3;
    public AudioClip ButtonPush;
    public AudioClip Damaged;
    public AudioClip Attack;
    public AudioClip EnemyAttack;

    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Play(Name name, float volume, float pitch)
    {
        // source.pitch = pitch;
        switch (name)
        {
            case Name.START:
                source.PlayOneShot(TitleStart, volume);
                break;
            case Name.BOOK:
                source.PlayOneShot(Book, volume);
                break;
            case Name.DROP_BREAK1:
                source.PlayOneShot(DropBreak1, volume);
                break;
            case Name.DROP_BREAK2:
                source.PlayOneShot(DropBreak2, volume);
                break;
            case Name.DROP_BREAK3:
                source.PlayOneShot(DropBreak3, volume);
                break;
            case Name.BUTTON:
                source.PlayOneShot(ButtonPush, volume);
                break;
            case Name.DAMAGED:
                source.PlayOneShot(Damaged, volume);
                break;
            case Name.ATTACK:
                source.PlayOneShot(Attack, volume);
                break;
            case Name.ENEMY_ATTACK:
                source.PlayOneShot(EnemyAttack, volume);
                break;
        }
    }
	
}
