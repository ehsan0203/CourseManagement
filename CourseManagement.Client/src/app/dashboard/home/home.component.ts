import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { Course, CourseService } from 'src/app/core/services/course.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, MatCardModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  private courseService = inject(CourseService);
  courses: Course[] = [];

  ngOnInit(): void {
    console.log('HomeComponent loaded');

    this.courseService.getCourses().subscribe(res => {
      this.courses = res;
    });
  }
}
