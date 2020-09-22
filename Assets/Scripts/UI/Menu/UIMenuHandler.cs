using System;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

public class UIMenuHandler : MonoBehaviour
{
    [SerializeField] private MenuDictionary menuDic;

    [Tooltip("시작 시 바로 켜지는 메뉴 오브젝트")] [SerializeField]
    private UIMenu startMenu;

    private void Start() {
        ActiveMenu(startMenu, true);
    }

    public void MenuOpen(string name) {
        ActiveMenu(name, false);
    }

    public void MenuOpen(UIMenu menu) {
        ActiveMenu(menu, false);
    }

    public void MenuOpenAlone(string name) {
        ActiveMenu(name, true);
    }

    public void MenuOpenAlone(UIMenu menu) {
        ActiveMenu(menu, true);
    }

    private void ActiveMenu(string name, bool preventDuplicate) {
        if (preventDuplicate)
            foreach (var menu in menuDic.Values)
                menu.rootMenuObject.SetActive(false);
        menuDic[name].rootMenuObject.SetActive(true);
    }

    private void ActiveMenu(UIMenu menu, bool preventDuplicate) {
        if (preventDuplicate)
            foreach (var tempMenu in menuDic.Values)
                tempMenu.rootMenuObject.SetActive(false);
        if (menuDic.ContainsValue(menu)) menu.rootMenuObject.SetActive(true);
    }

    [Serializable]
    public class MenuDictionary : SerializableDictionaryBase<string, UIMenu>
    { }
}