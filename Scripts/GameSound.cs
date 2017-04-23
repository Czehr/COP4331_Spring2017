using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSound : MonoBehaviour {

	public AudioClip click;
	public AudioClip swoop;
	public AudioClip fweewp;
	public AudioClip clappy;
	public AudioClip bloop;
	public AudioClip ddpddlldlp;
	public AudioSource channel1;
	public AudioSource channel2;

	private int chan;

	void Start ()
	{
		chan = 0;
	}

	void Update ()
	{
		
	}

	public void playSound(string clip)
	{
		AudioSource currentChannel = null;

		if((chan % 2) == 0)
			currentChannel = channel1;
		else if((chan % 2) == 1)
			currentChannel = channel2;

		if(clip.Equals("click"))
			currentChannel.clip = click;
		if(clip.Equals("swoop"))
			currentChannel.clip = swoop;
		if(clip.Equals("fweewp"))
			currentChannel.clip = fweewp;
		if(clip.Equals("clappy"))
			currentChannel.clip = clappy;
		if(clip.Equals("bloop"))
			currentChannel.clip = bloop;
		if(clip.Equals("ddpddlldlp"))
			currentChannel.clip = ddpddlldlp;

		currentChannel.Play();
		chan++;
	}
}
