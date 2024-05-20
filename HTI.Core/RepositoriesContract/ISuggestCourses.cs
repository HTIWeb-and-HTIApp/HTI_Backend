using HTI.Core.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HTI.Core.RepositoriesContract
{
    public interface ISuggestCourses
    {
        Task SuggestCourseswhenFail(StudentCourseHistory courseHistory );
        Task SuggestCourseWhenYouDontRegisterItBefore(Course course);

        
    }
}
