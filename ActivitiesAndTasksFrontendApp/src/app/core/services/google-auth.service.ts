import { Injectable, inject, signal } from '@angular/core';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';
import { APP_CONFIGS } from '../constants/app-configs';
import { GoogleLoginRequestOnlyToken } from '../models/auth.model';

declare global {
  interface Window {
    google: any;
  }
}

@Injectable({
  providedIn: 'root',
})
export class GoogleAuthService {
  private authService = inject(AuthService);
  private router = inject(Router);

  isGoogleScriptLoaded = signal(false);
  isLoading = signal(false);
  errorMessage = signal('');
  clientId = APP_CONFIGS.GOOGLE_CLIENT_ID; // '134777556733-no1ggqnpev3rv74t631ih876ul1hqj45.apps.googleusercontent.com';
  private returnUrl = '/dashboard';

  constructor() {
    this.initializeGoogleAuth();
  }

  private initializeGoogleAuth(): void {
    const script = document.createElement('script');
    script.src = 'https://accounts.google.com/gsi/client';
    script.async = true;
    script.defer = true;
    script.onload = () => {
      this.isGoogleScriptLoaded.set(true);
      this.initializeGoogleSignIn();
    };
    document.head.appendChild(script);
  }

  private initializeGoogleSignIn(): void {
    if (window.google && window.google.accounts) {
      window.google.accounts.id.initialize({
        client_id: this.clientId, // Replace with your Google Client ID
        callback: (response: any) => this.handleCredentialResponse(response),
      });
    }
  }

  renderGoogleButton(elementId: string): void {
    if (window.google && window.google.accounts) {
      window.google.accounts.id.renderButton(document.getElementById(elementId), {
        theme: 'outline',
        size: 'large',
        width: '100%',
      });
    }
  }

  private handleCredentialResponse(response: any): void {
    if (response.credential) {
      //       debugger;
      // Send the ID token to your backend for verification
      this.verifyGoogleToken(response.credential);
    }
  }

  private decodeGoogleToken(token: string): any {
    try {
      const base64Url = token.split('.')[1];
      const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
      const jsonPayload = decodeURIComponent(
        atob(base64)
          .split('')
          .map((c) => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
          .join('')
      );
      const decoded = JSON.parse(jsonPayload);
      console.log('Decoded Google Token Payload:', decoded);
      return decoded;
    } catch (error) {
      console.error('Error decoding Google token:', error);
      return null;
    }
  }

  private extractGoogleUserData(decodedToken: any): Record<string, any> {
    return {
      idToken: undefined, // Will be added separately
      email: decodedToken.email || '',
      userName: decodedToken.name || '',
      givenName: decodedToken.given_name || '',
      familyName: decodedToken.family_name || '',
      picture: decodedToken.picture || '',
      locale: decodedToken.locale || '',
      emailVerified: decodedToken.email_verified || false,
      aud: decodedToken.aud || '',
      iss: decodedToken.iss || '',
      iat: decodedToken.iat || 0,
      exp: decodedToken.exp || 0,
      sub: decodedToken.sub || '',
    };
  }

  private verifyGoogleToken(token: string): void {
    this.isLoading.set(true);
    this.errorMessage.set('');

    console.log('Google Token received, sending to backend for verification');

    // Decode the JWT token to extract user information
    const decodedToken = this.decodeGoogleToken(token);

    if (!decodedToken) {
      this.isLoading.set(false);
      this.errorMessage.set('Failed to process Google token');
      return;
    }

    // Extract all available Google user data
    const googleUserData = this.extractGoogleUserData(decodedToken);
    googleUserData['idToken'] = token;

    const glrk: GoogleLoginRequestOnlyToken = { idToken: token };

    console.log('Extracted Google user data:', googleUserData);

    this.authService.loginWithGoogle(glrk).subscribe({
      next: (response) => {
        console.log('Google login successful:', response);
        this.isLoading.set(false);

        // Redirect to dashboard or return URL
        setTimeout(() => {
          this.router.navigate([this.returnUrl]);
        }, 500);
      },
      error: (error) => {
        this.isLoading.set(false);
        this.errorMessage.set(error.error?.message || 'Google login failed. Please try again.');
        console.error('Google login error:', error);
      },
    });
  }

  setClientId(clientId: string): void {
    this.clientId = clientId;
  }

  setReturnUrl(url: string): void {
    this.returnUrl = url;
  }
}
