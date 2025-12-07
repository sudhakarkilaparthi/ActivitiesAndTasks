import { Component, inject, signal } from '@angular/core';
import { Router, RouterOutlet, RouterLink } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-main-layout',
  imports: [RouterOutlet, RouterLink, CommonModule],
  templateUrl: './main-layout.html',
  styleUrl: './main-layout.css',
})
export class MainLayout {
  private authService = inject(AuthService);
  private router = inject(Router);
  sidebarOpen = signal<boolean>(true);

  toggleSidebar(): void {
    this.sidebarOpen.update((open) => !open);
  }

  Logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
    console.log('User logged out');
  }
}
