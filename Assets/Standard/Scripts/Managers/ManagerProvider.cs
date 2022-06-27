using System.Collections.Generic;

namespace Gknzby.Managers
{
    public class ManagerProvider
    {
        private static ManagerProvider instance;
        private static ManagerProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ManagerProvider();
                }
                return instance;
            }
        }

        private readonly Dictionary<System.Type, IManager> managerDict = new();

        public static void AddManager<T>(IManager manager)
        {
            if (Instance.managerDict.ContainsKey(typeof(T)))
            {
                Instance.managerDict[typeof(T)] = manager;
            }
            else
            {
                Instance.managerDict.Add(typeof(T), manager);
            }
        }

        public static T GetManager<T>()
        {
            return Instance.managerDict.ContainsKey(typeof(T)) ? (T)Instance.managerDict[typeof(T)] : default;
        }

        public static void RemoveManager<T>()
        {
            if (Instance.managerDict.ContainsKey(typeof(T)))
            {
                Instance.managerDict.Remove(typeof(T));
            }
        }
    }
}