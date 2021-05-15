/*
using System;
using Chatbot;
using System.Collections.Generic;

//Code modified from Cole Fitcher's NRAKE implementation

namespace Chatbot
{
    public class RAKEParser : KeywordParserInterface
    {
        
		SortedSet<string> _uniqueWords = null;

        public SortedSet<string> UniqueWordIndex
        {
            get
            {
                return _uniqueWords;
            }
        }
		
		
		public List<string> parseQuestion(string question){
			string[] tokens = Tokenize(question);
            string[] phrases = ToPhrases(tokens);
            WordCooccurrenceMatrix matrix = new WordCooccurrenceMatrix(this.UniqueWordIndex);
            matrix.CompileOccurrences(phrases);
            SortedList<string, WordScore> leagueTable = matrix.LeagueTable;
            SortedList<string, double> aggregatedLeagueTable = WordCooccurrenceMatrix.AggregateLeagueTable(leagueTable, phrases);

            int count = (int)Math.Ceiling((double)phrases.Length / (double)3); //Take the top 1/3 of the key phrases

            return aggregatedLeagueTable.OrderByDescending(x => x.Value).Take(count).Select(x => x.Key).ToArray();
		}
		
		
		
		public string[] Tokenize(string inputText){
            List<string> tokens = new List<string>();
            foreach (string s in _reSplit.Split(inputText))
            {
                if (!string.IsNullOrEmpty(s))
                {
                    tokens.Add(s.Trim().ToLower());
                }
            }

            return tokens.ToArray();
        }
		
		public string[] ToPhrases(string[] tokens){
            _uniqueWords = new SortedSet<string>();
            List<string> phrases = new List<string>();

            string current = string.Empty;
            foreach (string t in tokens)
            {
                if (_stopWords.IsPunctuation(t) || _stopWords.IsStopWord(t))
                {
                    //Throw it away!
                    if (current.Length > 0)
                    {
                        phrases.Add(current);
                        current = string.Empty;
                    }
                }
                else
                {
                    _uniqueWords.Add(t);
                    if (current.Length == 0)
                    {
                        current = t;
                    }
                    else
                    {
                        current += " " + t;
                    }
                }
            }

            if (current.Length > 0)
            {
                phrases.Add(current);
            }

            return phrases.ToArray();
        }
		
    }
}
*/