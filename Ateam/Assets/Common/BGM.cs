using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer
{

    static BGM bgm = null;

    public static void Play(BGM.Name name)
    {
        if (bgm == null) bgm = GameObject.Find("BGM").GetComponent<BGM>();
        if (bgm == null) return;

        bgm.Play(name);
    }
}

public class BGM : MonoBehaviour {

    AudioSource source;

    public AudioClip Title;
    public AudioClip Menu;
    public AudioClip Battle;
    public AudioClip Boss;
    public AudioClip GameOver;
    public AudioClip Clear;

    public enum Name
    {
        TITLE,
        MENU,
        BATTLE,
        BOSS,
        GAME_OVER,
        CLEAR,
        NONE
    }

	// Use this for initialization
	void Start ()
    {
        source = GetComponent<AudioSource>();
        Play(Name.TITLE);
	}

    Name nowPlaying = Name.NONE;
    Name nextBGM = Name.NONE;
    public void Play(Name name)
    {
        nextBGM = name;
    }

    float volume=1f;
    void PlayStart()
    {
        switch (nextBGM)
        {
            case Name.TITLE:
                volume = 1f;
                source.clip = Title;
                break;
            case Name.MENU:
                volume = 0.15f;
                source.clip = Menu;
                break;
            case Name.BATTLE:
                volume = 0.3f;
                source.clip = Battle;
                break;
            case Name.BOSS:
                volume = 1f;
                source.clip = Boss;
                break;
            case Name.GAME_OVER:
                volume = 1f;
                source.clip = GameOver;
                break;
            case Name.CLEAR:
                volume = 1f;
                source.clip = Clear;
                break;
            case Name.NONE:
                source.clip = null;
                break;
        }
        source.volume = volume;
        source.Play();
        nowPlaying = nextBGM;
    }

    void Update()
    {
        if (nextBGM != nowPlaying)
        {
            source.volume -= Time.deltaTime/2f*volume;
            if(source.volume <= 0)
            {
                PlayStart();
            }
        }
    }
}
