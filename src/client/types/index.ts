export interface User {
  id: string
  email: string
  name: string
  phone?: string
  role: 'client' | 'barbershop_admin' | 'admin_ti'
  avatar?: string
  createdAt: string
  updatedAt: string
}

export interface Barbershop {
  id: string
  name: string
  email: string
  phone: string
  address: string
  city: string
  state: string
  zipCode: string
  latitude: number
  longitude: number
  description?: string
  image?: string
  openingHours: OpeningHours
  services: Service[]
  rating: number
  totalReviews: number
  isActive: boolean
  createdAt: string
  updatedAt: string
}

export interface OpeningHours {
  monday: TimeRange
  tuesday: TimeRange
  wednesday: TimeRange
  thursday: TimeRange
  friday: TimeRange
  saturday: TimeRange
  sunday: TimeRange
}

export interface TimeRange {
  open: string
  close: string
  closed: boolean
}

export interface Service {
  id: string
  name: string
  description: string
  duration: number
  price: number
}

export interface Appointment {
  id: string
  clientId: string
  barbershopId: string
  serviceId: string
  dateTime: string
  status: 'pending' | 'confirmed' | 'completed' | 'cancelled'
  notes?: string
  createdAt: string
  updatedAt: string
}

export interface Subscription {
  id: string
  userId: string
  plan: 'basic' | 'premium' | 'pro'
  status: 'active' | 'expired' | 'cancelled'
  startDate: string
  endDate: string
  autoRenew: boolean
  price: number
  createdAt: string
  updatedAt: string
}

export interface AuthResponse {
  token: string
  user: User
}