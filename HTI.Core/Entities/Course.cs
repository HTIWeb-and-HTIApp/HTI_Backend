﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTI.Core.Entities
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public string Description { get; set; }
        public string Links { get; set; }
        public int StudyYear { get; set; }
        public int Semester { get; set; }
        public bool Lap { get; set; }
        public string Type { get; set; }
        public int? PrerequisiteId { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public Course Prerequisite { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<StudentCourseHistory> StudentCourseHistories { get; set; }
    }

}
