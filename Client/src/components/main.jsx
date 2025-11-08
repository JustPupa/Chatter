import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import '../styles/index.css'
import Test from './test.jsx'
import Index from './index.jsx'
import Login from './login.jsx'

createRoot(document.getElementById('root')).render(
  <BrowserRouter>
    <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/test" element={<Test />} />
        <Route path="/index" element={<Index />} />
    </Routes>
  </BrowserRouter>
)