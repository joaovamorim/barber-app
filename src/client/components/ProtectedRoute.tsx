import { Navigate, Outlet } from 'react-router-dom'
import { useAuthStore } from '@store/authStore'
import { useEffect } from 'react'

const ProtectedRoute = () => {
  const { isAuthenticated, initialize, isLoading } = useAuthStore()

  useEffect(() => {
    initialize()
  }, [])

  if (isLoading) {
    return (
      <div className="flex items-center justify-center min-h-screen">
        <div className="text-center">
          <div className="inline-block animate-spin rounded-full h-12 w-12 border-b-2 border-blue-500"></div>
          <p className="mt-4 text-gray-600">Carregando...</p>
        </div>
      </div>
    )
  }

  return isAuthenticated ? <Outlet /> : <Navigate to="/login" replace />
}

export default ProtectedRoute