using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEPlayer
{
    static SE se;
    public static void Play(SE.Name name, float volume)
    {
        if (se == null) se = GameObject.Find("SE").GetComponent<SE>();
        se.Play(name, volume);
    }
}

public class SE : MonoBehaviour {

    public enum Name
    {
        START,
        BOOK
    }

    public AudioClip Start;
    public AudioClip Book;

    public void Play(Name name, float volume)
    {
        var source = GetComponent<AudioSource>();
        switch (name)
        {
            case Name.START:
                source.PlayOneShot(Start, volume);
                break;
            case Name.BOOK:
                source.PlayOneShot(Book, volume);
                break;
        }
    }
	
}
