import Navbar from '@components/Navbar'
import { useAuthStore } from '@store/authStore'
import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'

const AdminBarbershop = () => {
  const { user } = useAuthStore()
  const navigate = useNavigate()
  const [activeTab, setActiveTab] = useState('dashboard')

  useEffect(() => {
    if (user?.role !== 'barbershop_admin') {
      navigate('/')
    }
  }, [user, navigate])

  return (
    <div>
      <Navbar />
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <h1 className="text-3xl font-bold text-gray-900 mb-8">Painel da Barbearia</h1>

        <div className="border-b border-gray-200 mb-6">
          <nav className="flex gap-8 overflow-x-auto">
            {[{id: 'dashboard', label: 'Dashboard'}, {id: 'appointments', label: 'Agendamentos'}, {id: 'services', label: 'Serviços'}, {id: 'settings', label: 'Configurações'}].map((tab) => (
              <button key={tab.id} onClick={() => setActiveTab(tab.id)} className={`pb-2 font-medium transition whitespace-nowrap ${activeTab === tab.id ? 'border-b-2 border-blue-600 text-blue-600' : 'text-gray-600 hover:text-gray-900'}`}>
                {tab.label}
              </button>
            ))}
          </nav>
        </div>

        <div className="bg-white rounded-lg shadow p-6">
          {activeTab === 'dashboard' && (
            <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
              {[{label: 'Agendamentos Hoje', value: '0', icon: '📅'}, {label: 'Clientes do Mês', value: '0', icon: '👥'}, {label: 'Avaliação', value: '0.0', icon: '⭐'}].map((stat, index) => (
                <div key={index} className="bg-gradient-to-br from-blue-50 to-indigo-50 rounded-lg p-6">
                  <div className="text-3xl mb-2">{stat.icon}</div>
                  <p className="text-gray-600 text-sm">{stat.label}</p>
                  <p className="text-2xl font-bold text-gray-900">{stat.value}</p>
                </div>
              ))}
            </div>
          )}
          {activeTab === 'appointments' && (<div><h2 className="text-xl font-semibold text-gray-900 mb-4">Agendamentos</h2><p className="text-gray-600">Seus agendamentos serão exibidos aqui...</p></div>)}
          {activeTab === 'services' && (<div><h2 className="text-xl font-semibold text-gray-900 mb-4">Serviços</h2><p className="text-gray-600">Seus serviços serão exibidos aqui...</p></div>)}
          {activeTab === 'settings' && (<div><h2 className="text-xl font-semibold text-gray-900 mb-4">Configurações</h2><p className="text-gray-600">Configurações da barbearia aqui...</p></div>)}
        </div>
      </div>
    </div>
  )
}

export default AdminBarbershop