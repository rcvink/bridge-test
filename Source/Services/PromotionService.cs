using GradePromoter.Models;
using GradePromoter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GradePromoter.Services
{
    public class PromotionService : IPromotionService
    {
        public List<Pupil> GetPromotionResults(List<ExamResult> examResults)
        {
            var pupils = examResults
                .GroupBy(x => x.PupilId)
                .Select( x => this.CheckIfPromoted(x.ToList())).ToList();
            return pupils;
        }

        private Pupil CheckIfPromoted(List<ExamResult> examResults) {
          return new Pupil
            {
                PupilName = examResults.First().PupilName,
                Grade = examResults.First().Grade,
                PupilId = examResults.First().PupilId,
                Promoted = examResults.Average(x => x.Result) > 50
            };
        } 
    }
}
