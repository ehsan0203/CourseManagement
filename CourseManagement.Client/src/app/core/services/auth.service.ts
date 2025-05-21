import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7037/api/auth';

  login(username: string, password: string): Observable<{ token: string; refreshToken: string }> {
    return this.http.post<{ token: string; refreshToken: string }>(`${this.apiUrl}/login`, {
      username,
      password
    }).pipe(
      tap(res => {
        localStorage.setItem('token', res.token);
        localStorage.setItem('refreshToken', res.refreshToken);
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken');
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  getRefreshToken(): string | null {
    return localStorage.getItem('refreshToken');
  }

  getUserInfo(): { username: string; role: string } | null {
    const token = this.getToken();
    if (!token) return null;
  
    const payload = token.split('.')[1];
    try {
      const decoded = JSON.parse(atob(payload));
      return {
        username: decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
        role: decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
      };
    } catch (e) {
      return null;
    }
  }
  
}
