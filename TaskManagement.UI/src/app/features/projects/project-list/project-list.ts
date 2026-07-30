import { CommonModule } from '@angular/common';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { RouterLink } from '@angular/router';

import { ProjectService } from '../services/project';
import { Project } from '../models/project.model';

@Component({
  selector: 'app-project-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink
  ],
  templateUrl: './project-list.html',
  styleUrl: './project-list.css'
})
export class ProjectList implements OnInit {

  projects: Project[] = [];
  errorMessage: string = '';
  
  // Sorting variables
  sortColumn: keyof Project = 'id';
  sortDirection: 'asc' | 'desc' = 'asc';

  constructor(
    private projectService: ProjectService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadProjects();
  }

  loadProjects(): void {
    this.projectService.getAllProjects().subscribe({
      next: (response) => {
        this.projects = response;
        this.sortProjects(); // Apply default sort
        this.cdr.markForCheck();
      },
      error: (error) => {
        console.error(error);
        this.cdr.markForCheck();
      }
    });
  }

  deleteProject(id: number): void {
    this.errorMessage = ''; // Reset error state on each click

    const confirmDelete = confirm(
      'Are you sure you want to delete this project?'
    );

    if (!confirmDelete) {
      return;
    }

    this.projectService.deleteProject(id)
      .subscribe({
        next: () => {
          this.errorMessage = '';
          this.loadProjects();
        },
        error: (error) => {
          console.error('Delete Project Error:', error);

          if (error.status === 403) {
            this.errorMessage = 'You are not authorized to delete a project.';
          } else if (error.status === 400) {
            this.errorMessage = error?.error?.message || 'Unable to delete project.';
          } else {
            this.errorMessage = '';
          }

          this.cdr.markForCheck();
        }
      });
  }

  onSort(column: keyof Project): void {
    if (this.sortColumn === column) {
      // Toggle direction if clicking the same column
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
      // Set new column and default to ascending
      this.sortColumn = column;
      this.sortDirection = 'asc';
    }

    this.sortProjects();
    this.cdr.markForCheck();
  }

  getStatusClass(status: string): string {
    switch (status?.toLowerCase()) {
      case 'completed':
        return 'bg-success';
      case 'in progress':
      case 'active':
        return 'bg-primary';
      case 'pending':
      case 'on hold':
        return 'bg-warning text-dark';
      case 'cancelled':
        return 'bg-danger';
      default:
        return 'bg-secondary';
    }
  }

  private sortProjects(): void {
    this.projects.sort((a, b) => {
      const valA = a[this.sortColumn] ?? '';
      const valB = b[this.sortColumn] ?? '';

      let comparison = 0;

      if (typeof valA === 'string' && typeof valB === 'string') {
        comparison = valA.localeCompare(valB);
      } else {
        comparison = valA < valB ? -1 : valA > valB ? 1 : 0;
      }

      return this.sortDirection === 'asc' ? comparison : -comparison;
    });
  }
}