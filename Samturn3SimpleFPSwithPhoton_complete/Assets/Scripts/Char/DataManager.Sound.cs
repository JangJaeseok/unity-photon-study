using UnityEngine;
using System.Collections;

public partial class DataManager : MonoBehaviour{

	public AudioClip sound_eff01;
	public AudioClip sound_cut01;
	public AudioClip sound_swordswing1;
	public AudioClip sound_gun01;
	public AudioClip sound_hit01;
	public AudioClip sound_soldierhit01;
	public AudioClip sound_footstep;
	public AudioClip sound_click;
	public AudioClip sound_pop;

	public AudioClip bgm1;
	public AudioClip bgm2;
	public AudioSource playerAudio;

	public void SoundPlay(AudioClip sound, float volume){
		playerAudio.PlayOneShot (sound, volume);	
	}
	public void SoundPlayBGM(AudioClip sound){
		playerAudio.loop = true;
		playerAudio.clip = sound;
		playerAudio.Play();
	}
	public void SoundPlayBGMOff(){
		playerAudio.Stop();
	}
}
