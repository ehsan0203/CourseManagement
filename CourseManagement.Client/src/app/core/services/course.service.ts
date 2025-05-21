import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Course {
  id: number;
  title: string;
  instructor: string;
  duration: number;
  description: string;
}

@Injectable({
  providedIn: 'root'
})
export class CourseService {
  private apiUrl = 'https://localhost:7037/api/course';

  constructor(private http: HttpClient) {}

  getCourses(): Observable<Course[]> {
    return this.http.get<Course[]>(this.apiUrl);
  }
  addCourse(course: Partial<Course>) {
    return this.http.post(this.apiUrl, course);
  }
  
}
