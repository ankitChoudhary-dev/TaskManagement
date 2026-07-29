import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class Dashboard {

  private apiUrl = `${environment.apiUrl}/Dashboard`;

  constructor(
    private http: HttpClient
  ) {}

  getDashboard() {
    return this.http.get(this.apiUrl);
  }

}