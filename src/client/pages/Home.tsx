import { useAuthStore } from '@store/authStore'
import { useEffect } from 'react'
import Navbar from '@components/Navbar'
import { Link } from 'react-router-dom'

const Home = () => {
  const { initialize } = useAuthStore()

  useEffect(() => {
    initialize()
  }, [])

  return (
    <div>
      <Navbar />
      <div className="min-h-screen bg-gradient-to-br from-blue-50 to-indigo-100">
        <section className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-20">
          <div className="text-center">
            <h1 className="text-5xl font-bold text-gray-900 mb-4">✂️ Bem-vindo ao BarberApp</h1>
            <p className="text-xl text-gray-600 mb-8">Agende seus cortes favoritos nas melhores barbearias</p>
            <div className="flex gap-4 justify-center">
              <Link to="/login" className="px-6 py-3 bg-blue-600 text-white rounded-lg font-semibold hover:bg-blue-700 transition">Começar Agora</Link>
              <Link to="/login" className="px-6 py-3 bg-white text-blue-600 border-2 border-blue-600 rounded-lg font-semibold hover:bg-blue-50 transition">Saber Mais</Link>
            </div>
          </div>
        </section>

        <section className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-20">
          <h2 className="text-3xl font-bold text-center text-gray-900 mb-12">Por que escolher o BarberApp?</h2>
          <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
            {[{icon: '📍', title: 'Barbearias Próximas', description: 'Encontre as melhores barbearias perto de você'}, {icon: '📅', title: 'Agendamentos Fáceis', description: 'Agende seus cortes em minutos'}, {icon: '💳', title: 'Planos de Assinatura', description: 'Aproveite descontos exclusivos com planos mensais'}].map((feature, index) => (
              <div key={index} className="bg-white rounded-lg shadow-md p-8 text-center hover:shadow-lg transition">
                <div className="text-4xl mb-4">{feature.icon}</div>
                <h3 className="text-xl font-semibold text-gray-900 mb-2">{feature.title}</h3>
                <p className="text-gray-600">{feature.description}</p>
              </div>
            ))}
          </div>
        </section>
      </div>
    </div>
  )
}

export default Home