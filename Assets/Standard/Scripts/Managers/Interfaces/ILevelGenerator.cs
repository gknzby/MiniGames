using Gknzby.Components;

namespace Gknzby.Managers
{
    public interface ILevelGenerator : IManager
    {
        void SetLevelData(ILevelData levelData);
        void GenerateLevel();
    }
}