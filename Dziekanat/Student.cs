

using System.ComponentModel.DataAnnotations;

namespace Dziekanat
{
    public class Student
    {
        public int StudentId { get; set; }
        
        public string Pesel { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string DateOfBirth { get; set; }
        public string Town { get; set; }
        public string FieldOfStudy { get; set; }
        public Grade Grade { get; set; }


    }
}
