import { Routes } from '@angular/router';
import { Login } from './pages/auth/login/login';
import { MainLayout } from './layout/main-layout/main-layout';
import { Dashboard } from './pages/dashboard/dashboard/dashboard';
import { NotFound } from './pages/not-found/not-found';
import { Register } from './pages/auth/register/register';
import { authGuard } from './core/guards/auth.guard';
import { Users } from './pages/users/users';
import { Settings } from './pages/settings/settings';
import { Help } from './pages/help/help';

export const routes: Routes = [
  // 🔓 Public routes
  { path: 'login', component: Login },
  { path: 'register', component: Register },

  // 🔐 Protected area (Main layout + child routes)
  {
    path: '',
    component: MainLayout,
    canActivate: [authGuard],
    children: [
      { path: 'dashboard', component: Dashboard },
      { path: 'users', component: Users },
      { path: 'settings', component: Settings },
      { path: 'help', component: Help },

      // when user hits just `/` → go to `/dashboard`
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
    ],
  },

  // 🌟 Wildcard – must be last
  { path: '**', component: NotFound },
];
