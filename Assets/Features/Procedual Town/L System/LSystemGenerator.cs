using System;
using System.Text;
using NUnit.Framework;
using UnityEngine;
using Random = UnityEngine.Random;
using RangeAttribute = UnityEngine.RangeAttribute;

namespace ProceduralTown
{
    public class LSystemGenerator : MonoBehaviour
    {
        public Rule[] rules;
        public string root;
        [Range(0,10)] public int iterationLimit = 1;

        public bool randomIgnoreRuleModifier = true;
        [Range(0, 1)] public float chanceToIgnoreRule = 0.3f;

        
        public string GenerateSentence(string word = null)
        {
            if (word == null)
            {
                word = root;
            }
            return GrowRecursiveSentence(word);
        }

        private string GrowRecursiveSentence(string word, int iterationIndex = 0) 
        {
            if (iterationIndex >= iterationLimit)
            {
                return word;
            }
            StringBuilder newWord = new StringBuilder();
            
            foreach (var c in word)
            {
                newWord.Append(c);
                ProcessRulesRecursivelly(newWord, c, iterationIndex);
            }

            return newWord.ToString();
        }

        private void ProcessRulesRecursivelly(StringBuilder newWord, char c, int iterationIndex)
        {
            foreach (var rule in rules)
            {
                if (rule.letter != c.ToString())
                {
                    continue;
                }
                if (randomIgnoreRuleModifier && iterationIndex > 1)
                {
                    if (Random.value < chanceToIgnoreRule)
                    {
                        return;
                    }
                }
                newWord.Append(GrowRecursiveSentence(rule.GetResult(), iterationIndex + 1));
            }
        }
    }
}