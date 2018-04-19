using System.Linq;
using GradePromoter.Models;
using GradePromoter.Services;

namespace GradePromoter
{
  public class GradePromoter
  {
    private readonly IFileService fileService;
    private readonly IPromotionService promotionService;

    public GradePromoter(
      IFileService fileService,
      IPromotionService promotionService)
    {
        this.fileService = fileService;
        this.promotionService = promotionService;
    }

    public void CalculatePromotions(string input, string output)
    {
        // Read csv file and transform them into ExamResult objects
        var examResults = fileService.ParseExamResultsFromCsv(input);

        // Get the a list of the distinct grade names from the ExamResult objects
        var grades = examResults.Select(x => x.Grade ).Distinct().Select(x => new Grade { Name = x } ).ToList();

        // Process the exam results data
        var pupils = promotionService.GetPromotionResults(examResults);
                  
        // Write the promotion results to a file
        fileService.WritePromotionResults(grades, pupils, output);
    }
  }
}