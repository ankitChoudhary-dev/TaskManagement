import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';

// Prevents unauthenticated users from accessing protected pages
export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const token = localStorage.getItem('token');

  if (token) {
    return true;
  }

  router.navigate(['/login']);
  return false;
};

// Prevents logged-in users from accessing /login or /register
export const unauthGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const token = localStorage.getItem('token');

  if (token) {
    router.navigate(['/dashboard']);
    return false;
  }

  return true;
};