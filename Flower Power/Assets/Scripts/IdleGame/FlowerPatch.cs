using System;
using System.Diagnostics;
using Flowers;
using Managers;
using UnityEngine;

namespace IdleGame
{
    public class FlowerPatch : MonoBehaviour
    {
        public enum State {Idle, Purchasing};
        
        private State currentState;
        public State CurrentState
        {
            get => currentState;
            set => currentState = value;
            
        }
        
        private void Awake()
        {
            currentState = State.Idle;
        }
    }
}
