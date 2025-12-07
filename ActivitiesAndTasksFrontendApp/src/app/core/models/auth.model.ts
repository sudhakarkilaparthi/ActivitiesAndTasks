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
