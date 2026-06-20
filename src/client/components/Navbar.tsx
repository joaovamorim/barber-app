import { Link, useNavigate, useLocation } from 'react-router-dom'
import { useAuthStore } from '@store/authStore'
import { useState } from 'react'

const Navbar = () => {
  const { user, logout, isAuthenticated } = useAuthStore()
  const navigate = useNavigate()
  const location = useLocation()
  const [isMenuOpen, setIsMenuOpen] = useState(false)

  const handleLogout = () => {
    logout()
    navigate('/login')
  }

  const isActive = (path: string) => location.pathname === path

  return (
    <nav className="bg-white shadow-md sticky top-0 z-50">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex justify-between h-16">
          <div className="flex items-center">
            <Link to="/" className="text-2xl font-bold text-blue-600">
              ✂️ BarberApp
            </Link>
          </div>

          <div className="hidden md:flex items-center gap-8">
            {isAuthenticated ? (
              <>
                {user?.role === 'admin_ti' && (
                  <Link to="/admin/ti" className={`px-4 py-2 rounded-md transition ${isActive('/admin/ti') ? 'bg-blue-600 text-white' : 'text-gray-700 hover:bg-gray-100'}`}>
                    Admin TI
                  </Link>
                )}
                {user?.role === 'barbershop_admin' && (
                  <Link to="/admin/barbershop" className={`px-4 py-2 rounded-md transition ${isActive('/admin/barbershop') ? 'bg-blue-600 text-white' : 'text-gray-700 hover:bg-gray-100'}`}>
                    Minha Barbearia
                  </Link>
                )}
                {user?.role === 'client' && (
                  <Link to="/client" className={`px-4 py-2 rounded-md transition ${isActive('/client') ? 'bg-blue-600 text-white' : 'text-gray-700 hover:bg-gray-100'}`}>
                    Meus Agendamentos
                  </Link>
                )}
                <div className="flex items-center gap-4">
                  <span className="text-gray-700">{user?.name}</span>
                  <button onClick={handleLogout} className="px-4 py-2 bg-red-600 text-white rounded-md hover:bg-red-700 transition">Sair</button>
                </div>
              </>
            ) : (
              <Link to="/login" className="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 transition">Entrar</Link>
            )}
          </div>

          <div className="flex md:hidden items-center">
            <button onClick={() => setIsMenuOpen(!isMenuOpen)} className="text-gray-700 hover:text-gray-900">
              <svg className="h-6 w-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 6h16M4 12h16M4 18h16" />
              </svg>
            </button>
          </div>
        </div>
      </div>

      {isMenuOpen && (
        <div className="md:hidden bg-white border-t">
          <div className="px-2 pt-2 pb-3 space-y-1">
            {isAuthenticated ? (
              <>
                {user?.role === 'admin_ti' && (
                  <Link to="/admin/ti" className="block px-3 py-2 rounded-md text-base font-medium text-gray-700 hover:bg-gray-100">Admin TI</Link>
                )}
                {user?.role === 'barbershop_admin' && (
                  <Link to="/admin/barbershop" className="block px-3 py-2 rounded-md text-base font-medium text-gray-700 hover:bg-gray-100">Minha Barbearia</Link>
                )}
                {user?.role === 'client' && (
                  <Link to="/client" className="block px-3 py-2 rounded-md text-base font-medium text-gray-700 hover:bg-gray-100">Meus Agendamentos</Link>
                )}
                <button onClick={handleLogout} className="w-full text-left px-3 py-2 rounded-md text-base font-medium text-red-600 hover:bg-gray-100">Sair</button>
              </>
            ) : (
              <Link to="/login" className="block px-3 py-2 rounded-md text-base font-medium text-blue-600 hover:bg-gray-100">Entrar</Link>
            )}
          </div>
        </div>
      )}
    </nav>
  )
}

export default Navbar