import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [    CommonModule,
    FormsModule,
    RouterModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  username = '';
  password = '';

  constructor(private authService: AuthService, private router: Router) {}

  login() {
    this.authService.login(this.username, this.password).subscribe({
      next: () => {
        this.router.navigate(['/dashboard']);
      },
      error: () => {
        alert('نام کاربری یا رمز عبور اشتباه است');
      }
    });
  }
  

  
  }