import { CommonModule } from '@angular/common';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { TaskService } from '../services/task.service';
import { Task } from '../models/task.model';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './task-list.html',
  styleUrl: './task-list.css'
})
export class TaskList implements OnInit {

  tasks: Task[] = [];
  errorMessage: string = '';

  searchTerm: string = '';
  selectedStatus: string = '';
  selectedPriority: string = '';

  statusOptions: string[] = ['Pending', 'InProgress', 'Completed'];
  priorityOptions: string[] = ['Low', 'Medium', 'High'];

  sortColumn: keyof Task = 'id';
  sortDirection: 'asc' | 'desc' = 'asc';

  constructor(
    private taskService: TaskService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks(): void {
    this.taskService.getAllTasks(this.searchTerm, this.selectedStatus, this.selectedPriority).subscribe({
      next: (response: Task[]) => {
        let filteredTasks = response;

        if (this.searchTerm.trim()) {
          const search = this.searchTerm.toLowerCase().trim();
          filteredTasks = filteredTasks.filter(task =>
            task.title?.toLowerCase().includes(search) ||
            task.description?.toLowerCase().includes(search)
          );
        }

        if (this.selectedStatus) {
          filteredTasks = filteredTasks.filter(task =>
            task.status?.toLowerCase().replace(/\s+/g, '') === this.selectedStatus.toLowerCase().replace(/\s+/g, '')
          );
        }

        if (this.selectedPriority) {
          filteredTasks = filteredTasks.filter(task =>
            task.priority?.toLowerCase() === this.selectedPriority.toLowerCase()
          );
        }

        this.tasks = filteredTasks;
        this.sortTasks();
        this.cdr.markForCheck();
      },
      error: (error: any) => {
        console.error('Load Tasks Error:', error);
        this.errorMessage = 'Failed to load tasks.';
        this.cdr.markForCheck();
      }
    });
  }

  onFilterChange(): void {
    this.loadTasks();
  }

  clearFilters(): void {
    this.searchTerm = '';
    this.selectedStatus = '';
    this.selectedPriority = '';
    this.loadTasks();
  }

  deleteTask(id: number): void {
    this.errorMessage = '';

    if (!confirm('Are you sure you want to delete this task?')) {
      return;
    }

    this.taskService.deleteTask(id).subscribe({
      next: () => {
        this.loadTasks();
      },
      error: (error: any) => {
        console.error('Delete Task Error:', error);
        if (error?.status === 403) {
          this.errorMessage = '<strong>Access Denied:</strong> You are not authorized to delete this task.';
        } else {
          this.errorMessage = '<strong>Error:</strong> Unable to delete task.';
        }
        this.cdr.markForCheck();
      }
    });
  }

  onSort(column: keyof Task): void {
    if (this.sortColumn === column) {
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortColumn = column;
      this.sortDirection = 'asc';
    }
    this.sortTasks();
    this.cdr.markForCheck();
  }

  private sortTasks(): void {
    this.tasks.sort((a, b) => {
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

  getStatusClass(status: string): string {
    switch (status?.toLowerCase()) {
      case 'completed': return 'bg-success';
      case 'inprogress':
      case 'in progress': return 'bg-primary';
      case 'pending': return 'bg-warning text-dark';
      default: return 'bg-secondary';
    }
  }

  getPriorityClass(priority?: string): string {
    switch (priority?.toLowerCase()) {
      case 'high': return 'badge bg-danger';
      case 'medium': return 'badge bg-warning text-dark';
      case 'low': return 'badge bg-info text-dark';
      default: return 'badge bg-secondary';
    }
  }
}