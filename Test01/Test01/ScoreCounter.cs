﻿using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;

namespace Test01 {
    public class ScoreCounter {
        private IEnumerable<Student> _score;

        // コンストラクタ
        public ScoreCounter(string filePath) {
            _score = ReadScore(filePath);

        }

        //メソッドの概要： 
        private static IEnumerable<Student> ReadScore(string filePath) {
            var scores = new List<Student>();
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines) {
                string[] items = line.Split(',');
                var score = new Student() {
                    Name = items[0],
                    Subject = items[1],
                    Score = int.Parse(items[2])
                };
                scores.Add(score);
            }
            return scores;
        }
        //メソッドの概要： 
        public IDictionary<string, int> GetPerStudentScore() {
            var dict = new Dictionary<string, int>();
            foreach (var score in _score) {
                if (dict.ContainsKey(score.Name)) {
                    dict[score.Name] += score.Score;
                } else {
                    dict[score.Name] = score.Score;
                }
            }
            return dict;
        }
    }
}
