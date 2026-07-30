import { CommonModule } from '@angular/common';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { RouterLink } from '@angular/router';

import { TaskService } from '../services/task.service';
import { Task } from '../models/task.model';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './task-list.html',
  styleUrl: './task-list.css'
})
export class TaskList implements OnInit {

  tasks: Task[] = [];
  errorMessage: string = '';

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
    this.taskService.getAllTasks().subscribe({
      next: (response: Task[]) => {
        this.tasks = response;
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
          this.errorMessage = 'You are not authorized to delete a task.';
        } else {
          this.errorMessage = 'Unable to delete task.';
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