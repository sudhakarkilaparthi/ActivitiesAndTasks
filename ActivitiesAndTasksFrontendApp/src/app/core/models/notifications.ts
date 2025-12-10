export type NotificationType = 'success' | 'warning' | 'error' | 'info';

export interface AppNotification {
  id: number;
  message: string;
  type: NotificationType;
  isPinned: boolean;
  progress: number;
}
