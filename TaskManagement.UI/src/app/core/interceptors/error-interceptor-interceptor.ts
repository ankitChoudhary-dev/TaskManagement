import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {

      // 1. API is offline/unreachable
      if (error.status === 0) {
        alert('Unable to connect with server. Please check if the API is running.');
      } 
      // 2. Database / Server crash
      else if (error.status >= 500) {
        alert('Server error occurred. Please try again later.');
      }

      // Re-throw error for component handling
      return throwError(() => error);
    })
  );
};