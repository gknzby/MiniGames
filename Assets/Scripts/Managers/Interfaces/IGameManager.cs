namespace Gknzby.Managers
{
    public interface IGameManager : IManager
    {
        void SendGameAction(GameAction gameAction);
    }
}