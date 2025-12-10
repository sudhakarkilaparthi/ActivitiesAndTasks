import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LoaderSpinnerComponent } from './shared/components/loader-spinner/loader-spinner';
import { Notifications } from './shared/components/notifications/notifications';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, LoaderSpinnerComponent, Notifications],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected readonly title = signal('ActivitiesAndTasksFrontendApp');
}
