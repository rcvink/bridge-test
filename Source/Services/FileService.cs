using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using GradePromoter.Models;
using GradePromoter.ViewModels;

namespace GradePromoter.Services
{
    public class FileService : IFileService
    {
       public List<ExamResult> ParseExamResultsFromCsv(string filepath)
       {
            using (TextReader fileReader = File.OpenText(filepath))
            {
                var csv = new CsvReader(fileReader);
                csv.Configuration.HasHeaderRecord = false;
                csv.Read();
                var examResult = csv.GetRecords<ExamResult>().ToList();
                return examResult;
            }
       }

        public string WritePromotionResults(List<Grade> grades, List<Pupil> pupils, string outputPath)
        {
            if (pupils == null || pupils.Count == 0)
                throw new ArgumentNullException(nameof(pupils));

            var path = Path.Combine(outputPath);

            using (var writer = File.CreateText(path))
            {
                foreach (var grade in grades)
                {
                    var promotedPupils = pupils.Where(x => x.Grade == grade.Name && x.Promoted);
                    var promotedNotPupils = pupils.Where(x => x.Grade == grade.Name && !x.Promoted);

                    writer.WriteLine($"Grade {grade.Name}");
                    writer.WriteLine("");
                    writer.WriteLine("Promoted:");

                    foreach (var pupil in promotedPupils)
                    {
                        writer.WriteLine($"{pupil.PupilId},{pupil.PupilName}");
                    }

                    writer.WriteLine("");
                    writer.WriteLine("Not Promoted:");
                    foreach (var pupil in promotedNotPupils)
                    {
                        writer.WriteLine($"{pupil.PupilId},{pupil.PupilName}");
                    }

                    writer.WriteLine("");
                }

                return writer.ToString();
            }
        }
    }
}
