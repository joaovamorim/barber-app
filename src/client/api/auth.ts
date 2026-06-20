import client from './client'
import { AuthResponse, User } from '@types'

export const authAPI = {
  login: (email: string, password: string) =>
    client.post<AuthResponse>('/auth/login', { email, password }),

  register: (data: { email: string; password: string; name: string; phone: string; role: string }) =>
    client.post<AuthResponse>('/auth/register', data),

  me: () => client.get<User>('/auth/me'),

  logout: () => client.post('/auth/logout'),
}