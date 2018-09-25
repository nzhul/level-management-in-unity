using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

namespace LevelManagement
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField]
        private MainMenu mainMenuPrefab;
        //[SerializeField]
        //private SettingsMenu settingsMenuPrefab;
        //[SerializeField]
        //private CreditsScreen creditsScreenPrefab;
        //[SerializeField]
        //private GameMenu gameMenuPrefab;
        //[SerializeField]
        //private PauseMenu pauseMenuPrefab;
        //[SerializeField]
        //private WinScreen winScreenPrefab;

        [SerializeField]
        public Menu[] Menus;

        [SerializeField]
        private Transform _menuParent;

        private Stack<Menu> _menuStack = new Stack<Menu>();

        private static MenuManager _instance;

        public static MenuManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                InitializeMenus();

                // for this to work the object must be on root level in the hierarchy
                DontDestroyOnLoad(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        private void InitializeMenus()
        {
            if (_menuParent == null)
            {
                GameObject menuParentObject = new GameObject("Menus");
                _menuParent = menuParentObject.transform;
            }

            DontDestroyOnLoad(_menuParent.gameObject);

            //FieldInfo[] fields = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);

            //foreach (FieldInfo  field in fields)
            //{
            //    Menu prefab = field.GetValue(this) as Menu;

            //    if (prefab != null)
            //    {
            //        Menu menuInstance = Instantiate(prefab, _menuParent);
            //        if (prefab != mainMenuPrefab)
            //        {
            //            menuInstance.gameObject.SetActive(false);
            //        }
            //        else
            //        {
            //            // open main menu
            //            OpenMenu(menuInstance);
            //        }
            //    }
            //}

            foreach (Menu menu in Menus)
            {
                Menu menuInstance = Instantiate(menu, _menuParent);
                if (menu != mainMenuPrefab)
                {
                    menuInstance.gameObject.SetActive(false);
                }
                else
                {
                    // open main menu
                    OpenMenu(menuInstance);
                }
            }
        }

        public void OpenMenu(Menu menuInstance)
        {
            if (menuInstance == null)
            {
                Debug.LogWarning("MENUMANAGER: OpenMenu ERROR: invalid menu");
                return;
            }

            if (_menuStack.Count > 0)
            {
                foreach (Menu menu in _menuStack)
                {
                    menu.gameObject.SetActive(false);
                }
            }

            menuInstance.gameObject.SetActive(true);
            _menuStack.Push(menuInstance);
        }

        public void CloseMenu()
        {
            if (_menuStack.Count == 0)
            {
                Debug.LogWarning("MENUMANAGER CloseMenu ERROR: No menus in stack!");
                return;
            }

            Menu topMenu = _menuStack.Pop();
            topMenu.gameObject.SetActive(false);

            if (_menuStack.Count > 0)
            {
                Menu nextMenu = _menuStack.Peek();
                nextMenu.gameObject.SetActive(true);
            }
        }

    }
}
