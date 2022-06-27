namespace Gknzby.Managers
{
    public interface ILevelManager : IManager
    {
        int LevelCount { get; }
        bool LoadLevel(int index);

        //void AddNewLevelListener(INewLevelListener listener);
        //void RemoveNewLevelListener(INewLevelListener listener);
    }
}