

namespace Dziekanat
{
    public class Grade
    {
        public int GradeId { get; set; }
        public int YourGrade { get; set; }
        public string VerbalGrade { get; set; }
        public string RelaseDate { get; set; }
        public Student Student { get; set; }
        public int StudentId { get; set; }

            
        
    }
}
