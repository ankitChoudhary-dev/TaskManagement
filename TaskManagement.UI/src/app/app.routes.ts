import { Routes } from '@angular/router';
import { authGuard, unauthGuard } from './core/guards/auth-guard';

import { Login } from './features/auth/login/login';
import { Register } from './features/auth/register/register';
import { Dashboard } from './features/dashboard/dashboard/dashboard';
import { ProjectList } from './features/projects/project-list/project-list';
import { ProjectCreate } from './features/projects/project-create/project-create';
import { ProjectEdit } from './features/projects/project-edit/project-edit';

// Task Imports
import { TaskList } from './features/tasks/task-list/task-list';
import { TaskCreate } from './features/tasks/task-create/task-create';
import { TaskEdit } from './features/tasks/task-edit/task-edit';

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
      
      // Project routes
      { path: 'projects', component: ProjectList },
      { path: 'projects/create', component: ProjectCreate },
      { path: 'projects/edit/:id', component: ProjectEdit },
      
      // Task routes
      { path: 'tasks', component: TaskList },
      { path: 'tasks/create', component: TaskCreate },
      { path: 'tasks/edit/:id', component: TaskEdit }
    ]
  },
  {
    path: '**',
    redirectTo: 'login'
  }
];