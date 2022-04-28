using System;
using System.Collections.Generic;
using System.Linq;
using Case2.Runtime.States;
using Helper;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Case2.Managers
{
    public class InputManager : MonoSingleton<InputManager>, IDragHandler, IPointerDownHandler, IPointerUpHandler, IEndDragHandler
    {
        public State CurrentState;
        public string CurrentStateName => StateToString(CurrentState);

        private Dictionary<string, State> allStates;
        public Dictionary<string, State> AllStates
        {
            get => allStates;
            set => allStates = value;
        }

        [SerializeField]
        private StateChanger stateChanger;
        public void SetState(string stateName)
        {
            var state = StringToState(stateName);
            if (state == null)
            {
                Debug.LogError($"{stateName} - State Not Exist!");
                return;
            }
            CurrentState?.OnExit();
            if (CurrentState == state) return;
            CurrentState = state;
            CurrentState.OnStart();
        }
        public void SetState(string stateName, bool forceStart)
        {
            var state = StringToState(stateName);
            if (state == null)
            {
                Debug.LogError($"{stateName} - State Not Exist!");
                return;
            }
            CurrentState?.OnExit();
            if (CurrentState == state && !forceStart) return;
            CurrentState = state;
            CurrentState.OnStart();

        }

        private State StringToState(string stateName)
        {
            foreach (var state in allStates)
            {
                if (state.Key.ToLower() == stateName.ToLower())
                {
                    return state.Value;
                }
            }
            return null;
        }
        private string StateToString(State state)
        {
            if (state == null) return null;
            foreach (var s in allStates)
            {
                if (s.Value == state)
                {
                    return s.Key;
                }
            }
            return null;
        }
        private void Awake()
        {
            InitStates();
        }

        private void InitStates()
        {
            if (allStates == null) allStates = new Dictionary<string, State>();
            allStates.Add("EmptyState", new EmptyState());
            allStates.Add("RunState", new RunState());
            //[NEW_STATE]
        }

        private void Update()
        {
            CurrentState?.OnUpdate();
        }

        public void OnDrag(PointerEventData eventData)
        {
            CurrentState?.OnDrag(eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            CurrentState?.OnPointerDown(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            CurrentState?.OnPointerUp(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            CurrentState?.OnEndDrag(eventData);
        }

    }

    public class StateChanger : MonoBehaviour
    {
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(StateChanger)), Serializable]
    public class StateChangerDrawer : PropertyDrawer
    {
        private InputManager inputManager;
        private int selectedState = 0;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            if (!Application.isPlaying)
            {
                GUILayout.Label($"Waiting to play..", EditorStyles.helpBox);
                return;
            }
            inputManager = InputManager.Instance;
            if (inputManager.CurrentState != null) GUILayout.Label($"<color=\"White\">Selected state <b>{inputManager.CurrentStateName}</b></color>", GUIStyle.none);
            var allStateNames = inputManager.AllStates.Keys.ToArray();
            selectedState = EditorGUILayout.Popup("States", selectedState, allStateNames);
            if (GUILayout.Button("Select State"))
            {
                inputManager.SetState(allStateNames[selectedState]);
            }
            //base.OnGUI(position, property, label);

        }
    }
#endif
}
