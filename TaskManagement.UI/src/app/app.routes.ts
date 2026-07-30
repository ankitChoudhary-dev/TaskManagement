import { Routes } from '@angular/router';
import { authGuard, unauthGuard } from './core/guards/auth-guard'; // Both imported from same file

import { Login } from './features/auth/login/login';
import { Register } from './features/auth/register/register';
import { Dashboard } from './features/dashboard/dashboard/dashboard';
import { ProjectList } from './features/projects/project-list/project-list';
import { ProjectCreate } from './features/projects/project-create/project-create';
import { ProjectEdit } from './features/projects/project-edit/project-edit';
import { TaskList } from './features/tasks/task-list/task-list';
import { MainLayout } from './layouts/main-layout/main-layout';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'login',
    component: Login,
    canActivate: [unauthGuard]
  },
  {
    path: 'register',
    component: Register,
    canActivate: [unauthGuard]
  },
  {
    path: '',
    component: MainLayout,
    canActivate: [authGuard],
    children: [
      { path: 'dashboard', component: Dashboard },
      { path: 'projects', component: ProjectList },
      { path: 'projects/create', component: ProjectCreate },
      { path: 'projects/edit/:id', component: ProjectEdit },
      { path: 'tasks', component: TaskList }
    ]
  },
  {
    path: '**',
    redirectTo: 'login'
  }
];