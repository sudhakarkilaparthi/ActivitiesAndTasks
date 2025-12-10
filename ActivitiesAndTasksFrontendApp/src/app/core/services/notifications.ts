import { Injectable } from '@angular/core';
import { AppNotification, NotificationType } from '../models/notifications';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
@Injectable({ providedIn: 'root' })
export class NotificationService {
  private notifications$ = new BehaviorSubject<AppNotification[]>([]);
  public alerts: Observable<AppNotification[]> = this.notifications$.asObservable();
  private durationTime = 4000;

  add(type: NotificationType, message: string): void {
    const id = Date.now();
    const newNotif: AppNotification = { id, message, type, isPinned: false, progress: 100 };

    // Add the new notification to the list
    this.notifications$.next([...this.notifications$.getValue(), newNotif]);
    this.startTimer(id, type);
  }

  private startTimer(id: number, type: NotificationType) {
    // const duration = type === 'warning' ? 4000 : 2000;
    const duration = this.durationTime;
    const tick = 20;
    let remaining = duration; // Timer starts here

    const interval = setInterval(() => {
      const list = this.notifications$.getValue();
      const item = list.find((n) => n.id === id);

      // CRITICAL: Stop if item is pinned or doesn't exist (already removed)
      if (!item || item.isPinned) {
        clearInterval(interval);
        return;
      }

      remaining -= tick;
      item.progress = (remaining / duration) * 100;

      if (remaining <= 0) {
        // CRITICAL: This is the auto-close call
        this.remove(id);
        clearInterval(interval);
      }
      // CRITICAL: Update the state so Angular detects changes
      this.notifications$.next([...list]);
    }, tick);
  }

  remove(id: number) {
    // CRITICAL: This line updates the observable list, triggering the ':leave' animation.
    this.notifications$.next(this.notifications$.getValue().filter((n) => n.id !== id));
  }

  removeIfNotPinned(id: number) {
    const list = this.notifications$.getValue();
    const item = list.find((n) => n.id === id);
    if (item) {
      setTimeout(() => {
        if (!item.isPinned) this.remove(id);
      }, this.durationTime);
    }
  }

  togglePin(id: number) {
    const list = this.notifications$.getValue();
    const item = list.find((n) => n.id === id);
    if (item) {
      item.isPinned = !item.isPinned;
      // If unpinned, restart the timer
      if (!item.isPinned) this.startTimer(id, item.type);
      this.removeIfNotPinned(id);
    }
  }
}
