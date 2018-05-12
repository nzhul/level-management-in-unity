﻿using System.Collections;
using LevelManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.ThirdPerson;

namespace SampleGame
{
    public class GameManager : MonoBehaviour
    {
        // reference to player
        private ThirdPersonCharacter _player;

        // reference to goal effect
        private GoalEffect _goalEffect;

        // reference to player
        private Objective _objective;

        private bool _isGameOver;
        public bool IsGameOver { get { return _isGameOver; } }

        private static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                return _instance;
            }
        }

        [SerializeField]
        private TransitionFader _endTransitionPrefab;

        // initialize references
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }

            _player = Object.FindObjectOfType<ThirdPersonCharacter>();
            _objective = Object.FindObjectOfType<Objective>();
            _goalEffect = Object.FindObjectOfType<GoalEffect>();
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        // end the level
        public void EndLevel()
        {
            if (_player != null)
            {
                // disable the player controls
                ThirdPersonUserControl thirdPersonControl =
                    _player.GetComponent<ThirdPersonUserControl>();

                if (thirdPersonControl != null)
                {
                    thirdPersonControl.enabled = false;
                }

                // remove any existing motion on the player
                Rigidbody rbody = _player.GetComponent<Rigidbody>();
                if (rbody != null)
                {
                    rbody.velocity = Vector3.zero;
                }

                // force the player to a stand still
                _player.Move(Vector3.zero, false, false);
            }

            // check if we have set IsGameOver to true, only run this logic once
            if (_goalEffect != null && !_isGameOver)
            {
                _isGameOver = true;
                _goalEffect.PlayEffect();
                StartCoroutine(WinRoutine());
            }
        }

        private IEnumerator WinRoutine()
        {
            TransitionFader.PlayTransition(_endTransitionPrefab);

            float fadeDelay = (_endTransitionPrefab != null) ? _endTransitionPrefab.Delay + _endTransitionPrefab.FadeOnDuration : 0;

            yield return new WaitForSeconds(fadeDelay);
            WinScreen.Open();
        }

        // check for the end game condition on each frame
        private void Update()
        {
            if (_objective != null && _objective.IsComplete)
            {
                EndLevel();
            }
        }

    }
}