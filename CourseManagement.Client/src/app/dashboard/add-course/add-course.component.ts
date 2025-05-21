import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CourseService, Course } from 'src/app/core/services/course.service';
import { Router } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-add-course',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './add-course.component.html',
  styleUrls: ['./add-course.component.scss']
})
export class AddCourseComponent {
  private courseService = inject(CourseService);
  private router = inject(Router);

  course: Partial<Course> = {
    title: '',
    instructor: '',
    duration: 0,
    description: ''
  };

  submit() {
    this.courseService.addCourse(this.course).subscribe(() => {
      alert('✅ دوره با موفقیت اضافه شد');
      this.router.navigate(['/dashboard']);
    });
  }
}
