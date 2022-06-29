using Gknzby.Components;

namespace Gknzby.Managers
{
    public interface ILevelGenerator
    {
        void SetLevelData(ILevelData levelData);
        void GenerateLevel();
    }
}