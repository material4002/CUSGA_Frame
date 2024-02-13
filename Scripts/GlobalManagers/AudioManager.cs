using Mat.Kits;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

namespace Mat
{
	public class AudioManager : SingleTon<AudioManager>
	{
		public AudioManager():base() 
		{
			var entry = Entry.Instance;
			listener = entry.AddComponent<AudioListener>();
			Music_Player = entry.AddComponent<AudioSource>();
			Sound_Player= entry.AddComponent<AudioSource>();
			BGM_Player= entry.AddComponent<AudioSource>();

			Music_Player.loop = true;
			Sound_Player.loop = false;
			BGM_Player.loop = false;

			Music_Player.volume = 0f;
			Sound_Player.volume = 0f;
			BGM_Player.volume = 0f;

			list = AudioList.Instance;

			MusicDic = InitDic(list.Music);
			SoundDic = InitDic(list.Sound);
			BGMDic = InitDic(list.BGM);

			mixer = Resources.Load<AudioMixer>("AudioMixer");
			Music_Player.outputAudioMixerGroup = mixer.FindMatchingGroups("Master/Music")[0];
			BGM_Player.outputAudioMixerGroup = mixer.FindMatchingGroups("Master/BGM")[0];
			Sound_Player.outputAudioMixerGroup = mixer.FindMatchingGroups("Master/Sound")[0];
		}

		AudioSource Music_Player;
		AudioSource Sound_Player;
		AudioSource BGM_Player;
		AudioListener listener;
		AudioList list;
		Dictionary<string, AudioNode> MusicDic,SoundDic,BGMDic;
		AudioMixer mixer;

		#region 混音器名称常量
		public const string Music_V = "Music_V";
		public const string Music_P = "Music_P";
		public const string Sound_V = "Sound_V";
		public const string Sound_P = "Soumd_P";
		public const string BGM_V = "BGM_V";
		public const string BGM_P = "BGM_P";
		public const string Master_V = "Master_V";
		public const string Master_P = "Master_P";
		#endregion

		private Dictionary<string,AudioNode> InitDic(IEnumerable list )
		{
			Dictionary<string,AudioNode> dic = new Dictionary<string,AudioNode>();
			foreach(AudioNode i in list)
			{
				dic.Add(i.name, i);
			}
			return dic;
		}

		public void PlaySound(string name)
		{
			if (!SoundDic.ContainsKey(name)) return;
			AudioNode node = SoundDic[name];
			Sound_Player.volume = node.volume;
			Sound_Player.PlayOneShot(node.clip);
		}

		public void ChangeMusic(string name)
		{
			if(!MusicDic.ContainsKey(name)) return;
			AudioNode node = MusicDic[name];
			Music_Player.volume = node.volume;
			Music_Player.clip= node.clip;
		}

		public void PauseMusic()
		{
			Music_Player.Pause();
		}
		public void StopMusic()
		{
			Music_Player.Stop();
		}
		public void PlayMusic()
		{
			Music_Player.Play();
		}
		public void ResetMusic()
		{
			Music_Player.Stop();
			Music_Player.Play();
		}

		public void ChangeBGM(string name)
		{
			if (!BGMDic.ContainsKey(name)) return;
			AudioNode node = BGMDic[name];
			BGM_Player.volume = node.volume;
			BGM_Player.clip = node.clip;
		}
		public void PauseBGM()
		{
			BGM_Player.Pause();
		}
		public void StopBGM()
		{
			BGM_Player.Stop();
		}
		public void PlayBGM()
		{
			BGM_Player.Play();
		}
		public void ResetBGM()
		{
			BGM_Player.Stop();
			BGM_Player.Play();
		}

		/// <summary>
		/// 设置混音器的音量
		/// </summary>
		/// <param name="mixerName">混音器名称，以_V结尾</param>
		/// <param name="volume">音量-80 到 20</param>
		/// <returns></returns>
		public bool SetVolume(string mixerName,float volume)
		{
			if (mixerName[mixerName.Length-1] != 'V') return false;
			volume = Mathf.Clamp(volume, -80f, 20f);
			mixer.SetFloat(mixerName, volume);
			return true;
		}

		/// <summary>
		/// 设置混音器音调
		/// </summary>
		/// <param name="mixerName">混音器名称，以_P结尾</param>
		/// <param name="pitch">音调 1 —— 1000</param>
		/// <returns></returns>
		public bool SetPitch(string mixerName,float pitch)
		{
			if (mixerName[mixerName.Length - 1] != 'V') return false;
			pitch = Mathf.Clamp(pitch, 1f, 1000f);
			mixer.SetFloat(mixerName, pitch);
			return true;
		}
	}
}

