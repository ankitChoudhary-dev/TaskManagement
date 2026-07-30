import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Project } from '../models/project.model';
import { CreateProject } from '../models/create-project.model';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  private apiUrl = 'https://localhost:7088/api/projects';

  constructor(
    private http: HttpClient
  ) {}

  
  getAllProjects(): Observable<Project[]> {
    return this.http.get<Project[]>(this.apiUrl);
  }


  getProjectById(id: number): Observable<Project> {
    return this.http.get<Project>(`${this.apiUrl}/${id}`);
  }


  createProject(project: CreateProject): Observable<Project> {
    return this.http.post<Project>(
      this.apiUrl,
      project
    );
  }


  updateProject(id: number, project: CreateProject): Observable<Project> {
    return this.http.put<Project>(
      `${this.apiUrl}/${id}`,
      project
    );
  }


  deleteProject(id: number): Observable<boolean> {
    return this.http.delete<boolean>(
      `${this.apiUrl}/${id}`
    );
  }
}