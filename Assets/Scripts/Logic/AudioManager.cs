﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

namespace VillageKeeper.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public enum AudioClipNames
        {
            ArrowShot0,
            ArrowShot1,
            ArrowShot2,
            BackgroundBattle,
            BackgroundPeace,
            BuildingHit,
            Click,
            Monster0,
            Monster1,
            MonsterHit,
        }

        private Dictionary<AudioClipNames, AudioClip> audioClips;

        private AudioSource backgroundAS;
        private AudioSource arrowAS;
        private AudioSource monsterAS;
        private AudioSource clickAS;
        private AudioSource buildingAS;

        public void PlayArrowShot()
        {
            if (Core.Instance.Data.Saved.IsSoundEffectsEnabled.Get())
            {

                var n = UnityEngine.Random.Range(0, 3);
                switch (n)
                {
                    case 0:
                        arrowAS.clip = audioClips[AudioClipNames.ArrowShot0];
                        break;
                    case 1:
                        arrowAS.clip = audioClips[AudioClipNames.ArrowShot1];
                        break;
                    case 2:
                        arrowAS.clip = audioClips[AudioClipNames.ArrowShot2];
                        break;
                }
                arrowAS.Play();
            }
        }

        public void PlayBuildingHit()
        {
            if (Core.Instance.Data.Saved.IsSoundEffectsEnabled.Get())
            {
                buildingAS.Play();
            }
        }

        public void PlayClick()
        {
            if (Core.Instance.Data.Saved.IsSoundEffectsEnabled.Get())
            {
                clickAS.Play();
            }
        }

        public void PlayMonsterHit()
        {
            if (Core.Instance.Data.Saved.IsSoundEffectsEnabled.Get() && Core.Instance.FSM.Current == FSM.States.Battle)
            {
                monsterAS.clip = audioClips[AudioClipNames.MonsterHit];
                monsterAS.Play();
            }
        }

        public IEnumerator MonsterRandomSoundsCoroutine(float delayInSeconds)
        {
            yield return new WaitForSeconds(delayInSeconds);
            if (Core.Instance.FSM.Current == FSM.States.Battle)
            {
                if (!monsterAS.isPlaying)
                {
                    var n = UnityEngine.Random.Range(0, 2);
                    if (n == 0)
                        monsterAS.clip = audioClips[AudioClipNames.Monster0];
                    else
                        monsterAS.clip = audioClips[AudioClipNames.Monster1];
                    monsterAS.Play();
                }
                StartCoroutine(MonsterRandomSoundsCoroutine(UnityEngine.Random.Range(1f, 5f)));
            }
        }

        private AudioSource GetNewAudioSource(string name)
        {
            var a = new GameObject("name", typeof(AudioSource)).GetComponent<AudioSource>() as AudioSource;
            a.transform.parent = transform.parent;
            return a;
        }

        public void Init()
        {
            AudioClipNames[] ns = (AudioClipNames[])Enum.GetValues(typeof(AudioClipNames));
            audioClips = new Dictionary<AudioClipNames, AudioClip>();
            foreach (AudioClipNames n in ns)
            {
                audioClips.Add(n, Resources.Load<AudioClip>(Core.Instance.Data.Audio.);
            }

            backgroundAS = GetNewAudioSource("Background Music");
            arrowAS = GetNewAudioSource("Arrow Launch");
            monsterAS = GetNewAudioSource("Monster");

            clickAS = GetNewAudioSource("Click");
            clickAS.clip = audioClips[AudioClipNames.Click];
            buildingAS = GetNewAudioSource("building");
            buildingAS.clip = audioClips[AudioClipNames.BuildingHit];


            if (Core.Instance.Data.Saved.IsMusicEnabled.Get())
            {
                backgroundAS.clip = audioClips[AudioClipNames.BackgroundPeace];
                backgroundAS.volume = 1f;
                backgroundAS.Play();
            }
            backgroundAS.loop = true;

            Core.Instance.Data.Saved.IsMusicEnabled.OnValueChanged += () =>
            {
                if (Core.Instance.Data.Saved.IsMusicEnabled.Get())
                {
                    backgroundAS.Play();
                }
                else
                {
                    backgroundAS.Stop();
                }
            };

            Core.Instance.FSM.SubscribeToEnter(FSM.States.Battle, () =>
            {
                if (Core.Instance.Data.Saved.IsMusicEnabled.Get())
                {
                    if (backgroundAS.clip != audioClips[AudioClipNames.BackgroundBattle])
                    {
                        backgroundAS.clip = audioClips[AudioClipNames.BackgroundBattle];
                        backgroundAS.volume = 0.3f;
                        backgroundAS.Play();
                    }
                }
                if (Core.Instance.Data.Saved.IsSoundEffectsEnabled.Get())
                    StartCoroutine(MonsterRandomSoundsCoroutine(1f));
            });

            //    case CoreScript.GameStates.Paused:
            //    case CoreScript.GameStates.InHelp:
            //        break;
            //    default:
            //        if (CoreScript.Instance.Data.IsMusicEnabled)
            //        {
            //            if (BackgroundAS.clip != AudioClips[AudioClipNames.BackgroundPeace])
            //            {
            //                BackgroundAS.clip = AudioClips[AudioClipNames.BackgroundPeace];
            //                BackgroundAS.volume = 1f;
            //                BackgroundAS.Play();
            //            }
            //        }
            //        break;
            //}

            if (Core.Instance.Data.Saved.IsMusicEnabled.Get())
            {
                if (backgroundAS.clip != audioClips[AudioClipNames.BackgroundPeace])
                {
                    backgroundAS.clip = audioClips[AudioClipNames.BackgroundPeace];
                    backgroundAS.volume = 1f;
                    backgroundAS.Play();
                }
            }
        }
    }
}