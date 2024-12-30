using MinefieldGame.ConsoleApp.Extensions;
using MinefieldGame.ConsoleApp.Intefraces;

namespace MinefieldGame.ConsoleApp
{
    public class MinePlacer : IMinePlacer
    {
        private readonly IGameConfiguration _gameConfiguration;
        private readonly Random _random = new();
        private readonly int _maxMinesCountPerRow;

        public MinePlacer(IGameConfiguration gameConfiguration, Random? random = null)
        {
            _gameConfiguration = gameConfiguration;
            _random = random ?? new Random();
            _maxMinesCountPerRow = _gameConfiguration.Columns / 2 - 1;
        }

        public HashSet<int> PlaceMines(List<int> possibleExclusions)
        {
            if (possibleExclusions is null || possibleExclusions.Count == 0)
                throw new ArgumentException($"{nameof(possibleExclusions)} does not exist.");


            var selectedExlusions = SelectRandomExclusions(possibleExclusions, _gameConfiguration.Difficulty.CalculateMaxCountOfExlusions());

            var validNumbers = Enumerable.Range(0, _gameConfiguration.Columns - 1)
                                         .Where(x => !selectedExlusions.Contains(x))
                                         .ToArray();

            if (validNumbers.Length == 0)
                return new HashSet<int>();

            HashSet<int> generatedNumbers = new();

            while (generatedNumbers.Count <= _maxMinesCountPerRow)
            {
                int number = validNumbers[_random.Next(validNumbers.Length)];
                generatedNumbers.Add(number);
            }

            return generatedNumbers;
        }

        internal HashSet<int> SelectRandomExclusions(List<int> possibleExclusions, int maxCountOfExclusions)
        {
            // limit the number of exclusions to the smaller of maxCountOfExclusions or the list size
            int countToSelect = Math.Min(maxCountOfExclusions, possibleExclusions.Count);

            // shuffle the list and take the required number of items
            return possibleExclusions
                .OrderBy(_ => _random.Next())
                .Take(countToSelect)
                .ToHashSet();
        }
    }
}
