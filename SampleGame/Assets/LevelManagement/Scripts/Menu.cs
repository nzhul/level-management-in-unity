using SampleGame;
using UnityEngine;

namespace LevelManagement
{
    /// <summary>
    /// Curiously recurring template pattern
    /// https://en.wikipedia.org/wiki/Curiously_recurring_template_pattern
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Menu<T> : Menu where T : Menu<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = (T)this;
            }
        }

        protected virtual void OnDestroy()
        {
            _instance = null;
        }

        public static void Open()
        {
            if (MenuManager.Instance != null && Instance != null)
            {
                MenuManager.Instance.OpenMenu(Instance);
            }
        }
    }

    [RequireComponent(typeof(Canvas))]
    public abstract class Menu : MonoBehaviour
    {
        public virtual void OnBackPressed()
        {
            if (MenuManager.Instance != null)
            {
                MenuManager.Instance.CloseMenu();
            }
        }
    }
}
