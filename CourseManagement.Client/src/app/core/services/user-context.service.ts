import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';


export interface JwtPayload {
  name: string;
  role: string;
  exp: number;
  // بقیه claimهایی که تو توکن هست
}

@Injectable({
  providedIn: 'root'
})
export class UserContextService {
  get user(): JwtPayload | null {
    const token = localStorage.getItem('token');
    if (!token) return null;

    try {
      return jwtDecode<JwtPayload>(token);
    } catch {
      return null;
    }
  }
}
