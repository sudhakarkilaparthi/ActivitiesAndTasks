import { Component, Input, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GoogleAuthService } from '../../../core/services/google-auth.service';

@Component({
  selector: 'app-google-signin-button',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="google-signin-container">
      @if (!googleAuthService.isGoogleScriptLoaded()) {
      <button
        type="button"
        class="btn btn-outline-secondary w-100"
        disabled
        title="Loading Google Sign-In..."
      >
        <span class="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>
        Loading Google Sign-In...
      </button>
      } @else {
      <div [id]="buttonContainerId"></div>
      }
    </div>
  `,
  styles: `
    .google-signin-container {
      margin-bottom: 20px;
    }
  `,
})
export class GoogleSigninButtonComponent implements OnInit {
  @Input() googleClientId: string = ''; // Default Client ID
  @Input() buttonContainerId: string = 'google-signin-button';

  googleAuthService = inject(GoogleAuthService);

  ngOnInit(): void {
    if (this.googleClientId) {
      this.googleAuthService.setClientId(this.googleClientId);
    }

    // Wait for Google script to load before rendering button
    const interval = setInterval(() => {
      if (this.googleAuthService.isGoogleScriptLoaded()) {
        clearInterval(interval);
        setTimeout(() => {
          this.googleAuthService.renderGoogleButton(this.buttonContainerId);
        }, 100);
      }
    }, 100);
  }
}
