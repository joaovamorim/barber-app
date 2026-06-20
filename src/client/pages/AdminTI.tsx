import Navbar from '@components/Navbar'
import { useAuthStore } from '@store/authStore'
import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'

const AdminTI = () => {
  const { user } = useAuthStore()
  const navigate = useNavigate()
  const [activeTab, setActiveTab] = useState('overview')

  useEffect(() => {
    if (user?.role !== 'admin_ti') {
      navigate('/')
    }
  }, [user, navigate])

  return (
    <div>
      <Navbar />
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <h1 className="text-3xl font-bold text-gray-900 mb-8">Painel Admin TI</h1>

        <div className="border-b border-gray-200 mb-6">
          <nav className="flex gap-8 overflow-x-auto">
            {[{id: 'overview', label: 'Visão Geral'}, {id: 'barbershops', label: 'Barbearias'}, {id: 'users', label: 'Usuários'}, {id: 'reports', label: 'Relatórios'}].map((tab) => (
              <button key={tab.id} onClick={() => setActiveTab(tab.id)} className={`pb-2 font-medium transition whitespace-nowrap ${activeTab === tab.id ? 'border-b-2 border-blue-600 text-blue-600' : 'text-gray-600 hover:text-gray-900'}`}>
                {tab.label}
              </button>
            ))}
          </nav>
        </div>

        <div className="bg-white rounded-lg shadow p-6">
          {activeTab === 'overview' && (
            <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
              {[{label: 'Total de Usuários', value: '0', icon: '👥'}, {label: 'Barbearias Ativas', value: '0', icon: '✂️'}, {label: 'Agendamentos', value: '0', icon: '📅'}, {label: 'Receita', value: 'R$ 0', icon: '💰'}].map((stat, index) => (
                <div key={index} className="bg-gradient-to-br from-blue-50 to-indigo-50 rounded-lg p-6">
                  <div className="text-3xl mb-2">{stat.icon}</div>
                  <p className="text-gray-600 text-sm">{stat.label}</p>
                  <p className="text-2xl font-bold text-gray-900">{stat.value}</p>
                </div>
              ))}
            </div>
          )}
          {activeTab === 'barbershops' && (<div><h2 className="text-xl font-semibold text-gray-900 mb-4">Gerenciar Barbearias</h2><p className="text-gray-600">Lista de barbearias será exibida aqui...</p></div>)}
          {activeTab === 'users' && (<div><h2 className="text-xl font-semibold text-gray-900 mb-4">Gerenciar Usuários</h2><p className="text-gray-600">Lista de usuários será exibida aqui...</p></div>)}
          {activeTab === 'reports' && (<div><h2 className="text-xl font-semibold text-gray-900 mb-4">Relatórios</h2><p className="text-gray-600">Relatórios serão exibidos aqui...</p></div>)}
        </div>
      </div>
    </div>
  )
}

export default AdminTI