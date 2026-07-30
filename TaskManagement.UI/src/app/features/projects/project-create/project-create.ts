import { CommonModule } from '@angular/common';
import { Component, ChangeDetectorRef } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
  AbstractControl,
  ValidationErrors,
  ValidatorFn
} from '@angular/forms';
import { Router } from '@angular/router';

import { ProjectService } from '../services/project';
import { CreateProject } from '../models/create-project.model';

@Component({
  selector: 'app-project-create',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './project-create.html',
  styleUrl: './project-create.css'
})
export class ProjectCreate {

  projectForm: FormGroup;

  isSubmitted = false;
  isUnauthorized = false; // Flag to check 403 state
  errorMessage = '';
  successMessage = '';

  constructor(
    private fb: FormBuilder,
    private projectService: ProjectService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {

    this.projectForm = this.fb.group({
      name: [
        '',
        Validators.required
      ],
      description: [
        ''
      ],
      startDate: [
        null
      ],
      endDate: [
        { value: null, disabled: true }
      ],
      status: [
        'Active',
        Validators.required
      ]
    },
    {
      validators: this.dateRangeValidator()
    });

  }

  dateRangeValidator(): ValidatorFn {
    return (form: AbstractControl): ValidationErrors | null => {
      const startDate = form.get('startDate')?.value;
      const endDate = form.get('endDate')?.value;

      if (startDate && endDate) {
        if (new Date(endDate) < new Date(startDate)) {
          return { dateRangeInvalid: true };
        }
      }
      return null;
    };
  }

  onStartDateChange(): void {
    const startDate = this.projectForm.get('startDate')?.value;
    const endDateControl = this.projectForm.get('endDate');

    if (startDate) {
      endDateControl?.enable();

      const endDate = endDateControl?.value;
      if (endDate && new Date(endDate) < new Date(startDate)) {
        endDateControl.patchValue(null);
      }
    } else {
      endDateControl?.reset();
      endDateControl?.disable();
    }
  }

  submit(): void {
    this.isSubmitted = true;
    this.errorMessage = '';
    this.successMessage = '';
    this.isUnauthorized = false;

    if (this.projectForm.invalid) {
      this.projectForm.markAllAsTouched();
      return;
    }

    const project: CreateProject = this.projectForm.getRawValue();

    this.projectService.createProject(project)
      .subscribe({
        next: () => {
          this.successMessage = 'Project saved successfully!';
          this.cdr.markForCheck();

          setTimeout(() => {
            this.router.navigate(['/projects']);
          }, 1000);
        },

        error: (error) => {
          console.error('Create Project Error:', error);

          if (error.status === 403) {
            this.isUnauthorized = true;
            this.errorMessage = 'You are not authorized to create a project.';
          }
          else if (error.status === 400) {
            this.errorMessage =
              error?.error?.message ||
              'Please check the entered information.';
          }
          else {
            this.errorMessage = 'An error occurred while creating the project.';
          }

          this.cdr.markForCheck();
        }
      });
  }

  cancel(): void {
    this.router.navigate(['/projects']);
  }
}