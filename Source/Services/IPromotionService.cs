using GradePromoter.Models;
using GradePromoter.ViewModels;
using System.Collections.Generic;

namespace GradePromoter.Services
{
    public interface IPromotionService
    {
        List<Pupil> GetPromotionResults(List<ExamResult> examResults);
    }
}