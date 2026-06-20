import Navbar from '@components/Navbar'
import { useAuthStore } from '@store/authStore'
import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'

const ClientPage = () => {
  const { user } = useAuthStore()
  const navigate = useNavigate()
  const [activeTab, setActiveTab] = useState('bookings')

  useEffect(() => {
    if (user?.role !== 'client') {
      navigate('/')
    }
  }, [user, navigate])

  return (
    <div>
      <Navbar />
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <h1 className="text-3xl font-bold text-gray-900 mb-8">Meus Agendamentos</h1>

        <div className="border-b border-gray-200 mb-6">
          <nav className="flex gap-8 overflow-x-auto">
            {[{id: 'bookings', label: 'Próximos Agendamentos'}, {id: 'history', label: 'Histórico'}, {id: 'subscriptions', label: 'Minhas Assinaturas'}, {id: 'profile', label: 'Perfil'}].map((tab) => (
              <button key={tab.id} onClick={() => setActiveTab(tab.id)} className={`pb-2 font-medium transition whitespace-nowrap ${activeTab === tab.id ? 'border-b-2 border-blue-600 text-blue-600' : 'text-gray-600 hover:text-gray-900'}`}>
                {tab.label}
              </button>
            ))}
          </nav>
        </div>

        <div className="bg-white rounded-lg shadow p-6">
          {activeTab === 'bookings' && (<div><h2 className="text-xl font-semibold text-gray-900 mb-4">Próximos Agendamentos</h2><p className="text-gray-600">Você não tem agendamentos próximos.</p></div>)}
          {activeTab === 'history' && (<div><h2 className="text-xl font-semibold text-gray-900 mb-4">Histórico de Agendamentos</h2><p className="text-gray-600">Seu histórico será exibido aqui...</p></div>)}
          {activeTab === 'subscriptions' && (<div><h2 className="text-xl font-semibold text-gray-900 mb-4">Minhas Assinaturas</h2><p className="text-gray-600">Suas assinaturas serão exibidas aqui...</p></div>)}
          {activeTab === 'profile' && (<div><h2 className="text-xl font-semibold text-gray-900 mb-4">Meu Perfil</h2><p className="text-gray-600 mb-4">Nome: {user?.name}</p><p className="text-gray-600">Email: {user?.email}</p></div>)}
        </div>
      </div>
    </div>
  )
}

export default ClientPage