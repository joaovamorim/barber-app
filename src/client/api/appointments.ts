import client from './client'
import { Appointment } from '@types'

export const appointmentsAPI = {
  getAll: (filters?: { barbershopId?: string; status?: string }) =>
    client.get<Appointment[]>('/appointments', { params: filters }),

  getById: (id: string) => client.get<Appointment>(`/appointments/${id}`),

  create: (data: Partial<Appointment>) =>
    client.post<Appointment>('/appointments', data),

  update: (id: string, data: Partial<Appointment>) =>
    client.put<Appointment>(`/appointments/${id}`, data),

  cancel: (id: string) => client.put(`/appointments/${id}/cancel`),

  delete: (id: string) => client.delete(`/appointments/${id}`),
}