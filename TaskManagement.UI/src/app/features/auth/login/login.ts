import { Component, ChangeDetectorRef } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { Auth } from '../../../core/services/auth';
import { LoginRequest } from '../../../models/auth.models';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  isSubmitted = false;
  errorMessage = '';
  successMessage = '';
  loginForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: Auth,
    private cdr: ChangeDetectorRef,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  onSubmit(): void {
    this.isSubmitted = true;
    this.errorMessage = '';
    this.successMessage = '';

    if (this.loginForm.invalid) {
      return;
    }

    const request: LoginRequest = {
      email: this.loginForm.value.email ?? '',
      password: this.loginForm.value.password ?? ''
    };

    this.authService.login(request).subscribe({
      next: (response: any) => {
        this.errorMessage = '';
        this.successMessage = 'Login successful!';

        if (response?.name) {
          localStorage.setItem('userName', response.name);
        }

        if (response?.token) {
          localStorage.setItem('token', response.token);
        }

        this.cdr.markForCheck();

        setTimeout(() => {
          this.router.navigate(['/dashboard']);
        }, 1000);
      },
      error: (error) => {
        this.successMessage = '';

        // If it's a connection error (0) or server crash (500+), the alert handles it.
        // We only show the error div for actual auth/login failures (e.g. 400, 401, 404).
        if (error?.status === 0 || error?.status >= 500) {
          this.errorMessage = ''; 
          this.cdr.markForCheck();
          return;
        }

        const apiError = error?.error;

        if (typeof apiError === 'string' && apiError.trim()) {
          this.errorMessage = apiError;
        } else if (apiError?.message || apiError?.Message) {
          this.errorMessage = apiError.message || apiError.Message;
        } else {
          this.errorMessage = 'Invalid email or password.';
        }

        this.cdr.markForCheck();
      }
    });
  }

  navigateToSignUp(): void {
    this.router.navigate(['/register']);
  }
}