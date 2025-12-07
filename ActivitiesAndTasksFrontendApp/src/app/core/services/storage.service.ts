import { Injectable } from '@angular/core';
import { User } from '../models/user.model';

const TOKEN_KEY = 'access_token';

@Injectable({
  providedIn: 'root',
})
export class StorageService {
  setToken(token: string): void {
    if (token != '' && token != null && token != undefined && token != 'undefined') {
      localStorage.setItem(TOKEN_KEY, token);
    }
  }

  setUserDetails(user: User): void {
    localStorage.setItem('user_details', JSON.stringify(user));
  }

  getUserDetails(): User | null {
    const userDetails = localStorage.getItem('user_details');
    return userDetails ? (JSON.parse(userDetails) as User) : null;
  }

  getToken(): string | null {
    return localStorage.getItem(TOKEN_KEY);
  }

  clearToken(): void {
    localStorage.removeItem(TOKEN_KEY);
  }

  clearAll(): void {
    localStorage.clear();
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }
}
