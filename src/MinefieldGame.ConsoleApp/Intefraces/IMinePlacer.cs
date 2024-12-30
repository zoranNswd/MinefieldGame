namespace MinefieldGame.ConsoleApp.Intefraces
{
    public interface IMinePlacer
    {
        HashSet<int> PlaceMines(List<int> possibleExclusions);
    }
}
