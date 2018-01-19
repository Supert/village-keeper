using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using VillageKeeper.Model;
using System.Linq;

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

        private Dictionary<string, List<AudioClip>> audioClips;

        private AudioSource backgroundAS;
        private AudioSource arrowAS;
        private AudioSource monsterAS;
        private AudioSource clickAS;
        private AudioSource buildingAS;

        public void PlaySoundEffect(AudioSource audioSource, string audioField)
        {
            if (!Data.Saved.IsSoundEffectsEnabled)
                return;
            Play(audioSource, audioField);
        }

        public void PlayMusic(string audioField)
        {
            if (!Data.Saved.IsMusicEnabled)
                return;
            if (backgroundAS.clip != null && audioField == audioClips.FirstOrDefault(kvp => kvp.Value.Contains(backgroundAS.clip)).Key)
                return;
            Play(backgroundAS, audioField);
        }

        public void Play(AudioSource audioSource, string audioField)
        {
            AudioClip newClip = audioClips[audioField][Random.Range(0, audioClips[audioField].Count)];
            audioSource.clip = newClip;
            audioSource.Play();
        }

        public void PlayArrowShot()
        {
            PlaySoundEffect(arrowAS, "ArrowShots");
        }

        public void PlayBuildingHit()
        {
            PlaySoundEffect(buildingAS, "BuildingHit");
        }

        public void PlayClick()
        {
            PlaySoundEffect(clickAS, "Click");
        }

        public void PlayMonsterHit()
        {
            PlaySoundEffect(monsterAS, "MonsterHit");
        }

        public IEnumerator MonsterRandomSoundsCoroutine(float delayInSeconds)
        {
            yield return new WaitForSeconds(delayInSeconds);
            if (Core.Instance.FSM.Current == FSM.States.Battle)
            {
                if (!monsterAS.isPlaying)
                {
                    PlaySoundEffect(monsterAS, "MonsterSounds");
                }
                StartCoroutine(MonsterRandomSoundsCoroutine(UnityEngine.Random.Range(1f, 5f)));
            }
        }

        private AudioSource GetNewAudioSource(string name)
        {
            var a = new GameObject(name, typeof(AudioSource)).GetComponent<AudioSource>() as AudioSource;
            a.transform.parent = transform;
            return a;
        }

        private void OnBattleEnter()
        {
            if (Data.Saved.IsMusicEnabled)
            {
                PlayMusic(AudioClipNames.BackgroundBattle.ToString());
                backgroundAS.volume = 0.3f;
            }
            if (Data.Saved.IsSoundEffectsEnabled)
                StartCoroutine(MonsterRandomSoundsCoroutine(1f));
        }

        private void OnLeaveBattle()
        {
            PlayMusic(AudioClipNames.BackgroundPeace.ToString());
            backgroundAS.volume = 1f;
        }

        public void Init()
        {
            backgroundAS = GetNewAudioSource("Background Music");
            arrowAS = GetNewAudioSource("Arrow Launch");
            monsterAS = GetNewAudioSource("Monster");
            clickAS = GetNewAudioSource("Click");
            buildingAS = GetNewAudioSource("Building");

            audioClips = new Dictionary<string, List<AudioClip>>();
            foreach (var field in Data.Audio.Values)
            {
                audioClips[field.Key] = new List<AudioClip>();
                foreach (var path in (field.Value.GetValue() as string[]))
                    audioClips[field.Key].Add(Resources.Load<AudioClip>(path));
            }

            PlayMusic("BackgroundPeace");
            backgroundAS.volume = 1f;
            backgroundAS.loop = true;

            Data.Saved.IsMusicEnabled.OnValueChanged += () =>
            {
                if (Data.Saved.IsMusicEnabled)
                {
                    backgroundAS.Play();
                }
                else
                {
                    backgroundAS.Stop();
                }
            };

            Core.Instance.FSM.SubscribeToEnter(FSM.States.BattleHelp, OnBattleEnter);
            Core.Instance.FSM.SubscribeToEnter(FSM.States.Battle, OnBattleEnter);

            Core.Instance.FSM.SubscribeToEnter(FSM.States.Build, OnLeaveBattle);
            Core.Instance.FSM.SubscribeToEnter(FSM.States.Menu, OnLeaveBattle);
        }
    }
}