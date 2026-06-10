using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

// Wichtig für TextMeshPro

namespace Resources.Prefabs.UI.Common
{
    public class CycleController : MonoBehaviour
    {
        [Header("UI Elements")] [SerializeField]
        private TextMeshProUGUI buttonText;

        [Header("Settings")] [SerializeField] private List<string> options = new List<string> { "Niedig", "Mittel", "Hoch" };

        private List<GameObject> _indexButtons = new List<GameObject>();
        private Button _prevButton;
        private Button _nextButton;

        private int currentIndex = 0;
        private UnityEvent<string> onOptionChange { get; } = new UnityEvent<string>();

        public string CurrentSelection => options[currentIndex];
        public int CurrentIndex => currentIndex;

        void Start()
        {
            _prevButton = gameObject.transform.Find("Buttons").Find("Prev").gameObject.GetComponent<Button>();
            _prevButton.onClick.AddListener(OnPrev);
            _nextButton = gameObject.transform.Find("Buttons").Find("Next").gameObject.GetComponent<Button>();
            Debug.Log(_nextButton);

            _nextButton.onClick.AddListener(OnNext);

            var indexContainer = gameObject.transform.Find("Index").gameObject;

            var indexButtonPrefab = UnityEngine.Resources.Load<GameObject>("Prefabs/UI/Common/IndexButton");

            foreach (var option in options)
            {
                Debug.Log(option);
                var indexButton = Instantiate(indexButtonPrefab, indexContainer.transform);
                indexButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    OnSelectIndex(options.IndexOf(option));
                });
                _indexButtons.Add(indexButton);
            }
            UpdateUI();
        }

        protected void OnPrev()
        {
            Debug.Log("Prev");
            currentIndex = (currentIndex - 1 + options.Count) % options.Count;
            UpdateUI();
            InvokeEvent();
        }

        protected void OnNext()
        {
            Debug.Log("Next");
            currentIndex = (currentIndex + 1) % options.Count;
            UpdateUI();
            InvokeEvent();
        }

        protected void OnSelectIndex(int index)
        {
            currentIndex = index;
            UpdateUI();
            InvokeEvent();
        }

        private void UpdateUI()
        {
            if (buttonText != null && options.Count > 0)
            {
                buttonText.text = options[currentIndex];
            }
            _indexButtons[currentIndex].GetComponent<Image>().color = Color.yellow;
            foreach (var indexButton in _indexButtons)
            {
                if (indexButton != _indexButtons[currentIndex])
                {
                    indexButton.GetComponent<Image>().color = Color.white;
                }
            }
        }
        
        private void InvokeEvent()
        {
            onOptionChange?.Invoke(options[currentIndex]);
        }
    }
}