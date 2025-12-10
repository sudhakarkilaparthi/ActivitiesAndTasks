import { Component, inject } from '@angular/core';
import { NotificationService } from '../../../core/services/notifications';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-notifications',
  imports: [CommonModule],
  templateUrl: './notifications.html',
  styleUrl: './notifications.css',
})
export class Notifications {
  public service = inject(NotificationService);

  animateEnterFn(id: number) {
    console.log('Enter Animation Triggered for ID:', id);
    this.service.removeIfNotPinned(id);
  }
}
