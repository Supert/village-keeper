using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

namespace VillageKeeper.Audio
{
    public class AudioScript : MonoBehaviour
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

        private Dictionary<AudioClipNames, AudioClip> _audioClips;
        private Dictionary<AudioClipNames, AudioClip> AudioClips
        {
            get
            {
                if (_audioClips == null)
                {
                    AudioClipNames[] ns = (AudioClipNames[])Enum.GetValues(typeof(AudioClipNames));
                    _audioClips = new Dictionary<AudioClipNames, AudioClip>();
                    foreach (AudioClipNames n in ns)
                    {
                        _audioClips.Add(n, Resources.Load<AudioClip>("Audio/" + Enum.GetName(typeof(AudioClipNames), n)) as AudioClip);
                    }
                }
                return _audioClips;
            }
        }

        AudioSource backgroundAS;
        private AudioSource BackgroundAS
        {
            get
            {
                if (backgroundAS == null)
                    backgroundAS = GetNewAudioSource("Background Music");
                return backgroundAS;
            }
        }

        AudioSource arrowAS;
        private AudioSource ArrowAS
        {
            get
            {
                if (arrowAS == null)
                    arrowAS = GetNewAudioSource("Arrow Launch");
                return arrowAS;
            }
        }

        AudioSource monsterAS;
        private AudioSource MonsterAS
        {
            get
            {
                if (monsterAS == null)
                    monsterAS = GetNewAudioSource("Monster");
                return monsterAS;
            }
        }

        AudioSource clickAS;
        private AudioSource ClickAS
        {
            get
            {
                if (clickAS == null)
                {
                    clickAS = GetNewAudioSource("Click");
                    clickAS.clip = AudioClips[AudioClipNames.Click];
                }
                return clickAS;
            }
        }

        AudioSource buildingAS;
        private AudioSource BuildingAS
        {
            get
            {
                if (buildingAS == null)
                {
                    buildingAS = GetNewAudioSource("building");
                    buildingAS.clip = AudioClips[AudioClipNames.BuildingHit];
                }
                return buildingAS;
            }
        }

        public void PlayArrowShot()
        {
            if (CoreScript.Instance.Data.IsSoundEffectsEnabled)
            {

                var n = UnityEngine.Random.Range(0, 3);
                switch (n)
                {
                    case 0:
                        ArrowAS.clip = AudioClips[AudioClipNames.ArrowShot0];
                        break;
                    case 1:
                        ArrowAS.clip = AudioClips[AudioClipNames.ArrowShot1];
                        break;
                    case 2:
                        ArrowAS.clip = AudioClips[AudioClipNames.ArrowShot2];
                        break;
                }
                ArrowAS.Play();
            }
        }

        public void PlayBuildingHit()
        {
            if (CoreScript.Instance.Data.IsSoundEffectsEnabled)
            {
                BuildingAS.Play();
            }
        }

        public void PlayClick()
        {
            if (CoreScript.Instance.Data.IsSoundEffectsEnabled)
            {
                ClickAS.Play();
            }
        }

        public void PlayMonsterHit()
        {
            if (CoreScript.Instance.Data.IsSoundEffectsEnabled && CoreScript.Instance.FSM.Current == typeof(FSM.BattleState))
            {
                MonsterAS.clip = AudioClips[AudioClipNames.MonsterHit];
                MonsterAS.Play();
            }
        }

        public IEnumerator MonsterRandomSoundsCoroutine(float delayInSeconds)
        {
            yield return new WaitForSeconds(delayInSeconds);
            if (CoreScript.Instance.FSM.Current == typeof(FSM.BattleState))
            {
                if (!MonsterAS.isPlaying)
                {
                    var n = UnityEngine.Random.Range(0, 2);
                    if (n == 0)
                        MonsterAS.clip = AudioClips[AudioClipNames.Monster0];
                    else
                        MonsterAS.clip = AudioClips[AudioClipNames.Monster1];
                    MonsterAS.Play();
                }
                StartCoroutine(MonsterRandomSoundsCoroutine(UnityEngine.Random.Range(1f, 5f)));
            }
        }

        void Start()
        {
            if (CoreScript.Instance.Data.IsMusicEnabled)
            {
                BackgroundAS.clip = AudioClips[AudioClipNames.BackgroundPeace];
                BackgroundAS.volume = 1f;
                BackgroundAS.Play();
            }
            BackgroundAS.loop = true;

            CoreScript.Instance.Data.DataFieldChanged += (sender, e) =>
            {
                if (e.FieldChanged == DataScript.DataFields.IsMusicEnabled)
                {
                    if (CoreScript.Instance.Data.IsMusicEnabled)
                    {
                        BackgroundAS.Play();
                    }
                    else
                    {
                        BackgroundAS.Stop();
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
                if (BackgroundAS.clip != AudioClips[AudioClipNames.BackgroundPeace])
                {
                    BackgroundAS.clip = AudioClips[AudioClipNames.BackgroundPeace];
                    BackgroundAS.volume = 1f;
                    BackgroundAS.Play();
                }
            }
        }

        private AudioSource GetNewAudioSource(string name)
        {
            var a = new GameObject("name", typeof(AudioSource)).GetComponent<AudioSource>() as AudioSource;
            a.transform.parent = transform.parent;
            return a;
        }
    }
}