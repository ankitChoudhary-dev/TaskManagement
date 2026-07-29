import { Routes } from '@angular/router';

import { Login } from './features/auth/login/login';
import { Dashboard } from './features/dashboard/dashboard/dashboard';
import { ProjectList } from './features/projects/project-list/project-list';
import { TaskList } from './features/tasks/task-list/task-list';
import { MainLayout } from './layouts/main-layout/main-layout';

export const routes: Routes = [
  {
    path: 'login',
    component: Login
  },
  {
    path: '',
    component: MainLayout,
    children: [
      {
        path: 'dashboard',
        component: Dashboard
      },
      {
        path: 'projects',
        component: ProjectList
      },
      {
        path: 'tasks',
        component: TaskList
      },
      {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full'
      }
    ]
  },
  {
    path: '**',
    redirectTo: 'login'
  }
];