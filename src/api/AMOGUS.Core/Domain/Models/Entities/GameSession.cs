﻿using AMOGUS.Core.Domain.Enums;
using AMOGUS.Infrastructure.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMOGUS.Core.Domain.Models.Entities {
    public class GameSession {

        [Key]
        public string SessionId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public TimeSpan Playtime { get; set; }

        public int CorrectAnswersCount { get; set; }

        public int GivenAnswersCount { get; set; }

        public TimeSpan AverageTimePerQuestion { get; set; }

        public TimeSpan QuickestAnswer { get; set; }

        public TimeSpan SlowestAnswer { get; set; }

        public CategoryType Category { get; set; }

        public DateTime PlayedAt { get; set; }

        [NotMapped]
        public List<Question> Questions = new();

        [NotMapped]
        public double QuickestAnswerInMillis {
            get {
                return QuickestAnswer.TotalMilliseconds;
            }
        }

        [NotMapped]
        public double SlowestAnswerInSeconds {
            get {
                return SlowestAnswer.TotalSeconds;
            }
        }
    }
}
