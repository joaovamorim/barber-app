import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom'
import { Toaster } from 'react-hot-toast'
import Login from '@pages/Login'
import Home from '@pages/Home'
import AdminTI from '@pages/AdminTI'
import AdminBarbershop from '@pages/AdminBarbershop'
import ClientPage from '@pages/ClientPage'
import ProtectedRoute from '@components/ProtectedRoute'

function App() {
  return (
    <Router>
      <Toaster position="top-right" />
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/" element={<Home />} />
        
        <Route element={<ProtectedRoute />}>
          <Route path="/admin/ti" element={<AdminTI />} />
          <Route path="/admin/barbershop" element={<AdminBarbershop />} />
          <Route path="/client" element={<ClientPage />} />
        </Route>
        
        <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>
    </Router>
  )
}

export default App