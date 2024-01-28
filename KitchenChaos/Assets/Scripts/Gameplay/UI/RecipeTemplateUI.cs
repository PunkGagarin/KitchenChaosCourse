using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.KitchenObjects;
using Gameplay.KitchenObjects.Scriptables;
using Gameplay.UI.Gameplay.KitchenItems;
using TMPro;
using UnityEngine;

public class RecipeTemplateUI : MonoBehaviour
{
    // private List<PlateIconSingleUI> _icons = new();

    [SerializeField]
    private Transform _content;

    [SerializeField]
    private PlateIconSingleUI _plateIconSinglePrefab;

    [SerializeField]
    private KitchenItemSoFactory _soFactory;

    [field: SerializeField]
    private TextMeshProUGUI RecipeName { get; set; }

    [field: SerializeField]
    public RecipeType Type { get; set; }


    public void Init(RecipeType type, string name)
    {
        Type = type;
        RecipeName.text = name;
    }

    private void TurnOnIcons(List<KitchenItemType> iconTypes)
    {
        // foreach (var type in iconTypes)
        // {
        //     PlateIconSingleUI icon = _icons.FirstOrDefault(el => el.Type == type);
        //     if (icon != null)
        //     {
        //         icon.gameObject.SetActive(true);
        //     }
        // }
    }

    public void InitWithSpawn(RecipeType type, string name, List<KitchenItemType> iconTypes)
    {
        Init(type, name);
        SpawnIcons(iconTypes);
    }

    public void InitWithTurnOnIcons(RecipeType type, string name, List<KitchenItemType> iconTypes)
    {
        Init(type, name);
        TurnOnIcons(iconTypes);
    }

    private void SpawnIcons(List<KitchenItemType> iconTypes)
    {
        foreach (Transform child in _content)
        {
            Destroy(child.gameObject);
        }
        //todo: отдать спавн иконок другому классу
        foreach (var type in iconTypes)
        {
            PlateIconSingleUI icon = Instantiate(_plateIconSinglePrefab, _content);
            icon.Type = type;
            icon.ChangeImageSprite(_soFactory.GetKitchenItemIconByType(type));
            // _icons.Add(icon);
            icon.gameObject.SetActive(true);
        }
    }
}