export interface ApiBaseResponse<T = any> {
  error: boolean;
  message: string;
  data?: T;
}
