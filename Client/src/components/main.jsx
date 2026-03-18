import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import '../styles/index.css';
import Test from './test/test.jsx';
import Login from './login/login.jsx';
import ProtectedRoute from './login/protectedRoute.jsx';
import UserLayout from './userpage/user-layout.jsx';
import UserMain from './userpage/usermain.jsx';
import Chats from './userpage/chats.jsx';
import Feed from './userpage/feed.jsx';

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
          <Route path="/user" element={<UserLayout />}>
            <Route path="account"  
              element={
                <ProtectedRoute>
                  <UserMain />
                </ProtectedRoute>
              } 
            />
            <Route path="chats"  
              element={
                <ProtectedRoute>
                  <Chats />
                </ProtectedRoute>
              } 
            />
            <Route path="feed"  
              element={
                <ProtectedRoute>
                  <Feed />
                </ProtectedRoute>
              } 
            />
          </Route>
      </Routes>
  </BrowserRouter>
)