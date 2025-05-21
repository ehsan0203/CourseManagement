import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { AuthService } from '../core/services/auth.service';
import { MatMenuModule } from '@angular/material/menu';
import { MatBadgeModule } from '@angular/material/badge';


@Component({
  selector: 'app-layout',
  standalone: true,
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss'],
  imports: [
    CommonModule,
    RouterOutlet,
    RouterModule,
    MatSidenavModule,
    MatListModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatMenuModule, // ✅ خیلی مهم
    MatBadgeModule
  ]
})
export class LayoutComponent {
  private auth = inject(AuthService);
  userInfo = this.auth.getUserInfo();
  user = {
    username: 'admin',
    avatar: 'https://i.pravatar.cc/300?u=admin'
  };
  logout() {
    localStorage.clear();
    location.href = '/auth/login';
  }
  isDark = false;

toggleTheme() {
  this.isDark = !this.isDark;
  const body = document.body;
  this.isDark ? body.classList.add('dark-theme') : body.classList.remove('dark-theme');
}
isHandset = window.innerWidth < 768;

constructor() {
  window.addEventListener('resize', () => {
    this.isHandset = window.innerWidth < 768;
  });
}
notifications = [
  { message: 'دوره جدید اضافه شد', read: false },
  { message: 'درخواست پشتیبانی دارید', read: false }
];

get unreadCount(): number {
  return this.notifications.filter(n => !n.read).length;
}
get avatar(): string {
  return `https://i.pravatar.cc/150?u=${this.userInfo?.username}`;
}
}
