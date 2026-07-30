import { CommonModule } from '@angular/common';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core'; 
import { 
  FormBuilder, 
  FormGroup, 
  ReactiveFormsModule, 
  Validators,
  AbstractControl,
  ValidationErrors,
  ValidatorFn
} from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

import { ProjectService } from '../services/project';

@Component({
  selector: 'app-project-edit',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './project-edit.html',
  styleUrl: './project-edit.css'
})
export class ProjectEdit implements OnInit {

  projectForm: FormGroup;
  projectId!: number;
  isSubmitted = false;
  errorMessage = '';
  successMessage = '';

  constructor(
    private fb: FormBuilder,
    private projectService: ProjectService,
    private router: Router,
    private route: ActivatedRoute,
    private cdr: ChangeDetectorRef
  ) {
    this.projectForm = this.fb.group({
      id: [{ value: '', disabled: true }],
      name: ['', Validators.required],
      description: [''],
      startDate: [null],
      endDate: [{ value: null, disabled: true }],
      status: ['Active', Validators.required]
    },
    {
      validators: this.dateRangeValidator()
    });
  }

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.projectId = +idParam;
      this.loadProjectDetails(this.projectId);
    }
  }

  loadProjectDetails(id: number): void {
    this.projectService.getProjectById(id).subscribe({
      next: (project) => {
        const formattedStartDate = project.startDate 
          ? new Date(project.startDate).toISOString().split('T')[0] 
          : null;
        const formattedEndDate = project.endDate 
          ? new Date(project.endDate).toISOString().split('T')[0] 
          : null;

        this.projectForm.patchValue({
          id: project.id,
          name: project.name,
          description: project.description,
          status: project.status,
          startDate: formattedStartDate,
          endDate: formattedEndDate
        });

        if (formattedStartDate) {
          this.projectForm.get('endDate')?.enable();
        } else {
          this.projectForm.get('endDate')?.disable();
        }

        this.cdr.markForCheck();
      },
      error: (error) => {
        console.error(error);
        
        if (error?.status === 403) {
          this.errorMessage = 'You are not authorized to update this project.';
        } else {
          this.errorMessage = error?.error?.message || 'Failed to load project details.';
        }

        this.cdr.markForCheck();
      }
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
        endDateControl?.patchValue(null);
      }
    } else {
      endDateControl?.patchValue(null);
      endDateControl?.disable();
    }
  }

  submit(): void {
    this.isSubmitted = true;
    this.errorMessage = '';
    this.successMessage = '';

    if (this.projectForm.invalid) {
      this.projectForm.markAllAsTouched();
      return;
    }

    const updateData = this.projectForm.getRawValue();

    this.projectService.updateProject(this.projectId, updateData)
      .subscribe({
        next: () => {
          this.successMessage = 'Project updated successfully!';
          this.cdr.markForCheck();
          setTimeout(() => {
            this.router.navigate(['/projects']);
          }, 1000);
        },
        error: (error) => {
          console.error(error);

          if (error?.status === 403) {
            this.errorMessage = 'You are not authorized to update this project.';
          } else {
            this.errorMessage = error?.error?.message || 'Failed to update project.';
          }

          this.cdr.markForCheck();
        }
      });
  }

  closeErrorMessage(): void {
    this.errorMessage = '';
  }

  cancel(): void {
    this.router.navigate(['/projects']);
  }
}