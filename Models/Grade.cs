using System;
using System.Collections.Generic;

namespace ChasData.Models;

public partial class Grade
{
    public int GradeId { get; set; }

    public int? GradeValue { get; set; }

    public DateOnly? DateAssigned { get; set; }

    public int? StudentId { get; set; }

    public int? CourseId { get; set; }

    public int? StaffId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Staff? Staff { get; set; }

    public virtual Student? Student { get; set; }
}
