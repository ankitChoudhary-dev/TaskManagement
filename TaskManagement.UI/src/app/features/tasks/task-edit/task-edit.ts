import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule, Location } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs';

import { TaskService } from '../services/task.service';
import {
  Task,
  UpdateTask,
  TaskPriority,
  TaskStatus
} from '../models/task.model';

@Component({
  selector: 'app-task-edit',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './task-edit.html',
  styleUrls: ['./task-edit.css']
})
export class TaskEdit implements OnInit {

  taskForm!: FormGroup;
  taskId!: number;

  isLoading = true;
  isSubmitting = false;
  errorMessage = '';

  projects: any[] = [];
  users: any[] = [];

  statusOptions = Object.values(TaskStatus);
  priorityOptions = Object.values(TaskPriority);

  constructor(
    private fb: FormBuilder,
    private taskService: TaskService,
    private route: ActivatedRoute,
    private router: Router,
    private location: Location,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.initForm();

    const id = this.route.snapshot.paramMap.get('id');

    if (!id) {
      this.errorMessage = 'Invalid Task Id.';
      this.isLoading = false;
      return;
    }

    this.taskId = Number(id);
    this.loadData();
  }

  private initForm(): void {
    this.taskForm = this.fb.group({
      id: [0],
      title: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', Validators.maxLength(500)],
      projectId: [null, Validators.required],
      assignedToUserId: [null],
      priority: [TaskPriority.Medium, Validators.required],
      status: [TaskStatus.Pending, Validators.required],
      dueDate: ['', Validators.required]
    });
  }

  loadData(): void {
    this.isLoading = true;
    this.errorMessage = '';

    forkJoin({
      task: this.taskService.getTaskById(this.taskId),
      projects: this.taskService.getProjects(),
      users: this.taskService.getUsers()
    }).subscribe({
      next: (result) => {
        this.projects = result.projects;
        this.users = result.users;

        const task = result.task;

        this.taskForm.patchValue({
          id: task.id,
          title: task.title,
          description: task.description ?? '',
          projectId: task.projectId,
          assignedToUserId: task.assignedTo,
          priority: task.priority,
          status: task.status,
          dueDate: task.dueDate
            ? String(task.dueDate).substring(0, 10)
            : ''
        });

        this.isLoading = false;
        this.cdr.detectChanges();
      },
      error: (err: HttpErrorResponse) => {
        console.error(err);

        if (err.status === 403) {
          this.errorMessage = '<strong>Access Denied:</strong> You are not authorized to view this task.';
        } else {
          this.errorMessage = 'Failed to load task details.';
        }

        this.isLoading = false;
        this.cdr.detectChanges();
      }
    });
  }

  get f() {
    return this.taskForm.controls;
  }

  goBack(): void {
    this.location.back();
  }

  onSubmit(): void {
    if (this.taskForm.invalid) {
      this.taskForm.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = '';

    const payload: UpdateTask = {
      id: this.taskId,
      title: this.taskForm.value.title,
      description: this.taskForm.value.description,
      projectId: Number(this.taskForm.value.projectId),
      assignedTo: this.taskForm.value.assignedToUserId
        ? Number(this.taskForm.value.assignedToUserId)
        : 0,
      priority: this.taskForm.value.priority,
      status: this.taskForm.value.status,
      dueDate: this.taskForm.value.dueDate
    };

    this.taskService.updateTask(this.taskId, payload).subscribe({
      next: () => {
        this.router.navigate(['/tasks']);
      },
      error: (err: HttpErrorResponse) => {
        console.error(err);

        if (err.status === 403) {
          this.errorMessage = '<strong>Access Denied:</strong> You are not authorized to update the task.';
        } else {
          this.errorMessage = 'Failed to update task.';
        }

        this.isSubmitting = false;
        this.cdr.detectChanges();
      }
    });
  }

}