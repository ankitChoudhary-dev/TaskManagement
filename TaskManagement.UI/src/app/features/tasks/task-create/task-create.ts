import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { forkJoin, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { TaskService } from '../services/task.service';
import { TaskStatus, TaskPriority } from '../models/task.model';

@Component({
  selector: 'app-task-create',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './task-create.html',
  styleUrl: './task-create.css',
})
export class TaskCreate implements OnInit {
  taskForm!: FormGroup;
  isSubmitting = false;
  isLoadingData = true;
  errorMessage = '';

  statusOptions = Object.values(TaskStatus);
  priorityOptions = Object.values(TaskPriority);

  projects: any[] = [];
  users: any[] = [];

  constructor(
    private fb: FormBuilder,
    private taskService: TaskService,
    private router: Router,
    private cd: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.loadDropdownData();
  }

  private initForm(): void {
    this.taskForm = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.maxLength(500)]],
      projectId: ['', Validators.required],
      assignedToUserId: [null],
      priority: [TaskPriority.Medium, Validators.required],
      status: [TaskStatus.Pending, Validators.required],
      dueDate: ['', Validators.required]
    });
  }

  private loadDropdownData(): void {
    this.isLoadingData = true;

    forkJoin({
      projects: this.taskService.getProjects().pipe(
        catchError((err) => {
          console.error('Projects API Error:', err);
          return of([]);
        })
      ),
      users: this.taskService.getUsers().pipe(
        catchError((err) => {
          console.error('Users API Error:', err);
          return of([]);
        })
      )
    }).subscribe({
      next: (res: { projects: any[]; users: any[] }) => {
        this.projects = res.projects;
        this.users = res.users;
        this.isLoadingData = false;
        this.cd.detectChanges(); // Ensures UI updates as soon as DB data arrives
      },
      error: (error) => {
        console.error('Error fetching dropdown data:', error);
        this.errorMessage = 'Failed to load projects or users from database.';
        this.isLoadingData = false;
        this.cd.detectChanges();
      }
    });
  }

  get f() {
    return this.taskForm.controls;
  }

  onSubmit(): void {
    if (this.taskForm.invalid) {
      this.taskForm.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = '';

    const payload = {
      ...this.taskForm.value,
      projectId: Number(this.taskForm.value.projectId),
      assignedToUserId: this.taskForm.value.assignedToUserId ? Number(this.taskForm.value.assignedToUserId) : null
    };

    this.taskService.createTask(payload).subscribe({
      next: () => {
        this.isSubmitting = false;
        this.router.navigate(['/tasks']);
      },
      error: (error) => {
        console.error('Error creating task:', error);
        if (error?.status === 403) {
          this.errorMessage = '<strong>Access Denied:</strong> You are not authorized to create tasks.';
        } else {
          this.errorMessage = 'Failed to create task. Please check the inputs and try again.';
        }
        this.isSubmitting = false;
        this.cd.detectChanges();
      }
    });
  }
}