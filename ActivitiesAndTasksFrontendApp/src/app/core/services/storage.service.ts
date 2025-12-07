import { Injectable } from '@angular/core';
import { User } from '../models/user.model';

const TOKEN_KEY = 'access_token';
const TOKEN_EXPIRY_TIME_KEY = 'access_token_expiry_time';
const USER_DETAILS_KEY = 'user_details';

@Injectable({
  providedIn: 'root',
})
export class StorageService {
  setToken(token: string): void {
    if (token != '' && token != null && token != undefined && token != 'undefined') {
      localStorage.setItem(TOKEN_KEY, token);
    }
  }

  setTokenExpiryTime(expiryTime: string): void {
    localStorage.setItem(TOKEN_EXPIRY_TIME_KEY, expiryTime);
  }

  setUserDetails(user: User): void {
    localStorage.setItem(USER_DETAILS_KEY, JSON.stringify(user));
  }

  getUserDetails(): User | null {
    const userDetails = localStorage.getItem(USER_DETAILS_KEY);
    return userDetails ? (JSON.parse(userDetails) as User) : null;
  }

  getToken(): string | null {
    return localStorage.getItem(TOKEN_KEY);
  }

  getTokenExpiryTime(): string | null {
    return localStorage.getItem(TOKEN_EXPIRY_TIME_KEY);
  }

  clearToken(): void {
    localStorage.removeItem(TOKEN_KEY);
  }

  clearAll(): void {
    localStorage.clear();
  }

  isLoggedIn(): boolean {
    // return !!this.getToken();

    const token = this.getToken();
    const expiryTime = this.getTokenExpiryTime();

    if (!token || !expiryTime) {
      return false;
    }

    const currentTime = new Date().toISOString();
    return expiryTime > currentTime;
  }
}
