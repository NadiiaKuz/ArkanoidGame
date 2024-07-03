using System.Collections.Generic;

namespace ArkanoidGame.Statistics
{
    public class GameStats
    {
        public int NumberOfWins { get; set; }
        
        public int NumberOfFailures { get; set; }

        private Dictionary<string, int> counters;

        public GameStats()
        {
            ResetAll();
        }

        public bool AddGameCounter(string counterKey, int initalValue)
        {
            if (counterKey == null)
            {
                return false;
            }
            if (!counters.ContainsKey(counterKey))
            {
                counters.Add(counterKey, initalValue);
                return true;
            }
            return false;
        }

        public bool ResetGameCounter(string counterKey)
        {
            if (counterKey == null)
            {
                return false;
            }
            if (counters.ContainsKey(counterKey))
            {
                counters[counterKey] = 0;
                return true;
            }
            return false;
        }

        public bool IncrementGameCounter(string counterKey)
        {
            if (counterKey == null)
            {
                return false;
            }
            if (counters.ContainsKey(counterKey))
            {
                counters[counterKey]++;
                return true;
            }
            return false;
        }

        public bool SetGameCounterValue(string counterKey, int value)
        {
            if (counterKey == null)
            {
                return false;
            }
            if (counters.ContainsKey(counterKey))
            {
                counters[counterKey] = value;
                return true;
            }
            return false;
        }

        public int GetGameCounterValue(string counterKey)
        {
            if (counterKey == null)
            {
                return -1;
            }
            if (counters.ContainsKey(counterKey))
            {
                return counters[counterKey];
            }
            return -1;
        }

        public void IncrementNumberOfWins()
        {
            NumberOfWins++;
        }

        public void InctementNumberOfFailures()
        {
            NumberOfFailures++;
        }

        public void ResetNumberOfWins()
        {
            NumberOfWins = 0;
        }

        public void ResetNumberOfFailures()
        {
            NumberOfFailures = 0;
        }

        public void ResetAllGameCounters()
        {
            if (counters == null)
            {
                counters = new Dictionary<string, int>();
                return;
            }
            if (counters.Count > 0)
            {
                foreach (string counterKey in counters.Keys)
                {
                    counters[counterKey] = 0;
                }
            }
        }

        public void ResetAll()
        {
            ResetAllGameCounters();
            ResetNumberOfWins();
            ResetNumberOfFailures();
        }
    }
}
