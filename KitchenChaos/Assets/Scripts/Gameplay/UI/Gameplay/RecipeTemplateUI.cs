using System.Collections.Generic;
using Gameplay.KitchenObjects;
using Gameplay.KitchenObjects.Scriptables;
using Gameplay.UI.Gameplay.KitchenItems;
using TMPro;
using UnityEngine;

public class RecipeTemplateUI : MonoBehaviour
{
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

    public void InitWithSpawn(RecipeType type, string name, List<KitchenItemType> iconTypes)
    {
        Init(type, name);
        SpawnIcons(iconTypes);
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
            icon.gameObject.SetActive(true);
        }
    }
}