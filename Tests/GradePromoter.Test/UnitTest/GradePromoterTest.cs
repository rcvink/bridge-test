using GradePromoter.Models;
using GradePromoter.Services;
using GradePromoter.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GradePromoter.Test.UnitTest
{
    public class GradePromoterTest : IDisposable
    {
        private readonly Mock<IPromotionService> promotionsServiceMock;
        private readonly Mock<IFileService> fileServiceMock;
        private readonly GradePromoter gradePromoter;

        public GradePromoterTest()
        {
            this.promotionsServiceMock = new Mock<IPromotionService>(MockBehavior.Strict);
            this.fileServiceMock = new Mock<IFileService>(MockBehavior.Strict);
            this.gradePromoter = new GradePromoter(this.fileServiceMock.Object, promotionsServiceMock.Object);
        }

        [Fact]
        public void GradePromoter_Returns()
        {
            var input = "input.txt";
            var output = "output.txt";
            var grade = "grade1";
            var subject = "subject";
            var pupilName = "name";
            var examResults = new List<ExamResult>()
            {
                new ExamResult()
                {
                    Grade = grade,
                    Subject = subject,
                    PupilName = pupilName

                }
            };
            var pupils = new List<Pupil>()
            {
                new Pupil()
                {
                    PupilId = 0,
                    PupilName = pupilName,
                    Grade = grade,
                    Promoted = false
                }
            };
            var grades = new List<Grade>()
            {
                new Grade()
                {
                    Name = grade
                }
            };

            this.fileServiceMock
                .Setup(x => x.ParseExamResultsFromCsv(It.IsAny<string>()))
                .Returns(examResults);
            promotionsServiceMock
                .Setup(x => x.GetPromotionResults(examResults))
                .Returns(pupils);
            this.fileServiceMock.Setup(x => x.WritePromotionResults(
                It.Is<List<Grade>>(y => y.Count == 1 && y.First().Name == grade), pupils, output))
                .Returns(string.Empty);

            gradePromoter.CalculatePromotions(input, output);

        }

        public void Dispose() =>

            Mock.VerifyAll(this.promotionsServiceMock,
                this.fileServiceMock);
    }
}
