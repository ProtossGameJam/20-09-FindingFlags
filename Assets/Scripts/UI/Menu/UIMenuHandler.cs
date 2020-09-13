using System;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

public class UIMenuHandler : MonoBehaviour
{
    [System.Serializable]
    public class MenuDictionary : SerializableDictionaryBase<string, UIMenu>
    {
        public UIMenu MenuOpen(string name, bool isCollapse = false)
        {
            if (isCollapse) {
                foreach (var menu in this.Values) {
                    menu.rootMenuObject.SetActive(false);
                }
            }
            this[name].rootMenuObject.SetActive(true);
            return this[name];
        }
    }

    [SerializeField] private MenuDictionary menuDic;

    [Tooltip("시작 시 바로 켜지는 메뉴 오브젝트 이름")]
    [SerializeField] private string startMenuName;

    private void Start()
    {
        menuDic.MenuOpen(startMenuName, true);
    }

    public void MenuOpen(string name)
    {
        if (!menuDic[name].rootMenuObject.activeSelf)
        {
            menuDic.MenuOpen(name);
        }
    }

    public void MenuOpenAlone(string name)
    {
        if (!menuDic[name].rootMenuObject.activeSelf)
        {
            menuDic.MenuOpen(name, true);
        }
    }
}