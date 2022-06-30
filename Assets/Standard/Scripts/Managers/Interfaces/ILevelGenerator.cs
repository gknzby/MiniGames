using Gknzby.Components;

namespace Gknzby.Managers
{
    public interface ILevelGenerator
    {
        ILevelData CurrentLevelData { get; set; }
        void SetLevelData(ILevelData levelData);
        void GenerateLevel();
    }
}