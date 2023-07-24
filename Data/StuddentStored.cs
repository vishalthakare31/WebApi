using WebApi.dto;

namespace WebApi.Data
{
    public static class StuddentStored
    {    
        public static List<StudentDto> StudentList = new List<StudentDto>
            {
                new StudentDto{ Id=1 , Name="vishal" , City="Nashik"},
                new StudentDto{Id=2 , Name="amruta", City= "Malegaon"}
            };
    }
}
