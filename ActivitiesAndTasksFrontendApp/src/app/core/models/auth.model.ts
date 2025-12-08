import { User } from './user.model';

export interface LoginRequest {
  email: string;
  password: string;
}

export interface TokenInfo {
  token: string;
  expiresAt: string;
}

export interface LoginResponse {
  tokenInfo: TokenInfo;
  userInfo: User;
}

export interface RegisterRequest {
  userName: string;
  email: string;
  password: string;
}

export interface RegisterResponse {
  message: string;
}

export interface GoogleLoginRequestOnlyToken {
  idToken: string;
}

export interface GoogleLoginRequest {
  idToken: string;
  email?: string;
  userName?: string;
  givenName?: string;
  familyName?: string;
  picture?: string;
  locale?: string;
  emailVerified?: boolean;
  aud?: string;
  iss?: string;
  iat?: number;
  exp?: number;
  sub?: string;
}
