namespace HTI_Backend.DTOs
{
    public class ResultReturnDTO
    {
        public int StudyYear { get; set; }
        public int Semester { get; set; }

        public IEnumerable<coursesdTO> courses { get; set; }
    }
    public class coursesdTO
    {
        public string CourseCode { get; set; }
        public string Name { get; set; }
        public float WorkGrades { get; set; }
        public float FinalGrades { get; set; }
        public float MidtermGrades { get; set; }
        public float TotalGrades
        {
            get { return WorkGrades + FinalGrades + MidtermGrades; }
        }

    }
}

