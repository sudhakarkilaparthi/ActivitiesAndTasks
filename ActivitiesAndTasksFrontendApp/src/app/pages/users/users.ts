import { Component, inject, OnInit, signal } from '@angular/core';
import { UserService } from '../../core/services/user.service';
import { User } from '../../core/models/user.model';
import { UpperCasePipe } from '@angular/common';
import { NotificationService } from '../../core/services/notifications';

@Component({
  selector: 'app-users',
  imports: [UpperCasePipe],
  templateUrl: './users.html',
  styleUrl: './users.css',
})
export class Users implements OnInit {
  private userService = inject(UserService);
  private notify = inject(NotificationService);

  users = signal<User[]>([]);

  ngOnInit(): void {
    this.userService.getAll().subscribe({
      next: (response) => {
        this.users.set(response.data as User[]);
        console.log('Users fetched successfully:', response);
        this.notify.add('success', 'Users data fetched successfully');
      },
      error: (error) => {
        console.error('Error fetching users:', error);
        this.notify.add('success', error?.error?.message || 'Data Fetch failed');
      },
    });
  }
}
