import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from 'src/app/core/services/auth.service';
import { MatCardModule } from '@angular/material/card';

@Component({
  standalone: true,
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
  imports: [CommonModule, MatCardModule]
})
export class ProfileComponent {
  private auth = inject(AuthService);
  user = this.auth.getUserInfo();
}
