import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LoaderSpinnerComponent } from './shared/components/loader-spinner/loader-spinner';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, LoaderSpinnerComponent],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected readonly title = signal('ActivitiesAndTasksFrontendApp');
}
