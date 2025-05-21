import { Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { HomeComponent } from './dashboard/home/home.component';
import { AddCourseComponent } from './dashboard/add-course/add-course.component';
import { LayoutComponent } from './layout/layout.component';
import { ProfileComponent } from './dashboard/profile/profile.component';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: 'auth/login', component: LoginComponent },
  {
    path: 'dashboard',
    component: LayoutComponent,
    canActivate: [authGuard],
    children: [
      { path: '', component: HomeComponent },
      { path: 'add', component: AddCourseComponent },
      { path: 'profile', component: ProfileComponent }
    ]
  },
  { path: '', redirectTo: 'auth/login', pathMatch: 'full' },
  { path: '**', redirectTo: 'auth/login' }
];
