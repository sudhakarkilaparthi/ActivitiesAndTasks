import { Routes } from '@angular/router';
import { Login } from './pages/auth/login/login';
import { MainLayout } from './layout/main-layout/main-layout';
import { Component } from '@angular/core';
import { Dashboard } from './pages/dashboard/dashboard/dashboard';
import { NotFound } from './pages/not-found/not-found';
import { Register } from './pages/auth/register/register';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  },
  {
    path: 'login',
    component: Login,
  },
  {
    path: 'register',
    component: Register,
  },
  {
    path: '',
    canActivate: [authGuard],
    component: MainLayout,
    children: [
      {
        path: 'dashboard',
        component: Dashboard,
      },
      // { path: '**', component: NotFound },
    ],
  },
  { path: '**', component: NotFound },
];
