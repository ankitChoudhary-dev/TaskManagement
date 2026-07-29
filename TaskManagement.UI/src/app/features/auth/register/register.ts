import { Component, ChangeDetectorRef } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { Auth } from '../../../core/services/auth';

function passwordMatchValidator(control: AbstractControl): ValidationErrors | null {
  const password = control.get('password')?.value;
  const reenterPassword = control.get('reenterPassword')?.value;

  if (password && reenterPassword && password !== reenterPassword) {
    return { passwordMismatch: true };
  }
  return null;
}

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {

  isSubmitted = false;
  errorMessage = '';
  successMessage = '';
  registerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: Auth,
    private cdr: ChangeDetectorRef,
    private router: Router
  ) {
    this.registerForm = this.fb.group({
      name: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      reenterPassword: ['', [Validators.required]]
    }, { validators: passwordMatchValidator });
  }

  onSubmit(): void {
    this.isSubmitted = true;
    this.errorMessage = '';
    this.successMessage = '';

    if (this.registerForm.invalid) {
      return;
    }

    const request = {
      name: this.registerForm.value.name ?? '',
      email: this.registerForm.value.email ?? '',
      password: this.registerForm.value.password ?? ''
    };

    this.authService.register(request).subscribe({
      next: (response) => {
        this.successMessage = 'Registration successful! Redirecting to login...';
        this.cdr.markForCheck();
        setTimeout(() => {
          this.router.navigate(['/login']);
        }, 1500);
      },
      error: (error) => {
        const apiError = error?.error;
        this.errorMessage = apiError?.message || apiError?.Message || 'Registration failed. Please try again.';
        this.cdr.markForCheck();
      }
    });
  }
}