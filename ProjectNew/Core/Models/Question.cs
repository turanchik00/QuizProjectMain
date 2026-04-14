using ProjectNew.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectNew.Core.Models
{
    public class Question
    {
        public int Id { get; set; }                                             // Sualın unikal iD-si
        public string Text { get; set; }                                        // Sualın mətni
        public List<string> Options { get; set; } = new List<string>();         // Düzgün cavabın indeksi: A=0, B=1, C=2, D=3

        public int CorrectOptionIndex { get; set; }                             // Düzgün cavabın indeksi: A=0, B=1, C=2, D=3
        public DifficultyLevel Difficulty { get; set; }                         // Sualın çətinlik səviyyəsi (Easy, Medium, Hard)
    }
}
