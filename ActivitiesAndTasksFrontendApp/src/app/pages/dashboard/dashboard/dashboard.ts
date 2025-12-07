import { Component, inject, OnInit, signal } from '@angular/core';
import { User } from '../../../core/models/user.model';
import { StorageService } from '../../../core/services/storage.service';

@Component({
  selector: 'app-dashboard',
  imports: [],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard implements OnInit {
  storageService = inject(StorageService);
  user = signal<User | null>(null);

  ngOnInit(): void {
    this.user.set(this.storageService.getUserDetails()); // Initialization logic can be added here
  }
}
