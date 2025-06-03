export interface UserLogin {
  email: string;
  password: string;
}

export interface UserLoginResponse {
  id: number;
  name: string;
  email: string;
  role: string;
  token: string;
}
export interface Users {
  id: number;
  name: string;
  email: string;
  password?: string; // Optional because we usually don't show it
  role: string;
}

export interface currentUsers{
  name: string;
  email: string;
  role: string;
}