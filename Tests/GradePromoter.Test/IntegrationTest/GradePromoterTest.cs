using GradePromoter.Services;
using Shouldly;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace GradePromoter.IntegrationTest
{
    public class GradePromoterTest
    {
        [Fact]
        public async Task GradePromoter_ProducesExpectedOutput()
        {
            var outfile = @".\Output.txt";
            var gradePromoter = new GradePromoter(new FileService(), new PromotionService());
            gradePromoter.CalculatePromotions(@".\IntegrationTest\ExamResults.csv", outfile);

            var output = await File.ReadAllLinesAsync(outfile);
            output.Length.ShouldBe(15);
            output[0].ShouldBe("Grade Standard 1");
            output[1].ShouldBe("");
            output[2].ShouldBe("Promoted:");
            output[3].ShouldBe("9,Semaj Lawson");
            output[4].ShouldBe("8,Aedan Weaver");
            output[5].ShouldBe("2,Cierra Hendrix");
            output[6].ShouldBe("5,Thaddeus Fitzpatrick");
            output[7].ShouldBe("7,Francis Knight");
            output[8].ShouldBe("6,Roderick Morales");
            output[9].ShouldBe("1,Gisselle Haley");
            output[10].ShouldBe("4,Elise Larsen");
            output[11].ShouldBe("");
            output[12].ShouldBe("Not Promoted:");
            output[13].ShouldBe("3,Jon Rojas");
        }
    }
}
