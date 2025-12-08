import { Component, inject, signal, OnInit } from '@angular/core';
import {
  FormsModule,
  ReactiveFormsModule,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { LoginRequest } from '../../../core/models/auth.model';
import { GoogleSigninButtonComponent } from '../../../shared/components/google-signin-button/google-signin-button';
import { APP_CONFIGS } from '../../../core/constants/app-configs';
import { GoogleAuthService } from '../../../core/services/google-auth.service';

@Component({
  selector: 'app-login',
  imports: [FormsModule, ReactiveFormsModule, GoogleSigninButtonComponent],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login implements OnInit {
  private oneTapService = inject(GoogleAuthService);
  private authService = inject(AuthService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private fb = inject(FormBuilder);

  loginForm!: FormGroup;
  isLoading = signal(false);
  errorMessage = signal('');
  successMessage = signal('');
  returnUrl = signal('');
  googleClientId = APP_CONFIGS.GOOGLE_CLIENT_ID; // '134777556733-no1ggqnpev3rv74t631ih876ul1hqj45.apps.googleusercontent.com'; // Replace with your actual Google Client ID

  constructor() {
    this.initializeForm();
  }

  async ngOnInit(): Promise<void> {
    // Get return URL from route parameters or default to '/'
    this.returnUrl.set(this.route.snapshot.queryParams['returnUrl'] || '/dashboard');
    this.authService.logout(); // Ensure logged out on accessing login page
    await this.oneTapService.showOneTapIfNeeded(false);
  }

  private initializeForm(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      remember: [false],
    });
  }

  onSubmit(): void {
    if (this.loginForm.invalid) {
      this.errorMessage.set('Please fill in all required fields correctly');
      return;
    }

    this.isLoading.set(true);
    this.errorMessage.set('');
    this.successMessage.set('');

    const loginPayload: LoginRequest = {
      email: this.loginForm.get('email')?.value,
      password: this.loginForm.get('password')?.value,
    };

    this.authService.login(loginPayload).subscribe({
      next: (response) => {
        console.log('Login successful:', response);

        this.isLoading.set(false);
        this.successMessage.set('Login successful! Redirecting...');

        // Store remember me preference if checked
        if (this.loginForm.get('remember')?.value) {
          localStorage.setItem('rememberEmail', loginPayload.email);
        }

        // Redirect to return URL or dashboard
        setTimeout(() => {
          this.router.navigate([this.returnUrl()]);
        }, 10);
      },
      error: (error) => {
        // debugger;
        this.isLoading.set(false);
        this.errorMessage.set(
          error.error?.message || 'Login failed. Please check your credentials.'
        );
      },
    });
  }

  get email() {
    return this.loginForm.get('email');
  }

  get password() {
    return this.loginForm.get('password');
  }

  get isFormValid(): boolean {
    return this.loginForm.valid;
  }
}
