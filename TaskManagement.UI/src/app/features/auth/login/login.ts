import { Component, ChangeDetectorRef } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Auth } from '../../../core/services/auth';
import { LoginRequest } from '../../../models/login-request';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {

  isSubmitted = false;
  errorMessage = '';
  successMessage = '';
  showSignUpButton = false;
  loginForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: Auth,
    private cdr: ChangeDetectorRef,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      email: [
        '',
        [
          Validators.required,
          Validators.email
        ]
      ],
      password: [
        '',
        [
          Validators.required
        ]
      ]
    });
  }

  onSubmit(): void {

    this.isSubmitted = true;
    this.errorMessage = '';
    this.successMessage = '';
    this.showSignUpButton = false;

    if (this.loginForm.invalid) {
      return;
    }

    const request: LoginRequest = {
      email: this.loginForm.value.email ?? '',
      password: this.loginForm.value.password ?? ''
    };

    this.authService.login(request).subscribe({

      next: (response: any) => {

        console.log("LOGIN SUCCESS", response);

        this.errorMessage = '';
        this.successMessage = 'Login successful!';
        this.showSignUpButton = false;

        // Save JWT Token
        localStorage.setItem('token', response.token);

        this.cdr.markForCheck();

        // Redirect to dashboard after a brief delay
        setTimeout(() => {
          this.router.navigate(['/dashboard']);
        }, 1000);

      },

      error: (error) => {

        console.log("LOGIN ERROR", error);

        this.successMessage = '';

        const apiError = error?.error;

        if (typeof apiError === 'string' && apiError.trim()) {
          this.errorMessage = apiError;
        }
        else if (apiError?.message) {
          this.errorMessage = apiError.message;
        }
        else if (apiError?.Message) {
          this.errorMessage = apiError.Message;
        }
        else if (apiError?.title) {
          this.errorMessage = apiError.title;
        }
        else {
          this.errorMessage = "Account not found. Please sign up to continue.";
        }

        if (
          this.errorMessage.toLowerCase().includes("sign up") ||
          this.errorMessage.toLowerCase().includes("not found")
        ) {
          this.showSignUpButton = true;
        }

        this.cdr.markForCheck();

      }

    });

  }

  navigateToSignUp(): void {
    this.router.navigate(['/register']);
  }

}