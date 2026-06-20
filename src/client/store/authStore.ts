import { create } from 'zustand'
import { User, AuthResponse } from '@types'

interface AuthState {
  user: User | null
  token: string | null
  isLoading: boolean
  isAuthenticated: boolean
  setUser: (user: User | null) => void
  setToken: (token: string | null) => void
  setLoading: (loading: boolean) => void
  logout: () => void
  login: (response: AuthResponse) => void
  initialize: () => void
}

export const useAuthStore = create<AuthState>((set) => ({
  user: null,
  token: null,
  isLoading: true,
  isAuthenticated: false,

  setUser: (user) => set({ user }),
  setToken: (token) => set({ token }),
  setLoading: (isLoading) => set({ isLoading }),

  login: (response) => {
    localStorage.setItem('token', response.token)
    localStorage.setItem('user', JSON.stringify(response.user))
    set({
      token: response.token,
      user: response.user,
      isAuthenticated: true,
    })
  },

  logout: () => {
    localStorage.removeItem('token')
    localStorage.removeItem('user')
    set({
      token: null,
      user: null,
      isAuthenticated: false,
    })
  },

  initialize: () => {
    const token = localStorage.getItem('token')
    const userStr = localStorage.getItem('user')
    
    if (token && userStr) {
      try {
        const user = JSON.parse(userStr)
        set({
          token,
          user,
          isAuthenticated: true,
        })
      } catch (error) {
        localStorage.removeItem('token')
        localStorage.removeItem('user')
      }
    }
    set({ isLoading: false })
  },
}))