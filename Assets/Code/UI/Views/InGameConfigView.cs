using System;
using System.Collections;
using System.Collections.Generic;
using Code.UI.Presenter;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InGameConfigView : MonoBehaviour, IGameConfigView
{
    [SerializeField] private Transform layoutGroup; 
    [SerializeField] private InGameWeaponConfigView inGameWeaponConfigViewPrefab;
    [SerializeField] private Button applyChangesButton;
    private List<WeaponConfigPresenter> weaponsConfigs;
    private ISubject<IWeapon> _applyChange;

    public void CreateBoxesFor(IDictionary<WeaponType, IWeapon> weapons)
    {
        weaponsConfigs = new List<WeaponConfigPresenter>();
        foreach (var weapon in weapons)
        {
            var weaponConfigView = Instantiate(inGameWeaponConfigViewPrefab, layoutGroup);
            var weaponConfigPresenter = new WeaponConfigPresenter(weaponConfigView, weapon.Value);
            weaponsConfigs.Add(weaponConfigPresenter);
            weaponConfigPresenter.Initialize();
        }
    }

    public void Initialize(ISubject<IWeapon> applyChange)
    {
        _applyChange = applyChange;
        applyChangesButton.onClick.RemoveAllListeners();
        applyChangesButton.onClick.AddListener(ApplyChanges);
     
    }

    public void ApplyChanges()
    {
        foreach (var weaponConfigPresenter in weaponsConfigs)
        {
            _applyChange.OnNext(weaponConfigPresenter.GetWeaponChanges());
        }
    }
}
