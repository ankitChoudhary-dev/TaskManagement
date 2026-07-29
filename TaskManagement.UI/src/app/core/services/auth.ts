import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginRequest } from '../../models/login-request';
import { environment } from '../../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class Auth {


  private apiUrl = `${environment.apiUrl}/Auth`;


  constructor(
    private http: HttpClient
  ) {}


  login(request: LoginRequest) {

    return this.http.post(
      `${this.apiUrl}/login`,
      request
    );

  }

}