using System;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour
{
    private float _currentStamina;
    [Header("Stamina Defaults")]
    [SerializeField] private float maxStamina = 200f;
    [SerializeField] private float hitCost = 20f;
    [SerializeField] private float jumpCost = 10f;
    private bool isSprinting = false;
    private bool isRegenerated = true;
    
    [Header("Stamina Regen")]
    [SerializeField] private float regenRate = 0.5f;
    [SerializeField] private float drainRate = 0.5f;
    
    [Header("Stamina UI")]
    [SerializeField] private Image fillImage;
    [SerializeField] private CanvasGroup canvasGroup;
    

    private void Start()
    {
    }

    private void Update()
    {
        // We need to regen
        if (_currentStamina < maxStamina && !isSprinting)
        {
            
        }
        
    }

    public void OnSprint()
    {
        
    }

    private void OnHit()
    {
        if (_currentStamina >= hitCost)
        {
            _currentStamina -= hitCost;
            // allow hit
            UpdateUI(1);
        }
    }

    private void UpdateUI(int alpha)
    {
        fillImage.fillAmount = _currentStamina / maxStamina;
        canvasGroup.alpha = alpha;
    }
}
