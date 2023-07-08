using Game.Operator;
using UnityEngine;
using System;

namespace Game.Settings 
{
public class AudioManager : MonoBehaviour
{
	public AudioSource soundSource, musicSource;
	public float masterVolume, soundVolume, musicVolume;
	public Action RefreshControl;

	void Awake()
	{
		//Updated the master, sound and music volume value upon scene open
		SetMaster(masterVolume); SetSound(soundVolume); SetMusic(musicVolume);
	}

	public void SetMaster(float value)
	{
		//Set master volume to given value 
		masterVolume = value; 
		//Apply master volume to audio listener
		AudioListener.volume = masterVolume/100;
	}

	public void SetSound(float value)
	{
		//Set sound volume to given value 
		soundVolume = value; 
		//Apply sound volume to audio source
		soundSource.volume = soundVolume/100;
	}

	public void SetMusic(float value)
	{
		//Set music volume to given value 
		musicVolume = value; 
		//Apply music volume to audio source
		musicSource.volume = musicVolume/100;
	}
}
}