namespace Gamification.Pages.Models
{
    public class Subjects
    {
        Faculties faculty = new Faculties();

        public int Id { get; set; }
        public string Name { get; set; }

        //public int FacultiesId = faculty.Id;
        //todo dit is niet goed maar ik ga er ff geen tijd aan besteden

    }
}
