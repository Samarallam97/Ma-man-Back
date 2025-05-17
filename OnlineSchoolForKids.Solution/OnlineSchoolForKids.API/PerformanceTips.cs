// Projections for Performance: To avoid over-fetching, use projections
//var studentCourses = context.Students
//	.Select(s => new
//	{
//		s.Name,
//		Courses = s.Courses.Select(c => c.Title)
//	})
//	.ToList();

////////////////////////////////////////////////////////////////
/// Performance Considerations
//Indexes: Ensure foreign keys in the join table (StudentId, CourseId) are indexed. EF Core automatically creates indexes for foreign keys, but verify in the database.
//Lazy Loading: Avoid lazy loading in performance-critical scenarios. Use eager loading (.Include) or explicit loading instead.
// Change Tracking: Disable change tracking for read-only queries:
// var students = context.Students.AsNoTracking().ToList();