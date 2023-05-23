using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Code.UI.Presenter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameWeaponConfigView : MonoBehaviour, IWeaponConfigView
{
    [SerializeField] private TextMeshProUGUI weaponTypeLabel;
    [SerializeField] private TextMeshProUGUI speedModifierLabel;
    [SerializeField] private TextMeshProUGUI attackSpeedLabel;
    [SerializeField] private TextMeshProUGUI attackRangeModifierLabel;
    [SerializeField] private TextMeshProUGUI applyDamageSpeedLabel;
    
    [SerializeField] private TMP_InputField speedModifierInput;
    [SerializeField] private TMP_InputField attackSpeedInput;
    [SerializeField] private TMP_InputField attackRangeModifierInput;
    [SerializeField] private TMP_InputField applyDamageSpeedInput;
    private IWeapon _weapon;

    public void Initialize(IWeapon weaponValue)
    {
        _weapon = weaponValue;
        weaponTypeLabel.text = _weapon.Type.ToString();
        speedModifierLabel.text = $"Speed Modifier";
        speedModifierInput.text = _weapon.SpeedModifier.ToString(CultureInfo.InvariantCulture);
        
        attackSpeedLabel.text = $"Attack Speed";
        attackSpeedInput.text = _weapon.AttackSpeedModifier.ToString(CultureInfo.InvariantCulture);
        
        attackRangeModifierLabel.text = $"Attack Range Modifier";
        attackRangeModifierInput.text = _weapon.AttackRange.ToString(CultureInfo.InvariantCulture);
        
        applyDamageSpeedLabel.text = $"Apply Damage Speed";
        applyDamageSpeedInput.text = _weapon.ApplyDamageSpeed.ToString(CultureInfo.InvariantCulture);
    }

    public IWeapon GetNewValues() =>
        new WeaponDto(float.Parse(speedModifierInput.text),
            _weapon.Type,
            float.Parse(attackSpeedInput.text),
            float.Parse(attackRangeModifierInput.text),
            float.Parse(applyDamageSpeedInput.text));
}