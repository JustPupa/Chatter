import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import '../styles/index.css';
import Test from './test/test.jsx';
import Login from './login/login.jsx';
import ProtectedRoute from './login/protectedRoute.jsx';

createRoot(document.getElementById('root')).render(
    <BrowserRouter>
      <Routes>
          <Route path="/" element={<Login />} />
          <Route path="/login" element={<Login />} />
          <Route 
            path="/test" 
            element={
              <ProtectedRoute>
                <Test />
              </ProtectedRoute>
            } 
          />
      </Routes>
  </BrowserRouter>
)