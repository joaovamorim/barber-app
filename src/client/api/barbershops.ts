import client from './client'
import { Barbershop } from '@types'

export const barbershopsAPI = {
  getAll: (filters?: { latitude?: number; longitude?: number; radius?: number }) =>
    client.get<Barbershop[]>('/barbershops', { params: filters }),

  getById: (id: string) => client.get<Barbershop>(`/barbershops/${id}`),

  getNearby: (latitude: number, longitude: number, radius: number = 10) =>
    client.get<Barbershop[]>('/barbershops/nearby', { params: { latitude, longitude, radius } }),

  create: (data: Partial<Barbershop>) =>
    client.post<Barbershop>('/barbershops', data),

  update: (id: string, data: Partial<Barbershop>) =>
    client.put<Barbershop>(`/barbershops/${id}`, data),

  delete: (id: string) => client.delete(`/barbershops/${id}`),
}