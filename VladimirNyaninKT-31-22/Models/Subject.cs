namespace VladimirNyaninKT_31_22.Models
{
    public class Subject
    {
        public int SubjectId;

        public string Name;
        public bool IsDeleted { get; set; }


        public int TeacherId { get; set; }
        public int WorkloadId { get; set; }


        public Teacher Teacher { get; set; }
        public Workload Workload { get; set; }

    }
}
