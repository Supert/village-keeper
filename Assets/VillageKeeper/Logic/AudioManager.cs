using UnityEngine;
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
            if (CoreScript.Instance.Data.IsSoundEffectsEnabled)
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
            if (CoreScript.Instance.Data.IsSoundEffectsEnabled)
            {
                buildingAS.Play();
            }
        }

        public void PlayClick()
        {
            if (CoreScript.Instance.Data.IsSoundEffectsEnabled)
            {
                clickAS.Play();
            }
        }

        public void PlayMonsterHit()
        {
            if (CoreScript.Instance.Data.IsSoundEffectsEnabled && CoreScript.Instance.FSM.Current == FSM.States.Battle)
            {
                monsterAS.clip = audioClips[AudioClipNames.MonsterHit];
                monsterAS.Play();
            }
        }

        public IEnumerator MonsterRandomSoundsCoroutine(float delayInSeconds)
        {
            yield return new WaitForSeconds(delayInSeconds);
            if (CoreScript.Instance.FSM.Current == FSM.States.Battle)
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
                audioClips.Add(n, Resources.Load<AudioClip>("Audio/" + Enum.GetName(typeof(AudioClipNames), n)) as AudioClip);
            }

            backgroundAS = GetNewAudioSource("Background Music");
            arrowAS = GetNewAudioSource("Arrow Launch");
            monsterAS = GetNewAudioSource("Monster");

            clickAS = GetNewAudioSource("Click");
            clickAS.clip = audioClips[AudioClipNames.Click];
            buildingAS = GetNewAudioSource("building");
            buildingAS.clip = audioClips[AudioClipNames.BuildingHit];


            if (CoreScript.Instance.Data.IsMusicEnabled)
            {
                backgroundAS.clip = audioClips[AudioClipNames.BackgroundPeace];
                backgroundAS.volume = 1f;
                backgroundAS.Play();
            }
            backgroundAS.loop = true;

            CoreScript.Instance.Data.DataFieldChanged += (sender, e) =>
            {
                if (e.FieldChanged == DataScript.DataFields.IsMusicEnabled)
                {
                    if (CoreScript.Instance.Data.IsMusicEnabled)
                    {
                        backgroundAS.Play();
                    }
                    else
                    {
                        backgroundAS.Stop();
                    }
                }
            };

            //CoreScript.Instance.GameStateChanged += (sender, e) =>
            //{
            //    switch (e.NewState)
            //    {
            //        case CoreScript.GameStates.InBattle:
            //            if (CoreScript.Instance.Data.IsMusicEnabled)
            //            {
            //                if (BackgroundAS.clip != AudioClips[AudioClipNames.BackgroundBattle])
            //                {
            //                    BackgroundAS.clip = AudioClips[AudioClipNames.BackgroundBattle];
            //                    BackgroundAS.volume = 0.3f;
            //                    BackgroundAS.Play();
            //                }
            //            }
            //            if (CoreScript.Instance.Data.IsSoundEffectsEnabled)
            //                StartCoroutine(MonsterRandomSoundsCoroutine(1f));
            //            break;
            //        case CoreScript.GameStates.Paused:
            //        case CoreScript.GameStates.InHelp:
            //            break;
            //        default:
            //            if (CoreScript.Instance.Data.IsMusicEnabled)
            //            {
            //                if (BackgroundAS.clip != AudioClips[AudioClipNames.BackgroundPeace])
            //                {
            //                    BackgroundAS.clip = AudioClips[AudioClipNames.BackgroundPeace];
            //                    BackgroundAS.volume = 1f;
            //                    BackgroundAS.Play();
            //                }
            //            }
            //            break;
            //    }
            //};

            if (CoreScript.Instance.Data.IsMusicEnabled)
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