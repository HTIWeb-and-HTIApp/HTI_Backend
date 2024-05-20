using HTI.Core.Entities;
using HTI.Core.RepositoriesContract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HTI.Repository.Data
{
    public class SuggestCourses : ISuggestCourses
    {

        private readonly StoreContext _dbContext;
        private readonly StudentCourseHistory _studentCourseHistory;
        private readonly Course _course;

        public SuggestCourses(StoreContext dbContext, StudentCourseHistory studentCourseHistory, Course course)
        {

            _dbContext = dbContext;
            _studentCourseHistory = studentCourseHistory;
            _course = course;
        }

       

        public async Task SuggestCourseswhenFail(StudentCourseHistory courseHistory)
        {
            throw new NotImplementedException();

        }

        public Task SuggestCourseWhenYouDontRegisterItBefore(Course course)
        {
            throw new NotImplementedException();
        }
    }
}
