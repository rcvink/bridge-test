using GradePromoter.Services;
using GradePromoter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests
{
    public class PromotionServiceTests
    {
        private readonly PromotionService promotionService;

        public PromotionServiceTests()
        {
            promotionService = new PromotionService();
        }

        [Fact]
        public void PupilWithGreaterThan50PercentAverage_ReturnsPromoted()
        {
            var examResults = new List<ExamResult>
            {
                new ExamResult
                {
                    PupilId = 1,
                    Grade = "Standard 1",
                    AssesmentDate = new DateTime(),
                    AssessmentType = "End-Term",
                    PupilName = "Joe Bloggs",
                    Result = 70,
                    Subject = "Maths"
                },
                new ExamResult
                {
                    PupilId = 1,
                    Grade = "Standard 1",
                    AssesmentDate = new DateTime(),
                    AssessmentType = "End-Term",
                    PupilName = "Joe Bloggs",
                    Result = 45,
                    Subject = "English"
                }
            };

            var result = this.promotionService.GetPromotionResults(examResults);
            Assert.NotEmpty(result);
            Assert.True(result.First().Promoted);
        }

        [Fact]
        public void PupilWithLessThan50PercentAverage_ReturnsNotPromoted()
        {
            var examResults = new List<ExamResult>
            {
                new ExamResult
                {
                    PupilId = 1,
                    Grade = "Standard 1",
                    AssesmentDate = new DateTime(),
                    AssessmentType = "End-Term",
                    PupilName = "Joe Bloggs",
                    Result = 51,
                    Subject = "Maths"
                },
                 new ExamResult
                {
                    PupilId = 1,
                    Grade = "Standard 1",
                    AssesmentDate = new DateTime(),
                    AssessmentType = "End-Term",
                    PupilName = "Joe Bloggs",
                    Result = 49,
                    Subject = "English"
                }
            };

            var result = this.promotionService.GetPromotionResults(examResults);
            Assert.NotEmpty(result);
            Assert.False(result.First().Promoted);
        }
    }
}
