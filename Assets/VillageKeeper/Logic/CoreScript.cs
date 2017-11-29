using UnityEngine;
using System;
using VillageKeeper.FSM;
using VillageKeeper.Game;
using VillageKeeper.UI;
using VillageKeeper.Audio;

namespace VillageKeeper
{
    public class CoreScript : MonoBehaviour
    {
        public enum Specials
        {
            None,
            Winter,
        }

        public static CoreScript Instance { get; private set; }

        public StateMachine FSM { get; private set; }

        public UiManager UiManager { get; private set; }

        public GameController GameManager { get; private set; }
        
        public MonsterScript Monster { get; private set; }

        public ArcherScript Archer { get; private set; }
        
        public ControlsScript Controls { get; private set; }

        public MainMenuScript MainMenu { get; private set; }

        public DataScript Data { get; private set; }

        public BuildingsAreaScript BuildingsArea { get; private set; }

        public AudioManager AudioManager { get; private set; }
        
        public Specials TodaySpecial { get; private set; }

        //Day and Month matter only
        private bool CheckIfDateIsInPeriodOfYear(DateTime date, DateTime beginning, DateTime end)
        {
            var testDate = new DateTime(2012, date.Month, date.Day);
            var testBeginning = new DateTime(2012, beginning.Month, beginning.Day);
            var testEnd = new DateTime(2012, end.Month, end.Day);
            if (testBeginning > testEnd)
                testEnd = testEnd.AddYears(1);
            if (testDate >= testBeginning && testDate <= testEnd)
                return true;
            testDate = testDate.AddYears(1);
            if (testDate >= testBeginning && testDate <= testEnd)
                return true;
            return false;
        }

        private Specials GetTodaySpecial()
        {
            var winterSpecialBeginning = new DateTime(2012, 12, 24); //December 24th
            var winterSpecialEnd = new DateTime(2012, 1, 14); // January 14th
            if (CheckIfDateIsInPeriodOfYear(DateTime.Today, winterSpecialBeginning, winterSpecialEnd))
                return Specials.Winter;
            else
                return Specials.None;
        }

        void Awake()
        {
            Instance = this;
            FSM = new StateMachine();
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            Monster = FindObjectOfType(typeof(MonsterScript)) as MonsterScript;
            Archer = FindObjectOfType(typeof(ArcherScript)) as ArcherScript;
            Controls = FindObjectOfType(typeof(ControlsScript)) as ControlsScript;
            MainMenu = FindObjectOfType(typeof(MainMenuScript)) as MainMenuScript;
            Data = GetComponent<DataScript>() as DataScript;
            BuildingsArea = FindObjectOfType<BuildingsAreaScript>();
            TodaySpecial = GetTodaySpecial();

            GameManager = transform.Find("Game").GetComponent<global::GameController>();
            UiManager = transform.Find("Ui").GetComponent<UiManager>();
            AudioManager = transform.Find("Audio").GetComponent<AudioManager>();

            GameManager.Init();
            UiManager.Init();
            AudioManager.Init();

            FSM.Event(StateMachineEvents.GameInitialized);
        }
    }
}