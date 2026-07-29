import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth';
import { LoginRequest } from '../../../models/login-request';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {

  isSubmitted = false;

  loginForm;

  constructor(
  private fb: FormBuilder,
  private authService: AuthService,
  private router: Router
) {

  this.loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required]
  });

}

  onSubmit(): void {

  this.isSubmitted = true;

  if (this.loginForm.invalid) {
    return;
  }

  const request = this.loginForm.value as LoginRequest;

  this.authService.login(request).subscribe({

    next: (response) => {

      this.authService.saveToken(response.token);
      this.authService.saveRole(response.role);

      this.router.navigate(['/dashboard']);

    },

    error: (error) => {

      console.error(error);
      alert('Invalid credentials');

    }

  });

}

}