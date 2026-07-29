import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginRequest } from '../../models/login-request';
import { environment } from '../../../environments/environment';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class Auth {

  private apiUrl = `${environment.apiUrl}/Auth`;

  constructor(
    private http: HttpClient,
    private router: Router
  ) {}

  login(request: LoginRequest) {
    return this.http.post(
      `${this.apiUrl}/login`,
      request
    );
  }

  register(request: LoginRequest) {
    return this.http.post(
      `${this.apiUrl}/register`,
      request
    );
  }


  logout(): void {

    localStorage.removeItem('token');

    this.router.navigate(['/login']);

  }

}