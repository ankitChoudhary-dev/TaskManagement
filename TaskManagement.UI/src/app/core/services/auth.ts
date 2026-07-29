import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment.development';
import { ApiEndpoints } from '../constants/api-endpoints';
import { LoginRequest } from '../../models/login-request';
import { LoginResponse } from '../../models/login-response';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private http = inject(HttpClient);

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(
      `${environment.apiUrl}${ApiEndpoints.login}`,
      request
    );
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
  }

  saveToken(token: string): void {
    localStorage.setItem('token', token);
  }

  saveRole(role: string): void {
    localStorage.setItem('role', role);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isLoggedIn(): boolean {
    return this.getToken() !== null;
  }
}