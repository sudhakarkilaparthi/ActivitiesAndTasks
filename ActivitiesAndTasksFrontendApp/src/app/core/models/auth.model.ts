export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  expiresAt: string;
  firstName: string;
  lastName: string;
  email: string;
  role: string;
  token: string;
}
export interface RegisterRequest {
  userName: string;
  email: string;
  password: string;
}
export interface RegisterResponse {
  message: string;
}
